using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net; // Include the BCrypt.NET namespace

namespace ComputerStore 
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Get user inputs
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Validate inputs (you can add more validation as needed)
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please fill in all fields.";
                lblMessage.Visible = true;
                return;
            }

            try
            {
                // Hash the password securely using BCrypt
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                // Connect to the database using the connection string from web.config
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert the new customer's information into the Customers table
                    string insertQuery = "INSERT INTO Customers (Username, Password) VALUES (@Username, @Password)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
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
            catch
            {
                // Handle any exceptions (e.g., database connection error)
                lblMessage.Text = "An error occurred. Please try again later.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Visible = true;
                // You can also log the exception for debugging
            }
        }
    }
}