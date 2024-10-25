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
    public partial class Form5 : Form
    {
        private MySqlConnection conn;
        public delegate void DataInsertedHandler();
        public event DataInsertedHandler DataInserted;
        public Form5(MySqlConnection connect)
        {
            InitializeComponent();
            conn = connect;
        }

        private void PictureboxBook_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4(conn);
            form4.ShowDialog();
            this.Show();
        }

        private void Dashboardbook_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Show();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void ViewTransactions2_Click(object sender, EventArgs e)
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
                                          "t.datereturned " +
                                          "FROM transactions t " +
                                          "JOIN books b ON b.book_id = t.book_id " +
                                          "JOIN members m ON m.member_id = t.member_id;";

                MySqlDataAdapter da = new MySqlDataAdapter(TransactionQuery, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                returningBookContent.DataSource = dt;
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

        private void returningBookContent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void updateReturningBook_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9(conn);
            form9.ShowDialog();
        }
    }
}

