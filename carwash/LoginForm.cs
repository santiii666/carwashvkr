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

namespace carwash
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            string username = usernameTb.Text;
            string password = passwordTb.Text;

            if (username != "Имя пользователя" && password != "Пароль")
            {
                string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM users WHERE login_user = @username AND password_user = @password";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                        {
                            // Успешная аутентификация
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            // Неверный логин или пароль
                            MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                // Вывести сообщение о неправильном вводе имени пользователя или пароля
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                Application.Exit();
            }
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
            passwordTb.isPassword = true;
        }

        private void passwordTb_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passwordTb.Text))
            {
                passwordTb.ForeColor = Color.Gray;
                passwordTb.Text = "Пароль";
                passwordTb.isPassword = false;
            }
        }

        private bool mouseIsDown = false;
        private System.Windows.Forms.Timer timer;
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            mouseIsDown = true;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100; // установите необходимый интервал времени
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
            timer.Stop();
            passwordTb.isPassword = true;
            pictureBox3.Image = Properties.Resources.hidden;          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (mouseIsDown)
            {
                passwordTb.isPassword = false;
                pictureBox3.Image = Properties.Resources.vieweye;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Создаем и открываем форму регистрации
            RegForm regForm = new RegForm();
            regForm.ShowDialog();
        }
    }
}
