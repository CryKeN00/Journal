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
            Text = isEditMode ? $"Редактирование {GetRoleName(role)}" : $"Добавление {GetRoleName(role)}";

            // Показываем/скрываем поля в зависимости от роли
            bool isStudent = (role == "Student");
            label4.Visible = isStudent;
            txtClass.Visible = isStudent;

            // Сдвигаем кнопки если нужно
            if (isStudent)
            {
                btnSave.Location = new System.Drawing.Point(120, 150);
                btnCancel.Location = new System.Drawing.Point(210, 150);
                this.Height = 220;
            }
            else
            {
                btnSave.Location = new System.Drawing.Point(120, 120);
                btnCancel.Location = new System.Drawing.Point(210, 120);
                this.Height = 190;
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
            }
        }

        private string GetRoleName(string role)
        {
            return role switch
            {
                "Student" => "студента",
                "Teacher" => "преподавателя",
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

                    string query = "SELECT Username, Password, FullName FROM Users WHERE Id = @id";

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
                            string updateStudent = "UPDATE Students SET Class = @class WHERE UserId = @userId";
                            using (var command = new SqliteCommand(updateStudent, connection))
                            {
                                command.Parameters.AddWithValue("@class", txtClass.Text);
                                command.Parameters.AddWithValue("@userId", userId);
                                command.ExecuteNonQuery();
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
                            VALUES (@username, @password, @role, @fullName)";

                        using (var command = new SqliteCommand(insertUser, connection))
                        {
                            command.Parameters.AddWithValue("@username", txtUsername.Text);
                            command.Parameters.AddWithValue("@password", txtPassword.Text);
                            command.Parameters.AddWithValue("@role", role);
                            command.Parameters.AddWithValue("@fullName", txtFullName.Text);
                            command.ExecuteNonQuery();
                        }

                        if (role == "Student")
                        {
                            // Получаем ID нового пользователя
                            string getUserId = "SELECT last_insert_rowid()";
                            int newUserId;
                            using (var command = new SqliteCommand(getUserId, connection))
                            {
                                newUserId = Convert.ToInt32(command.ExecuteScalar());
                            }

                            string insertStudent = "INSERT INTO Students (UserId, Class) VALUES (@userId, @class)";
                            using (var command = new SqliteCommand(insertStudent, connection))
                            {
                                command.Parameters.AddWithValue("@userId", newUserId);
                                command.Parameters.AddWithValue("@class", txtClass.Text);
                                command.ExecuteNonQuery();
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