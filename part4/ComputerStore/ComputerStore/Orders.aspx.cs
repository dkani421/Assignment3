using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                       od.OrderDetailID, od.ComputerID, od.ComponentID, od.ComponentPrice, c.ComponentName
                FROM orders o
                LEFT JOIN orderdetails od ON o.OrderID = od.OrderID
                LEFT JOIN components c ON od.ComponentID = c.ComponentID
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


        private void PopulateComponentDropdown(string componentName, string ddlID, Panel container, string selectedValue)
        {
            DropDownList ddlComponent = container.FindControl(ddlID) as DropDownList;

            // Ensure the DropDownList control was found
            if (ddlComponent != null)
            {
                // Fetch the component options from the database and add them as ListItem objects
                List<Component> componentOptions = GetComponentOptions(componentName);

                // Clear existing items and add a default option
                ddlComponent.Items.Clear();
                ddlComponent.Items.Add(new ListItem("Select " + componentName, string.Empty));

                foreach (Component option in componentOptions)
                {
                    // Check if the option's name contains the component name (partial match)
                    if (option.Name.Contains(componentName))
                    {
                        ListItem listItem = new ListItem(option.Name, option.Price.ToString());
                        ddlComponent.Items.Add(listItem);
                    }
                }

                // Set the initial selected value for the dropdown
                ddlComponent.SelectedValue = selectedValue;
            }
        }

        private List<Component> GetComponentOptions(string componentName)
        {
            List<Component> components = new List<Component>();

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Modify the query to retrieve component options for the given componentName
                    string query = "SELECT * FROM components WHERE ComponentName LIKE @ComponentNamePattern"; // Use "ComponentName" here
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ComponentNamePattern", "%" + componentName + "%");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Component component = new Component
                                {
                                    ComponentID = Convert.ToInt32(reader["ComponentID"]),
                                    Name = reader["ComponentName"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"])
                                };

                                components.Add(component);

                                // Print query results for debugging
                                Console.WriteLine($"Component ID: {component.ComponentID}, Name: {component.Name}, Price: {component.Price}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions and print error message
                Console.WriteLine("Error in GetComponentOptions: " + ex.Message);
            }

            return components;
        }

        private void EditOrder(int orderID)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT o.OrderID, o.OrderDate, o.TotalPrice,
                       od.OrderDetailID, od.ComponentID, c.ComponentName
                FROM orders o
                INNER JOIN orderdetails od ON o.OrderID = od.OrderID
                INNER JOIN components c ON od.ComponentID = c.ComponentID
                WHERE o.OrderID = @OrderID";

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
                                Label3.Text = Convert.ToDecimal(reader["TotalPrice"]).ToString("C"); // Formats as currency

                                // Now, we have the ComponentName in the reader
                                string componentName = reader["ComponentName"].ToString();

                                // Define a list of component names
                                List<string> componentNames = new List<string>
                        {
                            "RAM", "Hard Drive", "CPU", "Display", "OS", "Sound Card"
                        };

                                // Iterate through component names and populate dropdowns
                                foreach (string component in componentNames)
                                {
                                    // Construct the dropdown ID based on the component name
                                    string ddlID = "ddl" + component.Replace(" ", "");

                                    // Get the selected component value from the reader
                                    string selectedValue = componentName == component ? componentName : "";

                                    // Populate the dropdown for the current component
                                    PopulateComponentDropdown(component, ddlID, pnlEditOrder, selectedValue);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the entire exception, including the query
                Console.WriteLine("Error in EditOrder: " + ex.ToString());
            }
        }

        private decimal GetInitialComputerPrice(int orderID)
        {
            decimal initialComputerPrice = 0.0m;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT c.Price
                FROM orderdetails od
                INNER JOIN computers c ON od.ComputerID = c.ComputerID
                WHERE od.OrderID = @OrderID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", orderID);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            initialComputerPrice = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or log the error message
                Console.WriteLine("Error fetching initial computer price: " + ex.Message);
            }

            return initialComputerPrice;
        }

        protected void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the order ID
                int orderID = Convert.ToInt32(Label1.Text);

                // Get the selected component values from dropdown lists
                string selectedRAM = ddlRam.SelectedValue;
                string selectedHardDrive = ddlHardDrive.SelectedValue;
                string selectedCPU = ddlCPU.SelectedValue;
                string selectedDisplay = ddlDisplay.SelectedValue;
                string selectedOS = ddlOS.SelectedValue;
                string selectedSoundCard = ddlSoundCard.SelectedValue;

                // Retrieve the initial computer price
                decimal initialComputerPrice = GetInitialComputerPrice(orderID);

                // Calculate the total price based on selected components and initial computer price
                decimal totalPrice = CalculateTotalPrice(initialComputerPrice.ToString(), selectedRAM, selectedHardDrive, selectedCPU, selectedDisplay, selectedOS, selectedSoundCard);

                // Update order details in the database
                UpdateOrderDetails(orderID, selectedRAM, selectedHardDrive, selectedCPU, selectedDisplay, selectedOS, selectedSoundCard);

                // Update the total price of the order in the orders table
                UpdateOrderTotalPrice(orderID, totalPrice);

                // Display a success message to the user
                lblUpdateStatus.Text = "Order updated successfully!";
            }
            catch (Exception ex)
            {
                // Handle exceptions and display an error message if needed
                lblUpdateStatus.Text = "Error updating order: " + ex.Message;
            }
        }

        private decimal CalculateTotalPrice(string initialComputerPrice, string ramPrice, string hardDrivePrice, string cpuPrice, string displayPrice, string osPrice, string soundCardPrice)
        {
            // Convert the selected component prices and initial computer price to decimal and sum them
            decimal totalPrice = decimal.Parse(initialComputerPrice) + decimal.Parse(ramPrice) + decimal.Parse(hardDrivePrice) + decimal.Parse(cpuPrice) + decimal.Parse(displayPrice) + decimal.Parse(osPrice) + decimal.Parse(soundCardPrice);

            return totalPrice;
        }

        public void UpdateOrderDetails(int orderID, string ram, string hardDrive, string cpu, string display, string os, string soundCard)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a dictionary to map component names to their selected values
                    Dictionary<string, (string Price, int Type)> componentMap = new Dictionary<string, (string Price, int Type)>
                     {
                         { "RAM",        (Price: ram,       Type: 1) },
                         { "Hard Drive", (Price: hardDrive, Type: 2) },
                         { "CPU",        (Price: cpu,       Type: 3) },
                         { "Display",    (Price: display,   Type: 4) },
                         { "OS",         (Price: os,        Type: 5) },
                         { "Sound Card", (Price: soundCard, Type: 6) }
                     };

                    foreach (var entry in componentMap)
                    {
                        // Fetch the component ID and price based on the selected value
                        int componentID = GetComponentIDByPrice(connection, entry.Key, entry.Value.Price);

                        // Update the ComponentID and ComponentPrice for each component in the "orderdetails" table
                        UpdateComponentInOrderDetails(connection, orderID, componentID, entry.Value.Type, entry.Value.Price);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or log the error message
                Console.WriteLine("Error updating order details: " + ex.Message);
            }
        }

        private int GetComponentIDByPrice(MySqlConnection connection, string componentName, string price)
        {
            int componentID = -1; // Default value if not found

            // Create a query to retrieve the component ID based on the component name that contains the given name (partial match)
            // and where the price matches exactly.
            string query = "SELECT ComponentID FROM components WHERE ComponentName LIKE @ComponentName AND Price = @Price";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // Use '%' to perform a partial match based on the component name
                command.Parameters.AddWithValue("@ComponentName", "%" + componentName + "%");
                command.Parameters.AddWithValue("@Price", decimal.Parse(price));

                // Execute the query and retrieve the component ID
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    componentID = Convert.ToInt32(result);
                }
            }

            return componentID;
        }


        private void UpdateComponentInOrderDetails(MySqlConnection connection, int orderID, int newComponentID, int targetComponentType, string selectedValue)
        {
            string query = @"
            UPDATE orderdetails od
            SET od.ComponentID = @ComponentID, od.ComponentPrice = @ComponentPrice
            WHERE od.OrderID = @OrderID AND od.ComponentType = @ComponentType";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderID", orderID);
                command.Parameters.AddWithValue("@ComponentID", newComponentID);
                command.Parameters.AddWithValue("@ComponentType", targetComponentType);

                if (!string.IsNullOrEmpty(selectedValue))
                {
                    // Convert the selected value to decimal and use it as the component price
                    command.Parameters.AddWithValue("@ComponentPrice", decimal.Parse(selectedValue));
                }
                else
                {
                    // Handle the case where selectedValue is null or empty (if needed)
                    // You can set a default value or handle it according to your business logic.
                }

                command.ExecuteNonQuery();
            }
        }


        private void UpdateOrderTotalPrice(int orderID, decimal totalPrice)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Update the total price in the orders table based on OrderID
                    string updateOrderTotalPriceQuery = "UPDATE orders SET TotalPrice = @TotalPrice WHERE OrderID = @OrderID";

                    using (MySqlCommand command = new MySqlCommand(updateOrderTotalPriceQuery, connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", orderID);
                        command.Parameters.AddWithValue("@TotalPrice", totalPrice);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or log the error message
                Console.WriteLine("Error updating order total price: " + ex.Message);
            }
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
