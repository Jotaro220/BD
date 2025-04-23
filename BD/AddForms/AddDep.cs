using System.Data.OleDb;

namespace BD.AddForms
{
    public partial class AddDep : Form1
    {
        private OleDbConnection connection;
        public AddDep()
        {
            InitializeComponent();

            connection = new OleDbConnection(connectionString);
            connection.Open();

            try
            {
                OleDbCommand command1 = new OleDbCommand();
                command1.Connection = connection;
                command1.CommandText = "SELECT [Номер магазина] FROM Магазин";
                OleDbDataReader reader = command1.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        NumberShop.Items.Add(reader[0].ToString());
                    }
                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                command2.CommandText = "SELECT [Табельный номер сотрудника] FROM Сотрудник";
                reader = command2.ExecuteReader();
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
            if (NumberShop.Text == null || NameDep.Text == null || NumberWorker.Text == null)
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
                    command.CommandText = "INSERT INTO Отдел([Название отдела],[Номер магазина], [Табельный номер заведующего]) VALUES (@NameDep, @NumberShop, @NumberWorker)";
                    command.Parameters.AddWithValue("@NameDep", NameDep.Text);
                    command.Parameters.AddWithValue("@NumberShop", NumberShop.Text);
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
        private TextBox NameDep;
        private Label label2;
        private ComboBox NumberShop;
        private ComboBox NumberWorker;
        private Label label3;
        private Label label1;

        private void InitializeComponent()
        {
            Add = new Button();
            Cansel = new Button();
            label1 = new Label();
            NameDep = new TextBox();
            label2 = new Label();
            NumberShop = new ComboBox();
            NumberWorker = new ComboBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // Add
            // 
            Add.Location = new Point(10, 298);
            Add.Name = "Add";
            Add.Size = new Size(147, 39);
            Add.TabIndex = 0;
            Add.Text = "Добавить";
            Add.UseVisualStyleBackColor = true;
            Add.Click += AddEntryClik;
            // 
            // Cansel
            // 
            Cansel.Location = new Point(187, 298);
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
            label1.Location = new Point(12, 27);
            label1.Name = "label1";
            label1.Size = new Size(159, 25);
            label1.TabIndex = 2;
            label1.Text = "Название отдела";
            // 
            // NameDep
            // 
            NameDep.Location = new Point(14, 55);
            NameDep.Name = "NameDep";
            NameDep.Size = new Size(306, 23);
            NameDep.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 90);
            label2.Name = "label2";
            label2.Size = new Size(155, 25);
            label2.TabIndex = 4;
            label2.Text = "Номер магазина";
            // 
            // NumberShop
            // 
            NumberShop.DropDownStyle = ComboBoxStyle.DropDownList;
            NumberShop.FormattingEnabled = true;
            NumberShop.Location = new Point(12, 128);
            NumberShop.Name = "NumberShop";
            NumberShop.Size = new Size(308, 23);
            NumberShop.TabIndex = 5;
            // 
            // NumberWorker
            // 
            NumberWorker.DropDownStyle = ComboBoxStyle.DropDownList;
            NumberWorker.FormattingEnabled = true;
            NumberWorker.Location = new Point(12, 207);
            NumberWorker.Name = "NumberWorker";
            NumberWorker.Size = new Size(308, 23);
            NumberWorker.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 168);
            label3.Name = "label3";
            label3.Size = new Size(290, 25);
            label3.TabIndex = 6;
            label3.Text = "Табельный номер заведующего";
            // 
            // AddDep
            // 
            ClientSize = new Size(346, 367);
            Controls.Add(NumberWorker);
            Controls.Add(label3);
            Controls.Add(NumberShop);
            Controls.Add(label2);
            Controls.Add(NameDep);
            Controls.Add(label1);
            Controls.Add(Cansel);
            Controls.Add(Add);
            Name = "AddDep";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}