using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace ComputerStore
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Get user inputs
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string email = txtEmail.Text; // Retrieve email input

            // Validate inputs 
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                lblMessage.Text = "Please fill in all fields.";
                lblMessage.Visible = true;
                return;
            }

            // Validate email format 
            if (!IsValidEmail(email))
            {
                lblMessage.Text = "Invalid email format.";
                lblMessage.Visible = true;
                return;
            }
            // Validate username and password length
            if (username.Length < 5 || password.Length < 5)
            {
                lblMessage.Text = "Username and password must be at least 5 characters long.";
                lblMessage.Visible = true;
                return;
            }
            // Check if password and confirm password match
            if (password != confirmPassword)
            {
                lblMessage.Text = "Password and confirm password do not match.";
                lblMessage.Visible = true;
                return;
            }

            try
            {
                // Hash the password securely using BCrypt
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                // Connect to the MySQL database using the connection string from web.config
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString)) // Use MySqlConnection
                {
                    connection.Open();

                    // Insert the new customer's information into the Customers table
                    string insertQuery = "INSERT INTO customers (FirstName, LastName, Email, Username, Password) VALUES (@FirstName, @LastName, @Email, @Username, @Password)";
                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection)) // Use MySqlCommand
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", hashedPassword);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Registration successful
                            lblMessage.Text = "Registration successful!";
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                            lblMessage.Visible = true;
                            // You can redirect the user to a login page or perform other actions
                        }
                        else
                        {
                            // Registration failed
                            lblMessage.Text = "Registration failed. Please try again later.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any other exceptions 
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Visible = true;
            }

        }
        // Protected method to validate email format
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
