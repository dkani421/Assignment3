using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using BCrypt.Net;
using System.Web.Security;
using Microsoft.Ajax.Utilities;

namespace ComputerStore
{
    public partial class Login : Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                // Display an error message if fields are empty
                lblLoginMessage.Text = "Please fill in both fields.";
                lblLoginMessage.ForeColor = System.Drawing.Color.Red;
                lblLoginMessage.Visible = true;
                return;
            }

            try
            {
                // Retrieve the hashed password from the database based on the provided username
                string hashedPasswordFromDatabase = GetHashedPasswordFromDatabase(username);

                // Compare the provided password with the hashed password from the database
                if (BCrypt.Net.BCrypt.Verify(password, hashedPasswordFromDatabase))
                {
                    // Authentication successful
                    FormsAuthentication.SetAuthCookie(username, false); // or true for "Remember Me" functionality
                    Response.Redirect("Orders.aspx");
                }
                else
                {
                    // Authentication failed
                    lblLoginMessage.Text = "Invalid username or password.";
                    lblLoginMessage.ForeColor = System.Drawing.Color.Red;
                    lblLoginMessage.Visible = true;
                }
            }
            catch
            {
                // Handle database or other exceptions
                lblLoginMessage.Text = "An error occurred. Please try again later.";
                lblLoginMessage.ForeColor = System.Drawing.Color.Red;
                lblLoginMessage.Visible = true;
            }
        }

        protected void btnRecoverPassword_Click(object sender, EventArgs e)
        {
            string username = txtUsernameRecovery.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(username))
            {
                // Display an error message if the username field is empty
                lblRecoveryMessage.Text = "Please enter your username.";
                lblRecoveryMessage.ForeColor = System.Drawing.Color.Red;
                lblRecoveryMessage.Visible = true;
                return;
            }

            try
            {
                // Check if the username exists in the database
                bool usernameExists = CheckIfUsernameExists(username);

                if (usernameExists)
                {
                    // Generate a new password
                    string newPassword = GenerateRandomPassword();

                    // Update the user's password in the database
                    UpdateUserPassword(username, newPassword);

                    // Inform the user of their new password
                    lblRecoveryMessage.Text = $"Your new password is: {newPassword}";
                    lblRecoveryMessage.ForeColor = System.Drawing.Color.Green;
                    lblRecoveryMessage.Visible = true;
                }
                else
                {
                    // Username not found
                    lblRecoveryMessage.Text = "Username not found.";
                    lblRecoveryMessage.ForeColor = System.Drawing.Color.Red;
                    lblRecoveryMessage.Visible = true;
                }
            }
            catch
            {
                // Handle database or other exceptions
                lblRecoveryMessage.Text = "An error occurred. Please try again later.";
                lblRecoveryMessage.ForeColor = System.Drawing.Color.Red;
                lblRecoveryMessage.Visible = true;
            }
        }

        // Generate a random password
        private string GenerateRandomPassword()
        {
            // Generate a random password
            string newPassword = Guid.NewGuid().ToString().Substring(0, 8);
            return newPassword;
        }

        // Update the user's password in the database
        private void UpdateUserPassword(string username, string newPassword)
        {
            try
            {
                // Update the Password column for the specified username
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Update the Password column for the specified username
                    string query = "UPDATE customers SET Password = @Password WHERE Username = @Username";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Password", BCrypt.Net.BCrypt.HashPassword(newPassword));
                        command.Parameters.AddWithValue("@Username", username);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
            }
        }

        private string GetHashedPasswordFromDatabase(string username)
        {
            string hashedPassword = null;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to retrieve the hashed password from the database
                    string query = "SELECT Password FROM customers WHERE Username = @Username";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            hashedPassword = result.ToString();
                        }
                    }
                }
            }
            catch
            {
            }

            return hashedPassword;
        }

        private bool CheckIfUsernameExists(string username)
        {

            // Connect to the database and check if the username exists
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to check if the username exists in the customers table
                    string query = "SELECT COUNT(*) FROM customers WHERE Username = @Username";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        string count = "";
                        command.Parameters.AddWithValue("@Username", username);
                        count = command.ExecuteScalar().ToString();
                        return !(count=="0");
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
