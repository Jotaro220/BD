using BD.AddForms;
using BD.UpdateForms;
using System.Data.OleDb;
using System.Reflection.Metadata;

namespace BD
{
    public class OpenForm
    {
        public static void OpenAddContract()
        {
            AddContract addForm = new AddContract();
            addForm.ShowDialog();
        }
        public static void OpenAddDep()
        {
            AddDep addForm = new AddDep();
            addForm.ShowDialog();
        }
        public static void OpenAddProduct()
        {
            AddProduct addForm = new AddProduct();
            addForm.ShowDialog();
        }
        public static void OpenAddShop()
        {
            AddShop addForm = new AddShop();
            addForm.ShowDialog();
        }
        public static void OpenAddSupper()
        {
            AddSupper addForm = new AddSupper();
            addForm.ShowDialog();
        }
        public static void OpenAddWorker()
        {
            AddWorker addForm = new AddWorker();
            addForm.ShowDialog();
        }

        public static void OpenDelete(string TableName)
        {
            DeleteForm addForm = new DeleteForm(TableName);
            addForm.ShowDialog();
        }

        public static void OpenUpdateContract()
        {
            UpdateContract addForm = new UpdateContract();
            addForm.ShowDialog();
        }
        public static void OpenUpdateDep()
        {
            UpdateDep addForm = new UpdateDep();
            addForm.ShowDialog();
        }
        public static void OpenUpdateProduct()
        {
            UpdateProduct addForm = new UpdateProduct();
            addForm.ShowDialog();
        }
        public static void OpenUpdateShop()
        {
            UpdateShop addForm = new UpdateShop();
            addForm.ShowDialog();
        }
        public static void OpenUpdateSupper()
        {
            UpdateSupper addForm = new UpdateSupper();
            addForm.ShowDialog();
        }
        public static void OpenUpdateWorker()
        {
            UpdateWorker addForm = new UpdateWorker();
            addForm.ShowDialog();
        }

        public static void OpenParamsQuery(string messeg,string parms, string tableName)
        {
            ParamsQuery addForm = new ParamsQuery(messeg,parms,tableName);
            addForm.ShowDialog();
        }
        public static void OpenOtherQuery(string messeg, string parms, string tableName)
        {
            OtherQuery addForm = new OtherQuery(messeg, parms, tableName);
            addForm.ShowDialog();
        }

        public static void OpenDeleteQuery(string connectionString, string tableName)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();

            try
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"EXEC [{tableName}]";
                int deleteCount = command.ExecuteNonQuery();
                MessageBox.Show(deleteCount + " товаров удалено");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в запросе на удаление: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}