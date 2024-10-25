using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DelaCruz_Midterm_Exam
{
    public partial class Form3 : Form
    {
        private MySqlConnection conn;
        public delegate void DataInsertedHandler();
        public event DataInsertedHandler DataInserted;
        public Form3(MySqlConnection connection)
        {
            InitializeComponent();
            conn = connection;
        }

        private void MemberFirsName_TextChanged(object sender, EventArgs e)
        {

        }
        private void MemberLast_TextChanged(object sender, EventArgs e)
        {

        }
        private void MemberEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddingMember_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string insertQuery = "INSERT INTO members (first_name,last_name,email) " +
                    "VALUES (@memberFirstName, @memberLastName, @memberEmail)";
                MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@memberFirstName", MemberFirsName.Text);
                cmd.Parameters.AddWithValue("@memberLastName", MemberLast.Text);
                cmd.Parameters.AddWithValue("@memberEmail", MemberEmail.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Member has been Added successfully!");
                DataInserted?.Invoke();
                this.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error Adding Member: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        

    }
}

