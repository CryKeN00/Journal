using System;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace SchoolJournal
{
    public partial class LoginForm : Form
    {
        public static User CurrentUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();

            // Подписываемся на событие закрытия формы
            this.FormClosing += LoginForm_FormClosing;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            DatabaseHelper.InitializeDatabase();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Заполните все поля";
                return;
            }

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Id, Username, Password, Role, FullName FROM Users WHERE Username = @username";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedPassword = reader["Password"].ToString();

                                if (password == storedPassword)
                                {
                                    CurrentUser = new User
                                    {
                                        Id = reader.GetInt32(0),
                                        Username = reader.GetString(1),
                                        Password = reader.GetString(2),
                                        Role = reader.GetString(3),
                                        FullName = reader.GetString(4)
                                    };

                                    OpenRoleSpecificForm();
                                    this.Hide();
                                }
                                else
                                {
                                    lblError.Text = "Неверный пароль";
                                }
                            }
                            else
                            {
                                lblError.Text = "Пользователь не найден";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка авторизации: {ex.Message}");
            }
        }

        private void OpenRoleSpecificForm()
        {
            switch (CurrentUser.Role)
            {
                case "Student":
                    var studentForm = new StudentForm();
                    studentForm.Show();
                    break;
                case "Teacher":
                    var teacherForm = new TeacherForm();
                    teacherForm.Show();
                    break;
                case "Admin":
                    var adminForm = new AdminForm();
                    adminForm.Show();
                    break;
                case "Parent":
                    var parentForm = new ParentForm();
                    parentForm.Show();
                    break;
                default:
                    MessageBox.Show($"Неизвестная роль пользователя: {CurrentUser.Role}");
                    break;
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Полное завершение приложения при закрытии формы
            Application.Exit();
        }
    }
}