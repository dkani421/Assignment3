using System;
using System.Collections.Generic;
using System.Data;
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

                if (orders.Count > 0)
                {
                    // Orders found, bind and show the GridView
                    gridViewOrders.DataSource = orders;
                    gridViewOrders.DataBind();
                    lblNoOrders.Visible = false; // Hide the "No orders" message
                }
                else
                {
                    // No orders found, show the "No orders" message and hide the GridView
                    lblNoOrders.Visible = true;
                    gridViewOrders.Visible = false;
                }
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
                DisplayOrderDetails(orderID);

                pnlViewOrder.Visible = true;
                pnlEditOrder.Visible = false;
            }
            else if (e.CommandName == "EditOrder")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                EditOrder(orderID);

                pnlEditOrder.Visible = true;
                pnlViewOrder.Visible = false;
            }
            else if (e.CommandName == "DeleteOrder")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                DeleteOrder(orderID);
                RefreshGridView();

                pnlEditOrder.Visible = false;
                pnlViewOrder.Visible = false;
            }
        }

        private void DisplayOrderDetails(int orderID)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT o.OrderID, o.OrderDate, o.TotalPrice,
                               od.OrderDetailID, od.ComputerID, od.ComponentID, od.ComponentPrice
                        FROM orders o
                        LEFT JOIN orderdetails od ON o.OrderID = od.OrderID
                        WHERE o.OrderID = @OrderID AND o.CustomerID = @CustomerID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", orderID);
                        command.Parameters.AddWithValue("@CustomerID", GetCurrentCustomerId());

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                lblViewOrderID.Text = dt.Rows[0]["OrderID"].ToString();
                                lblViewOrderDate.Text = dt.Rows[0]["OrderDate"].ToString();
                                decimal totalPrice = Convert.ToDecimal(dt.Rows[0]["TotalPrice"]);
                                lblViewTotalPrice.Text = totalPrice.ToString("C"); // Formats as currency

                                // Bind order details to the GridView
                                gridViewOrderDetails.DataSource = dt;
                                gridViewOrderDetails.DataBind();
                            }
                            else
                            {
                                // No order details found for the selected order
                                // You can display a message here if needed
                            }
                        }
                    }
                }
            }
            catch
            {
                // Handle any exceptions
            }
        }

        private void EditOrder(int orderID)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM orders WHERE OrderID = @OrderID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", orderID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate labels with order information
                                Label1.Text = reader["OrderID"].ToString();
                                Label2.Text = reader["OrderDate"].ToString();
                                // Format and display TotalPrice as currency
                                decimal totalPrice = Convert.ToDecimal(reader["TotalPrice"]);
                                Label3.Text = totalPrice.ToString("C"); // Formats as currency

                                // Populate dropdown lists for component selection
                                PopulateComponentDropdown("Ram", "ddlRam", pnlEditOrder, reader["Ram"].ToString());
                                PopulateComponentDropdown("HardDrive", "ddlHardDrive", pnlEditOrder, reader["HardDrive"].ToString());
                                PopulateComponentDropdown("CPU", "ddlCPU", pnlEditOrder, reader["CPU"].ToString());
                                PopulateComponentDropdown("Display", "ddlDisplay", pnlEditOrder, reader["Display"].ToString());
                                PopulateComponentDropdown("OS", "ddlOS", pnlEditOrder, reader["OS"].ToString());
                                PopulateComponentDropdown("SoundCard", "ddlSoundCard", pnlEditOrder, reader["SoundCard"].ToString());
                            }
                        }
                    }
                }
            }
            catch
            {
                // Handle any exceptions
            }
        }

        private void PopulateComponentDropdown(string componentName, string ddlID, Panel container, string selectedValue)
        {
            DropDownList ddlComponent = new DropDownList();
            ddlComponent.ID = ddlID; // Assign a unique ID to each dropdown
            ddlComponent.CssClass = "component-dropdown"; // Add CSS class for styling
            container.Controls.Add(ddlComponent);

            // Populate the dropdown with component options based on your database or data source
            // You can fetch the component options from the database and add them as ListItem objects
            // Example:
            // ddlComponent.Items.Add(new ListItem("Option 1", "1"));
            // ddlComponent.Items.Add(new ListItem("Option 2", "2"));
            // ...

            // Set the initial selected value for the dropdown
            ddlComponent.SelectedValue = selectedValue;
        }

        protected void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            // Your update logic here
        }

        private void DeleteOrder(int orderID)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Begin a transaction to ensure both order and order details are deleted or none at all
                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Delete order details associated with the order
                            string deleteOrderDetailsQuery = "DELETE FROM orderdetails WHERE OrderID = @OrderID";
                            using (MySqlCommand deleteOrderDetailsCommand = new MySqlCommand(deleteOrderDetailsQuery, connection, transaction))
                            {
                                deleteOrderDetailsCommand.Parameters.AddWithValue("@OrderID", orderID);
                                deleteOrderDetailsCommand.ExecuteNonQuery();
                            }

                            // Delete the order itself
                            string deleteOrderQuery = "DELETE FROM orders WHERE OrderID = @OrderID";
                            using (MySqlCommand deleteOrderCommand = new MySqlCommand(deleteOrderQuery, connection, transaction))
                            {
                                deleteOrderCommand.Parameters.AddWithValue("@OrderID", orderID);
                                deleteOrderCommand.ExecuteNonQuery();
                            }

                            // Commit the transaction
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            // An error occurred, rollback the transaction
                            transaction.Rollback();
                            throw; // You can handle or log the exception as needed
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Handle any exceptions
            }
        }

        private void RefreshGridView()
        {
            try
            {
                int customerId = GetCurrentCustomerId();
                List<Order> orders = GetOrdersByCustomer(customerId);

                if (orders.Count > 0)
                {
                    // Orders found, bind and show the GridView
                    gridViewOrders.DataSource = orders;
                    gridViewOrders.DataBind();
                    lblNoOrders.Visible = false; // Hide the "No orders" message
                }
                else
                {
                    // No orders found, show the "No orders" message and hide the GridView
                    lblNoOrders.Visible = true;
                    gridViewOrders.Visible = false;
                }
            }
            catch
            {
                // Handle any exceptions
            }
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            // Implement logic to navigate back to the list of orders.
            pnlViewOrder.Visible = false;
            pnlEditOrder.Visible = false;
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            // Implement logic to cancel the order editing operation.
            pnlEditOrder.Visible = false;
            pnlViewOrder.Visible = false;
        }
    }
}
