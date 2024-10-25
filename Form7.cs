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
    public partial class Form7 : Form
    {
        private MySqlConnection conn;
        public delegate void DataInsertedHandler();
        public event DataInsertedHandler DataInserted;
        public Form7(MySqlConnection connect)
        {
            InitializeComponent();
            conn = connect;
        }

        private void DeleteTransaction_TextChanged(object sender, EventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // Check if the Transaction ID field is filled
            if (string.IsNullOrWhiteSpace(DeleteTransaction.Text))
            {
                MessageBox.Show("Please enter a valid Transaction ID.");
                return;
            }

            // Confirm deletion
            var confirmResult = MessageBox.Show("Are you sure you want to delete this transaction?",
                                                "Confirm Delete",
                                                MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // Open the connection if it's closed
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    // Prepare the delete query
                    string deleteTransactionQuery = "DELETE FROM transactions WHERE transaction_id = @IdTransaction;";
                    MySqlCommand cmd = new MySqlCommand(deleteTransactionQuery, conn);
                    cmd.Parameters.AddWithValue("@IdTransaction", DeleteTransaction.Text);

                    // Execute the query and check if any row was affected
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Transaction successfully deleted!");
                        DataInserted?.Invoke(); // Trigger event if needed
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No transaction found with the given ID.");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error deleting transaction: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }
        }
    }
}

