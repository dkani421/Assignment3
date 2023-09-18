using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace ComputerStore
{
    public partial class Orders : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Login.aspx");
                }
                int customerId = GetCurrentCustomerId(); 
                List<Order> orders = GetOrdersByCustomer(customerId); 
                gridViewOrders.DataSource = orders;
                gridViewOrders.DataBind();
            }
        }

        private int GetCurrentCustomerId()
        {
            int customerId = -1;

            try
            {
                string username = User.Identity.Name;


                
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT CustomerID FROM customers WHERE Username = @Username";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            customerId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch
            {
                // If an error occurs, the customerId will remain as the default value (-1).
            }

            return customerId;
        }

        private List<Order> GetOrdersByCustomer(int customerId)
        {
            List<Order> orders = new List<Order>();
            try
            {
                // Establish a database connection
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SQL query to retrieve orders for the specified customer
                    string query = "SELECT * FROM orders WHERE CustomerID = @CustomerId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customerId);

                        // Execute the query and read the results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Order order = new Order
                                {
                                    OrderID = Convert.ToInt32(reader["OrderID"]),
                                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                    TotalPrice = Convert.ToDecimal(reader["TotalPrice"])
                                };

                                orders.Add(order);
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return orders;
        }

        protected void GridViewOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewOrder")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                // Implement logic to display order details in the pnlViewOrder panel.
                pnlViewOrder.Visible = true;
                pnlEditOrder.Visible = false;
            }
            else if (e.CommandName == "EditOrder")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                // Implement logic to populate and display order details for editing in the pnlEditOrder panel.
                pnlEditOrder.Visible = true;
                pnlViewOrder.Visible = false;
            }
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            // Implement logic to navigate back to the list of orders.
            pnlViewOrder.Visible = false;
            pnlEditOrder.Visible = false;
        }

        protected void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            // Implement logic to update the order details in the database.
            pnlEditOrder.Visible = false;
            pnlViewOrder.Visible = false;
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            // Implement logic to cancel the order editing operation.
            pnlEditOrder.Visible = false;
            pnlViewOrder.Visible = false;
        }
    }
}
