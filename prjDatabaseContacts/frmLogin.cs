using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjDatabaseContacts
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
               using (SqlConnection connection = new SqlConnection(Connection.conn))
                {
                    connection.Open();
                    String sql = "SELECT * FROM tblUsers where Username = '"+txtUsername.Text+"' " +
                        "AND Password ='"+txtPassword.Text+"' ;";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Success");
                        frmContactForms c = new frmContactForms(txtUsername.Text);
                        txtPassword.Text = "";
                        txtUsername.Text = "";
                        this.Hide();
                        c.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Credentials");
                    }
                    reader.Close();
                    command.Dispose();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }
    }
}
