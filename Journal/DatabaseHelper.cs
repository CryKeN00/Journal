using Microsoft.Data.Sqlite;
using System.IO;
using System;

public static class DatabaseHelper
{
    private static string databasePath = "school_journal.db";
    private static string connectionString = $"Data Source={databasePath};";

    public static SqliteConnection GetConnection()
    {
        return new SqliteConnection(connectionString);
    }

    public static void InitializeDatabase()
    {
        if (!File.Exists(databasePath))
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Таблица пользователей
                string createUsersTable = @"
                    CREATE TABLE Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT UNIQUE NOT NULL,
                        Password TEXT NOT NULL,
                        Role TEXT NOT NULL CHECK(Role IN ('Student', 'Teacher', 'Admin', 'Parent')),
                        FullName TEXT NOT NULL
                    )";

                // Таблица студентов
                string createStudentsTable = @"
                    CREATE TABLE Students (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        Class TEXT NOT NULL,
                        FOREIGN KEY (UserId) REFERENCES Users(Id)
                    )";

                // Таблица связей родитель-ученик
                string createParentStudentsTable = @"
                    CREATE TABLE ParentStudents (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ParentId INTEGER NOT NULL,
                        StudentId INTEGER NOT NULL,
                        FOREIGN KEY (ParentId) REFERENCES Users(Id),
                        FOREIGN KEY (StudentId) REFERENCES Students(Id),
                        UNIQUE(ParentId, StudentId)
                    )";

                // Таблица предметов учителей
                string createTeacherSubjectsTable = @"
                    CREATE TABLE TeacherSubjects (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        TeacherId INTEGER NOT NULL,
                        Subject TEXT NOT NULL,
                        FOREIGN KEY (TeacherId) REFERENCES Users(Id),
                        UNIQUE(TeacherId, Subject)
                    )";

                // Таблица оценок
                string createGradesTable = @"
                    CREATE TABLE Grades (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StudentId INTEGER NOT NULL,
                        Subject TEXT NOT NULL,
                        Grade INTEGER NOT NULL CHECK(Grade BETWEEN 1 AND 5),
                        Date TEXT NOT NULL,
                        TeacherId INTEGER NOT NULL,
                        Note TEXT,
                        FOREIGN KEY (StudentId) REFERENCES Students(Id),
                        FOREIGN KEY (TeacherId) REFERENCES Users(Id)
                    )";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = createUsersTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createStudentsTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createParentStudentsTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createTeacherSubjectsTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createGradesTable;
                    command.ExecuteNonQuery();
                }

                // Добавляем администратора по умолчанию
                AddDefaultAdmin(connection);
            }
        }
    }

    private static void AddDefaultAdmin(SqliteConnection connection)
    {
        string insertAdmin = @"
            INSERT INTO Users (Username, Password, Role, FullName)
            SELECT 'admin', 'admin', 'Admin', 'Администратор системы'
            WHERE NOT EXISTS (SELECT 1 FROM Users WHERE Username = 'admin')";

        using (var command = connection.CreateCommand())
        {
            command.CommandText = insertAdmin;
            command.ExecuteNonQuery();
        }
    }
}