using System.Data.OleDb;

namespace BD.UpdateForms
{
    public class UpdateWorker : Form1
    {
        public UpdateWorker()
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
                command.CommandText = $"SELECT [Табельный номер сотрудника] FROM Сотрудник";
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
                command.CommandText = $"SELECT Фамилия,Имя,Отчество, Адрес, Пол, [Семейное положение], [Дата рождения]" +
                    $" FROM Сотрудник WHERE [Табельный номер сотрудника]={Number.Text}";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Surname.Text = reader[0].ToString();
                    NameWorker.Text = reader[1].ToString();
                    Patronymic.Text = reader[2].ToString();
                    Adress.Text = reader[3].ToString();
                    Gender.Text = reader[4].ToString();
                    Family.Text = reader[5].ToString();
                    Date.Text = reader[6].ToString();
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
            if (Date.Text == null || Adress.Text == null || Patronymic.Text == null || Family.Text == null || NameWorker.Text == null
                || Surname.Text == null || Gender.Text == null || Number.Text == null)
            {
                MessageBox.Show("Все поля должны быть заполненны!");
                return;
            }
            else if (!Checks.DateCheck(Date.Text))
            {
                MessageBox.Show("Дата введена не верно!");
                return;
            }
            else if (!Checks.NameCheck(NameWorker.Text) || !Checks.NameCheck(Surname.Text) || !Checks.NameCheck(Patronymic.Text))
            {
                MessageBox.Show("Для ФИО только буквы!");
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
                    command.CommandText = "Update Сотрудник SET Фамилия=@Surname, Имя=@NameWorker, Отчество=@Patronymic," +
                        " Адрес=@Adress, Пол=@Gender, [Семейное положение]=@Family, [Дата рождения]=@Date" +
                        " WHERE [Табельный номер сотрудника]=@Number";
                    command.Parameters.AddWithValue("@Surname", Surname.Text);
                    command.Parameters.AddWithValue("@NameWorker", NameWorker.Text);
                    command.Parameters.AddWithValue("@Patronymic", Patronymic.Text);
                    command.Parameters.AddWithValue("@Adress", Adress.Text);
                    command.Parameters.AddWithValue("@Gender", Gender.Text);
                    command.Parameters.AddWithValue("@Family", Family.Text);
                    command.Parameters.AddWithValue("@Date", Date.Text);
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
            Family = new ComboBox();
            Gender = new ComboBox();
            Date = new TextBox();
            Surname = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            Adress = new TextBox();
            label4 = new Label();
            Patronymic = new TextBox();
            label3 = new Label();
            label2 = new Label();
            NameWorker = new TextBox();
            label1 = new Label();
            Number = new ComboBox();
            Cansel = new Button();
            Upload = new Button();
            SuspendLayout();
            // 
            // Family
            // 
            Family.DropDownStyle = ComboBoxStyle.DropDownList;
            Family.FormattingEnabled = true;
            Family.Items.AddRange(new object[] { "Женат", "Замужем", "Не женат", "Не замужем" });
            Family.Location = new Point(10, 475);
            Family.Name = "Family";
            Family.Size = new Size(312, 23);
            Family.TabIndex = 45;
            // 
            // Gender
            // 
            Gender.DropDownStyle = ComboBoxStyle.DropDownList;
            Gender.FormattingEnabled = true;
            Gender.Items.AddRange(new object[] { "Муж.", "Жен." });
            Gender.Location = new Point(10, 395);
            Gender.Name = "Gender";
            Gender.Size = new Size(312, 23);
            Gender.TabIndex = 44;
            // 
            // Date
            // 
            Date.Location = new Point(10, 556);
            Date.Name = "Date";
            Date.Size = new Size(312, 23);
            Date.TabIndex = 43;
            // 
            // Surname
            // 
            Surname.Location = new Point(12, 92);
            Surname.Name = "Surname";
            Surname.Size = new Size(310, 23);
            Surname.TabIndex = 42;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(10, 517);
            label7.Name = "label7";
            label7.Size = new Size(146, 25);
            label7.TabIndex = 41;
            label7.Text = "Дата рождения";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(10, 443);
            label6.Name = "label6";
            label6.Size = new Size(204, 25);
            label6.TabIndex = 40;
            label6.Text = "Семейное положение";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(12, 368);
            label5.Name = "label5";
            label5.Size = new Size(47, 25);
            label5.TabIndex = 39;
            label5.Text = "Пол";
            // 
            // Adress
            // 
            Adress.Location = new Point(10, 328);
            Adress.Name = "Adress";
            Adress.Size = new Size(310, 23);
            Adress.TabIndex = 38;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(10, 288);
            label4.Name = "label4";
            label4.Size = new Size(64, 25);
            label4.TabIndex = 37;
            label4.Text = "Адрес";
            // 
            // Patronymic
            // 
            Patronymic.Location = new Point(10, 247);
            Patronymic.Name = "Patronymic";
            Patronymic.Size = new Size(310, 23);
            Patronymic.TabIndex = 36;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(10, 207);
            label3.Name = "label3";
            label3.Size = new Size(93, 25);
            label3.TabIndex = 35;
            label3.Text = "Отчество";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 55);
            label2.Name = "label2";
            label2.Size = new Size(91, 25);
            label2.TabIndex = 34;
            label2.Text = "Фамилия";
            // 
            // NameWorker
            // 
            NameWorker.Location = new Point(10, 172);
            NameWorker.Name = "NameWorker";
            NameWorker.Size = new Size(310, 23);
            NameWorker.TabIndex = 33;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(10, 132);
            label1.Name = "label1";
            label1.Size = new Size(49, 25);
            label1.TabIndex = 32;
            label1.Text = "Имя";
            // 
            // Number
            // 
            Number.DropDownStyle = ComboBoxStyle.DropDownList;
            Number.FormattingEnabled = true;
            Number.Location = new Point(14, 12);
            Number.Name = "Number";
            Number.Size = new Size(308, 23);
            Number.TabIndex = 48;
            // 
            // Cansel
            // 
            Cansel.Location = new Point(173, 602);
            Cansel.Name = "Cansel";
            Cansel.Size = new Size(147, 39);
            Cansel.TabIndex = 47;
            Cansel.Text = "Отмена";
            Cansel.UseVisualStyleBackColor = true;
            Cansel.Click += CanselClick;
            // 
            // Upload
            // 
            Upload.Location = new Point(9, 602);
            Upload.Name = "Upload";
            Upload.Size = new Size(147, 39);
            Upload.TabIndex = 46;
            Upload.Text = "Обнавить";
            Upload.UseVisualStyleBackColor = true;
            Upload.Click += UpdateEntryClik;
            // 
            // UpdateWorker
            // 
            ClientSize = new Size(340, 653);
            Controls.Add(Number);
            Controls.Add(Cansel);
            Controls.Add(Upload);
            Controls.Add(Family);
            Controls.Add(Gender);
            Controls.Add(Date);
            Controls.Add(Surname);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(Adress);
            Controls.Add(label4);
            Controls.Add(Patronymic);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(NameWorker);
            Controls.Add(label1);
            Name = "UpdateWorker";
            ResumeLayout(false);
            PerformLayout();
        }

        private ComboBox Family;
        private ComboBox Gender;
        private TextBox Date;
        private TextBox Surname;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox Adress;
        private Label label4;
        private TextBox Patronymic;
        private Label label3;
        private Label label2;
        private TextBox NameWorker;
        protected ComboBox Number;
        protected Button Cansel;
        protected Button Upload;
        private Label label1;

    }
}