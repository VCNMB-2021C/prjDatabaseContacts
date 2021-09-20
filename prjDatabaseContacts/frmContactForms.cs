using System;
using System.Collections;
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
    public partial class frmContactForms : Form
    {
        static String UserLogin ="";
        ArrayList contacts = new ArrayList();

        public frmContactForms(string text)
        {
            InitializeComponent();
            UserLogin = text;
        }

        private void frmContactForms_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Connection.conn))
                {
 
                    String sql = "SELECT * FROM tblContacts where Username = '"+ UserLogin + "';";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} ‐ {1} {2}",
                                reader.GetInt32(0), reader.GetString(1), reader.GetString(2));



                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }

        
    }
}
