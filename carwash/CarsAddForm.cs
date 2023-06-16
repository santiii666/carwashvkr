using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace carwash
{
    public partial class CarsAddForm : Form
    {
        public CarsAddForm()
        {
            InitializeComponent();
            OnLoad(EventArgs.Empty);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (SelectedCarId > 0)
            {
                // Изменение текста кнопки и названия формы для режима изменения
                CarsAddButton.Text = "Изменить";
                this.Text = "Изменить автомобиль";

                // Установка значения в OrderAddCb1
                CarsAddTb1.Text = SelectedCarId.ToString();

                // Блокировка поля OrderAddTb1
                CarsAddTb1.Enabled = false;
            }
            else
            {
                // Изменение текста кнопки и названия формы для режима добавления
                CarsAddButton.Text = "Добавить";
                this.Text = "Добавить автомобиль";

                // Разблокировка поля OrderAddTb1
                CarsAddTb1.Enabled = true;
            }
        }

        public int SelectedCarId { get; set; }

        private void AddCarToDataBase()
        {
            // Проверяем, что все текстовые поля не пустые
            if (string.IsNullOrEmpty(CarsAddTb1.Text) ||
                string.IsNullOrEmpty(CarsAddTb2.Text) ||
                string.IsNullOrEmpty(CarsAddTb3.Text) ||
                string.IsNullOrEmpty(CarsAddTb4.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Выполняем добавление автомобиля в базу данных
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Проверяем уникальность ID автомобиля
                string checkQuery = "SELECT COUNT(*) FROM car WHERE ID_Car = @ID_Car";
                using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@ID_Car", CarsAddTb1.Text);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Автомобиль с таким ID уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Выполняем добавление автомобиля в базу данных
                string query = "INSERT INTO car (ID_Car, Brand, Model, Car_Number) VALUES (@ID_Car, @Brand, @Model, @Car_Number)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Car", CarsAddTb1.Text);
                    command.Parameters.AddWithValue("@Brand", CarsAddTb2.Text);
                    command.Parameters.AddWithValue("@Model", CarsAddTb3.Text);
                    command.Parameters.AddWithValue("@Car_Number", CarsAddTb4.Text);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            // Выводим диалоговое окно с сообщением об успешном добавлении
            MessageBox.Show("Автомобиль успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Закрываем форму
            this.Close();
        }

        private void UpdateCarInDataBase(int carId)
        {
            // Проверка на заполнение всех полей
            if (string.IsNullOrEmpty(CarsAddTb1.Text) ||
                string.IsNullOrEmpty(CarsAddTb2.Text) ||
                string.IsNullOrEmpty(CarsAddTb3.Text) ||
                string.IsNullOrEmpty(CarsAddTb4.Text))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            // Обновляем данные в таблице "car" по указанному carId
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE car SET ID_Car = @ID_Car, Brand = @Brand, Model = @Model, Car_Number = @Car_Number WHERE ID_Car = @CarId";

                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@ID_Car", CarsAddTb1.Text);
                    updateCommand.Parameters.AddWithValue("@Brand", CarsAddTb2.Text);
                    updateCommand.Parameters.AddWithValue("@Model", CarsAddTb3.Text);
                    updateCommand.Parameters.AddWithValue("@Car_Number", CarsAddTb4.Text);
                    updateCommand.Parameters.AddWithValue("@CarId", carId);

                    updateCommand.ExecuteNonQuery();
                }
            }

            // Выводим диалоговое окно с сообщением об успешном изменении
            MessageBox.Show("Автомобиль успешно изменен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Закрываем форму
            this.Close();
        }

        private void CarsAddButton_Click(object sender, EventArgs e)
        {
            if (SelectedCarId > 0)
            {
                // Выполнение действий для режима изменения автомобиля
                UpdateCarInDataBase(SelectedCarId);
            }
            else
            {
                // Выполнение действий для режима добавления автомобиля
                AddCarToDataBase();
            }
        }

        private void CarsAddTb1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли вводимый символ цифрой или клавишей Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                // Если символ не цифра и не клавиша Backspace, отменяем его ввод
                e.Handled = true;
            }
        }

        private void CarsAddTb1_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, нажата ли клавиша пробела
            if (e.KeyCode == Keys.Space)
            {
                // Если нажата клавиша пробела, блокируем ее
                e.Handled = true;
            }
        }

        private void CarsAddForm_Load(object sender, EventArgs e)
        {
            CarsAddTb1.Text = string.Empty;
            CarsAddTb2.Text = "";
            CarsAddTb3.Text = "";
            CarsAddTb4.Text = "";
        }
    }
}
