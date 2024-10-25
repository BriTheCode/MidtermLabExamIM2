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
    public partial class Form9 : Form
    {
        private MySqlConnection conn;
        public delegate void DataInsertedHandler();
        public event DataInsertedHandler DataInserted;
        private DateTime returnDate;
        public Form9(MySqlConnection connect)
        {
            InitializeComponent();
            conn = connect;
        }

        private void IdReturnBook_TextChanged(object sender, EventArgs e)
        {

        }

        private void IdReturning_TextChanged(object sender, EventArgs e)
        {

        }



        private void ReturnBook_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(ReturnTrasactionID.Text))
            {
                MessageBox.Show("Please enter the Transaction ID.");
                return;
            }

            // Validate the return date
            if (returnDate == DateTime.MinValue)
            {
                MessageBox.Show("Please select a valid return date.");
                return;
            }

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Check if there's an active borrowing transaction for the given Transaction ID
                string checkTransactionQuery = "SELECT book_id FROM transactions " +
                                               "WHERE transaction_id = @transactionID AND datereturned IS NULL";
                MySqlCommand checkCmd = new MySqlCommand(checkTransactionQuery, conn);
                checkCmd.Parameters.AddWithValue("@transactionID", ReturnTrasactionID.Text);
                object bookID = checkCmd.ExecuteScalar();

                if (bookID == null)
                {
                    MessageBox.Show("No active borrowing transaction found for this Transaction ID.");
                    return;
                }

                // Update the transactions table to set datereturned using the selected date
                string updateTransactionQuery = "UPDATE transactions SET datereturned = @returnDate WHERE transaction_id = @transactionID";
                MySqlCommand updateTransactionCmd = new MySqlCommand(updateTransactionQuery, conn);
                updateTransactionCmd.Parameters.AddWithValue("@returnDate", returnDate.ToString("yyyy-MM-dd")); // Use the selected return date
                updateTransactionCmd.Parameters.AddWithValue("@transactionID", ReturnTrasactionID.Text);

                int rowsAffectedTransaction = updateTransactionCmd.ExecuteNonQuery();

                if (rowsAffectedTransaction > 0)
                {
                    // Update book availability in the books table
                    string updateBookQuery = "UPDATE books SET availability = 1 WHERE book_id = @bookID";
                    MySqlCommand updateBookCmd = new MySqlCommand(updateBookQuery, conn);
                    updateBookCmd.Parameters.AddWithValue("@bookID", bookID.ToString());
                    int rowsAffectedBook = updateBookCmd.ExecuteNonQuery();

                    if (rowsAffectedBook > 0)
                    {
                        MessageBox.Show("Book successfully returned! Book availability updated.");
                        DataInserted?.Invoke();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update book availability.");
                    }
                }
                else
                {
                    MessageBox.Show("Failed to return the book. Please try again.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error returning book: " + ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }




        private void returnPicker_ValueChanged(object sender, EventArgs e)
        {
            returnDate = returnPicker.Value;
        }

        private void ReturnTrasactionID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
