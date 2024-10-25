using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
    public partial class Form4 : Form
    {
        private MySqlConnection conn;
        public delegate void DataInsertedHandler();
        public event DataInsertedHandler DataInserted;
        public Form4(MySqlConnection connect)
        {
            InitializeComponent();
            conn = connect;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Show();
        }

        private void ReturningBook_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 f5 = new Form5(conn);
            f5.ShowDialog();
            this.Show();
        }

        private void updateBook_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f6 = new Form6(conn);
            f6.ShowDialog();
            this.Show();

        }

        private void DeleteBook_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f7 = new Form7(conn);
            f7.ShowDialog();
            this.Show();

        }

        private void ViewTransactions_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Query to display transactions with book availability status as "Available" or "Unavailable"
                string TransactionQuery = "SELECT b.book_id, " +
                                          "CASE WHEN b.availability = 1 THEN 'Available' ELSE 'Unavailable' END AS availability, " +
                                          "b.title, " +
                                          "b.yearpublished, " +
                                          "m.member_id, " +
                                          "m.first_name, " +
                                          "m.last_name, " +
                                          "t.transaction_id, " +
                                          "t.dateborrowed " +
                                          "FROM transactions t " +
                                          "JOIN books b ON b.book_id = t.book_id " +
                                          "JOIN members m ON m.member_id = t.member_id;";

                MySqlDataAdapter da = new MySqlDataAdapter(TransactionQuery, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataTransactions.DataSource = dt;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Unable to retrieve transaction data: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        private void dataTransactions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CreateTransaction_Click(object sender, EventArgs e)
        {
           Form8 form8 = new Form8(conn);   
           form8.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
