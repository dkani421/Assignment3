using System;
using System.Data.SqlClient;
using System.Web.UI;

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
                // You can use lblLoginMessage to display messages to the user
                lblLoginMessage.Text = "Please fill in both fields.";
                lblLoginMessage.ForeColor = System.Drawing.Color.Red;
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
                    // Redirect the user to the dashboard or another protected page
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    // Authentication failed
                    lblLoginMessage.Text = "Invalid username or password.";
                    lblLoginMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch 
            {
                // Handle database or other exceptions
                lblLoginMessage.Text = "An error occurred. Please try again later.";
                lblLoginMessage.ForeColor = System.Drawing.Color.Red;
                // Log the exception for debugging
            }
        }


        protected void btnRecoverPassword_Click(object sender, EventArgs e)
        {
            string username = txtUsernameRecovery.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(username))
            {
                // Display an error message if the username field is empty
                // You can use lblRecoveryMessage to display messages to the user
                lblRecoveryMessage.Text = "Please enter your username.";
                lblRecoveryMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                // Check if the username exists in the database
                bool usernameExists = CheckIfUsernameExists(username);

                if (usernameExists)
                {
                    // Generate a password reset token and send it to the user's email
                    string resetToken = GeneratePasswordResetToken(username);
                    SendPasswordResetEmail(username, resetToken);

                    // Inform the user that a password reset email has been sent
                    lblRecoveryMessage.Text = "A password reset email has been sent to your registered email address.";
                    lblRecoveryMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    // Username not found
                    lblRecoveryMessage.Text = "Username not found.";
                    lblRecoveryMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch 
            {
                // Handle database or email sending exceptions
                lblRecoveryMessage.Text = "An error occurred. Please try again later.";
                lblRecoveryMessage.ForeColor = System.Drawing.Color.Red;
                // Log the exception for debugging
            }
        }

        private string GetHashedPasswordFromDatabase(string username)
        {
            string hashedPassword = null;

            // Connect to the database and fetch the hashed password for the provided username
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to retrieve the hashed password from the database
                    string query = "SELECT Password FROM Customers WHERE Username = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
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
                // Handle database connection or query error
                // Log the exception or perform appropriate error handling
            }

            return hashedPassword;
        }

        private bool CheckIfUsernameExists(string username)
        {
            bool usernameExists = false;

            // Connect to the database and check if the username exists
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to check if the username exists in the Customers table
                    string query = "SELECT COUNT(*) FROM Customers WHERE Username = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        int count = (int)command.ExecuteScalar();
                        usernameExists = (count > 0);
                    }
                }
            }
            catch 
            {
                // Handle database connection or query error
                // Log the exception or perform appropriate error handling
            }

            return usernameExists;
        }

        private string GeneratePasswordResetToken(string username)
        {
            // Generate a unique password reset token for the user (you can use a library for this)
            // For example, you can use a GUID (Globally Unique Identifier)
            string resetToken = Guid.NewGuid().ToString();
            return resetToken;
        }

        private void SendPasswordResetEmail(string username, string resetToken)
        {
            // Implement email sending logic here
            // You can use a library like SmtpClient to send emails
            // Construct the email message with a link that includes the resetToken
            // Send the email to the user's registered email address
        }
    }
}
