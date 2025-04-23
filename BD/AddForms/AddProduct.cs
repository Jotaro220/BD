using System.Data.OleDb;

namespace BD.AddForms
{
    public partial class AddProduct : Form1
    {
        private OleDbConnection connection;
        public AddProduct()
        {
            InitializeComponent();

            connection = new OleDbConnection(connectionString);
            connection.Open();

            try
            {
                OleDbCommand command1 = new OleDbCommand();
                command1.Connection = connection;
                command1.CommandText = "SELECT [Номер отдела] FROM Отдел";
                OleDbDataReader reader = command1.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        NumberDep.Items.Add(reader[0].ToString());
                    }
                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                command2.CommandText = "SELECT [Номер поставщика] FROM Поставщик";
                reader = command2.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        NumberSupp.Items.Add(reader[0].ToString());
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
            if (Price.Text == null || NumberDep.Text == null || Count.Text == null || ShelfLife.Text == null ||
                Date.Text == null || NumberSupp.Text == null)
            {
                MessageBox.Show("Все поля должны быть заполненны!");
                return;
            }
            else if (!Checks.DateCheck(Date.Text) && !Checks.DateCheck(ShelfLife.Text))
            {
                MessageBox.Show("Дата введена не верно!");
                return;
            }
            else if (!Checks.IntCheck(Count.Text))
            {
                MessageBox.Show("Количество - положительное число!");
                return;
            }
            else if (!Checks.DoubleCheck(Price.Text))
            {
                MessageBox.Show("Цена - положительное число!");
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
                    command.CommandText = "INSERT INTO Товар([Номер отдела], Цена, Количество, [Срок годности], [Дата поставки], [Номер поставщика]) VALUES (@NumberDep, @Price, @Count, @ShelfLife, @Date, @NumberSupp)";
                    command.Parameters.AddWithValue("@NumberDep", NumberDep.Text);
                    command.Parameters.AddWithValue("@Price", Price.Text);
                    command.Parameters.AddWithValue("@Count", Count.Text);
                    command.Parameters.AddWithValue("@ShelfLife", ShelfLife.Text);
                    command.Parameters.AddWithValue("@Date", Date.Text);
                    command.Parameters.AddWithValue("@NumberSupp", NumberSupp.Text);
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
        private TextBox Price;
        private Label label2;
        private ComboBox NumberDep;
        private TextBox Count;
        private Label label3;
        private TextBox ShelfLife;
        private Label label4;
        private TextBox Date;
        private Label label5;
        private ComboBox NumberSupp;
        private Label label6;
        private Label label1;

        private void InitializeComponent()
        {
            Add = new Button();
            Cansel = new Button();
            label1 = new Label();
            Price = new TextBox();
            label2 = new Label();
            NumberDep = new ComboBox();
            Count = new TextBox();
            label3 = new Label();
            ShelfLife = new TextBox();
            label4 = new Label();
            Date = new TextBox();
            label5 = new Label();
            NumberSupp = new ComboBox();
            label6 = new Label();
            SuspendLayout();
            // 
            // Add
            // 
            Add.Location = new Point(12, 513);
            Add.Name = "Add";
            Add.Size = new Size(147, 39);
            Add.TabIndex = 0;
            Add.Text = "Добавить";
            Add.UseVisualStyleBackColor = true;
            Add.Click += AddEntryClik;
            // 
            // Cansel
            // 
            Cansel.Location = new Point(187, 513);
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
            label1.Location = new Point(10, 95);
            label1.Name = "label1";
            label1.Size = new Size(57, 25);
            label1.TabIndex = 2;
            label1.Text = "Цена";
            // 
            // Price
            // 
            Price.Location = new Point(10, 135);
            Price.Name = "Price";
            Price.Size = new Size(310, 23);
            Price.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 18);
            label2.Name = "label2";
            label2.Size = new Size(134, 25);
            label2.TabIndex = 4;
            label2.Text = "Номер отдела";
            // 
            // NumberDep
            // 
            NumberDep.DropDownStyle = ComboBoxStyle.DropDownList;
            NumberDep.FormattingEnabled = true;
            NumberDep.Location = new Point(12, 56);
            NumberDep.Name = "NumberDep";
            NumberDep.Size = new Size(308, 23);
            NumberDep.TabIndex = 5;
            // 
            // Count
            // 
            Count.Location = new Point(10, 210);
            Count.Name = "Count";
            Count.Size = new Size(310, 23);
            Count.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(10, 170);
            label3.Name = "label3";
            label3.Size = new Size(114, 25);
            label3.TabIndex = 6;
            label3.Text = "Количество";
            // 
            // ShelfLife
            // 
            ShelfLife.Location = new Point(10, 291);
            ShelfLife.Name = "ShelfLife";
            ShelfLife.Size = new Size(310, 23);
            ShelfLife.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(10, 251);
            label4.Name = "label4";
            label4.Size = new Size(138, 25);
            label4.TabIndex = 8;
            label4.Text = "Срок годности";
            // 
            // Date
            // 
            Date.Location = new Point(12, 371);
            Date.Name = "Date";
            Date.Size = new Size(310, 23);
            Date.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(12, 331);
            label5.Name = "label5";
            label5.Size = new Size(137, 25);
            label5.TabIndex = 10;
            label5.Text = "Дата поставки";
            // 
            // NumberSupp
            // 
            NumberSupp.DropDownStyle = ComboBoxStyle.DropDownList;
            NumberSupp.FormattingEnabled = true;
            NumberSupp.Location = new Point(10, 444);
            NumberSupp.Name = "NumberSupp";
            NumberSupp.Size = new Size(308, 23);
            NumberSupp.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(10, 406);
            label6.Name = "label6";
            label6.Size = new Size(180, 25);
            label6.TabIndex = 12;
            label6.Text = "Номер поставщика";
            // 
            // AddProduct
            // 
            ClientSize = new Size(346, 567);
            Controls.Add(NumberSupp);
            Controls.Add(label6);
            Controls.Add(Date);
            Controls.Add(label5);
            Controls.Add(ShelfLife);
            Controls.Add(label4);
            Controls.Add(Count);
            Controls.Add(label3);
            Controls.Add(NumberDep);
            Controls.Add(label2);
            Controls.Add(Price);
            Controls.Add(label1);
            Controls.Add(Cansel);
            Controls.Add(Add);
            Name = "AddProduct";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}