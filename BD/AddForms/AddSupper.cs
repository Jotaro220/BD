using System.Data.OleDb;

namespace BD.AddForms
{
    public partial class AddSupper : Form1
    {
        private OleDbConnection connection;
        public AddSupper()
        {
            InitializeComponent();
        }

        public void AddEntryClik(object sender, EventArgs e)
        {
            if (Adress.Text == null || NameSupper.Text == null)
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
                    command.CommandText = "INSERT INTO Поставщик([Название поставщика], [Адрес поставщика]) VALUES (@NameSupper, @Adress)";
                    command.Parameters.AddWithValue("@NameSupper", NameSupper.Text);
                    command.Parameters.AddWithValue("@Adress", Adress.Text);
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
        private TextBox NameSupper;
        private Label label2;
        private TextBox Adress;
        private Label label1;

        private void InitializeComponent()
        {
            Add = new Button();
            Cansel = new Button();
            label1 = new Label();
            NameSupper = new TextBox();
            label2 = new Label();
            Adress = new TextBox();
            SuspendLayout();
            // 
            // Add
            // 
            Add.Location = new Point(10, 164);
            Add.Name = "Add";
            Add.Size = new Size(147, 39);
            Add.TabIndex = 0;
            Add.Text = "Добавить";
            Add.UseVisualStyleBackColor = true;
            Add.Click += AddEntryClik;
            // 
            // Cansel
            // 
            Cansel.Location = new Point(173, 164);
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
            label1.Location = new Point(12, 23);
            label1.Name = "label1";
            label1.Size = new Size(205, 25);
            label1.TabIndex = 2;
            label1.Text = "Название поставщика";
            // 
            // NameSupper
            // 
            NameSupper.Location = new Point(14, 51);
            NameSupper.Name = "NameSupper";
            NameSupper.Size = new Size(306, 23);
            NameSupper.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 86);
            label2.Name = "label2";
            label2.Size = new Size(174, 25);
            label2.TabIndex = 4;
            label2.Text = "Адрес поставщика";
            // 
            // Adress
            // 
            Adress.Location = new Point(12, 114);
            Adress.Name = "Adress";
            Adress.Size = new Size(306, 23);
            Adress.TabIndex = 5;
            // 
            // AddSupper
            // 
            ClientSize = new Size(346, 252);
            Controls.Add(Adress);
            Controls.Add(label2);
            Controls.Add(NameSupper);
            Controls.Add(label1);
            Controls.Add(Cansel);
            Controls.Add(Add);
            Name = "AddSupper";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}