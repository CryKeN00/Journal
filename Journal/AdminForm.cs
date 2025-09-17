using System;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace SchoolJournal
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();

            // Подписка на событие закрытия формы
            this.FormClosing += AdminForm_FormClosing;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            lblAdminName.Text = LoginForm.CurrentUser.FullName;
            LoadStudents();
            LoadTeachers();
        }

        private void LoadStudents()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT s.Id, u.Username, u.FullName, s.Class
                        FROM Students s
                        INNER JOIN Users u ON s.UserId = u.Id
                        ORDER BY u.FullName";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            dataGridViewStudents.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки студентов: {ex.Message}");
            }
        }

        private void LoadTeachers()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT Id, Username, FullName
                        FROM Users
                        WHERE Role = 'Teacher'
                        ORDER BY FullName";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            dataGridViewTeachers.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки преподавателей: {ex.Message}");
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            using (var form = new EditUserForm("Student"))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadStudents();
                }
            }
        }

        private void btnEditStudent_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите студента для редактирования");
                return;
            }

            var selectedRow = dataGridViewStudents.SelectedRows[0];
            int studentId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            using (var form = new EditUserForm("Student", studentId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadStudents();
                }
            }
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите студента для удаления");
                return;
            }

            if (MessageBox.Show("Удалить выбранного студента?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var selectedRow = dataGridViewStudents.SelectedRows[0];
                int studentId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                try
                {
                    using (var connection = DatabaseHelper.GetConnection())
                    {
                        connection.Open();

                        // Сначала удаляем оценки студента
                        string deleteGrades = "DELETE FROM Grades WHERE StudentId = @id";
                        using (var command = new SqliteCommand(deleteGrades, connection))
                        {
                            command.Parameters.AddWithValue("@id", studentId);
                            command.ExecuteNonQuery();
                        }

                        // Затем удаляем студента
                        string deleteStudent = "DELETE FROM Students WHERE Id = @id";
                        using (var command = new SqliteCommand(deleteStudent, connection))
                        {
                            command.Parameters.AddWithValue("@id", studentId);
                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Студент удален");
                        LoadStudents();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления студента: {ex.Message}");
                }
            }
        }

        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            using (var form = new EditUserForm("Teacher"))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadTeachers();
                }
            }
        }

        private void btnEditTeacher_Click(object sender, EventArgs e)
        {
            if (dataGridViewTeachers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите преподавателя для редактирования");
                return;
            }

            var selectedRow = dataGridViewTeachers.SelectedRows[0];
            int teacherId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            using (var form = new EditUserForm("Teacher", teacherId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadTeachers();
                }
            }
        }

        private void btnDeleteTeacher_Click(object sender, EventArgs e)
        {
            if (dataGridViewTeachers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите преподавателя для удаления");
                return;
            }

            if (MessageBox.Show("Удалить выбранного преподавателя?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var selectedRow = dataGridViewTeachers.SelectedRows[0];
                int teacherId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                try
                {
                    using (var connection = DatabaseHelper.GetConnection())
                    {
                        connection.Open();

                        // Удаляем пользователя
                        string deleteUser = "DELETE FROM Users WHERE Id = @id AND Role = 'Teacher'";
                        using (var command = new SqliteCommand(deleteUser, connection))
                        {
                            command.Parameters.AddWithValue("@id", teacherId);
                            int affected = command.ExecuteNonQuery();

                            if (affected > 0)
                            {
                                MessageBox.Show("Преподаватель удален");
                                LoadTeachers();
                            }
                            else
                            {
                                MessageBox.Show("Ошибка удаления преподавателя");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления преподавателя: {ex.Message}");
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStudents();
            LoadTeachers();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Полное завершение приложения при закрытии формы
            Application.Exit();
        }
    }
}