using System;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace SchoolJournal
{
    public partial class EditGradeForm : Form
    {
        private int gradeId;
        private bool isEditMode;

        public EditGradeForm(int gradeId = 0)
        {
            InitializeComponent();
            this.gradeId = gradeId;
            this.isEditMode = gradeId > 0;
        }

        private void EditGradeForm_Load(object sender, EventArgs e)
        {
            Text = isEditMode ? "Редактирование оценки" : "Добавление оценки";
            LoadStudents();

            if (isEditMode)
            {
                LoadGradeData();
            }
            else
            {
                dtpDate.Value = DateTime.Now;
            }
        }

        private void LoadStudents()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT s.Id, u.FullName 
                        FROM Students s 
                        INNER JOIN Users u ON s.UserId = u.Id 
                        ORDER BY u.FullName";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            cmbStudents.Items.Clear();
                            while (reader.Read())
                            {
                                cmbStudents.Items.Add(new
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                            }

                            if (cmbStudents.Items.Count > 0)
                                cmbStudents.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки студентов: {ex.Message}");
            }
        }

        private void LoadGradeData()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT g.StudentId, g.Subject, g.Grade, g.Date
                        FROM Grades g
                        WHERE g.Id = @gradeId";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@gradeId", gradeId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int studentId = reader.GetInt32(0);

                                // Устанавливаем студента
                                for (int i = 0; i < cmbStudents.Items.Count; i++)
                                {
                                    dynamic item = cmbStudents.Items[i];
                                    if (item.Id == studentId)
                                    {
                                        cmbStudents.SelectedIndex = i;
                                        break;
                                    }
                                }

                                // Устанавливаем предмет
                                cmbSubject.Text = reader.GetString(1);

                                // Устанавливаем оценку
                                numericGrade.Value = reader.GetInt32(2);

                                // Устанавливаем дату
                                if (DateTime.TryParse(reader.GetString(3), out DateTime date))
                                {
                                    dtpDate.Value = date;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных оценки: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbStudents.SelectedItem == null || string.IsNullOrEmpty(cmbSubject.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            try
            {
                dynamic selectedStudent = cmbStudents.SelectedItem;
                int studentId = selectedStudent.Id;

                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    if (isEditMode)
                    {
                        // Обновляем оценку
                        string updateQuery = @"
                            UPDATE Grades 
                            SET StudentId = @studentId, 
                                Subject = @subject, 
                                Grade = @grade, 
                                Date = @date,
                                TeacherId = @teacherId
                            WHERE Id = @gradeId";

                        using (var command = new SqliteCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@studentId", studentId);
                            command.Parameters.AddWithValue("@subject", cmbSubject.Text);
                            command.Parameters.AddWithValue("@grade", (int)numericGrade.Value);
                            command.Parameters.AddWithValue("@date", dtpDate.Value.ToString("yyyy-MM-dd"));
                            command.Parameters.AddWithValue("@teacherId", LoginForm.CurrentUser.Id);
                            command.Parameters.AddWithValue("@gradeId", gradeId);

                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Добавляем новую оценку
                        string insertQuery = @"
                            INSERT INTO Grades (StudentId, Subject, Grade, Date, TeacherId)
                            VALUES (@studentId, @subject, @grade, @date, @teacherId)";

                        using (var command = new SqliteCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@studentId", studentId);
                            command.Parameters.AddWithValue("@subject", cmbSubject.Text);
                            command.Parameters.AddWithValue("@grade", (int)numericGrade.Value);
                            command.Parameters.AddWithValue("@date", dtpDate.Value.ToString("yyyy-MM-dd"));
                            command.Parameters.AddWithValue("@teacherId", LoginForm.CurrentUser.Id);

                            command.ExecuteNonQuery();
                        }
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения оценки: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}