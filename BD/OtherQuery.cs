using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BD
{
    public class OtherQuery : Form1
    {
        string paramtr;
        string table;
        public OtherQuery(string messege, string parm, string tableName)
        {
            InitializeComponent();
            Messege.Text = messege;
            paramtr = parm;
            table = tableName;
        }

        public void performQueryClik(object sender, EventArgs e)
        {
            if (Parms.Text == null)
            {
                MessageBox.Show("Поле параметра должно быть заполнено");
                return;
            }
            else if (!Checks.IntCheck(Parms.Text))
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
                    command.Parameters.AddWithValue(paramtr, Parms.Text);//
                    command.CommandText = $"EXEC [{table}]";
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка в запросе с параметром: " + ex.Message);
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
            Messege = new TextBox();
            Parms = new TextBox();
            Ok = new Button();
            Cansele = new Button();
            SuspendLayout();
            // 
            // Messege
            // 
            Messege.Location = new Point(12, 12);
            Messege.Name = "Messege";
            Messege.ReadOnly = true;
            Messege.Size = new Size(328, 23);
            Messege.TabIndex = 0;
            // 
            // Parms
            // 
            Parms.Location = new Point(12, 63);
            Parms.Name = "Parms";
            Parms.Size = new Size(328, 23);
            Parms.TabIndex = 1;
            // 
            // Ok
            // 
            Ok.Location = new Point(18, 108);
            Ok.Name = "Ok";
            Ok.Size = new Size(124, 45);
            Ok.TabIndex = 2;
            Ok.Text = "Выполнить";
            Ok.UseVisualStyleBackColor = true;
            Ok.Click += performQueryClik;
            // 
            // Cansele
            // 
            Cansele.Location = new Point(216, 108);
            Cansele.Name = "Cansele";
            Cansele.Size = new Size(124, 45);
            Cansele.TabIndex = 3;
            Cansele.Text = "Отмена";
            Cansele.UseVisualStyleBackColor = true;
            Cansele.Click += CanselClick;
            // 
            // ParamsQuery
            // 
            ClientSize = new Size(362, 175);
            Controls.Add(Cansele);
            Controls.Add(Ok);
            Controls.Add(Parms);
            Controls.Add(Messege);
            Name = "ParamsQuery";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox Messege;
        private Button Ok;
        private Button Cansele;
        private TextBox Parms;
    }
}