using System.Data.OleDb;
using System.Diagnostics;

namespace BD.UpdateForms
{
    public class UpdateShop : Form1
    {
        public UpdateShop()
        {
            InitializeComponent();
            LoadWorker();
            LoadTableIntoComboBox();
        }

        private void LoadWorker()
        {
            connection = new OleDbConnection(connectionString);
            connection.Open();

            try
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT [Табельный номер Сотрудника] FROM Сотрудник";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        NumberWorker.Items.Add(reader[0].ToString());
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
                command.CommandText = $"SELECT [Номер магазина] FROM Магазин";
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
                command.CommandText = $"SELECT [Название магазина],Специализация, ИНН, Адрес, [Табельный номер директора]" +
                    $" FROM Магазин WHERE [Номер магазина]={Number.Text}";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    NameShop.Text = reader[0].ToString();
                    Spesholize.Text = reader[1].ToString();
                    INN.Text = reader[2].ToString();
                    Adress.Text = reader[3].ToString();
                    NumberWorker.Text = reader[4].ToString();
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
            if (NumberWorker.Text == null || NameShop.Text == null || Spesholize.Text == null || INN.Text == null ||
                Adress.Text == null || Number.Text == null)
            {
                MessageBox.Show("Все поля должны быть заполненны!");
                return;
            }
            else if (!Checks.IntCheck(INN.Text) || INN.Text.Length != 10)
            {
                MessageBox.Show("ИНН состоит из 10 цифр!");
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
                    command.CommandText = "Update Магазин SET [Название магазина]=@NameShop, Специализация=@Spesholize," +
                        " ИНН=@INN, Адрес=@Adress, [Табельный номер директора]=@NumberWorker WHERE [Номер Магазина]=@Number";
                    command.Parameters.AddWithValue("@NameShop", NameShop.Text);
                    command.Parameters.AddWithValue("@Spesholize", Spesholize.Text);
                    command.Parameters.AddWithValue("@INN", INN.Text);
                    command.Parameters.AddWithValue("@Adress", Adress.Text);
                    command.Parameters.AddWithValue("@NumberWorker", NumberWorker.Text);
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
            label5 = new Label();
            INN = new TextBox();
            label4 = new Label();
            Spesholize = new TextBox();
            label3 = new Label();
            NumberWorker = new ComboBox();
            label2 = new Label();
            NameShop = new TextBox();
            label1 = new Label();
            Number = new ComboBox();
            Cansel = new Button();
            Upload = new Button();
            SuspendLayout();
            // 
            // Adress
            // 
            Adress.Location = new Point(10, 289);
            Adress.Name = "Adress";
            Adress.Size = new Size(306, 23);
            Adress.TabIndex = 21;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(8, 261);
            label5.Name = "label5";
            label5.Size = new Size(64, 25);
            label5.TabIndex = 20;
            label5.Text = "Адрес";
            // 
            // INN
            // 
            INN.Location = new Point(10, 225);
            INN.Name = "INN";
            INN.Size = new Size(306, 23);
            INN.TabIndex = 19;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(8, 197);
            label4.Name = "label4";
            label4.Size = new Size(52, 25);
            label4.TabIndex = 18;
            label4.Text = "ИНН";
            // 
            // Spesholize
            // 
            Spesholize.Location = new Point(10, 153);
            Spesholize.Name = "Spesholize";
            Spesholize.Size = new Size(306, 23);
            Spesholize.TabIndex = 17;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(8, 125);
            label3.Name = "label3";
            label3.Size = new Size(148, 25);
            label3.TabIndex = 16;
            label3.Text = "Специализация";
            // 
            // NumberWorker
            // 
            NumberWorker.DropDownStyle = ComboBoxStyle.DropDownList;
            NumberWorker.FormattingEnabled = true;
            NumberWorker.Location = new Point(10, 365);
            NumberWorker.Name = "NumberWorker";
            NumberWorker.Size = new Size(308, 23);
            NumberWorker.TabIndex = 15;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(10, 327);
            label2.Name = "label2";
            label2.Size = new Size(265, 25);
            label2.TabIndex = 14;
            label2.Text = "Табельный номер директора";
            // 
            // NameShop
            // 
            NameShop.Location = new Point(12, 85);
            NameShop.Name = "NameShop";
            NameShop.Size = new Size(306, 23);
            NameShop.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(10, 57);
            label1.Name = "label1";
            label1.Size = new Size(180, 25);
            label1.TabIndex = 12;
            label1.Text = "Название магазина";
            // 
            // Number
            // 
            Number.DropDownStyle = ComboBoxStyle.DropDownList;
            Number.FormattingEnabled = true;
            Number.Location = new Point(8, 12);
            Number.Name = "Number";
            Number.Size = new Size(308, 23);
            Number.TabIndex = 31;
            // 
            // Cansel
            // 
            Cansel.Location = new Point(173, 419);
            Cansel.Name = "Cansel";
            Cansel.Size = new Size(147, 39);
            Cansel.TabIndex = 30;
            Cansel.Text = "Отмена";
            Cansel.UseVisualStyleBackColor = true;
            Cansel.Click += CanselClick;
            // 
            // Upload
            // 
            Upload.Location = new Point(12, 419);
            Upload.Name = "Upload";
            Upload.Size = new Size(147, 39);
            Upload.TabIndex = 29;
            Upload.Text = "Обнавить";
            Upload.UseVisualStyleBackColor = true;
            Upload.Click += UpdateEntryClik;
            // 
            // UpdateShop
            // 
            ClientSize = new Size(332, 479);
            Controls.Add(Number);
            Controls.Add(Cansel);
            Controls.Add(Upload);
            Controls.Add(Adress);
            Controls.Add(label5);
            Controls.Add(INN);
            Controls.Add(label4);
            Controls.Add(Spesholize);
            Controls.Add(label3);
            Controls.Add(NumberWorker);
            Controls.Add(label2);
            Controls.Add(NameShop);
            Controls.Add(label1);
            Name = "UpdateShop";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox Adress;
        private Label label5;
        private TextBox INN;
        private Label label4;
        private TextBox Spesholize;
        private Label label3;
        private ComboBox NumberWorker;
        private Label label2;
        private TextBox NameShop;
        protected ComboBox Number;
        protected Button Cansel;
        protected Button Upload;
        private Label label1;
    }
}