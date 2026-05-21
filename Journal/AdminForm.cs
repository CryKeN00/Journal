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
            this.FormClosing += AdminForm_FormClosing;

            // Подписываем события на кнопки
            btnAddStudent.Click += btnAddStudent_Click;
            btnEditStudent.Click += btnEditStudent_Click;
            btnDeleteStudent.Click += btnDeleteStudent_Click;
            btnAddTeacher.Click += btnAddTeacher_Click;
            btnEditTeacher.Click += btnEditTeacher_Click;
            btnDeleteTeacher.Click += btnDeleteTeacher_Click;
            btnManageSubjects.Click += btnManageSubjects_Click;
            btnAddParent.Click += btnAddParent_Click;
            btnEditParent.Click += btnEditParent_Click;
            btnDeleteParent.Click += btnDeleteParent_Click;
            btnLinkChildren.Click += btnLinkChildren_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnLogout.Click += btnLogout_Click;

            // Подписываем события на ошибки DataGridView
            dataGridViewStudents.DataError += DataGridView_DataError;
            dataGridViewTeachers.DataError += DataGridView_DataError;
            dataGridViewParents.DataError += DataGridView_DataError;
        }

        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Игнорируем ошибки форматирования
            e.ThrowException = false;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            lblAdminName.Text = LoginForm.CurrentUser.FullName;
            LoadStudents();
            LoadTeachers();
            LoadParents();
        }

        private void LoadStudents()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT s.Id as StudentRecordId, s.UserId, u.Username, u.FullName, s.Class
                        FROM Students s
                        INNER JOIN Users u ON s.UserId = u.Id
                        ORDER BY u.FullName";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            // Очищаем источник данных перед установкой нового
                            dataGridViewStudents.DataSource = null;
                            dataGridViewStudents.DataSource = dt;

                            // Настройка колонок после установки источника данных
                            if (dataGridViewStudents.Columns["StudentRecordId"] != null)
                            {
                                dataGridViewStudents.Columns["StudentRecordId"].HeaderText = "ID";
                                dataGridViewStudents.Columns["StudentRecordId"].Visible = true;
                            }
                            if (dataGridViewStudents.Columns["UserId"] != null)
                            {
                                dataGridViewStudents.Columns["UserId"].Visible = false;
                            }
                            if (dataGridViewStudents.Columns["Username"] != null)
                            {
                                dataGridViewStudents.Columns["Username"].HeaderText = "Логин";
                            }
                            if (dataGridViewStudents.Columns["FullName"] != null)
                            {
                                dataGridViewStudents.Columns["FullName"].HeaderText = "ФИО";
                            }
                            if (dataGridViewStudents.Columns["Class"] != null)
                            {
                                dataGridViewStudents.Columns["Class"].HeaderText = "Класс";
                            }

                            dataGridViewStudents.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
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
                        SELECT u.Id, u.Username, u.FullName, 
                               IFNULL(GROUP_CONCAT(ts.Subject, ', '), '') as Subjects
                        FROM Users u
                        LEFT JOIN TeacherSubjects ts ON u.Id = ts.TeacherId
                        WHERE u.Role = 'Teacher'
                        GROUP BY u.Id
                        ORDER BY u.FullName";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            // Очищаем источник данных перед установкой нового
                            dataGridViewTeachers.DataSource = null;
                            dataGridViewTeachers.DataSource = dt;

                            if (dataGridViewTeachers.Columns["Id"] != null)
                            {
                                dataGridViewTeachers.Columns["Id"].HeaderText = "ID";
                                dataGridViewTeachers.Columns["Id"].Visible = true;
                            }
                            if (dataGridViewTeachers.Columns["Username"] != null)
                            {
                                dataGridViewTeachers.Columns["Username"].HeaderText = "Логин";
                            }
                            if (dataGridViewTeachers.Columns["FullName"] != null)
                            {
                                dataGridViewTeachers.Columns["FullName"].HeaderText = "ФИО";
                            }
                            if (dataGridViewTeachers.Columns["Subjects"] != null)
                            {
                                dataGridViewTeachers.Columns["Subjects"].HeaderText = "Предметы";
                            }

                            dataGridViewTeachers.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки преподавателей: {ex.Message}");
            }
        }

        private void LoadParents()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT Id, Username, FullName
                        FROM Users
                        WHERE Role = 'Parent'
                        ORDER BY FullName";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            // Очищаем источник данных перед установкой нового
                            dataGridViewParents.DataSource = null;
                            dataGridViewParents.DataSource = dt;

                            if (dataGridViewParents.Columns["Id"] != null)
                            {
                                dataGridViewParents.Columns["Id"].HeaderText = "ID";
                            }
                            if (dataGridViewParents.Columns["Username"] != null)
                            {
                                dataGridViewParents.Columns["Username"].HeaderText = "Логин";
                            }
                            if (dataGridViewParents.Columns["FullName"] != null)
                            {
                                dataGridViewParents.Columns["FullName"].HeaderText = "ФИО";
                            }

                            dataGridViewParents.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки родителей: {ex.Message}");
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

            if (!dataGridViewStudents.Columns.Contains("UserId"))
            {
                MessageBox.Show("Ошибка: колонка UserId не найдена");
                return;
            }

            int userId = Convert.ToInt32(selectedRow.Cells["UserId"].Value);

            using (var form = new EditUserForm("Student", userId))
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

                if (!dataGridViewStudents.Columns.Contains("StudentRecordId"))
                {
                    MessageBox.Show("Ошибка: колонка StudentRecordId не найдена");
                    return;
                }

                int studentRecordId = Convert.ToInt32(selectedRow.Cells["StudentRecordId"].Value);
                int userId = Convert.ToInt32(selectedRow.Cells["UserId"].Value);

                try
                {
                    using (var connection = DatabaseHelper.GetConnection())
                    {
                        connection.Open();

                        string deleteGrades = "DELETE FROM Grades WHERE StudentId = @studentRecordId";
                        using (var command = new SqliteCommand(deleteGrades, connection))
                        {
                            command.Parameters.AddWithValue("@studentRecordId", studentRecordId);
                            command.ExecuteNonQuery();
                        }

                        string deleteStudent = "DELETE FROM Students WHERE Id = @studentRecordId";
                        using (var command = new SqliteCommand(deleteStudent, connection))
                        {
                            command.Parameters.AddWithValue("@studentRecordId", studentRecordId);
                            command.ExecuteNonQuery();
                        }

                        string deleteUser = "DELETE FROM Users WHERE Id = @userId";
                        using (var command = new SqliteCommand(deleteUser, connection))
                        {
                            command.Parameters.AddWithValue("@userId", userId);
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

            if (!dataGridViewTeachers.Columns.Contains("Id"))
            {
                MessageBox.Show("Ошибка: колонка Id не найдена");
                return;
            }

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

                if (!dataGridViewTeachers.Columns.Contains("Id"))
                {
                    MessageBox.Show("Ошибка: колонка Id не найдена");
                    return;
                }

                int teacherId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                try
                {
                    using (var connection = DatabaseHelper.GetConnection())
                    {
                        connection.Open();

                        string deleteSubjects = "DELETE FROM TeacherSubjects WHERE TeacherId = @id";
                        using (var command = new SqliteCommand(deleteSubjects, connection))
                        {
                            command.Parameters.AddWithValue("@id", teacherId);
                            command.ExecuteNonQuery();
                        }

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

        private void btnManageSubjects_Click(object sender, EventArgs e)
        {
            if (dataGridViewTeachers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите преподавателя для редактирования предметов");
                return;
            }

            var selectedRow = dataGridViewTeachers.SelectedRows[0];
            int teacherId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            using (var form = new EditUserForm("Teacher", teacherId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadTeachers();
                    MessageBox.Show("Предметы учителя обновлены");
                }
            }
        }

        private void btnAddParent_Click(object sender, EventArgs e)
        {
            using (var form = new EditUserForm("Parent"))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadParents();
                }
            }
        }

        private void btnEditParent_Click(object sender, EventArgs e)
        {
            if (dataGridViewParents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите родителя для редактирования");
                return;
            }

            var selectedRow = dataGridViewParents.SelectedRows[0];

            if (!dataGridViewParents.Columns.Contains("Id"))
            {
                MessageBox.Show("Ошибка: колонка Id не найдена");
                return;
            }

            int parentId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            using (var form = new EditUserForm("Parent", parentId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadParents();
                }
            }
        }

        private void btnDeleteParent_Click(object sender, EventArgs e)
        {
            if (dataGridViewParents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите родителя для удаления");
                return;
            }

            if (MessageBox.Show("Удалить выбранного родителя?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var selectedRow = dataGridViewParents.SelectedRows[0];

                if (!dataGridViewParents.Columns.Contains("Id"))
                {
                    MessageBox.Show("Ошибка: колонка Id не найдена");
                    return;
                }

                int parentId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                try
                {
                    using (var connection = DatabaseHelper.GetConnection())
                    {
                        connection.Open();

                        string deleteLinks = "DELETE FROM ParentStudents WHERE ParentId = @id";
                        using (var command = new SqliteCommand(deleteLinks, connection))
                        {
                            command.Parameters.AddWithValue("@id", parentId);
                            command.ExecuteNonQuery();
                        }

                        string deleteUser = "DELETE FROM Users WHERE Id = @id AND Role = 'Parent'";
                        using (var command = new SqliteCommand(deleteUser, connection))
                        {
                            command.Parameters.AddWithValue("@id", parentId);
                            int affected = command.ExecuteNonQuery();

                            if (affected > 0)
                            {
                                MessageBox.Show("Родитель удален");
                                LoadParents();
                            }
                            else
                            {
                                MessageBox.Show("Ошибка удаления родителя");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления родителя: {ex.Message}");
                }
            }
        }

        private void btnLinkChildren_Click(object sender, EventArgs e)
        {
            if (dataGridViewParents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите родителя для привязки детей");
                return;
            }

            var selectedRow = dataGridViewParents.SelectedRows[0];

            if (!dataGridViewParents.Columns.Contains("Id") || !dataGridViewParents.Columns.Contains("FullName"))
            {
                MessageBox.Show("Ошибка: необходимые колонки не найдены");
                return;
            }

            int parentId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
            string parentName = selectedRow.Cells["FullName"].Value.ToString();

            using (var form = new LinkChildrenForm(parentId, parentName))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadParents();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStudents();
            LoadTeachers();
            LoadParents();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}