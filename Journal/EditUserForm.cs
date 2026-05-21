using System;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace SchoolJournal
{
    public partial class EditUserForm : Form
    {
        private string role;
        private int userId;
        private bool isEditMode;

        public EditUserForm(string role, int userId = 0)
        {
            InitializeComponent();
            this.role = role;
            this.userId = userId;
            this.isEditMode = userId > 0;
        }

        private void EditUserForm_Load(object sender, EventArgs e)
        {
            // Если это режим редактирования, определяем реальную роль пользователя
            if (isEditMode)
            {
                DetermineRealRole();
            }

            Text = isEditMode ? $"Редактирование {GetRoleName(role)}" : $"Добавление {GetRoleName(role)}";

            // Настраиваем видимость полей в зависимости от роли
            if (role == "Student")
            {
                label4.Visible = true;
                txtClass.Visible = true;
                label5.Visible = false;
                txtSubject.Visible = false;
                this.Height = 320;
            }
            else if (role == "Teacher")
            {
                label4.Visible = false;
                txtClass.Visible = false;
                label5.Visible = true;
                txtSubject.Visible = true;
                this.Height = 320;
            }
            else
            {
                label4.Visible = false;
                txtClass.Visible = false;
                label5.Visible = false;
                txtSubject.Visible = false;
                this.Height = 280;
            }

            if (isEditMode)
            {
                LoadUserData();
            }
            else
            {
                // Для новых пользователей генерируем пароль по умолчанию
                if (role == "Student")
                {
                    txtPassword.Text = "student123";
                }
                else if (role == "Teacher")
                {
                    txtPassword.Text = "teacher123";
                }
                else if (role == "Parent")
                {
                    txtPassword.Text = "parent123";
                }
            }
        }

        // Метод для определения реальной роли пользователя по его ID
        private void DetermineRealRole()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Role FROM Users WHERE Id = @id";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", userId);
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            string actualRole = result.ToString();
                            if (actualRole != role)
                            {
                                role = actualRole;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка определения роли: {ex.Message}");
            }
        }

        private string GetRoleName(string role)
        {
            return role switch
            {
                "Student" => "студента",
                "Teacher" => "преподавателя",
                "Parent" => "родителя",
                "Admin" => "администратора",
                _ => "пользователя"
            };
        }

        private void LoadUserData()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Username, Password, FullName, Role FROM Users WHERE Id = @id";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", userId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtUsername.Text = reader.GetString(0);
                                txtPassword.Text = reader.GetString(1);
                                txtFullName.Text = reader.GetString(2);
                                string actualRole = reader.GetString(3);
                                if (actualRole != role)
                                {
                                    role = actualRole;
                                    Text = $"Редактирование {GetRoleName(role)}";
                                }
                            }
                        }
                    }

                    if (role == "Student")
                    {
                        query = "SELECT Class FROM Students WHERE UserId = @userId";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@userId", userId);
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtClass.Text = reader.GetString(0);
                                }
                            }
                        }
                    }
                    else if (role == "Teacher")
                    {
                        // Загружаем предметы учителя (если несколько, через запятую)
                        query = "SELECT Subject FROM TeacherSubjects WHERE TeacherId = @teacherId";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@teacherId", userId);
                            using (var reader = command.ExecuteReader())
                            {
                                string subjects = "";
                                while (reader.Read())
                                {
                                    if (subjects != "") subjects += ", ";
                                    subjects += reader.GetString(0);
                                }
                                txtSubject.Text = subjects;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) ||
                string.IsNullOrEmpty(txtPassword.Text) ||
                string.IsNullOrEmpty(txtFullName.Text))
            {
                MessageBox.Show("Заполните все обязательные поля");
                return;
            }

            if (role == "Student" && string.IsNullOrEmpty(txtClass.Text))
            {
                MessageBox.Show("Заполните поле 'Класс'");
                return;
            }

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    if (isEditMode)
                    {
                        // Проверяем, не занят ли логин другим пользователем
                        string checkUsername = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Id != @id";
                        using (var checkCommand = new SqliteCommand(checkUsername, connection))
                        {
                            checkCommand.Parameters.AddWithValue("@username", txtUsername.Text);
                            checkCommand.Parameters.AddWithValue("@id", userId);
                            long count = (long)checkCommand.ExecuteScalar();

                            if (count > 0)
                            {
                                MessageBox.Show("Пользователь с таким логином уже существует");
                                return;
                            }
                        }

                        // Обновляем пользователя
                        string updateUser = @"
                            UPDATE Users 
                            SET Username = @username, Password = @password, FullName = @fullName 
                            WHERE Id = @id";

                        using (var command = new SqliteCommand(updateUser, connection))
                        {
                            command.Parameters.AddWithValue("@username", txtUsername.Text);
                            command.Parameters.AddWithValue("@password", txtPassword.Text);
                            command.Parameters.AddWithValue("@fullName", txtFullName.Text);
                            command.Parameters.AddWithValue("@id", userId);
                            command.ExecuteNonQuery();
                        }

                        if (role == "Student")
                        {
                            string checkStudent = "SELECT COUNT(*) FROM Students WHERE UserId = @userId";
                            using (var checkCommand = new SqliteCommand(checkStudent, connection))
                            {
                                checkCommand.Parameters.AddWithValue("@userId", userId);
                                long count = (long)checkCommand.ExecuteScalar();

                                if (count > 0)
                                {
                                    string updateStudent = "UPDATE Students SET Class = @class WHERE UserId = @userId";
                                    using (var command = new SqliteCommand(updateStudent, connection))
                                    {
                                        command.Parameters.AddWithValue("@class", txtClass.Text);
                                        command.Parameters.AddWithValue("@userId", userId);
                                        command.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    string insertStudent = "INSERT INTO Students (UserId, Class) VALUES (@userId, @class)";
                                    using (var command = new SqliteCommand(insertStudent, connection))
                                    {
                                        command.Parameters.AddWithValue("@userId", userId);
                                        command.Parameters.AddWithValue("@class", txtClass.Text);
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        else if (role == "Teacher")
                        {
                            // Сохраняем предметы учителя
                            // Сначала удаляем старые
                            string deleteSubjects = "DELETE FROM TeacherSubjects WHERE TeacherId = @teacherId";
                            using (var command = new SqliteCommand(deleteSubjects, connection))
                            {
                                command.Parameters.AddWithValue("@teacherId", userId);
                                command.ExecuteNonQuery();
                            }

                            // Добавляем новые предметы (разделенные запятой)
                            if (!string.IsNullOrEmpty(txtSubject.Text))
                            {
                                string[] subjects = txtSubject.Text.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                string insertSubject = "INSERT INTO TeacherSubjects (TeacherId, Subject) VALUES (@teacherId, @subject)";

                                foreach (string subject in subjects)
                                {
                                    string trimmedSubject = subject.Trim();
                                    if (!string.IsNullOrEmpty(trimmedSubject))
                                    {
                                        using (var command = new SqliteCommand(insertSubject, connection))
                                        {
                                            command.Parameters.AddWithValue("@teacherId", userId);
                                            command.Parameters.AddWithValue("@subject", trimmedSubject);
                                            command.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // Проверяем, не занят ли логин
                        string checkUsername = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                        using (var checkCommand = new SqliteCommand(checkUsername, connection))
                        {
                            checkCommand.Parameters.AddWithValue("@username", txtUsername.Text);
                            long count = (long)checkCommand.ExecuteScalar();

                            if (count > 0)
                            {
                                MessageBox.Show("Пользователь с таким логином уже существует");
                                return;
                            }
                        }

                        // Добавляем нового пользователя
                        string insertUser = @"
                            INSERT INTO Users (Username, Password, Role, FullName)
                            VALUES (@username, @password, @role, @fullName);
                            SELECT last_insert_rowid();";

                        int newUserId;
                        using (var command = new SqliteCommand(insertUser, connection))
                        {
                            command.Parameters.AddWithValue("@username", txtUsername.Text);
                            command.Parameters.AddWithValue("@password", txtPassword.Text);
                            command.Parameters.AddWithValue("@role", role);
                            command.Parameters.AddWithValue("@fullName", txtFullName.Text);
                            newUserId = Convert.ToInt32(command.ExecuteScalar());
                        }

                        if (role == "Student")
                        {
                            string insertStudent = "INSERT INTO Students (UserId, Class) VALUES (@userId, @class)";
                            using (var command = new SqliteCommand(insertStudent, connection))
                            {
                                command.Parameters.AddWithValue("@userId", newUserId);
                                command.Parameters.AddWithValue("@class", txtClass.Text);
                                command.ExecuteNonQuery();
                            }
                        }
                        else if (role == "Teacher")
                        {
                            // Добавляем предметы учителя
                            if (!string.IsNullOrEmpty(txtSubject.Text))
                            {
                                string[] subjects = txtSubject.Text.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                string insertSubject = "INSERT INTO TeacherSubjects (TeacherId, Subject) VALUES (@teacherId, @subject)";

                                foreach (string subject in subjects)
                                {
                                    string trimmedSubject = subject.Trim();
                                    if (!string.IsNullOrEmpty(trimmedSubject))
                                    {
                                        using (var command = new SqliteCommand(insertSubject, connection))
                                        {
                                            command.Parameters.AddWithValue("@teacherId", newUserId);
                                            command.Parameters.AddWithValue("@subject", trimmedSubject);
                                            command.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    MessageBox.Show("Данные сохранены успешно!");
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (SqliteException ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
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