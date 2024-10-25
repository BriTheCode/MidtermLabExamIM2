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
    public partial class Form8 : Form
    {
        private MySqlConnection conn;
        public delegate void DataInsertedHandler();
        public event DataInsertedHandler DataInserted;
        public Form8(MySqlConnection connect)
        {
            InitializeComponent();
            conn = connect;
        }

        private void IdBook_TextChanged(object sender, EventArgs e)
        {

        }

        private void IdMember_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void CreateTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Query to display transactions with book availability status as "Available" or "Unavailable"
                string TransactionQuery = "INSERT INTO transactions(book_id,member_id,dateborrowed) VALUES (@IdBook,@IdMember,@borrowedate)";
                MySqlCommand cmd = new MySqlCommand(TransactionQuery, conn);
                cmd.Parameters.AddWithValue("@IdBook", @IdBook.Text);
                cmd.Parameters.AddWithValue("@IdMember", IdMember.Text);
                cmd.Parameters.AddWithValue("@borrowedate", DateBorrowed.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Transaction Successfully Added!");
                DataInserted?.Invoke();
                this.Close();
            }

            catch (MySqlException ex)
            {
                MessageBox.Show("Error Create Transaction: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
    

