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
    public partial class Form2 : Form
    {
        private MySqlConnection conn;
        public delegate void DataInsertedHandler();
        public event DataInsertedHandler DataInserted;
        public Form2(MySqlConnection connection)
        {
            InitializeComponent();
            conn = connection;
        }

        private void BookTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void BookAuthor_TextChanged(object sender, EventArgs e)
        {

        }

        private void BookGenre_TextChanged(object sender, EventArgs e)
        {

        }

        private void BookYearPublished_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddingBook_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string insertQuery = "INSERT INTO books (title, author,genre,yearpublished) " +
                    "VALUES (@bookTitle, @bookAuthor, @bookGenre, @bookYearPublished)";
                MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@bookTitle",  BookTitle.Text);  
                cmd.Parameters.AddWithValue("@bookAuthor", BookAuthor.Text);
                cmd.Parameters.AddWithValue("@bookGenre", BookGenre.Text); 
                cmd.Parameters.AddWithValue("@bookYearPublished", BookYearPublished.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Book has been Added successfully!");
                DataInserted?.Invoke();
                this.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error Adding Book: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
