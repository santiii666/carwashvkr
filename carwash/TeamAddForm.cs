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
    public partial class TeamAddForm : Form
    {
        public TeamAddForm()
        {
            InitializeComponent();
            OnLoad(EventArgs.Empty);
        }

        public int SelectedTeamId { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (SelectedTeamId > 0)
            {
                // Изменение текста кнопки и названия формы для режима изменения
                TeamAddButton.Text = "Изменить";
                this.Text = "Изменить бригаду";

                // Установка значения в OrderAddCb1
                TeamAddTb1.Text = SelectedTeamId.ToString();

                // Блокировка поля OrderAddTb1
                TeamAddTb1.Enabled = false;
            }
            else
            {
                // Изменение текста кнопки и названия формы для режима добавления
                TeamAddButton.Text = "Добавить";
                this.Text = "Добавить бригаду";

                // Разблокировка поля OrderAddTb1
                TeamAddTb1.Enabled = true;
            }
        }

        private void AddTeamToDatabase()
        {
            if (string.IsNullOrEmpty(TeamAddTb1.Text) ||
                string.IsNullOrEmpty(TeamAddTb2.Text) ||
                TeamAddDate.Value == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверяем уникальность ID бригады
            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM workgroup WHERE ID_Workgroup = @ID_Workgroup";
                using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@ID_Workgroup", TeamAddTb1.Text);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Бригада с таким ID уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Выполняем добавление в базу данных
                string query = "INSERT INTO workgroup (ID_Workgroup, Number_Workgroup, Date_Of_Creation) VALUES (@ID_Workgroup, @Number_Workgroup, @Date_Of_Creation)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Workgroup", TeamAddTb1.Text);
                    command.Parameters.AddWithValue("@Number_Workgroup", TeamAddTb2.Text);
                    command.Parameters.AddWithValue("@Date_Of_Creation", TeamAddDate.Value);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            // Выводим диалоговое окно с сообщением об успешном добавлении
            MessageBox.Show("Бригада успешно добавлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Закрываем форму
            this.Close();
        }

        private void UpdateTeamInDatabase(string teamId)
        {
            if (string.IsNullOrEmpty(TeamAddTb1.Text) ||
                string.IsNullOrEmpty(TeamAddTb2.Text) ||
                TeamAddDate.Value == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Выполняем обновление данных в таблице "workgroup"
                string query = "UPDATE workgroup SET ID_Workgroup = @ID_Workgroup, Number_Workgroup = @Number_Workgroup, Date_Of_Creation = @Date_Of_Creation WHERE ID_Workgroup = @TeamId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Workgroup", TeamAddTb1.Text);
                    command.Parameters.AddWithValue("@Number_Workgroup", TeamAddTb2.Text);
                    command.Parameters.AddWithValue("@Date_Of_Creation", TeamAddDate.Value);
                    command.Parameters.AddWithValue("@TeamId", teamId);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            // Выводим диалоговое окно с сообщением об успешном обновлении
            MessageBox.Show("Бригада успешно изменена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Закрываем форму
            this.Close();
        }

        private void TeamAddButton_Click(object sender, EventArgs e)
        {
            // Проверяем, открыта ли форма для добавления новой бригады или для изменения существующей
            if (SelectedTeamId > 0)
            {
                UpdateTeamInDatabase(SelectedTeamId.ToString());
            }
            else
            {
                AddTeamToDatabase();
            }
        }

        private void TeamAddTb1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли вводимый символ цифрой или клавишей Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                // Если символ не цифра и не клавиша Backspace, отменяем его ввод
                e.Handled = true;
            }
        }

        private void TeamAddTb1_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, нажата ли клавиша пробела
            if (e.KeyCode == Keys.Space)
            {
                // Если нажата клавиша пробела, блокируем ее
                e.Handled = true;
            }
        }

        private void TeamAddForm_Load(object sender, EventArgs e)
        {
            TeamAddTb1.Text = string.Empty;
            TeamAddTb2.Text = "";
            TeamAddDate.Value = DateTime.Now;
        }
    }
}
