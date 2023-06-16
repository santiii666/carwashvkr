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
    public partial class OrderAddForm : Form
    {
        public OrderAddForm()
        {
            InitializeComponent();
            OnLoad(EventArgs.Empty);
        }

        public int SelectedOrderId { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (SelectedOrderId > 0)
            {
                // Изменение текста кнопки и названия формы для режима изменения
                OrderAddButton.Text = "Изменить";
                this.Text = "Изменить заказ";

                // Установка значения в OrderAddCb1
                OrderAddTb1.Text = SelectedOrderId.ToString();

                // Блокировка поля OrderAddTb1
                OrderAddTb1.Enabled = false;
            }
            else
            {
                // Изменение текста кнопки и названия формы для режима добавления
                OrderAddButton.Text = "Добавить";
                this.Text = "Добавить заказ";

                // Разблокировка поля OrderAddTb1
                OrderAddTb1.Enabled = true;
            }
        }

        private void OrderAddTb1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли вводимый символ цифрой или клавишей Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                // Если символ не цифра и не клавиша Backspace, отменяем его ввод
                e.Handled = true;
            }
        }

        private void OrderAddTb1_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, нажата ли клавиша пробела
            if (e.KeyCode == Keys.Space)
            {
                // Если нажата клавиша пробела, блокируем ее
                e.Handled = true;
            }
        }

        private void LoadWorkgroups()
        {
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID_Workgroup, CONCAT('Бригада ', Number_Workgroup) AS TeamName FROM workgroup";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Очистить комбобокс перед загрузкой новых данных
                        OrderAddCb1.Items.Clear();

                        while (reader.Read())
                        {
                            int id = reader.GetInt32("ID_Workgroup");
                            string teamName = reader.GetString("TeamName");
                            OrderAddCb1.Items.Add(new KeyValuePair<int, string>(id, teamName));
                        }
                    }
                }

                connection.Close();
            }
        }

        private void LoadClients()
        {
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID_Client, Name_Client FROM client";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Очистить комбобокс перед загрузкой новых данных
                        OrderAddCb2.Items.Clear();

                        while (reader.Read())
                        {
                            int id = reader.GetInt32("ID_Client");
                            string name = reader.GetString("Name_Client");
                            OrderAddCb2.Items.Add(new KeyValuePair<int, string>(id, name));
                        }
                    }
                }

                connection.Close();
            }
        }

        private void LoadServices()
        {
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID_Service, Name_Service, Cost FROM service";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Очистить ListBox перед загрузкой новых данных
                        OrderAddLb.Items.Clear();

                        while (reader.Read())
                        {
                            int serviceId = reader.GetInt32(0);
                            string serviceName = reader.GetString(1);
                            decimal serviceCost = reader.GetDecimal(2);

                            ServiceItem serviceItem = new ServiceItem(serviceId, serviceName, serviceCost);

                            OrderAddLb.Items.Add(serviceItem);
                        }
                    }
                }

                connection.Close();
            }

            // Включение горизонтального скроллбара
            OrderAddLb.HorizontalScrollbar = true;
            // Изменяем режим выбора элементов в ListBox на One
            OrderAddLb.SelectionMode = SelectionMode.One;
        }

        private void OrderAddLb_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateTotalCost();
        }

        private void CalculateTotalCost()
        {
            decimal totalCost = 0;

            foreach (ServiceItem service in OrderAddLb.CheckedItems)
            {
                totalCost += service.Cost;
            }

            OrderAddNum.Value = totalCost;
        }

        private class ServiceItem
        {
            public int ID_Service { get; }
            public string Name_Service { get; }
            public decimal Cost { get; }
            public bool IsChecked { get; set; }

            public ServiceItem(int id, string name, decimal cost)
            {
                ID_Service = id;
                Name_Service = name;
                Cost = cost;
            }

            public override string ToString()
            {
                return $"ID: {ID_Service}, {Name_Service} (Стоимость: {Cost})";
            }
        }

        private void OrderAddLb_MouseDown(object sender, MouseEventArgs e)
        {
            int index = OrderAddLb.IndexFromPoint(e.Location);

            if (index != ListBox.NoMatches)
            {
                ServiceItem selectedService = (ServiceItem)OrderAddLb.Items[index];
                selectedService.IsChecked = !selectedService.IsChecked;

                OrderAddLb.Invalidate();
                CalculateTotalCost();
            }
        }

        private void AddOrderToDatabase()
        {
            // Проверка на заполнение всех полей
            if (string.IsNullOrEmpty(OrderAddTb1.Text) || OrderAddCb1.SelectedItem == null ||
                OrderAddCb2.SelectedItem == null || OrderAddCb4.SelectedItem == null || OrderAddLb.CheckedItems.Count == 0)
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Получаем значения из полей на форме
            int orderId = int.Parse(OrderAddTb1.Text);
            DateTime orderDate = OrderAddDate.Value;
            decimal orderAmount = OrderAddNum.Value;

            // Формируем строку с названиями выбранных услуг
            string services = "";
            List<string> selectedServices = new List<string>();
            foreach (ServiceItem item in OrderAddLb.CheckedItems)
            {
                selectedServices.Add(item.Name_Service);
            }
            services = string.Join(", ", selectedServices);

            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            // Проверяем уникальность ID заказа
            string checkQuery = "SELECT COUNT(*) FROM orders WHERE ID_Order = @ID_Order";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@ID_Order", orderId);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Заказ с таким ID уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // Добавляем данные в таблицу "orders"
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO orders (ID_Order, WorkgroupID, ClientID, Order_Date, Order_Amount, Order_Status, Services) " +
                    "VALUES (@OrderId, @WorkgroupId, @ClientId, @OrderDate, @OrderAmount, @OrderStatus, @Services)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.Parameters.AddWithValue("@WorkgroupId", ((KeyValuePair<int, string>)OrderAddCb1.SelectedItem).Key);
                    command.Parameters.AddWithValue("@ClientId", ((KeyValuePair<int, string>)OrderAddCb2.SelectedItem).Key);
                    command.Parameters.AddWithValue("@OrderDate", orderDate);
                    command.Parameters.AddWithValue("@OrderAmount", orderAmount);
                    command.Parameters.AddWithValue("@OrderStatus", OrderAddCb4.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@Services", services);

                    command.ExecuteNonQuery();
                }
            }

            // Выводим диалоговое окно с сообщением об успешном добавлении
            MessageBox.Show("Заказ успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Закрываем форму
            this.Close();
        }

        private void UpdateOrderInDatabase(int orderId)
        {
            // Проверка на заполнение всех полей
            if (OrderAddCb1.SelectedItem == null ||
                OrderAddCb2.SelectedItem == null ||
                OrderAddCb4.SelectedItem == null ||
                OrderAddLb.CheckedItems.Count == 0)
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Получаем значения из полей на форме
            int workgroupId = ((KeyValuePair<int, string>)OrderAddCb1.SelectedItem).Key;
            int clientId = ((KeyValuePair<int, string>)OrderAddCb2.SelectedItem).Key;
            DateTime orderDate = OrderAddDate.Value;
            decimal orderAmount = OrderAddNum.Value;
            string orderStatus = OrderAddCb4.SelectedItem.ToString();

            // Формируем строку с названиями выбранных услуг
            string services = "";
            List<string> selectedServices = new List<string>();
            foreach (ServiceItem item in OrderAddLb.CheckedItems)
            {
                selectedServices.Add(item.Name_Service);
            }
            services = string.Join(", ", selectedServices);

            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            // Обновляем данные в таблице "orders" по указанному orderId
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE orders SET WorkgroupID = @WorkgroupId, ClientID = @ClientId, Order_Date = @OrderDate, " +
                    "Order_Amount = @OrderAmount, Order_Status = @OrderStatus, Services = @Services WHERE ID_Order = @OrderId";

                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@WorkgroupId", workgroupId);
                    updateCommand.Parameters.AddWithValue("@ClientId", clientId);
                    updateCommand.Parameters.AddWithValue("@OrderDate", orderDate);
                    updateCommand.Parameters.AddWithValue("@OrderAmount", orderAmount);
                    updateCommand.Parameters.AddWithValue("@OrderStatus", orderStatus);
                    updateCommand.Parameters.AddWithValue("@Services", services);
                    updateCommand.Parameters.AddWithValue("@OrderId", orderId);

                    updateCommand.ExecuteNonQuery();
                }
            }

            // Выводим диалоговое окно с сообщением об успешном изменении
            MessageBox.Show("Заказ успешно изменен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Закрываем форму
            this.Close();
        }

        private void OrderAddButton_Click(object sender, EventArgs e)
        {
            if (SelectedOrderId > 0)
            {
                // Выполнение действий для режима изменения заказа
                UpdateOrderInDatabase(SelectedOrderId);
            }
            else
            {
                // Выполнение действий для режима добавления заказа
                AddOrderToDatabase();
            }
        }

        private void OrderAddForm_Load(object sender, EventArgs e)
        {
            ClearForm();
            // Загрузка данных для ComboBox'ов
            LoadWorkgroups();
            LoadClients();
            LoadServices();
        }

        private void ClearForm()
        {
            OrderAddTb1.Text = string.Empty;
            OrderAddCb1.SelectedIndex = -1;
            OrderAddCb2.SelectedIndex = -1;
            OrderAddLb.ClearSelected();
            OrderAddDate.Value = DateTime.Now;
            OrderAddNum.Value = 0;
            OrderAddCb4.SelectedIndex = -1;
        }
    }
}
