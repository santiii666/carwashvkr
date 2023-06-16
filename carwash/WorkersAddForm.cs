using Guna.UI2.WinForms.Suite;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carwash
{
    public partial class WorkersAddForm : Form
    {
        public WorkersAddForm()
        {
            InitializeComponent();
            OnLoad(EventArgs.Empty);
        }

        public int SelectedWorkerId { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (SelectedWorkerId > 0)
            {
                // Изменение текста кнопки и названия формы для режима изменения
                WorkersAddButton1.Text = "Изменить";
                this.Text = "Изменить работника";

                // Установка значения в OrderAddCb1
                WorkersAddTb1.Text = SelectedWorkerId.ToString();

                // Блокировка поля OrderAddTb1
                WorkersAddTb1.Enabled = false;
            }
            else
            {
                // Изменение текста кнопки и названия формы для режима добавления
                WorkersAddButton1.Text = "Добавить";
                this.Text = "Добавить работника";

                // Разблокировка поля OrderAddTb1
                WorkersAddTb1.Enabled = true;
            }
        }

        private void WorkersAddButton2_Click(object sender, EventArgs e)
        {
            // Создание и настройка OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.png, *.bmp)|*.jpg;*.png;*.bmp";

            // Проверка, была ли выбрана фотография
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Загрузка выбранной фотографии в PictureBox
                string selectedImagePath = openFileDialog.FileName;
                Image selectedImage = Image.FromFile(selectedImagePath);
                WorkersAddPb1.Image = selectedImage;
            }
        }

        private void AddWorkerToDatabase()
        {
            if (string.IsNullOrEmpty(WorkersAddTb1.Text) ||
                string.IsNullOrEmpty(WorkersAddTb2.Text) ||
                string.IsNullOrEmpty(WorkersAddTb3.Text) ||
                WorkersAddCb1.SelectedItem == null ||
                string.IsNullOrEmpty(WorkersAddDate.Text) ||
                WorkersAddPb1.Image == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и выберите фото работника.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            KeyValuePair<int, string> selectedWorkgroup = (KeyValuePair<int, string>)WorkersAddCb1.SelectedItem;
            int selectedWorkgroupId = selectedWorkgroup.Key;

            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM workers WHERE ID_Worker = @ID_Worker";
                using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@ID_Worker", WorkersAddTb1.Text);

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Работник с таким ID уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string query = "INSERT INTO workers (ID_Worker, IDWorkgroup, Name_Worker, Number_Contract, Photo, Date_Of_Employment) " +
                               "VALUES (@ID_Worker, @IDWorkgroup, @Name_Worker, @Number_Contract, @Photo, @Date_Of_Employment)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Worker", WorkersAddTb1.Text);
                    command.Parameters.AddWithValue("@IDWorkgroup", selectedWorkgroupId);
                    command.Parameters.AddWithValue("@Name_Worker", WorkersAddTb2.Text);
                    command.Parameters.AddWithValue("@Number_Contract", WorkersAddTb3.Text);
                    command.Parameters.AddWithValue("@Photo", GetByteArrayFromImage(WorkersAddPb1.Image));
                    command.Parameters.AddWithValue("@Date_Of_Employment", WorkersAddDate.Value);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            MessageBox.Show("Работник успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void UpdateWorkerInDatabase(string workerId)
        {
            if (string.IsNullOrEmpty(WorkersAddTb1.Text) ||
                string.IsNullOrEmpty(WorkersAddTb2.Text) ||
                string.IsNullOrEmpty(WorkersAddTb3.Text) ||
                WorkersAddCb1.SelectedItem == null ||
                string.IsNullOrEmpty(WorkersAddDate.Text) ||
                WorkersAddPb1.Image == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и выберите фото работника.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            KeyValuePair<int, string> selectedWorkgroup = (KeyValuePair<int, string>)WorkersAddCb1.SelectedItem;
            int selectedWorkgroupId = selectedWorkgroup.Key;

            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE workers SET ID_Worker = @ID_Worker, IDWorkgroup = @IDWorkgroup, Name_Worker = @Name_Worker, " +
                                     "Number_Contract = @Number_Contract, Photo = @Photo, Date_Of_Employment = @Date_Of_Employment " +
                                     "WHERE ID_Worker = @WorkerId";

                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@ID_Worker", WorkersAddTb1.Text);
                    updateCommand.Parameters.AddWithValue("@IDWorkgroup", selectedWorkgroupId);
                    updateCommand.Parameters.AddWithValue("@Name_Worker", WorkersAddTb2.Text);
                    updateCommand.Parameters.AddWithValue("@Number_Contract", WorkersAddTb3.Text);
                    updateCommand.Parameters.AddWithValue("@Photo", GetByteArrayFromImage(WorkersAddPb1.Image));
                    updateCommand.Parameters.AddWithValue("@Date_Of_Employment", WorkersAddDate.Value);
                    updateCommand.Parameters.AddWithValue("@WorkerId", workerId);

                    updateCommand.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Работник успешно изменен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void WorkersAddButton1_Click(object sender, EventArgs e)
        {
            if (SelectedWorkerId > 0)
            {
                UpdateWorkerInDatabase(SelectedWorkerId.ToString());
            }
            else
            {
                AddWorkerToDatabase();
            }
        }

        // Метод для преобразования изображения в массив байтов
        private byte[] GetByteArrayFromImage(Image image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();
            }
        }

        private void ClearForm()
        {
            WorkersAddTb1.Text = string.Empty;
            WorkersAddCb1.SelectedIndex = -1;
            WorkersAddTb2.Text = "";
            WorkersAddTb3.Text = " ";
            WorkersAddDate.Value = DateTime.Now;
            WorkersAddPb1.Image = null;
        }

        private void WorkersAddForm_Load(object sender, EventArgs e)
        {
            ClearForm();

            string connectionString = "Server=localhost;Database=carwash_db;Uid=root;Pwd=2331;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Выполняем запрос для выбора бригад с количеством работников меньше или равным 3,
                // или бригады, связанные с выбранным работником
                string query = "SELECT ID_Workgroup, CONCAT('Бригада', ' ', Number_Workgroup) AS WorkgroupInfo " +
                               "FROM workgroup " +
                               "WHERE (ID_Workgroup NOT IN " +
                               "(SELECT IDWorkgroup FROM workers GROUP BY IDWorkgroup HAVING COUNT(*) >= 3))";

                if (SelectedWorkerId > 0)
                {
                    // Добавляем условие, чтобы включить бригаду, связанную с выбранным работником
                    query += " OR ID_Workgroup = (SELECT IDWorkgroup FROM workers WHERE ID_Worker = @SelectedWorkerId)";
                }

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (SelectedWorkerId > 0)
                    {
                        // Передаем выбранный ID_Worker, чтобы включить связанную бригаду в результаты запроса
                        command.Parameters.AddWithValue("@SelectedWorkerId", SelectedWorkerId);
                    }

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        WorkersAddCb1.Items.Clear();

                        while (reader.Read())
                        {
                            int workgroupId = reader.GetInt32("ID_Workgroup");
                            string workgroupInfo = reader.GetString("WorkgroupInfo");

                            WorkersAddCb1.Items.Add(new KeyValuePair<int, string>(workgroupId, workgroupInfo));
                        }
                    }
                }

                connection.Close();
            }
        }

        private void WorkersAddTb1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли вводимый символ цифрой или клавишей Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                // Если символ не цифра и не клавиша Backspace, отменяем его ввод
                e.Handled = true;
            }
        }

        private void WorkersAddTb1_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, нажата ли клавиша пробела
            if (e.KeyCode == Keys.Space)
            {
                // Если нажата клавиша пробела, блокируем ее
                e.Handled = true;
            }
        }

        private void WorkersAddTb3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли вводимый символ цифрой или клавишей Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                // Если символ не цифра и не клавиша Backspace, отменяем его ввод
                e.Handled = true;
            }
        }

        private void WorkersAddTb3_KeyDown(object sender, KeyEventArgs e)
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
