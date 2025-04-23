using System.Data.OleDb;
using System.Data;
using BD.AddForms;

namespace BD
{
    public partial class MainForm : Form1
    {
        protected Button button1;
        protected Button button2;
        protected Button button3;
        protected ComboBox comboBox1;
        protected DataGridView dataGridView1;
        public static DataTable Channel;
        public MainForm()
        {
            InitializeComponent();
            connection = new OleDbConnection(connectionString);
            try
            {
                connection.Open();

                string[] tabels = { "Договоры", "Магазин", "Отдел", "Поставщик", "Сотрудник", "Товар",
                    "Количество товаров в отделах","Сотрудники в браке","Количество товаров в отделах",
                    "Договоры с поставщиком","Заканчивающиеся товары","Товары в отделе",
                    "Новая поставка","Подписание нового договора","Удаление испорченных товаров",
                    "MSysObjects" };
                foreach (string tabel in tabels)
                {
                    comboBox1.Items.Add(tabel);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTable();
        }

        private void GetTable()
        {
            // Выбранная таблица
            string tableName = comboBox1.SelectedItem.ToString();

            // Запрос для получения данных из таблицы
            if (IsTable(tableName))
            {
                if (tableName == "Договоры с поставщиком" || tableName == "Заканчивающиеся товары" || tableName == "Товары в отделе"
                    || tableName == "Новая поставка" || tableName== "Подписание нового договора" || tableName == "Удаление испорченных товаров")
                {
                    this.Enabled = false;
                    switch (tableName)
                    {
                        case ("Договоры с поставщиком"):
                            OpenForm.OpenParamsQuery("Введите номер поставщика", "[Номер]", "Договоры с поставщиком");
                            dataGridView1.DataSource = Channel;
                            break;
                        case ("Товары в отделе"):
                            OpenForm.OpenParamsQuery("Введите номер отдела", "[Номер]", tableName);
                            dataGridView1.DataSource = Channel;
                            break;
                        case ("Заканчивающиеся товары"):
                            OpenForm.OpenParamsQuery("Товары с количеством <", "[Меньше какого колличества товара]", tableName);
                            dataGridView1.DataSource = Channel;
                            break;
                        case ("Новая поставка"):
                            OpenForm.OpenOtherQuery("Идентификатор товара", "[Индентификатор]", tableName);
                            comboBox1.SelectedItem = "Товар";
                            break;
                        case ("Подписание нового договора"):
                            OpenForm.OpenOtherQuery("Номер поставщика", "[Номер]", tableName);
                            comboBox1.SelectedItem = "Поставщик";
                            break;
                        case ("Удаление испорченных товаров"):
                            OpenForm.OpenDeleteQuery(connectionString, tableName);
                            comboBox1.SelectedItem = "Товар";
                            break;

                    }
                    this.Enabled = true;
                }
                else
                {
                    try
                    {
                        string query;
                        connection.Open();
                        if (tableName == "MSysObjects")
                            query = $"SELECT Name, Id,ParentId,Type,DateCreate FROM [{tableName}]";
                        else
                            query = $"SELECT * FROM [{tableName}]";
                        OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
                    }
                    finally { connection.Close(); }
                }
            }
            else
            {
                MessageBox.Show("Таблица не найдена в базе данных!");
            }
        }

        public bool IsTable(string tableName)
        {
            bool isTable = false;
            connection = new OleDbConnection(connectionString);
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT Name FROM MSysObjects";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader[0].ToString() == tableName)
                            isTable = true;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в обращении к базе данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isTable;
        }
        private void buttonAddRecord_Click(object sender, EventArgs e)
        {

            // Получаем имя выбранной таблицы
            string tableName = comboBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("Выберите таблицу для добавления записи.");
                return;
            }
            this.Enabled = false;
            switch (tableName)
            {
                case ("Договоры"):
                    OpenForm.OpenAddContract();
                    break;
                case ("Поставщик"):
                    OpenForm.OpenAddSupper();
                    break;
                case ("Магазин"):
                    OpenForm.OpenAddShop();
                    break;
                case ("Отдел"):
                    OpenForm.OpenAddDep();
                    break;
                case ("Сотрудник"):
                    OpenForm.OpenAddWorker();
                    break;
                case ("Товар"):
                    OpenForm.OpenAddProduct();
                    break;
            }
            this.Enabled = true;
            GetTable();
        }

        private void buttonUploadRecord_Click(object sender, EventArgs e)
        {

            // Получаем имя выбранной таблицы
            string tableName = comboBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("Выберите таблицу для добавления записи.");
                return;
            }
            this.Enabled = false;
            switch (tableName)
            {
                case ("Договоры"):
                    OpenForm.OpenUpdateContract();
                    break;
                case ("Поставщик"):
                    OpenForm.OpenUpdateSupper();
                    break;
                case ("Магазин"):
                    OpenForm.OpenUpdateShop();
                    break;
                case ("Отдел"):
                    OpenForm.OpenUpdateDep();
                    break;
                case ("Сотрудник"):
                    OpenForm.OpenUpdateWorker();
                    break;
                case ("Товар"):
                    OpenForm.OpenUpdateProduct();
                    break;
            }
            this.Enabled = true;
            GetTable();
        }


        private void buttonDeleteRecord_Click(object sender, EventArgs e)
        {

            // Получаем имя выбранной таблицы
            string tableName = comboBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("Выберите таблицу для Удаления записи.");
                return;
            }
            this.Enabled = false;
            OpenForm.OpenDelete(tableName);
            this.Enabled = true;
            GetTable();
        }


        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            comboBox1 = new ComboBox();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(22, 456);
            button1.Name = "button1";
            button1.Size = new Size(130, 34);
            button1.TabIndex = 0;
            button1.Text = "Добавить запись";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonAddRecord_Click;
            // 
            // button2
            // 
            button2.Location = new Point(194, 456);
            button2.Name = "button2";
            button2.Size = new Size(130, 34);
            button2.TabIndex = 2;
            button2.Text = "Удалить запись";
            button2.UseVisualStyleBackColor = true;
            button2.Click += buttonDeleteRecord_Click;
            // 
            // button3
            // 
            button3.Location = new Point(368, 456);
            button3.Name = "button3";
            button3.Size = new Size(130, 34);
            button3.TabIndex = 3;
            button3.Text = "Изменить запись";
            button3.UseVisualStyleBackColor = true;
            button3.Click += buttonUploadRecord_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(22, 23);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(323, 23);
            comboBox1.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.GridColor = SystemColors.Control;
            dataGridView1.Location = new Point(22, 62);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(954, 361);
            dataGridView1.TabIndex = 5;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1025, 598);
            Controls.Add(dataGridView1);
            Controls.Add(comboBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }
    }


}