using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace DelaCruz_Midterm_Exam
{
    public partial class Form1 : Form
    {
        // MySQL connection string
        static string connectionString = "Server=localhost;Database=librarydb;User ID=root;Password=;";
        static MySqlConnection conn = new MySqlConnection(connectionString);
        public Form1()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the connection
                conn.Open();
                MessageBox.Show("Connection to the database was successful!");
            }
            catch (MySqlException ex)
            {
                // Display detailed error message
                MessageBox.Show("Error connecting to the database: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed after the operation
                conn.Close();
            }
        }

        private void AddBook_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(conn);
            f2.ShowDialog();
        }

        private void AddMember_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(conn);
            f3.ShowDialog();
        }

        private void ViewBook_Click(object sender, EventArgs e)
        { 
            // Fetch books from the database and display in DataGridView
            try
            {
                conn.Open();

                // SQL query to fetch all books
                string query = "SELECT * FROM books";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                // Fill a DataTable with the result of the query
                DataTable booksTable = new DataTable();
                adapter.Fill(booksTable);

                // Display the data in the DataGridView
                DataContent.DataSource = booksTable;

                MessageBox.Show("Books loaded successfully!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error fetching books: " + ex.Message);
            }
            finally
            {
                conn.Close();  // Ensure connection is closed
            }

        }

        private void ViewMember_Click(object sender, EventArgs e)
        {
            // Fetch books from the database and display in DataGridView
            try
            {
                conn.Open();

                // SQL query to fetch all books
                string query = "SELECT * FROM members";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                // Fill a DataTable with the result of the query
                DataTable booksTable = new DataTable();
                adapter.Fill(booksTable);

                // Display the data in the DataGridView
                DataContent.DataSource = booksTable;

                MessageBox.Show("Members loaded successfully!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error fetching books: " + ex.Message);
            }
            finally
            {
                conn.Close();  // Ensure connection is closed
            }

        }


        private void DataContent_CellContentClick(object sender, DataGridViewCellEventArgs e)

        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        

        private void PictureboxBook_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4(conn);
            f4.ShowDialog();
            this.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void ReturningBook_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 f5 = new Form5(conn); 
            f5.ShowDialog();
            this.Show();
        }
    }
}



