using System.Data;
using System.Data.OleDb;

namespace BD
{
    public partial class AddForm : Form1
    {
        protected string tableName;
        private List<TextBox> textBoxes = new List<TextBox>();
        private Dictionary<string, string> columnTypes = new Dictionary<string, string>();
        private OleDbConnection connection;
        public AddForm(/*string tableName*/)
        {
            InitializeComponent();
            tableName = tableName;
            connection = new OleDbConnection(connectionString);
            connection.Open();

            //try
            //{
            //    OleDbCommand command = new OleDbCommand();
            //    command.Connection = connection;
            //    command.CommandText = "SELECT * FROM " + tableName;

            //    //command.Parameters.AddWithValue("[@Table]", tableName);

            //    OleDbDataReader reader = command.ExecuteReader();
            //    if (reader.HasRows)
            //        for (int i = 0; i < reader.FieldCount; i++)
            //        {
            //            TextBox textBox = new TextBox();
            //            textBox.Location = new Point(10, 30 + textBoxes.Count * 30);
            //            textBox.Name = reader.GetName(i);
            //            textBox.Text = reader.GetName(i);
            //            textBox.Width = 200;
            //            Controls.Add(textBox);
            //            textBoxes.Add(textBox);
            //        }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Ошибка подключения:" + ex.Message);
            //}
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Собираем данные из полей ввода
                Dictionary<string, string> data = new Dictionary<string, string>();
                foreach (TextBox textBox in textBoxes)
                {
                    data[textBox.Name] = textBox.Text;
                }

                // Проверка введенных данных
                bool isValid = true;
                foreach (KeyValuePair<string, string> entry in data)
                {
                    string columnName = entry.Key;
                    string value = entry.Value;

                    // Проверка типа данных
                    switch (columnTypes[columnName])
                    {
                        case "INT":
                        case "INTEGER":
                            if (!int.TryParse(value, out int intValue))
                            {
                                MessageBox.Show($"В поле '{columnName}' должно быть целое число.");
                                isValid = false;
                                break;
                            }
                            break;
                        case "DATETIME":
                            if (!DateOnly.TryParse(value, out DateOnly dateTimeValue))
                            {
                                MessageBox.Show($"В поле '{columnName}' должно быть значение даты");
                                isValid = false;
                                break;
                            }
                            break;
                            // Добавьте проверки для других типов данных, если необходимо
                    }

                    if (!isValid)
                    {
                        break; // Выход из цикла, если обнаружена ошибка
                    }
                }

                if (isValid)
                {
                    // Формируем запрос на добавление записи
                    string query = $"INSERT INTO {tableName} (";
                    foreach (string columnName in data.Keys)
                    {
                        query += $"[{columnName}], ";
                    }
                    query = query.Substring(0, query.Length - 2); // Удаляем лишнюю запятую
                    query += ") VALUES (";
                    foreach (string value in data.Values)
                    {
                        query += $"{value}, ";
                    }
                    query = query.Substring(0, query.Length - 2); // Удаляем лишнюю запятую
                    query += ")";

                    // Выполняем запрос
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Закрываем форму
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления записи: " + ex.Message);
            }
        }
        protected void InitializeComponent()
        {
            SuspendLayout();
            // 
            // AddForm
            // 
            ClientSize = new Size(315, 406);
            Name = "AddForm";
            ResumeLayout(false);
        }
    }
}