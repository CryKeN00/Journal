using System;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SchoolJournal
{
    public partial class ParentForm : Form
    {
        private List<Student> childrenList;

        public ParentForm()
        {
            InitializeComponent();
            this.FormClosing += ParentForm_FormClosing;
        }

        private void ParentForm_Load(object sender, EventArgs e)
        {
            lblParentName.Text = LoginForm.CurrentUser.FullName;
            LoadChildren();
            LoadGrades();
        }

        private void LoadChildren()
        {
            try
            {
                childrenList = new List<Student>();

                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT s.Id, s.UserId, s.Class, u.FullName, u.Username
                        FROM Students s
                        INNER JOIN Users u ON s.UserId = u.Id
                        INNER JOIN ParentStudents ps ON s.Id = ps.StudentId
                        WHERE ps.ParentId = @parentId
                        ORDER BY u.FullName";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@parentId", LoginForm.CurrentUser.Id);

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
                                childrenList.Add(student);
                            }
                        }
                    }
                }

                cmbChildren.Items.Clear();
                foreach (var child in childrenList)
                {
                    cmbChildren.Items.Add($"{child.FullName} (Класс: {child.Class})");
                }

                if (cmbChildren.Items.Count > 0)
                {
                    cmbChildren.SelectedIndex = 0;
                    lblNoChildren.Visible = false;
                    cmbChildren.Visible = true;
                    btnRefresh.Enabled = true;
                }
                else
                {
                    lblNoChildren.Visible = true;
                    lblNoChildren.Text = "У вас пока нет привязанных детей. Обратитесь к администратору.";
                    cmbChildren.Visible = false;
                    btnRefresh.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка детей: {ex.Message}");
            }
        }

        private void LoadGrades()
        {
            if (childrenList.Count == 0 || cmbChildren.SelectedIndex == -1)
                return;

            try
            {
                var selectedChild = childrenList[cmbChildren.SelectedIndex];
                lblSelectedChild.Text = $"Оценки ученика: {selectedChild.FullName} (Класс: {selectedChild.Class})";

                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT g.Subject, g.Grade, g.Date, u.FullName as TeacherName, g.Note
                        FROM Grades g
                        INNER JOIN Users u ON g.TeacherId = u.Id
                        WHERE g.StudentId = @studentId
                        ORDER BY g.Date DESC";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@studentId", selectedChild.Id);

                        using (var reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            dataGridViewGrades.DataSource = dt;

                            if (dataGridViewGrades.Columns.Contains("Subject"))
                                dataGridViewGrades.Columns["Subject"].HeaderText = "Предмет";
                            if (dataGridViewGrades.Columns.Contains("Grade"))
                                dataGridViewGrades.Columns["Grade"].HeaderText = "Оценка";
                            if (dataGridViewGrades.Columns.Contains("Date"))
                                dataGridViewGrades.Columns["Date"].HeaderText = "Дата";
                            if (dataGridViewGrades.Columns.Contains("TeacherName"))
                                dataGridViewGrades.Columns["TeacherName"].HeaderText = "Учитель";
                            if (dataGridViewGrades.Columns.Contains("Note"))
                                dataGridViewGrades.Columns["Note"].HeaderText = "Примечание";
                        }
                    }
                }

                CalculateAverageGrade();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки оценок: {ex.Message}");
            }
        }

        private void CalculateAverageGrade()
        {
            try
            {
                if (dataGridViewGrades.Rows.Count == 0)
                {
                    lblAverageGrade.Text = "Средний балл: нет оценок";
                    return;
                }

                double sum = 0;
                int count = 0;

                foreach (DataGridViewRow row in dataGridViewGrades.Rows)
                {
                    if (row.Cells["Grade"].Value != null)
                    {
                        sum += Convert.ToDouble(row.Cells["Grade"].Value);
                        count++;
                    }
                }

                if (count > 0)
                {
                    double average = sum / count;
                    lblAverageGrade.Text = $"Средний балл: {average:F2}";

                    if (average >= 4.5)
                        lblAverageGrade.ForeColor = System.Drawing.Color.Green;
                    else if (average >= 3.5)
                        lblAverageGrade.ForeColor = System.Drawing.Color.Blue;
                    else if (average >= 2.5)
                        lblAverageGrade.ForeColor = System.Drawing.Color.Orange;
                    else
                        lblAverageGrade.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblAverageGrade.Text = "Средний балл: нет оценок";
                }
            }
            catch (Exception ex)
            {
                lblAverageGrade.Text = "Средний балл: ошибка расчета";
            }
        }

        private void cmbChildren_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrades();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrades();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}