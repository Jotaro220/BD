using System.Data.OleDb;

namespace BD.AddForms
{
    public partial class AddWorker : Form1
    {
        private OleDbConnection connection;
        public AddWorker()
        {
            InitializeComponent();

        }

        public void AddEntryClik(object sender, EventArgs e)
        {
            if (Date.Text == null || Adress.Text == null || Patronymic.Text == null || Family.Text == null || NameWorker.Text == null
                || Surname.Text == null || Gender.Text == null)
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
                    command.CommandText = "INSERT INTO Сотрудник(Фамилия, Имя, Отчество, Адрес, Пол, [Семейное положение], [Дата рождения])" +
                        " VALUES (@Surname, @NameWorker, @Patronymic, @Adress, @Gender, @Family, @Date)";
                    command.Parameters.AddWithValue("@Surname", Surname.Text);
                    command.Parameters.AddWithValue("@NameWorker", NameWorker.Text);
                    command.Parameters.AddWithValue("@Patronymic", Patronymic.Text);
                    command.Parameters.AddWithValue("@Adress", Adress.Text);
                    command.Parameters.AddWithValue("@Gender", Gender.Text);
                    command.Parameters.AddWithValue("@Family", Family.Text);
                    command.Parameters.AddWithValue("@Date", Date.Text);
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
        private Label label6;
        private Label label5;
        private TextBox Adress;
        private Label label4;
        private TextBox Patronymic;
        private Label label3;
        private Label label2;
        private TextBox NameWorker;
        private Label label1;
        private Label label7;
        private TextBox Surname;
        private TextBox Date;
        private ComboBox Gender;
        private ComboBox Family;
        private Button Cansel;

        private void InitializeComponent()
        {
            Add = new Button();
            Cansel = new Button();
            label6 = new Label();
            label5 = new Label();
            Adress = new TextBox();
            label4 = new Label();
            Patronymic = new TextBox();
            label3 = new Label();
            label2 = new Label();
            NameWorker = new TextBox();
            label1 = new Label();
            label7 = new Label();
            Surname = new TextBox();
            Date = new TextBox();
            Gender = new ComboBox();
            Family = new ComboBox();
            SuspendLayout();
            // 
            // Add
            // 
            Add.Location = new Point(10, 570);
            Add.Name = "Add";
            Add.Size = new Size(147, 39);
            Add.TabIndex = 0;
            Add.Text = "Добавить";
            Add.UseVisualStyleBackColor = true;
            Add.Click += AddEntryClik;
            // 
            // Cansel
            // 
            Cansel.Location = new Point(187, 570);
            Cansel.Name = "Cansel";
            Cansel.Size = new Size(147, 39);
            Cansel.TabIndex = 1;
            Cansel.Text = "Отмена";
            Cansel.UseVisualStyleBackColor = true;
            Cansel.Click += CanselClick;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(8, 408);
            label6.Name = "label6";
            label6.Size = new Size(204, 25);
            label6.TabIndex = 24;
            label6.Text = "Семейное положение";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(10, 333);
            label5.Name = "label5";
            label5.Size = new Size(47, 25);
            label5.TabIndex = 22;
            label5.Text = "Пол";
            // 
            // Adress
            // 
            Adress.Location = new Point(8, 293);
            Adress.Name = "Adress";
            Adress.Size = new Size(310, 23);
            Adress.TabIndex = 21;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(8, 253);
            label4.Name = "label4";
            label4.Size = new Size(64, 25);
            label4.TabIndex = 20;
            label4.Text = "Адрес";
            // 
            // Patronymic
            // 
            Patronymic.Location = new Point(8, 212);
            Patronymic.Name = "Patronymic";
            Patronymic.Size = new Size(310, 23);
            Patronymic.TabIndex = 19;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(8, 172);
            label3.Name = "label3";
            label3.Size = new Size(93, 25);
            label3.TabIndex = 18;
            label3.Text = "Отчество";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(10, 20);
            label2.Name = "label2";
            label2.Size = new Size(91, 25);
            label2.TabIndex = 16;
            label2.Text = "Фамилия";
            // 
            // NameWorker
            // 
            NameWorker.Location = new Point(8, 137);
            NameWorker.Name = "NameWorker";
            NameWorker.Size = new Size(310, 23);
            NameWorker.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(8, 97);
            label1.Name = "label1";
            label1.Size = new Size(49, 25);
            label1.TabIndex = 14;
            label1.Text = "Имя";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(8, 482);
            label7.Name = "label7";
            label7.Size = new Size(146, 25);
            label7.TabIndex = 26;
            label7.Text = "Дата рождения";
            // 
            // Surname
            // 
            Surname.Location = new Point(10, 57);
            Surname.Name = "Surname";
            Surname.Size = new Size(310, 23);
            Surname.TabIndex = 28;
            // 
            // Date
            // 
            Date.Location = new Point(8, 521);
            Date.Name = "Date";
            Date.Size = new Size(312, 23);
            Date.TabIndex = 29;
            // 
            // Gender
            // 
            Gender.DropDownStyle = ComboBoxStyle.DropDownList;
            Gender.FormattingEnabled = true;
            Gender.Items.AddRange(new object[] { "Муж.", "Жен." });
            Gender.Location = new Point(8, 360);
            Gender.Name = "Gender";
            Gender.Size = new Size(312, 23);
            Gender.TabIndex = 30;
            // 
            // Family
            // 
            Family.DropDownStyle = ComboBoxStyle.DropDownList;
            Family.FormattingEnabled = true;
            Family.Items.AddRange(new object[] { "Женат", "Замужем", "Не женат", "Не замужем" });
            Family.Location = new Point(8, 440);
            Family.Name = "Family";
            Family.Size = new Size(312, 23);
            Family.TabIndex = 31;
            // 
            // AddWorker
            // 
            ClientSize = new Size(346, 625);
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
            Controls.Add(Cansel);
            Controls.Add(Add);
            Name = "AddWorker";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}