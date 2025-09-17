using System;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace SchoolJournal
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();

            // Подписка на событие закрытия формы
            this.FormClosing += StudentForm_FormClosing;
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            lblStudentName.Text = LoginForm.CurrentUser.FullName;
            LoadGrades();
        }

        private void LoadGrades()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT g.Subject, g.Grade, g.Date, u.FullName as TeacherName
                        FROM Grades g
                        INNER JOIN Students s ON g.StudentId = s.Id
                        INNER JOIN Users u ON g.TeacherId = u.Id
                        WHERE s.UserId = @userId
                        ORDER BY g.Date DESC";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", LoginForm.CurrentUser.Id);

                        using (var reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            dataGridViewGrades.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки оценок: {ex.Message}");
            }
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

        private void StudentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Полное завершение приложения при закрытии формы
            Application.Exit();
        }
    }
}