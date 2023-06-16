using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static carwash.LoginForm;
using Word = Microsoft.Office.Interop.Word;

namespace carwash
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            MenuTab.SelectedIndexChanged += MenuTab_SelectedIndexChanged;
        }

        private void LoadCarsData()
        {
            // Загружаем данные из таблицы "car" в DataGridView
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM car";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Привязываем DataTable к DataGridView
                        CarsGrid.DataSource = dataTable;
                    }
                }

                connection.Close();
            }
            CarsGrid.ClearSelection();
        }

        private void LoadServiceData()
        {
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM service";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Привязываем DataTable к DataGridView
                        ServicesGrid.DataSource = dataTable;
                    }
                }

                connection.Close();
            }
            ServicesGrid.ClearSelection();
        }

        private void LoadTeamData()
        {
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT workgroup.ID_Workgroup, workgroup.Number_Workgroup, workgroup.Date_Of_Creation, GROUP_CONCAT(workers.Name_Worker SEPARATOR ', ') AS TeamMembers FROM workgroup LEFT JOIN workers ON workgroup.ID_Workgroup = workers.IDWorkgroup GROUP BY workgroup.ID_Workgroup";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Привязываем DataTable к DataGridView
                        TeamGrid.DataSource = dataTable;
                    }
                }

                connection.Close();
            }

            TeamGrid.ClearSelection();
        }

        private void LoadClientsData()
        {
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID_Client, Name_Client, PhoneNumber_Client, CONCAT(Brand, ' ', Model, ' ', Car_Number) AS CarInfo " + "FROM client LEFT JOIN car ON client.IDCar = car.ID_Car";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Привязываем DataTable к DataGridView
                        ClientsGrid.DataSource = dataTable;
                    }
                }

                connection.Close();
            }
            ClientsGrid.ClearSelection();
        }

        private void LoadWorkersData()
        {
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT workers.ID_Worker, workgroup.Number_Workgroup, workers.Name_Worker, workers.Number_Contract, workers.Date_Of_Employment " + "FROM workers LEFT JOIN workgroup ON workers.IDWorkgroup = workgroup.ID_Workgroup";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Привязываем DataTable к DataGridView
                        WorkersGrid.DataSource = dataTable;
                    }
                }

                connection.Close();
            }

            WorkersGrid.ClearSelection();
        }

        private void LoadOrderData()
        {
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT o.ID_Order,
                                IFNULL(w.Number_Workgroup, '') AS WorkgroupID,
                                IFNULL(c.Name_Client, '') AS ClientID,
                                o.Order_Date,
                                o.Order_Amount,
                                o.Order_Status,
                                o.Services
                         FROM orders o
                         LEFT JOIN workgroup w ON o.WorkgroupID = w.ID_Workgroup
                         LEFT JOIN client c ON o.ClientID = c.ID_Client";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Привязываем DataTable к DataGridView
                        OrderGrid.DataSource = dataTable;
                    }
                }

                connection.Close();
            }

            OrderGrid.ClearSelection();
        }

        private void OrderAddButton_Click(object sender, EventArgs e)
        {
            OrderAddForm orderaddForm = new OrderAddForm();
            orderaddForm.FormClosed += OrderAddForm_FormClosed;
            orderaddForm.Show();
        }

        private void OrderAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadOrderData();
        }

        private void ServicesAddButton_Click(object sender, EventArgs e)
        {
            ServicesAddForm servicesaddForm = new ServicesAddForm();
            servicesaddForm.FormClosed += ServicesaddForm_FormClosed;
            servicesaddForm.Show();
        }

        private void ServicesaddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadServiceData();
        }

        private void ClientsAddButton_Click(object sender, EventArgs e)
        {
            ClientsAddForm clientsaddForm = new ClientsAddForm();
            clientsaddForm.FormClosed += ClientsAddForm_FormClosed;
            clientsaddForm.Show();
        }

        private void ClientsAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadClientsData();
        }

        private void CarsAddButton_Click(object sender, EventArgs e)
        {
            CarsAddForm carsaddForm = new CarsAddForm();
            carsaddForm.FormClosed += CarsAddForm_FormClosed;
            carsaddForm.Show();
        }

        private void CarsAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadCarsData();
        }

        private void WorkersAddButton_Click(object sender, EventArgs e)
        {
            WorkersAddForm workersaddForm = new WorkersAddForm();
            workersaddForm.FormClosed += WorkersAddForm_FormClosed;
            workersaddForm.Show();
        }

        private void WorkersAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadWorkersData();
        }

        private void TeamAddButton_Click(object sender, EventArgs e)
        {
            TeamAddForm teamaddForm = new TeamAddForm();
            teamaddForm.FormClosed += TeamAddForm_FormClosed;
            teamaddForm.Show();
        }

        private void TeamAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadTeamData();
        }

        private void MenuTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получаем текущий выбранный TabPage
            TabPage selectedTabPage = MenuTab.SelectedTab;

            // Проверяем, что это нужный TabPage, на котором хотим загрузить данные
            if (selectedTabPage == carspage)
            {
                LoadCarsData();
            }
            else if (selectedTabPage == servicespage)
            {
                LoadServiceData();
            }
            else if (selectedTabPage == teampage)
            {
                LoadTeamData();
            }
            else if (selectedTabPage == clientspage)
            {
                LoadClientsData();
            }
            else if (selectedTabPage == workerspage)
            {
                LoadWorkersData();
            }
            else if (selectedTabPage == orderpage)
            {
                LoadOrderData();
            }
            else if (selectedTabPage == mainpage)
            {
                MainStartDate.Value = DateTime.Now;
                MainEndDate.Value = DateTime.Now;   
            }
        }

        private void CarsGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CarsGrid.Columns["ID_Car"].HeaderText = "Код автомобиля";
            CarsGrid.Columns["Brand"].HeaderText = "Марка";
            CarsGrid.Columns["Model"].HeaderText = "Модель";
            CarsGrid.Columns["Car_Number"].HeaderText = "Гос. номер";
        }

        private void CarsDeleteButton_Click(object sender, EventArgs e)
        {
            // Проверяем, что есть выбранная строка
            if (CarsGrid.SelectedRows.Count > 0)
            {
                // Отображаем диалоговое окно подтверждения удаления
                DialogResult result = MessageBox.Show("Вы действительно хотите удалить выбранную строку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Получаем ID выбранной строки
                    int idCar = (int)CarsGrid.SelectedRows[0].Cells["ID_Car"].Value;

                    // Проверяем, привязан ли автомобиль к таблице client
                    bool isCarLinked = false;
                    string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT COUNT(*) FROM client WHERE IDCar = @IDCar";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IDCar", idCar);
                            int count = Convert.ToInt32(command.ExecuteScalar());
                            isCarLinked = count > 0;
                        }

                        connection.Close();
                    }

                    // Если автомобиль привязан к таблице client, обновляем IDCar в таблице client на NULL
                    if (isCarLinked)
                    {
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();

                            string updateQuery = "UPDATE client SET IDCar = NULL WHERE IDCar = @ID_Car";
                            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@ID_Car", idCar);
                                updateCommand.ExecuteNonQuery();
                            }

                            connection.Close();
                        }
                    }

                    // Удаляем строку из базы данных
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string deleteQuery = "DELETE FROM car WHERE ID_Car = @ID_Car";
                        using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@ID_Car", idCar);
                            deleteCommand.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    // Обновляем DataGridView
                    LoadCarsData();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ServicesGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ServicesGrid.Columns["ID_Service"].HeaderText = "Код услуги";
            ServicesGrid.Columns["Name_Service"].HeaderText = "Наименование";
            ServicesGrid.Columns["Cost"].HeaderText = "Цена";
        }

        private void ServicesDeleteButton_Click(object sender, EventArgs e)
        {
            // Проверяем, что есть выбранная строка
            if (ServicesGrid.SelectedRows.Count > 0)
            {
                // Отображаем диалоговое окно подтверждения удаления
                DialogResult result = MessageBox.Show("Вы действительно хотите удалить выбранную строку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Получаем ID выбранной строки
                    int idService = (int)ServicesGrid.SelectedRows[0].Cells["ID_Service"].Value;

                    // Удаляем строку из базы данных
                    string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM service WHERE ID_Service = @ID_Service";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID_Service", idService);
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    // Обновляем DataGridView
                    LoadServiceData();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TeamGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            TeamGrid.Columns["ID_Workgroup"].HeaderText = "Код бригады";
            TeamGrid.Columns["Number_Workgroup"].HeaderText = "Номер бригады";
            TeamGrid.Columns["Date_Of_Creation"].HeaderText = "Дата формирования";
            TeamGrid.Columns["TeamMembers"].HeaderText = "Состав бригады";
        }

        private void TeamDeleteButton_Click(object sender, EventArgs e)
        {
            // Проверяем, что есть выбранная строка
            if (TeamGrid.SelectedRows.Count > 0)
            {
                // Отображаем диалоговое окно подтверждения удаления
                DialogResult result = MessageBox.Show("Вы действительно хотите удалить выбранную строку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Получаем ID выбранной строки
                    int idTeam = (int)TeamGrid.SelectedRows[0].Cells["ID_Workgroup"].Value;

                    // Обновляем связанные записи в других таблицах
                    string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // Проверяем, привязана ли бригада к таблице workers
                        string checkWorkersQuery = "SELECT COUNT(*) FROM workers WHERE IDWorkgroup = @ID_Workgroup";
                        using (MySqlCommand checkWorkersCommand = new MySqlCommand(checkWorkersQuery, connection))
                        {
                            checkWorkersCommand.Parameters.AddWithValue("@ID_Workgroup", idTeam);
                            int workersCount = Convert.ToInt32(checkWorkersCommand.ExecuteScalar());

                            // Если бригада привязана к workers, обновляем IDWorkgroup на пустое значение
                            if (workersCount > 0)
                            {
                                string updateWorkersQuery = "UPDATE workers SET IDWorkgroup = NULL WHERE IDWorkgroup = @ID_Workgroup";
                                using (MySqlCommand updateWorkersCommand = new MySqlCommand(updateWorkersQuery, connection))
                                {
                                    updateWorkersCommand.Parameters.AddWithValue("@ID_Workgroup", idTeam);
                                    updateWorkersCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        // Проверяем, привязана ли бригада к таблице orders
                        string checkOrdersQuery = "SELECT COUNT(*) FROM orders WHERE WorkgroupID = @ID_Workgroup";
                        using (MySqlCommand checkOrdersCommand = new MySqlCommand(checkOrdersQuery, connection))
                        {
                            checkOrdersCommand.Parameters.AddWithValue("@ID_Workgroup", idTeam);
                            int ordersCount = Convert.ToInt32(checkOrdersCommand.ExecuteScalar());

                            // Если бригада привязана к orders, обновляем WorkgroupID на пустое значение
                            if (ordersCount > 0)
                            {
                                string updateOrdersQuery = "UPDATE orders SET WorkgroupID = NULL WHERE WorkgroupID = @ID_Workgroup";
                                using (MySqlCommand updateOrdersCommand = new MySqlCommand(updateOrdersQuery, connection))
                                {
                                    updateOrdersCommand.Parameters.AddWithValue("@ID_Workgroup", idTeam);
                                    updateOrdersCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        // Удаляем строку из таблицы workgroup
                        string deleteTeamQuery = "DELETE FROM workgroup WHERE ID_Workgroup = @ID_Workgroup";
                        using (MySqlCommand deleteTeamCommand = new MySqlCommand(deleteTeamQuery, connection))
                        {
                            deleteTeamCommand.Parameters.AddWithValue("@ID_Workgroup", idTeam);
                            deleteTeamCommand.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    // Обновляем DataGridView
                    LoadTeamData();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClientsGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ClientsGrid.Columns["ID_Client"].HeaderText = "Код клиента";
            ClientsGrid.Columns["Name_Client"].HeaderText = "Имя клиента";
            ClientsGrid.Columns["PhoneNumber_Client"].HeaderText = "Номер телефона";
            ClientsGrid.Columns["CarInfo"].HeaderText = "Автомобиль";
        }

        private void ClientsDeleteButton_Click(object sender, EventArgs e)
        {
            // Проверяем, что есть выбранная строка
            if (ClientsGrid.SelectedRows.Count > 0)
            {
                // Отображаем диалоговое окно подтверждения удаления
                DialogResult result = MessageBox.Show("Вы действительно хотите удалить выбранную строку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Получаем ID выбранной строки
                    int idClient = (int)ClientsGrid.SelectedRows[0].Cells["ID_Client"].Value;

                    // Проверяем, привязан ли клиент к заказам
                    bool isClientLinkedToOrders = false;
                    string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT COUNT(*) FROM orders WHERE ClientID = @ID_Client";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID_Client", idClient);
                            int count = Convert.ToInt32(command.ExecuteScalar());
                            if (count > 0)
                            {
                                isClientLinkedToOrders = true;
                            }
                        }

                        connection.Close();
                    }

                    // Если клиент привязан к заказам, обновляем соответствующие записи в таблице orders
                    if (isClientLinkedToOrders)
                    {
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();

                            string query = "UPDATE orders SET ClientID = NULL WHERE ClientID = @ID_Client";
                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@ID_Client", idClient);
                                command.ExecuteNonQuery();
                            }

                            connection.Close();
                        }
                    }

                    // Удаляем строку из базы данных
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM client WHERE ID_Client = @ID_Client";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID_Client", idClient);
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    // Обновляем DataGridView
                    LoadClientsData();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void WorkersGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            WorkersGrid.Columns["ID_Worker"].HeaderText = "ID работника";
            WorkersGrid.Columns["Number_Workgroup"].HeaderText = "Бригада";
            WorkersGrid.Columns["Name_Worker"].HeaderText = "Имя работника";
            WorkersGrid.Columns["Number_Contract"].HeaderText = "Номер договора";
            WorkersGrid.Columns["Date_Of_Employment"].HeaderText = "Дата трудоустройства";
        }

        private void WorkersGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (WorkersGrid.SelectedRows.Count > 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = WorkersGrid.SelectedRows[0];

                // Получаем значение ID_Worker из выбранной строки
                int selectedWorkerId = Convert.ToInt32(selectedRow.Cells["ID_Worker"].Value);

                // Загрузка фото работника из базы данных
                string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Photo FROM workers WHERE ID_Worker = @ID_Worker";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Worker", selectedWorkerId);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            // Преобразовываем полученные данные в массив байтов
                            byte[] photoBytes = (byte[])result;

                            // Отображаем фото в PictureBox
                            using (MemoryStream ms = new MemoryStream(photoBytes))
                            {
                                WorkersPictureBox.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            // Очищаем PictureBox
                            WorkersPictureBox.Image = null;
                        }
                    }

                    connection.Close();
                }
            }
            else
            {
                // Очищаем PictureBox, если ни одна строка не выбрана
                WorkersPictureBox.Image = null;
            }
        }

        private void WorkersDeleteButton_Click(object sender, EventArgs e)
        {
            // Проверяем, что есть выбранная строка
            if (WorkersGrid.SelectedRows.Count > 0)
            {
                // Отображаем диалоговое окно подтверждения удаления
                DialogResult result = MessageBox.Show("Вы действительно хотите удалить выбранную строку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Получаем ID выбранной строки
                    int idWorker = (int)WorkersGrid.SelectedRows[0].Cells["ID_Worker"].Value;

                    // Удаляем строку из базы данных
                    string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM workers WHERE ID_Worker = @ID_Worker";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID_Worker", idWorker);
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    // Обновляем DataGridView
                    LoadWorkersData();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OrderGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            OrderGrid.Columns["ID_Order"].HeaderText = "ID заказа";
            OrderGrid.Columns["WorkgroupID"].HeaderText = "Бригада";
            OrderGrid.Columns["ClientID"].HeaderText = "Клиент";
            OrderGrid.Columns["Order_Date"].HeaderText = "Дата";
            OrderGrid.Columns["Order_Amount"].HeaderText = "Сумма заказа";
            OrderGrid.Columns["Order_Status"].HeaderText = "Статус";
            OrderGrid.Columns["Services"].HeaderText = "Наименования услуг";
        }

        private void OrderDeleteButton_Click(object sender, EventArgs e)
        {
            // Проверяем, что есть выбранная строка
            if (OrderGrid.SelectedRows.Count > 0)
            {
                // Отображаем диалоговое окно подтверждения удаления
                DialogResult result = MessageBox.Show("Вы действительно хотите удалить выбранную строку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Получаем ID выбранной строки
                    int idOrder = (int)OrderGrid.SelectedRows[0].Cells["ID_Order"].Value;

                    // Удаляем строку из базы данных
                    string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM orders WHERE ID_Order = @ID_Order";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID_Order", idOrder);
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    // Обновляем DataGridView
                    LoadOrderData();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OrderChangeButton_Click(object sender, EventArgs e)
        {
            if (OrderGrid.SelectedRows.Count > 0)
            {
                int selectedOrderId = Convert.ToInt32(OrderGrid.SelectedRows[0].Cells["ID_Order"].Value);

                OrderAddForm orderAddForm = new OrderAddForm();
                orderAddForm.SelectedOrderId = selectedOrderId;
                orderAddForm.Show();
                orderAddForm.FormClosed += OrderAddForm_FormClosed;
            }
            else
            {
                MessageBox.Show("Выберите заказ для изменения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OrderChangeButton2_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка в OrderGrid
            if (OrderGrid.SelectedRows.Count > 0)
            {
                // Получаем выбранный orderId из выделенной строки
                int selectedOrderId = Convert.ToInt32(OrderGrid.SelectedRows[0].Cells["ID_Order"].Value);

                // Создаем экземпляр формы ChangeStatusForm и передаем selectedOrderId в конструктор
                ChangeStatusForm changeStatusForm = new ChangeStatusForm(selectedOrderId);

                // Открываем форму ChangeStatusForm в диалоговом режиме
                DialogResult result = changeStatusForm.ShowDialog(this);

                // Проверяем результат диалога
                if (result == DialogResult.OK)
                {
                    // Получаем выбранный статус из формы ChangeStatusForm
                    string selectedStatus = changeStatusForm.SelectedStatus;

                    // Обновляем статус заказа в базе данных по выбранному orderId
                    UpdateOrderStatus(selectedOrderId, selectedStatus);
                }

                // Закрываем форму ChangeStatusForm
                changeStatusForm.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заказ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateOrderStatus(int orderId, string status)
        {
            // Обновляем статус заказа в базе данных
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE orders SET Order_Status = @Status WHERE ID_Order = @OrderId";
                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Status", status);
                    updateCommand.Parameters.AddWithValue("@OrderId", orderId);

                    updateCommand.ExecuteNonQuery();
                }
            }

            LoadOrderData();
            OrderGrid.ClearSelection();
            MessageBox.Show("Статус заказа успешно обновлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void WorkersPictureBox_Click(object sender, EventArgs e)
        {
            if (WorkersPictureBox.Image != null)
            {
                // Открытие формы с увеличенным изображением
                var enlargedForm = new EnlargedForm(WorkersPictureBox.Image);
                enlargedForm.ShowDialog();
            }
        }

        private void ServicesChangeButton_Click(object sender, EventArgs e)
        {
            if (ServicesGrid.SelectedRows.Count > 0)
            {
                int selectedServiceId = Convert.ToInt32(ServicesGrid.SelectedRows[0].Cells["ID_Service"].Value);

                ServicesAddForm servicesAddForm = new ServicesAddForm();
                servicesAddForm.SelectedServiceId = selectedServiceId;
                servicesAddForm.Show();
                servicesAddForm.FormClosed += ServicesaddForm_FormClosed;
            }
            else
            {
                MessageBox.Show("Выберите услугу для изменения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClientsChangeButton_Click(object sender, EventArgs e)
        {
            if (ClientsGrid.SelectedRows.Count > 0)
            {
                int selectedClientId = Convert.ToInt32(ClientsGrid.SelectedRows[0].Cells["ID_Client"].Value);

                ClientsAddForm clientAddForm = new ClientsAddForm();
                clientAddForm.SelectedClientId = selectedClientId;
                clientAddForm.Show();
                clientAddForm.FormClosed += ClientsAddForm_FormClosed;
            }
            else
            {
                MessageBox.Show("Выберите клиента для изменения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CarsChangeButton_Click(object sender, EventArgs e)
        {
            if (CarsGrid.SelectedRows.Count > 0)
            {
                int selectedCarId = Convert.ToInt32(CarsGrid.SelectedRows[0].Cells["ID_Car"].Value);

                CarsAddForm carAddForm = new CarsAddForm();
                carAddForm.SelectedCarId = selectedCarId;
                carAddForm.Show();
                carAddForm.FormClosed += CarsAddForm_FormClosed;

            }
            else
            {
                MessageBox.Show("Выберите автомобиль для изменения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void WorkersChangeButton_Click(object sender, EventArgs e)
        {
            if (WorkersGrid.SelectedRows.Count > 0)
            {
                int selectedWorkerId = Convert.ToInt32(WorkersGrid.SelectedRows[0].Cells["ID_Worker"].Value);

                WorkersAddForm workersAddForm = new WorkersAddForm();
                workersAddForm.SelectedWorkerId = selectedWorkerId;
                workersAddForm.Show();
                workersAddForm.FormClosed += WorkersAddForm_FormClosed;

            }
            else
            {
                MessageBox.Show("Выберите работника для изменения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TeamChangeButton_Click(object sender, EventArgs e)
        {
            if (TeamGrid.SelectedRows.Count > 0)
            {
                int selectedTeamId = Convert.ToInt32(TeamGrid.SelectedRows[0].Cells["ID_Workgroup"].Value);

                TeamAddForm teamAddForm = new TeamAddForm();
                teamAddForm.SelectedTeamId = selectedTeamId;
                teamAddForm.Show();
                teamAddForm.FormClosed += TeamAddForm_FormClosed;

            }
            else
            {
                MessageBox.Show("Выберите бригаду для изменения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OrderCb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OrderCb1.SelectedItem != null) // Добавляем проверку на null
            {
                string selectedFilter = OrderCb1.SelectedItem.ToString();

                switch (selectedFilter)
                {
                    case "По дате":
                        OrderGrid.Sort(OrderGrid.Columns["Order_Date"], ListSortDirection.Ascending);
                        break;
                    case "По сумме":
                        OrderGrid.Sort(OrderGrid.Columns["Order_Amount"], ListSortDirection.Descending);
                        break;
                    default:
                        // Очистить сортировку
                        OrderGrid.Sort(OrderGrid.Columns["ID_Order"], ListSortDirection.Ascending);
                        break;
                }
            }
            OrderGrid.ClearSelection();
        }

        private void MainButton1_Click(object sender, EventArgs e)
        {
            DateTime startDate = MainStartDate.Value;
            DateTime endDate = MainEndDate.Value;

            // Проверка, чтобы дата начала не была позже даты окончания
            if (startDate > endDate)
            {
                MessageBox.Show("Пожалуйста, выберите корректные даты.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            generatereport();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MainStartDate.Value = DateTime.Now;
            MainEndDate.Value = DateTime.Now;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Size = new Size(943, 430);
        }

        private void OrderButton_Click(object sender, EventArgs e)
        {
            MenuTab.SelectedIndex = 1;
            this.Text = "Заказы";
            OrderCb1.SelectedIndex = -1;
            // Сброс выбранных путей и текста на кнопках
            templatePath = null;
            saveFolderPath = null;
            SelectTemplateButton.Text = "Выберите путь к шаблону";
            SelectSavePathButton.Text = "Выберите путь сохранения";
        }

        private void MainButton_Click(object sender, EventArgs e)
        {
            MenuTab.SelectedIndex = 0;
            this.Text = "Главная";
        }

        private void ServiceButton_Click(object sender, EventArgs e)
        {
            MenuTab.SelectedIndex = 2;
            this.Text = "Услуги";
        }

        private void ClientsButton_Click(object sender, EventArgs e)
        {
            MenuTab.SelectedIndex = 3;
            this.Text = "Клиенты";
        }

        private void CarButton_Click(object sender, EventArgs e)
        {
            MenuTab.SelectedIndex = 4;
            this.Text = "Автомобили";
        }

        private void WorkerButton_Click(object sender, EventArgs e)
        {
            MenuTab.SelectedIndex = 5;
            this.Text = "Работники";
        }

        private void TeamButton_Click(object sender, EventArgs e)
        {
            MenuTab.SelectedIndex = 6;
            this.Text = "Бригады";
        }


        private string templatePath; // переменная для хранения пути к шаблону
        private string saveFolderPath; // переменная для хранения пути сохранения отчета
        private void SelectTemplateButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Документ Word (*.docx)|*.docx";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Установка начальной директории
            openFileDialog.Title = "Выберите шаблон отчета";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                templatePath = openFileDialog.FileName;
                SelectTemplateButton.Text = "Путь к шаблону выбран";
            }
        }

        private void generatereport()
        {
            if (string.IsNullOrEmpty(templatePath) || string.IsNullOrEmpty(saveFolderPath))
            {
                MessageBox.Show("Выберите путь к шаблону и путь для сохранения отчета.", "Ошибка");
                return;
            }

            // Получаем значения дат начала и конца из элементов управления на форме
            DateTime startDate = MainStartDate.Value;
            DateTime endDate = MainEndDate.Value;

            // Устанавливаем строку подключения к базе данных
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            // Создаем объект Word.Application
            Word.Application wordApp = new Word.Application();

            try
            {
                Word.Document reportDoc = wordApp.Documents.Open(templatePath, ReadOnly: true, ConfirmConversions: false);

                // Устанавливаем значения дат в шаблоне
                reportDoc.Content.Find.Execute(FindText: "date1", ReplaceWith: startDate.ToShortDateString());
                reportDoc.Content.Find.Execute(FindText: "date2", ReplaceWith: endDate.ToShortDateString());

                // Создаем подключение к базе данных
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Выполняем SQL-запросы для получения данных
                    MySqlCommand command;
                    MySqlDataReader reader;

                    // Запрос 1: Количество заказов со статусом "Выполнен"
                    command = new MySqlCommand("SELECT COUNT(*) AS Count FROM orders WHERE Order_Status = 'Выполнен' AND Order_Date BETWEEN @StartDate AND @EndDate", connection);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    int orderCount = Convert.ToInt32(command.ExecuteScalar());
                    reportDoc.Content.Find.Execute(FindText: "count", ReplaceWith: orderCount.ToString());

                    // Запрос 2: Сумма всех заказов со статусом "Выполнен"
                    command = new MySqlCommand("SELECT SUM(Order_Amount) AS Sum FROM orders WHERE Order_Status = 'Выполнен' AND Order_Date BETWEEN @StartDate AND @EndDate", connection);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    decimal orderSum = Convert.ToDecimal(command.ExecuteScalar());
                    reportDoc.Content.Find.Execute(FindText: "sum", ReplaceWith: orderSum.ToString());

                    // Запрос 3: Номер бригады, выполнившей больше всего заказов
                    command = new MySqlCommand("SELECT Number_Workgroup AS Brigade FROM workgroup WHERE ID_Workgroup = (SELECT WorkgroupID FROM (SELECT WorkgroupID, COUNT(*) AS Count FROM orders WHERE Order_Date BETWEEN @StartDate AND @EndDate GROUP BY WorkgroupID ORDER BY COUNT(*) DESC LIMIT 1) AS T)", connection);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    int brigadeNumber = Convert.ToInt32(command.ExecuteScalar());
                    reportDoc.Content.Find.Execute(FindText: "brigade", ReplaceWith: brigadeNumber.ToString());

                    // Запрос 4: Количество заказов, выполненных указанной бригадой
                    command = new MySqlCommand("SELECT COUNT(*) AS Count FROM orders WHERE WorkgroupID = (SELECT WorkgroupID FROM (SELECT WorkgroupID, COUNT(*) AS Count FROM orders WHERE Order_Date BETWEEN @StartDate AND @EndDate GROUP BY WorkgroupID ORDER BY COUNT(*) DESC LIMIT 1) AS T) AND Order_Date BETWEEN @StartDate AND @EndDate", connection);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    int brigadeOrderCount = Convert.ToInt32(command.ExecuteScalar());
                    reportDoc.Content.Find.Execute(FindText: "countbrigada", ReplaceWith: brigadeOrderCount.ToString());

                    // Запрос 5: ID самого большого выполненного заказа
                    command = new MySqlCommand("SELECT ID_Order AS OrderId FROM orders WHERE Order_Status = 'Выполнен' AND Order_Date BETWEEN @StartDate AND @EndDate ORDER BY Order_Amount DESC LIMIT 1", connection);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    int largestOrderId = Convert.ToInt32(command.ExecuteScalar());
                    reportDoc.Content.Find.Execute(FindText: "orderid", ReplaceWith: largestOrderId.ToString());

                    // Запрос 6: Сумма самого большого выполненного заказа
                    command = new MySqlCommand("SELECT Order_Amount AS Sum FROM orders WHERE Order_Status = 'Выполнен' AND Order_Date BETWEEN @StartDate AND @EndDate ORDER BY Order_Amount DESC LIMIT 1", connection);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    decimal largestOrderSum = Convert.ToDecimal(command.ExecuteScalar());
                    reportDoc.Content.Find.Execute(FindText: "sumorder", ReplaceWith: largestOrderSum.ToString());

                    // Запрос 7: Услуги из самого большого выполненного заказа
                    command = new MySqlCommand("SELECT Services FROM orders WHERE ID_Order = @OrderId", connection);
                    command.Parameters.AddWithValue("@OrderId", largestOrderId);
                    string orderServices = Convert.ToString(command.ExecuteScalar());
                    reportDoc.Content.Find.Execute(FindText: "uslugi", ReplaceWith: orderServices);
                }
                // Генерируем имя файла на основе текущей даты и времени
                string fileName = "Отчет_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

                // Преобразуем путь сохранения в кодировку Windows-1251
                byte[] pathBytes = Encoding.GetEncoding("Windows-1251").GetBytes(saveFolderPath);
                saveFolderPath = Encoding.GetEncoding("Windows-1251").GetString(pathBytes);

                // Сохраняем отчет в выбранной папке с сгенерированным именем файла
                string saveFilePath = Path.Combine(saveFolderPath, fileName);
                reportDoc.SaveAs2(saveFilePath);

                // Отображаем отчет
                wordApp.Visible = true;
                // Открываем сохраненный отчет для пользователя
                Process.Start(saveFilePath);
            }
            catch (Exception ex)
            {
                // В случае возникновения ошибки выводим сообщение
                MessageBox.Show("Ошибка при создании отчета: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Закрываем приложение Word
                wordApp.Quit();
            }
        }

        private void SelectSavePathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Выберите папку для сохранения отчета";
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                saveFolderPath = folderBrowserDialog.SelectedPath;
                SelectSavePathButton.Text = "Путь к сохранению выбран";
            }
        }
    }
}