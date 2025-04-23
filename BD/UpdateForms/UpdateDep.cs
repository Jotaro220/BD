using System.Data.OleDb;

namespace BD.UpdateForms
{
    public class UpdateDep : Form1
    {
        private OleDbConnection connection;
        public UpdateDep()
        {

            InitializeComponent();
            LoadShopWorker();
            LoadTableIntoComboBox();
        }

        private void LoadShopWorker()
        {
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

        private void LoadTableIntoComboBox()
        {
            connection.Open();
            try
            {

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT * FROM [Отдел]";
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
                command.CommandText = $"SELECT [Название отдела], [Номер магазина], [Табельный номер заведующего] FROM Отдел WHERE [Номер Отдела]={Number.Text}";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    NameDep.Text = reader[0].ToString();
                    NumberShop.Text = reader[1].ToString();
                    NumberWorker.Text = reader[2].ToString();
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
            if (NumberShop.Text == null || NameDep.Text == null || NumberWorker.Text == null || Number.Text == null)
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
                    command.CommandText = "Update Отдел SET [Название отдела]=@NameDep,[Номер магазина]=@NumberShop, " +
                        "[Табельный номер заведующего]=@NumberWorker WHERE [Номер отдела]=@Number";
                    command.Parameters.AddWithValue("@NameDep", NameDep.Text);
                    command.Parameters.AddWithValue("@NumberShop", NumberShop.Text);
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
            Number = new ComboBox();
            Cansel = new Button();
            Upload = new Button();
            NumberWorker = new ComboBox();
            label3 = new Label();
            NumberShop = new ComboBox();
            label2 = new Label();
            NameDep = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // Number
            // 
            Number.DropDownStyle = ComboBoxStyle.DropDownList;
            Number.FormattingEnabled = true;
            Number.Location = new Point(12, 12);
            Number.Name = "Number";
            Number.Size = new Size(308, 23);
            Number.TabIndex = 15;
            // 
            // Cansel
            // 
            Cansel.Location = new Point(173, 279);
            Cansel.Name = "Cansel";
            Cansel.Size = new Size(147, 39);
            Cansel.TabIndex = 14;
            Cansel.Text = "Отмена";
            Cansel.UseVisualStyleBackColor = true;
            Cansel.Click += CanselClick;
            // 
            // Upload
            // 
            Upload.Location = new Point(12, 279);
            Upload.Name = "Upload";
            Upload.Size = new Size(147, 39);
            Upload.TabIndex = 13;
            Upload.Text = "Обнавить";
            Upload.UseVisualStyleBackColor = true;
            Upload.Click += UpdateEntryClik;
            // 
            // NumberWorker
            // 
            NumberWorker.DropDownStyle = ComboBoxStyle.DropDownList;
            NumberWorker.FormattingEnabled = true;
            NumberWorker.Location = new Point(12, 228);
            NumberWorker.Name = "NumberWorker";
            NumberWorker.Size = new Size(308, 23);
            NumberWorker.TabIndex = 21;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 189);
            label3.Name = "label3";
            label3.Size = new Size(290, 25);
            label3.TabIndex = 20;
            label3.Text = "Табельный номер заведующего";
            // 
            // NumberShop
            // 
            NumberShop.DropDownStyle = ComboBoxStyle.DropDownList;
            NumberShop.FormattingEnabled = true;
            NumberShop.Location = new Point(12, 149);
            NumberShop.Name = "NumberShop";
            NumberShop.Size = new Size(308, 23);
            NumberShop.TabIndex = 19;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 111);
            label2.Name = "label2";
            label2.Size = new Size(155, 25);
            label2.TabIndex = 18;
            label2.Text = "Номер магазина";
            // 
            // NameDep
            // 
            NameDep.Location = new Point(14, 76);
            NameDep.Name = "NameDep";
            NameDep.Size = new Size(306, 23);
            NameDep.TabIndex = 17;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 48);
            label1.Name = "label1";
            label1.Size = new Size(159, 25);
            label1.TabIndex = 16;
            label1.Text = "Название отдела";
            // 
            // UpdateDep
            // 
            ClientSize = new Size(336, 349);
            Controls.Add(NumberWorker);
            Controls.Add(label3);
            Controls.Add(NumberShop);
            Controls.Add(label2);
            Controls.Add(NameDep);
            Controls.Add(label1);
            Controls.Add(Number);
            Controls.Add(Cansel);
            Controls.Add(Upload);
            Name = "UpdateDep";
            ResumeLayout(false);
            PerformLayout();
        }

        private ComboBox NumberWorker;
        private Label label3;
        private ComboBox NumberShop;
        private Label label2;
        private TextBox NameDep;
        protected ComboBox Number;
        protected Button Cansel;
        protected Button Upload;
        private Label label1;
    }
}