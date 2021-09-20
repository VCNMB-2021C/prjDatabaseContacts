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
        static String UserLogin = "";
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
                    connection.Open();
                    String sql = "SELECT * FROM tblContacts where Username = '" + UserLogin + "';";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                Contacts temp = new Contacts(reader.GetInt32(0), reader.GetString(1),
                                reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                                contacts.Add(temp);

                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            refreshUI();
        }



        private void refreshUI()
        {
            txtEmail.Clear();
            txtName.Clear();
            txtPhoneNumber.Clear();
            txtSurname.Clear();

            contacts.Sort();
            lvOutput.Items.Clear();
            foreach( Contacts contact in contacts)
            {
                lvOutput.Items.Add(contact.FirstName + " " + contact.Surname);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Connection.conn))
                {
                    SqlCommand command = new
                        SqlCommand("INSERT INTO tblContacts " +
                                   "VALUES(@Name, @Surname, @Phonenumber,@Email,@UserName) ;" +
                                   "Select SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@Name", txtName.Text);
                    command.Parameters.AddWithValue("@Surname", txtSurname.Text);
                    command.Parameters.AddWithValue("@Phonenumber", txtPhoneNumber.Text);
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.Parameters.AddWithValue("@UserName", UserLogin);
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.InsertCommand = command;

                    int id = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());
                    MessageBox.Show("User has been added to the database " + id);
                    adapter.Dispose();
                    Contacts temp = new Contacts(id, txtName.Text, txtSurname.Text, txtPhoneNumber.Text, txtEmail.Text, UserLogin);
                    contacts.Add(temp);
                    refreshUI();

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error Connecting to the Database", "Connection Error");
            }
        }
    }
}
