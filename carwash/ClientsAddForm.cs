using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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
    public partial class ClientsAddForm : Form
    {
        public ClientsAddForm()
        {
            InitializeComponent();
            OnLoad(EventArgs.Empty);
        }

        public int SelectedClientId { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (SelectedClientId > 0)
            {
                // Изменение текста кнопки и названия формы для режима изменения
                ClientsAddButton.Text = "Изменить";
                this.Text = "Изменить клиента";

                // Установка значения в ClientAddTb1
                ClientsAddTb1.Text = SelectedClientId.ToString();

                // Блокировка поля ClientAddTb1
                ClientsAddTb1.Enabled = false;
            }
            else
            {
                // Изменение текста кнопки и названия формы для режима добавления
                ClientsAddButton.Text = "Добавить";
                this.Text = "Добавить клиента";

                // Разблокировка поля ClientAddTb1
                ClientsAddTb1.Enabled = true;
            }
        }

        private void ClientsAddForm_Load(object sender, EventArgs e)
        {
            ClientsAddTb1.Text = string.Empty;
            ClientsAddCb1.SelectedIndex = -1;
            ClientsAddTb2.Text = "";
            ClientsAddTb3.Text = "";

            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Выполняем запрос для получения всех автомобилей, не привязанных к клиентам
                string query = "SELECT ID_Car, CONCAT(Brand, ' ', Model, ' ', Car_Number) AS CarInfo FROM car WHERE ID_Car NOT IN (SELECT IDCar FROM client)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Очищаем предыдущие значения в ComboBox, если они есть
                        ClientsAddCb1.Items.Clear();

                        // Загружаем данные об автомобилях в ComboBox
                        while (reader.Read())
                        {
                            int carId = reader.GetInt32("ID_Car");
                            string carInfo = reader.GetString("CarInfo");
                            ClientsAddCb1.Items.Add(new KeyValuePair<int, string>(carId, carInfo));
                        }
                    }
                }

                // Если форма открыта в режиме изменения клиента, загружаем данные о клиенте и его привязанном автомобиле
                if (SelectedClientId > 0)
                {
                    query = "SELECT cl.IDCar, CONCAT(c.Brand, ' ', c.Model, ' ', c.Car_Number) AS CarInfo FROM client cl INNER JOIN car c ON cl.IDCar = c.ID_Car WHERE cl.ID_Client = @SelectedClientId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SelectedClientId", SelectedClientId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Если есть результаты, устанавливаем выбранный автомобиль в ComboBox
                            if (reader.Read())
                            {
                                int carId = reader.GetInt32("IDCar");
                                string carInfo = reader.GetString("CarInfo");
                                ClientsAddCb1.Items.Add(new KeyValuePair<int, string>(carId, carInfo));
                                ClientsAddCb1.SelectedItem = new KeyValuePair<int, string>(carId, carInfo);
                            }
                        }
                    }
                }

                connection.Close();
            }
        }

        private void AddClientToDatabase()
        {
            // Проверяем, что все поля заполнены
            if (string.IsNullOrEmpty(ClientsAddTb1.Text) ||
                string.IsNullOrEmpty(ClientsAddTb2.Text) ||
                string.IsNullOrEmpty(ClientsAddTb3.Text) ||
                ClientsAddCb1.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и выберите машину из списка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Получаем выбранный элемент ComboBox
            KeyValuePair<int, string> selectedCar = (KeyValuePair<int, string>)ClientsAddCb1.SelectedItem;

            int selectedCarId = selectedCar.Key;

            // Выполняем добавление данных в таблицу "client"
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Проверяем уникальность ID клиента
                string checkQuery = "SELECT COUNT(*) FROM client WHERE ID_Client = @ID_Client";
                using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@ID_Client", ClientsAddTb1.Text);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Клиент с таким ID уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Выполняем добавление выбранного ID_Car в таблицу "client"
                string query = "INSERT INTO client (ID_Client, IDCar, Name_Client, PhoneNumber_Client) VALUES (@ID_Client, @IDCar, @Name_Client, @PhoneNumber_Client)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Client", ClientsAddTb1.Text);
                    command.Parameters.AddWithValue("@IDCar", selectedCarId);
                    command.Parameters.AddWithValue("@Name_Client", ClientsAddTb2.Text);
                    command.Parameters.AddWithValue("@PhoneNumber_Client", ClientsAddTb3.Text);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            // Выводим диалоговое окно с сообщением об успешном добавлении
            MessageBox.Show("Клиент успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Закрываем форму
            this.Close();
        }

        private void UpdateClientInDatabase(int clientId)
        {
            // Проверяем, что все поля заполнены
            if (string.IsNullOrEmpty(ClientsAddTb1.Text) ||
                string.IsNullOrEmpty(ClientsAddTb2.Text) ||
                string.IsNullOrEmpty(ClientsAddTb3.Text) ||
                ClientsAddCb1.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и выберите машину из списка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Получаем значения из полей на форме
            int carId = ((KeyValuePair<int, string>)ClientsAddCb1.SelectedItem).Key;
            string clientName = ClientsAddTb2.Text;
            string phoneNumber = ClientsAddTb3.Text;

            // Выполняем обновление в базе данных
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Выполняем обновление в базе данных
                string query = "UPDATE client SET IDCar = @IDCar, Name_Client = @Name_Client, PhoneNumber_Client = @PhoneNumber_Client WHERE ID_Client = @ID_Client";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDCar", carId);
                    command.Parameters.AddWithValue("@Name_Client", clientName);
                    command.Parameters.AddWithValue("@PhoneNumber_Client", phoneNumber);
                    command.Parameters.AddWithValue("@ID_Client", clientId);

                    command.ExecuteNonQuery();
                }
            }

            // Выводим диалоговое окно с сообщением об успешном изменении
            MessageBox.Show("Клиент успешно изменен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Закрываем форму
            this.Close();
        }

        private void ClientsAddButton_Click(object sender, EventArgs e)
        {
            if (SelectedClientId > 0)
            {
                // Выполнение действий для режима изменения клиента
                UpdateClientInDatabase(SelectedClientId);
            }
            else
            {
                // Выполнение действий для режима добавления клиента
                AddClientToDatabase();
            }
        }

        private void ClientsAddTb1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли вводимый символ цифрой или клавишей Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                // Если символ не цифра и не клавиша Backspace, отменяем его ввод
                e.Handled = true;
            }
        }

        private void ClientsAddTb1_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, нажата ли клавиша пробела
            if (e.KeyCode == Keys.Space)
            {
                // Если нажата клавиша пробела, блокируем ее
                e.Handled = true;
            }
        }
    }
}
