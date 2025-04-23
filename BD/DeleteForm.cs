using System.Data;
using System.Data.OleDb;

namespace BD
{
    public partial class DeleteForm : Form1
    {
        private string TableName;
        private Button Delete;
        private Button Cansel;
        private Label label1;
        private ComboBox Number;
        private OleDbConnection connection;
        private TextBox textBox1;
        private string PrivetKeyName;
        public DeleteForm(string tableName)
        {
            InitializeComponent();
            TableName = tableName;
            connection = new OleDbConnection(connectionString);
            LoadTableIntoComboBox();
        }

        private void LoadTableIntoComboBox()
        {
            connection.Open();
            try
            {

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT * FROM [{TableName}]";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    PrivetKeyName = reader.GetName(0);
                    while (reader.Read())
                    {
                        Number.Items.Add(reader[0].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message);
            }
            finally { connection.Close(); }
            Number.SelectedIndexChanged += Number_SelectedIndexChanged;
        }

        private void Number_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            connection.Open();
            try
            {

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT * FROM {TableName} WHERE [{PrivetKeyName}]={Number.Text}";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    PrivetKeyName = reader.GetName(0);
                    while (reader.Read())
                    {
                        for (int i = 1; i < reader.FieldCount; i++)
                        {
                            textBox1.Text += $"{reader.GetName(i)}: {reader[i]}\r\n\n\n";
                        }
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
        }


        private void Delete_Click(object sender, EventArgs e)
        {
            if (Number.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для удаления.");
            }
            connection.Open();
            try
            {

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"DELETE FROM {TableName} WHERE [{PrivetKeyName}] = {Number.Text}";
                command.ExecuteNonQuery();
                Number.Items.Remove(Number.SelectedItem);
                textBox1.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении записи: {ex.Message}");
            }
            finally { connection.Close(); }
            MessageBox.Show("Запись успешно удалена");
        }

        public void CanselClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void InitializeComponent()
        {
            Delete = new Button();
            Cansel = new Button();
            label1 = new Label();
            Number = new ComboBox();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // Delete
            // 
            Delete.Location = new Point(12, 561);
            Delete.Name = "Delete";
            Delete.Size = new Size(147, 39);
            Delete.TabIndex = 1;
            Delete.Text = "Удалить";
            Delete.UseVisualStyleBackColor = true;
            Delete.Click += Delete_Click;
            // 
            // Cansel
            // 
            Cansel.Location = new Point(187, 561);
            Cansel.Name = "Cansel";
            Cansel.Size = new Size(147, 39);
            Cansel.TabIndex = 2;
            Cansel.Text = "Отмена";
            Cansel.UseVisualStyleBackColor = true;
            Cansel.Click += CanselClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(70, 25);
            label1.TabIndex = 3;
            label1.Text = "Номер";
            // 
            // Number
            // 
            Number.DropDownStyle = ComboBoxStyle.DropDownList;
            Number.FormattingEnabled = true;
            Number.Location = new Point(12, 51);
            Number.Name = "Number";
            Number.Size = new Size(308, 23);
            Number.TabIndex = 6;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 100);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(308, 23);
            textBox1.TabIndex = 7;
            textBox1.Height = 140;
            // 
            // DeleteForm
            // 
            ClientSize = new Size(346, 612);
            Controls.Add(textBox1);
            Controls.Add(Number);
            Controls.Add(label1);
            Controls.Add(Cansel);
            Controls.Add(Delete);
            Name = "DeleteForm";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}