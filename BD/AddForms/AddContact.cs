using System.Data.OleDb;

namespace BD.AddForms
{
    public partial class AddContract : Form1
    {
        private OleDbConnection connection;
        public AddContract()
        {
            InitializeComponent();

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

        public void AddEntryClik(object sender, EventArgs e)
        {
            if (Supp.Text == null || Date.Text == null)
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
                    command.CommandText = "INSERT INTO Договоры(Дата, [Номер поставщика]) VALUES (@Date, @Number)";
                    command.Parameters.AddWithValue("@Date", Date.Text);
                    command.Parameters.AddWithValue("@Number", Supp.Text);
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

        protected Button Add;
        protected Button Cansel;
        protected TextBox Date;
        protected Label label2;
        protected ComboBox Supp;
        protected Label label1;

        protected void InitializeComponent()
        {
            Add = new Button();
            Cansel = new Button();
            label1 = new Label();
            Date = new TextBox();
            label2 = new Label();
            Supp = new ComboBox();
            SuspendLayout();
            // 
            // Add
            // 
            Add.Location = new Point(10, 236);
            Add.Name = "Add";
            Add.Size = new Size(147, 39);
            Add.TabIndex = 0;
            Add.Text = "Добавить";
            Add.UseVisualStyleBackColor = true;
            Add.Click += AddEntryClik;
            // 
            // Cansel
            // 
            Cansel.Location = new Point(187, 236);
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
            label1.Location = new Point(10, 24);
            label1.Name = "label1";
            label1.Size = new Size(53, 25);
            label1.TabIndex = 2;
            label1.Text = "Дата";
            // 
            // Date
            // 
            Date.Location = new Point(12, 52);
            Date.Name = "Date";
            Date.Size = new Size(306, 23);
            Date.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(10, 87);
            label2.Name = "label2";
            label2.Size = new Size(180, 25);
            label2.TabIndex = 4;
            label2.Text = "Номер поставщика";
            // 
            // Supp
            // 
            Supp.DropDownStyle = ComboBoxStyle.DropDownList;
            Supp.FormattingEnabled = true;
            Supp.Location = new Point(10, 125);
            Supp.Name = "Supp";
            Supp.Size = new Size(308, 23);
            Supp.TabIndex = 5;
            // 
            // AddContract
            // 
            ClientSize = new Size(346, 317);
            Controls.Add(Supp);
            Controls.Add(label2);
            Controls.Add(Date);
            Controls.Add(label1);
            Controls.Add(Cansel);
            Controls.Add(Add);
            Name = "AddContract";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}