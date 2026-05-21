using System;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SchoolJournal
{
    public partial class TeacherForm : Form
    {
        private List<string> teacherSubjects;

        public TeacherForm()
        {
            InitializeComponent();
            this.FormClosing += TeacherForm_FormClosing;
        }

        private void TeacherForm_Load(object sender, EventArgs e)
        {
            lblTeacherName.Text = LoginForm.CurrentUser.FullName;
            LoadTeacherSubjects();
            LoadStudents();
            LoadGrades();
        }

        private void LoadTeacherSubjects()
        {
            try
            {
                teacherSubjects = new List<string>();

                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT Subject
                        FROM TeacherSubjects
                        WHERE TeacherId = @teacherId";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@teacherId", LoginForm.CurrentUser.Id);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string subject = reader.GetString(0);
                                teacherSubjects.Add(subject);
                            }
                        }
                    }
                }

                // Заполняем ComboBox предметами учителя
                cmbSubject.Items.Clear();
                foreach (var subject in teacherSubjects)
                {
                    cmbSubject.Items.Add(subject);
                }

                // Добавляем также стандартные предметы, если список пуст
                if (cmbSubject.Items.Count == 0)
                {
                    string[] defaultSubjects = { "Математика", "Русский язык", "Литература", "Физика", "Химия", "Биология", "История", "Обществознание", "География", "Английский язык", "Физкультура", "Информатика" };
                    foreach (var subject in defaultSubjects)
                    {
                        cmbSubject.Items.Add(subject);
                    }
                }

                if (cmbSubject.Items.Count > 0)
                {
                    cmbSubject.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки предметов: {ex.Message}");
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
                        SELECT s.Id, u.FullName, s.Class
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
                                    Name = $"{reader.GetString(1)} (Класс: {reader.GetString(2)})"
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
                        SELECT g.Id, u.FullName as StudentName, g.Subject, g.Grade, g.Date, g.Note
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

                            // Настройка заголовков
                            if (dataGridViewGrades.Columns.Contains("Id"))
                                dataGridViewGrades.Columns["Id"].Visible = false;
                            if (dataGridViewGrades.Columns.Contains("StudentName"))
                                dataGridViewGrades.Columns["StudentName"].HeaderText = "Ученик";
                            if (dataGridViewGrades.Columns.Contains("Subject"))
                                dataGridViewGrades.Columns["Subject"].HeaderText = "Предмет";
                            if (dataGridViewGrades.Columns.Contains("Grade"))
                                dataGridViewGrades.Columns["Grade"].HeaderText = "Оценка";
                            if (dataGridViewGrades.Columns.Contains("Date"))
                                dataGridViewGrades.Columns["Date"].HeaderText = "Дата";
                            if (dataGridViewGrades.Columns.Contains("Note"))
                                dataGridViewGrades.Columns["Note"].HeaderText = "Примечание";

                            dataGridViewGrades.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
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
                        INSERT INTO Grades (StudentId, Subject, Grade, Date, TeacherId, Note)
                        VALUES (@studentId, @subject, @grade, @date, @teacherId, @note)";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@studentId", studentId);
                        command.Parameters.AddWithValue("@subject", cmbSubject.Text);
                        command.Parameters.AddWithValue("@grade", (int)numericGrade.Value);
                        command.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@teacherId", LoginForm.CurrentUser.Id);
                        command.Parameters.AddWithValue("@note", string.IsNullOrEmpty(txtNote.Text) ? (object)DBNull.Value : txtNote.Text);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Оценка добавлена");
                        LoadGrades();

                        // Очищаем поле примечания
                        txtNote.Text = "";
                        numericGrade.Value = 5;
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
            Application.Exit();
        }
    }
}