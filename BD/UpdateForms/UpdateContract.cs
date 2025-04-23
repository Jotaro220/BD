using BD.AddForms;
using System.Data.OleDb;

namespace BD.UpdateForms
{
    public class UpdateContract : Form1
    {
        private OleDbConnection connection;
        public UpdateContract()
        {

            InitializeComponent();
            LoadSupp();
            LoadTableIntoComboBox();
        }

        private void LoadSupp()
        {
            connection = new OleDbConnection(connectionString);
            connection.Open();

            try
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT [Номер поставщика] FROM Поставщик";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        Supp.Items.Add(reader[0].ToString());
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в подключении к базе данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        private void LoadTableIntoComboBox()
        {
            connection.Open();
            try
            {

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT * FROM [Договоры]";
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
                command.CommandText = $"SELECT Дата, [Номер поставщика] FROM Договоры WHERE [Номер договора]={Number.Text}";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Date.Text = reader[0].ToString();
                    Supp.Text = reader[1].ToString();
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
            if (Supp.Text == null || Date.Text == null || Number.Text == null)
            {
                MessageBox.Show("Все поля должны быть заполненны!");
                return;
            }
            else if (!Checks.DateCheck(Date.Text))
            {
                MessageBox.Show("Дата введена не верно!");
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
                    command.CommandText = "Update Договоры SET Дата=@Data, [Номер поставщика]=@NumberSupp" +
                        " WHERE [Номер договора]=@Number";
                    command.Parameters.AddWithValue("@Date", Date.Text);
                    command.Parameters.AddWithValue("@NumberSupp", Supp.Text);
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
            Supp = new ComboBox();
            label2 = new Label();
            Date = new TextBox();
            label1 = new Label();
            Cansel = new Button();
            Upload = new Button();
            Number = new ComboBox();
            SuspendLayout();
            // 
            // Supp
            // 
            Supp.DropDownStyle = ComboBoxStyle.DropDownList;
            Supp.FormattingEnabled = true;
            Supp.Location = new Point(10, 168);
            Supp.Name = "Supp";
            Supp.Size = new Size(308, 23);
            Supp.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(10, 130);
            label2.Name = "label2";
            label2.Size = new Size(180, 25);
            label2.TabIndex = 10;
            label2.Text = "Номер поставщика";
            // 
            // Date
            // 
            Date.Location = new Point(12, 95);
            Date.Name = "Date";
            Date.Size = new Size(306, 23);
            Date.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(10, 67);
            label1.Name = "label1";
            label1.Size = new Size(53, 25);
            label1.TabIndex = 8;
            label1.Text = "Дата";
            // 
            // Cansel
            // 
            Cansel.Location = new Point(171, 279);
            Cansel.Name = "Cansel";
            Cansel.Size = new Size(147, 39);
            Cansel.TabIndex = 7;
            Cansel.Text = "Отмена";
            Cansel.UseVisualStyleBackColor = true;
            Cansel.Click += CanselClick;
            // 
            // Upload
            // 
            Upload.Location = new Point(10, 279);
            Upload.Name = "Upload";
            Upload.Size = new Size(147, 39);
            Upload.TabIndex = 6;
            Upload.Text = "Обнавить";
            Upload.UseVisualStyleBackColor = true;
            Upload.Click += UpdateEntryClik;
            // 
            // Number
            // 
            Number.DropDownStyle = ComboBoxStyle.DropDownList;
            Number.FormattingEnabled = true;
            Number.Location = new Point(10, 12);
            Number.Name = "Number";
            Number.Size = new Size(308, 23);
            Number.TabIndex = 12;
            // 
            // UpdateContract
            // 
            ClientSize = new Size(334, 336);
            Controls.Add(Number);
            Controls.Add(Supp);
            Controls.Add(label2);
            Controls.Add(Date);
            Controls.Add(label1);
            Controls.Add(Cansel);
            Controls.Add(Upload);
            Name = "UpdateContract";
            ResumeLayout(false);
            PerformLayout();
        }

        protected ComboBox Supp;
        protected Label label2;
        protected TextBox Date;
        protected Label label1;
        protected Button Cansel;
        protected Button Upload;
        protected ComboBox Number;
    }
}