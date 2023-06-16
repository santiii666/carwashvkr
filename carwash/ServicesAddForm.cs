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
    public partial class ServicesAddForm : Form
    {
        public ServicesAddForm()
        {
            InitializeComponent();
            OnLoad(EventArgs.Empty);
        }

        public int SelectedServiceId { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (SelectedServiceId > 0)
            {
                // Изменение текста кнопки и названия формы для режима изменения
                ServiceAddButton.Text = "Изменить";
                this.Text = "Изменить услугу";

                // Установка значения в ServiceAddTb1
                ServiceAddTb1.Text = SelectedServiceId.ToString();

                // Блокировка поля ServiceAddTb1
                ServiceAddTb1.Enabled = false;
            }
            else
            {
                // Изменение текста кнопки и названия формы для режима добавления
                ServiceAddButton.Text = "Добавить";
                this.Text = "Добавить услугу";

                // Разблокировка поля ServiceAddTb1
                ServiceAddTb1.Enabled = true;
            }
        }

        private void AddServiceToDatabase()
        {
            // Проверяем, что все поля заполнены и значение в NumericUpdown больше нуля
            if (string.IsNullOrEmpty(ServiceAddTb1.Text) ||
                string.IsNullOrEmpty(ServiceAddTb2.Text) ||
                ServiceAddNum.Value <= 0)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и введите корректное значение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Преобразуем значение из NumericUpdown в числовой формат (decimal)
            decimal cost = ServiceAddNum.Value;

            // Выполняем добавление в базу данных
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Проверяем уникальность ID услуги
                string checkQuery = "SELECT COUNT(*) FROM service WHERE ID_Service = @ID_Service";
                using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@ID_Service", ServiceAddTb1.Text);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Услуга с таким ID уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Выполняем добавление в базу данных
                string query = "INSERT INTO service (ID_Service, Name_Service, Cost) VALUES (@ID_Service, @Name_Service, @Cost)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Service", ServiceAddTb1.Text);
                    command.Parameters.AddWithValue("@Name_Service", ServiceAddTb2.Text);
                    command.Parameters.AddWithValue("@Cost", cost);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            // Выводим диалоговое окно с сообщением об успешном добавлении
            MessageBox.Show("Услуга успешно добавлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Закрываем форму
            this.Close();
        }

        private void UpdateServiceInDatabase(int serviceId)
        {
            // Проверяем, что все поля заполнены и значение в NumericUpdown больше нуля
            if (string.IsNullOrEmpty(ServiceAddTb1.Text) ||
                string.IsNullOrEmpty(ServiceAddTb2.Text) ||
                ServiceAddNum.Value <= 0)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и введите корректное значение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Преобразуем значение из NumericUpdown в числовой формат (decimal)
            decimal cost = ServiceAddNum.Value;

            // Выполняем обновление в базе данных
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Выполняем обновление в базе данных
                string query = "UPDATE service SET Name_Service = @Name_Service, Cost = @Cost WHERE ID_Service = @ID_Service";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name_Service", ServiceAddTb2.Text);
                    command.Parameters.AddWithValue("@Cost", cost);
                    command.Parameters.AddWithValue("@ID_Service", serviceId);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            // Выводим диалоговое окно с сообщением об успешном изменении
            MessageBox.Show("Услуга успешно изменена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Закрываем форму
            this.Close();
        }


        private void ServiceAddButton_Click(object sender, EventArgs e)
        {
            if (SelectedServiceId > 0)
            {
                // Выполнение действий для режима изменения услуги
                UpdateServiceInDatabase(SelectedServiceId);
            }
            else
            {
                // Выполнение действий для режима добавления услуги
                AddServiceToDatabase();
            }
        }

        private void ServiceAddTb1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли вводимый символ цифрой или клавишей Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                // Если символ не цифра и не клавиша Backspace, отменяем его ввод
                e.Handled = true;
            }
        }

        private void ServiceAddTb1_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, нажата ли клавиша пробела
            if (e.KeyCode == Keys.Space)
            {
                // Если нажата клавиша пробела, блокируем ее
                e.Handled = true;
            }
        }

        private void ServicesAddForm_Load(object sender, EventArgs e)
        {
            ServiceAddTb1.Text = string.Empty;
            ServiceAddTb2.Text = "";
            ServiceAddNum.Value = 0;
        }
    }
}
