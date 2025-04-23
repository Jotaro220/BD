using System.Data.OleDb;

namespace BD.UpdateForms
{
    public class UpdateSupper : Form1
    {
        public UpdateSupper()
        {
            InitializeComponent();
            LoadTableIntoComboBox();
        }

        private void LoadTableIntoComboBox()
        {
            connection = new OleDbConnection(connectionString);
            connection.Open();
            try
            {

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT [Номер поставщика] FROM Поставщик";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
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
            connection.Open();
            try
            {

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT [Название поставщика], [Адрес поставщика]" +
                    $" FROM Поставщик WHERE [Номер поставщика]={Number.Text}";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    NameSupper.Text = reader[0].ToString();
                    Adress.Text = reader[1].ToString();
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

        public void UpdateEntryClik(object sender, EventArgs e)
        {
            if (Adress.Text == null || NameSupper.Text == null || Number == null)
            {
                MessageBox.Show("Все поля должны быть заполненны!");
                return;
            }
            else
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();

                try
                {
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = "Update Поставщик SET [Название поставщика]=@NameSupper, [Адрес поставщика]=@Adress" +
                        " WHERE [Номер поставщика]=@Number";
                    command.Parameters.AddWithValue("@NameSupper", NameSupper.Text);
                    command.Parameters.AddWithValue("@Adress", Adress.Text);
                    command.Parameters.AddWithValue("@Number", Number.Text);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка в добавлении данных: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                DialogResult = DialogResult.OK;

            }
        }

        public void CanselClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void InitializeComponent()
        {
            Adress = new TextBox();
            label2 = new Label();
            NameSupper = new TextBox();
            label1 = new Label();
            Number = new ComboBox();
            Cansel = new Button();
            Upload = new Button();
            SuspendLayout();
            // 
            // Adress
            // 
            Adress.Location = new Point(10, 147);
            Adress.Name = "Adress";
            Adress.Size = new Size(306, 23);
            Adress.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(10, 119);
            label2.Name = "label2";
            label2.Size = new Size(174, 25);
            label2.TabIndex = 8;
            label2.Text = "Адрес поставщика";
            // 
            // NameSupper
            // 
            NameSupper.Location = new Point(12, 84);
            NameSupper.Name = "NameSupper";
            NameSupper.Size = new Size(306, 23);
            NameSupper.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(10, 56);
            label1.Name = "label1";
            label1.Size = new Size(205, 25);
            label1.TabIndex = 6;
            label1.Text = "Название поставщика";
            // 
            // Number
            // 
            Number.DropDownStyle = ComboBoxStyle.DropDownList;
            Number.FormattingEnabled = true;
            Number.Location = new Point(12, 12);
            Number.Name = "Number";
            Number.Size = new Size(308, 23);
            Number.TabIndex = 34;
            // 
            // Cansel
            // 
            Cansel.Location = new Point(173, 206);
            Cansel.Name = "Cansel";
            Cansel.Size = new Size(147, 39);
            Cansel.TabIndex = 33;
            Cansel.Text = "Отмена";
            Cansel.UseVisualStyleBackColor = true;
            Cansel.Click += CanselClick;
            // 
            // Upload
            // 
            Upload.Location = new Point(12, 206);
            Upload.Name = "Upload";
            Upload.Size = new Size(147, 39);
            Upload.TabIndex = 32;
            Upload.Text = "Обнавить";
            Upload.UseVisualStyleBackColor = true;
            Upload.Click += UpdateEntryClik;
            // 
            // UpdateSupper
            // 
            ClientSize = new Size(331, 264);
            Controls.Add(Number);
            Controls.Add(Cansel);
            Controls.Add(Upload);
            Controls.Add(Adress);
            Controls.Add(label2);
            Controls.Add(NameSupper);
            Controls.Add(label1);
            Name = "UpdateSupper";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox Adress;
        private Label label2;
        private TextBox NameSupper;
        protected ComboBox Number;
        protected Button Cansel;
        protected Button Upload;
        private Label label1;
    }
}