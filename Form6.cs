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
    public partial class Form6 : Form
    {
        private MySqlConnection conn;
        public delegate void DataInsertedHandler();
        public event DataInsertedHandler DataInserted;

        public Form6(MySqlConnection connection)
        {
            InitializeComponent();
            conn = connection;
        }


        private void updatingBook_Click(object sender, EventArgs e)
        {
         
                try
                {
                    // Validate if all required fields are filled
                    if (string.IsNullOrWhiteSpace(memberID.Text) || string.IsNullOrWhiteSpace(bookID.Text))
                    {
                        MessageBox.Show("Please enter both Book ID and Member ID to proceed!");
                        return;
                    }

                    // Get the date from DateTimePicker
                    DateTime dateBorrowed = datePicker.Value;

                    if (conn != null)
                    {
                        // Open the connection if it's not already open
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        // Check if MemberID exists in the members table
                        string checkMemberQuery = "SELECT COUNT(*) FROM members WHERE member_id = @memberID";
                        MySqlCommand checkMemberCmd = new MySqlCommand(checkMemberQuery, conn);
                        checkMemberCmd.Parameters.AddWithValue("@memberID", memberID.Text);
                        int memberExists = Convert.ToInt32(checkMemberCmd.ExecuteScalar());

                        // Check if BookID exists in the books table and is available
                        string checkBookQuery = "SELECT availability FROM books WHERE book_id = @bookID";
                        MySqlCommand checkBookCmd = new MySqlCommand(checkBookQuery, conn);
                        checkBookCmd.Parameters.AddWithValue("@bookID", bookID.Text);
                        object availabilityResult = checkBookCmd.ExecuteScalar();

                        // If the MemberID does not exist
                        if (memberExists == 0)
                        {
                            MessageBox.Show("Invalid Member ID. Please enter a valid Member ID.");
                            return;
                        }

                        // If the BookID does not exist in books table
                        if (availabilityResult == null)
                        {
                            MessageBox.Show("Invalid Book ID. Please enter a valid Book ID.");
                            return;
                        }

                        // Check if the book is available (availability = 1)
                        bool isAvailable = Convert.ToInt32(availabilityResult) == 1;

                        if (!isAvailable)
                        {
                            MessageBox.Show("The book is currently unavailable or already borrowed.");
                            return;
                        }

                        // Update the transactions table to set dateborrowed
                        string updateTransactionQuery = "UPDATE transactions SET dateborrowed = @dateBorrowed WHERE book_id = @bookID AND member_id = @memberID";
                        MySqlCommand updateTransactionCmd = new MySqlCommand(updateTransactionQuery, conn);
                        updateTransactionCmd.Parameters.AddWithValue("@dateBorrowed", dateBorrowed.ToString("yyyy-MM-dd"));
                        updateTransactionCmd.Parameters.AddWithValue("@bookID", bookID.Text);
                        updateTransactionCmd.Parameters.AddWithValue("@memberID", memberID.Text);

                        int rowsAffectedTransaction = updateTransactionCmd.ExecuteNonQuery();

                        // If the transaction update was successful, update book availability
                        if (rowsAffectedTransaction > 0)
                        {
                            string updateBookQuery = "UPDATE books SET availability = 0 WHERE book_id = @bookID";
                            MySqlCommand updateBookCmd = new MySqlCommand(updateBookQuery, conn);
                            updateBookCmd.Parameters.AddWithValue("@bookID", bookID.Text);

                            int rowsAffectedBook = updateBookCmd.ExecuteNonQuery();

                            if (rowsAffectedBook > 0)
                            {
                                MessageBox.Show("You successfully borrowed the book! Book availability updated.");
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
                            MessageBox.Show("Unable to borrow the book. Please check your Book ID and Member ID.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can't Borrow Book: Connection is not available.");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Ensure the connection is closed after the operation
                    if (conn != null && conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }

        





        private void memberID_TextChanged(object sender, EventArgs e)
        {

        }

        private void bookID_TextChanged(object sender, EventArgs e)
        {

        }

        private void datePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime dateBorrowed = datePicker.Value;
        }
    }
}
