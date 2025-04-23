using System.Data.OleDb;

namespace BD.UpdateForms
{
    public class UpdateProduct : Form1
    {
        public UpdateProduct()
        {
            InitializeComponent();
            LoadDepSupp();
            LoadTableIntoComboBox();
        }

        private void LoadDepSupp()
        {
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

        private void LoadTableIntoComboBox()
        {
            connection.Open();
            try
            {

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT [Идентификатор товара] FROM [Товар]";
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
                command.CommandText = $"SELECT [Номер отдела],Цена ,Количество, [Срок годности]," +
                    $" [Дата поставки], [Номер поставщика] FROM Товар WHERE [Идентификатор товара]={Number.Text}";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    NumberDep.Text = reader[0].ToString();
                    Price.Text = reader[1].ToString();
                    Count.Text = reader[2].ToString();
                    ShelfLife.Text = reader[3].ToString();
                    Date.Text = reader[4].ToString();
                    NumberSupp.Text = reader[5].ToString();
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
            if (Price.Text == null || NumberDep.Text == null || Count.Text == null || ShelfLife.Text == null ||
                Date.Text == null || NumberSupp.Text == null || Number.Text == null)
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
                    command.CommandText = "Update Товар SET [Номер отдела]=@NumberDep, Цена=@Price, Количество=@Count," +
                        " [Срок годности]=@ShelfLife, [Дата поставки]=@Date, [Номер поставщика]=@NumberSupp " +
                        "WHERE [Идентификатор товара]=@Number";
                    command.Parameters.AddWithValue("@NumberDep", NumberDep.Text);
                    command.Parameters.AddWithValue("@Price", Price.Text);
                    command.Parameters.AddWithValue("@Count", Count.Text);
                    command.Parameters.AddWithValue("@ShelfLife", ShelfLife.Text);
                    command.Parameters.AddWithValue("@Date", Date.Text);
                    command.Parameters.AddWithValue("@NumberSupp", NumberSupp.Text);
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
            NumberSupp = new ComboBox();
            label6 = new Label();
            Date = new TextBox();
            label5 = new Label();
            ShelfLife = new TextBox();
            label4 = new Label();
            Count = new TextBox();
            label3 = new Label();
            NumberDep = new ComboBox();
            label2 = new Label();
            Price = new TextBox();
            label1 = new Label();
            Number = new ComboBox();
            Cansel = new Button();
            Upload = new Button();
            SuspendLayout();
            // 
            // NumberSupp
            // 
            NumberSupp.DropDownStyle = ComboBoxStyle.DropDownList;
            NumberSupp.FormattingEnabled = true;
            NumberSupp.Location = new Point(10, 484);
            NumberSupp.Name = "NumberSupp";
            NumberSupp.Size = new Size(308, 23);
            NumberSupp.TabIndex = 25;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(10, 446);
            label6.Name = "label6";
            label6.Size = new Size(180, 25);
            label6.TabIndex = 24;
            label6.Text = "Номер поставщика";
            // 
            // Date
            // 
            Date.Location = new Point(12, 411);
            Date.Name = "Date";
            Date.Size = new Size(310, 23);
            Date.TabIndex = 23;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(12, 371);
            label5.Name = "label5";
            label5.Size = new Size(137, 25);
            label5.TabIndex = 22;
            label5.Text = "Дата поставки";
            // 
            // ShelfLife
            // 
            ShelfLife.Location = new Point(10, 331);
            ShelfLife.Name = "ShelfLife";
            ShelfLife.Size = new Size(310, 23);
            ShelfLife.TabIndex = 21;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(10, 291);
            label4.Name = "label4";
            label4.Size = new Size(138, 25);
            label4.TabIndex = 20;
            label4.Text = "Срок годности";
            // 
            // Count
            // 
            Count.Location = new Point(10, 250);
            Count.Name = "Count";
            Count.Size = new Size(310, 23);
            Count.TabIndex = 19;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(10, 210);
            label3.Name = "label3";
            label3.Size = new Size(114, 25);
            label3.TabIndex = 18;
            label3.Text = "Количество";
            // 
            // NumberDep
            // 
            NumberDep.DropDownStyle = ComboBoxStyle.DropDownList;
            NumberDep.FormattingEnabled = true;
            NumberDep.Location = new Point(12, 96);
            NumberDep.Name = "NumberDep";
            NumberDep.Size = new Size(308, 23);
            NumberDep.TabIndex = 17;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 58);
            label2.Name = "label2";
            label2.Size = new Size(134, 25);
            label2.TabIndex = 16;
            label2.Text = "Номер отдела";
            // 
            // Price
            // 
            Price.Location = new Point(10, 175);
            Price.Name = "Price";
            Price.Size = new Size(310, 23);
            Price.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(10, 135);
            label1.Name = "label1";
            label1.Size = new Size(57, 25);
            label1.TabIndex = 14;
            label1.Text = "Цена";
            // 
            // Number
            // 
            Number.DropDownStyle = ComboBoxStyle.DropDownList;
            Number.FormattingEnabled = true;
            Number.Location = new Point(14, 21);
            Number.Name = "Number";
            Number.Size = new Size(308, 23);
            Number.TabIndex = 28;
            // 
            // Cansel
            // 
            Cansel.Location = new Point(175, 555);
            Cansel.Name = "Cansel";
            Cansel.Size = new Size(147, 39);
            Cansel.TabIndex = 27;
            Cansel.Text = "Отмена";
            Cansel.UseVisualStyleBackColor = true;
            Cansel.Click += CanselClick;
            // 
            // Upload
            // 
            Upload.Location = new Point(14, 555);
            Upload.Name = "Upload";
            Upload.Size = new Size(147, 39);
            Upload.TabIndex = 26;
            Upload.Text = "Обнавить";
            Upload.UseVisualStyleBackColor = true;
            Upload.Click += UpdateEntryClik;
            // 
            // UpdateProduct
            // 
            ClientSize = new Size(348, 628);
            Controls.Add(Number);
            Controls.Add(Cansel);
            Controls.Add(Upload);
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
            Name = "UpdateProduct";
            ResumeLayout(false);
            PerformLayout();
        }

        private ComboBox NumberSupp;
        private Label label6;
        private TextBox Date;
        private Label label5;
        private TextBox ShelfLife;
        private Label label4;
        private TextBox Count;
        private Label label3;
        private ComboBox NumberDep;
        private Label label2;
        private TextBox Price;
        protected ComboBox Number;
        protected Button Cansel;
        protected Button Upload;
        private Label label1;
    }
}