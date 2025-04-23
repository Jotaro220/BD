

using System.Data;
using System.Data.OleDb;

namespace BD
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        protected OleDbConnection connection;
        protected string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path to acces BD}Jet OLEDB:Create System Database=true;Jet OLEDB:System database={path to System.mdw}";

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>


    }
    #endregion
}