using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace carwash
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, EventArgs e)
        {
            // Получаем введенные данные из текстовых полей
            string login = usernameTb.Text;
            string password = passwordTb.Text;

            // Выполняем проверку на заполненность полей
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || login == "Имя пользователя" || password == "Пароль")
            {
                MessageBox.Show("Пожалуйста, заполните все поля и введите корректные данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Выполняем проверку на наличие кириллических символов в логине и пароле
            if (ContainsCyrillicCharacters(login) || ContainsCyrillicCharacters(password))
            {
                MessageBox.Show("Пожалуйста, используйте только латинские символы в логине и пароле.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Создайте команду для выполнения SQL-запроса
                string query = "INSERT INTO users (login_user, password_user) VALUES (@login, @password)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Добавьте параметры для логина и пароля
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);

                    // Выполните команду
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private bool ContainsCyrillicCharacters(string text)
        {
            Regex regex = new Regex(@"\p{IsCyrillic}");
            return regex.IsMatch(text);
        }

        private void usernameTb_Enter(object sender, EventArgs e)
        {
            if (usernameTb.Text == "Имя пользователя")
            {
                usernameTb.ForeColor = Color.Black;
                usernameTb.Text = "";
            }
        }

        private void usernameTb_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTb.Text))
            {
                usernameTb.ForeColor = Color.Gray;
                usernameTb.Text = "Имя пользователя";
            }
        }

        private void passwordTb_Enter(object sender, EventArgs e)
        {
            if (passwordTb.Text == "Пароль")
            {
                passwordTb.ForeColor = Color.Black;
                passwordTb.Text = "";
            }
        }

        private void passwordTb_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passwordTb.Text))
            {
                passwordTb.ForeColor = Color.Gray;
                passwordTb.Text = "Пароль";
            }
        }
    }
}
