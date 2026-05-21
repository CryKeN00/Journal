using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace SchoolJournal
{
    public partial class LinkChildrenForm : Form
    {
        private int parentId;
        private string parentName;
        private List<Student> allStudents;
        private List<int> linkedStudentIds;

        public LinkChildrenForm(int parentId, string parentName)
        {
            InitializeComponent();
            this.parentId = parentId;
            this.parentName = parentName;
        }

        private void LinkChildrenForm_Load(object sender, EventArgs e)
        {
            Text = $"Привязка детей к родителю: {parentName}";
            LoadStudents();
            LoadLinkedStudents();
        }

        private void LoadStudents()
        {
            try
            {
                allStudents = new List<Student>();

                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT s.Id, s.UserId, s.Class, u.FullName, u.Username
                        FROM Students s
                        INNER JOIN Users u ON s.UserId = u.Id
                        ORDER BY u.FullName";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var student = new Student
                                {
                                    Id = reader.GetInt32(0),
                                    UserId = reader.GetInt32(1),
                                    Class = reader.GetString(2),
                                    FullName = reader.GetString(3),
                                    Username = reader.GetString(4)
                                };
                                allStudents.Add(student);
                            }
                        }
                    }
                }

                clbStudents.Items.Clear();
                foreach (var student in allStudents)
                {
                    clbStudents.Items.Add($"{student.FullName} (Класс: {student.Class})", false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки студентов: {ex.Message}");
            }
        }

        private void LoadLinkedStudents()
        {
            try
            {
                linkedStudentIds = new List<int>();

                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT StudentId FROM ParentStudents WHERE ParentId = @parentId";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@parentId", parentId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                linkedStudentIds.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }

                for (int i = 0; i < allStudents.Count; i++)
                {
                    if (linkedStudentIds.Contains(allStudents[i].Id))
                    {
                        clbStudents.SetItemChecked(i, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки привязанных детей: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM ParentStudents WHERE ParentId = @parentId";
                    using (var command = new SqliteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@parentId", parentId);
                        command.ExecuteNonQuery();
                    }

                    string insertQuery = "INSERT INTO ParentStudents (ParentId, StudentId) VALUES (@parentId, @studentId)";

                    for (int i = 0; i < clbStudents.Items.Count; i++)
                    {
                        if (clbStudents.GetItemChecked(i))
                        {
                            var student = allStudents[i];
                            using (var command = new SqliteCommand(insertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@parentId", parentId);
                                command.Parameters.AddWithValue("@studentId", student.Id);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}