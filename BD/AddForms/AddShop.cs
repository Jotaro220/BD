using System.Data.OleDb;

namespace BD.AddForms
{
    public partial class AddShop : Form1
    {
        private OleDbConnection connection;
        public AddShop()
        {
            InitializeComponent();

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

        public void AddEntryClik(object sender, EventArgs e)
        {
            if (NumberWorker.Text == null || NameShop.Text == null || Spesholize.Text == null || INN.Text == null || Adress.Text == null)
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
                    command.CommandText = "INSERT INTO Магазин([Название магазина], [Специализация], [ИНН], [Адрес], [Табельный номер директора]) VALUES (@NameShop, @Spesholize, @INN, @Adress, @NumberWorker)";
                    command.Parameters.AddWithValue("@NameShop", NameShop.Text);
                    command.Parameters.AddWithValue("@Spesholize", Spesholize.Text);
                    command.Parameters.AddWithValue("@INN", INN.Text);
                    command.Parameters.AddWithValue("@Adress", Adress.Text);
                    command.Parameters.AddWithValue("@NumberWorker", NumberWorker.Text);
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

        private Button Add;
        private Button Cansel;
        private TextBox NameShop;
        private Label label2;
        private ComboBox NumberWorker;
        private TextBox Spesholize;
        private Label label3;
        private TextBox INN;
        private Label label4;
        private TextBox Adress;
        private Label label5;
        private Label label1;

        private void InitializeComponent()
        {
            Add = new Button();
            Cansel = new Button();
            label1 = new Label();
            NameShop = new TextBox();
            label2 = new Label();
            NumberWorker = new ComboBox();
            Spesholize = new TextBox();
            label3 = new Label();
            INN = new TextBox();
            label4 = new Label();
            Adress = new TextBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // Add
            // 
            Add.Location = new Point(14, 397);
            Add.Name = "Add";
            Add.Size = new Size(147, 39);
            Add.TabIndex = 0;
            Add.Text = "Добавить";
            Add.UseVisualStyleBackColor = true;
            Add.Click += AddEntryClik;
            // 
            // Cansel
            // 
            Cansel.Location = new Point(173, 397);
            Cansel.Name = "Cansel";
            Cansel.Size = new Size(147, 39);
            Cansel.TabIndex = 1;
            Cansel.Text = "Отмена";
            Cansel.UseVisualStyleBackColor = true;
            Cansel.Click += CanselClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(180, 25);
            label1.TabIndex = 2;
            label1.Text = "Название магазина";
            // 
            // NameShop
            // 
            NameShop.Location = new Point(14, 50);
            NameShop.Name = "NameShop";
            NameShop.Size = new Size(306, 23);
            NameShop.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 292);
            label2.Name = "label2";
            label2.Size = new Size(265, 25);
            label2.TabIndex = 4;
            label2.Text = "Табельный номер директора";
            // 
            // NumberWorker
            // 
            NumberWorker.DropDownStyle = ComboBoxStyle.DropDownList;
            NumberWorker.FormattingEnabled = true;
            NumberWorker.Location = new Point(12, 330);
            NumberWorker.Name = "NumberWorker";
            NumberWorker.Size = new Size(308, 23);
            NumberWorker.TabIndex = 5;
            // 
            // Spesholize
            // 
            Spesholize.Location = new Point(12, 118);
            Spesholize.Name = "Spesholize";
            Spesholize.Size = new Size(306, 23);
            Spesholize.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(10, 90);
            label3.Name = "label3";
            label3.Size = new Size(148, 25);
            label3.TabIndex = 6;
            label3.Text = "Специализация";
            // 
            // INN
            // 
            INN.Location = new Point(12, 190);
            INN.Name = "INN";
            INN.Size = new Size(306, 23);
            INN.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(10, 162);
            label4.Name = "label4";
            label4.Size = new Size(52, 25);
            label4.TabIndex = 8;
            label4.Text = "ИНН";
            // 
            // Adress
            // 
            Adress.Location = new Point(12, 254);
            Adress.Name = "Adress";
            Adress.Size = new Size(306, 23);
            Adress.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(10, 226);
            label5.Name = "label5";
            label5.Size = new Size(64, 25);
            label5.TabIndex = 10;
            label5.Text = "Адрес";
            // 
            // AddShop
            // 
            ClientSize = new Size(346, 471);
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
            Controls.Add(Cansel);
            Controls.Add(Add);
            Name = "AddShop";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}