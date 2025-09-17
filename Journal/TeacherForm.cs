using System;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace SchoolJournal
{
    public partial class TeacherForm : Form
    {
        public TeacherForm()
        {
            InitializeComponent();

            // Подписка на событие закрытия формы
            this.FormClosing += TeacherForm_FormClosing;
        }

        private void TeacherForm_Load(object sender, EventArgs e)
        {
            lblTeacherName.Text = LoginForm.CurrentUser.FullName;
            LoadStudents();
            LoadGrades();
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

        private void LoadGrades()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT g.Id, u.FullName as StudentName, g.Subject, g.Grade, g.Date
                        FROM Grades g
                        INNER JOIN Students s ON g.StudentId = s.Id
                        INNER JOIN Users u ON s.UserId = u.Id
                        WHERE g.TeacherId = @teacherId
                        ORDER BY g.Date DESC";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@teacherId", LoginForm.CurrentUser.Id);

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

        private void btnAddGrade_Click(object sender, EventArgs e)
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

                    string query = @"
                        INSERT INTO Grades (StudentId, Subject, Grade, Date, TeacherId)
                        VALUES (@studentId, @subject, @grade, @date, @teacherId)";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@studentId", studentId);
                        command.Parameters.AddWithValue("@subject", cmbSubject.Text);
                        command.Parameters.AddWithValue("@grade", (int)numericGrade.Value);
                        command.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@teacherId", LoginForm.CurrentUser.Id);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Оценка добавлена");
                        LoadGrades();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления оценки: {ex.Message}");
            }
        }

        private void btnEditGrade_Click(object sender, EventArgs e)
        {
            if (dataGridViewGrades.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите оценку для редактирования");
                return;
            }

            var selectedRow = dataGridViewGrades.SelectedRows[0];
            int gradeId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            using (var form = new EditGradeForm(gradeId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadGrades();
                }
            }
        }

        private void btnDeleteGrade_Click(object sender, EventArgs e)
        {
            if (dataGridViewGrades.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите оценку для удаления");
                return;
            }

            if (MessageBox.Show("Удалить выбранную оценку?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var selectedRow = dataGridViewGrades.SelectedRows[0];
                int gradeId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                try
                {
                    using (var connection = DatabaseHelper.GetConnection())
                    {
                        connection.Open();

                        string query = "DELETE FROM Grades WHERE Id = @id";

                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", gradeId);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Оценка удалена");
                            LoadGrades();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления оценки: {ex.Message}");
                }
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

        private void TeacherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Полное завершение приложения при закрытии формы
            Application.Exit();
        }
    }
}