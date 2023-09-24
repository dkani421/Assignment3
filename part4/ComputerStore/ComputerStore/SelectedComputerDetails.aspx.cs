using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using MySql.Data.MySqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ComputerStore
{
    public partial class SelectedComputerDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int computerId;
                decimal totalPrice;
                string ram;
                string hardDrive;
                string cpu;
                string display;
                string os;
                string soundCard;
                string totalPriceLabel; 

                // Check if the computerId and other query string parameters are present and can be parsed
                if (int.TryParse(Request.QueryString["computerId"], out computerId) &&
                    decimal.TryParse(Request.QueryString["totalPrice"], out totalPrice) &&
                    !string.IsNullOrEmpty(Request.QueryString["Ram"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["HardDrive"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["CPU"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["Display"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["OS"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["SoundCard"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["TotalPriceLabel"])) 
                {
                    Computer selectedComputer = GetComputerById(computerId);
                    ram = Request.QueryString["Ram"];
                    hardDrive = Request.QueryString["HardDrive"];
                    cpu = Request.QueryString["CPU"];
                    display = Request.QueryString["Display"];
                    os = Request.QueryString["OS"];
                    soundCard = Request.QueryString["SoundCard"];
                    totalPriceLabel = Request.QueryString["TotalPriceLabel"]; 

                    if (selectedComputer != null)
                    {
                        SelectedComputerImage.ImageUrl = selectedComputer.ImagePath;
                        SelectedComputerModel.Text = selectedComputer.Model;
                        SelectedComputerDescription.Text = selectedComputer.Description;

                        // Display the total price
                        SelectedComputerPrice.Text = totalPrice.ToString();

                        // Display the customization details
                        RamSelectionLabel.Text = ram;
                        HardDriveSelectionLabel.Text = hardDrive;
                        CpuSelectionLabel.Text = cpu;
                        DisplaySelectionLabel.Text = display;
                        OsSelectionLabel.Text = os;
                        SoundCardSelectionLabel.Text = soundCard;

                        // Display the TotalPriceLabel value
                        TotalPriceLabel.Text = totalPriceLabel;
                    }
                }
                else
                {
                }
            }
        }
        protected void OrderButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["computerId"], out int computerId))
            {
                int customerId = GetCurrentCustomerId();

                // Parse the component values from the query string
                string[] components = new string[]
                {
            Request.QueryString["Ram"],
            Request.QueryString["HardDrive"],
            Request.QueryString["CPU"],
            Request.QueryString["Display"],
            Request.QueryString["OS"],
            Request.QueryString["SoundCard"]
                };

                decimal totalPrice = decimal.Parse(TotalPriceLabel.Text);

                // Insert the order into the database and get the newly created OrderID
                int newOrderID = InsertOrder(customerId, totalPrice);

                // Loop through the components and insert an order detail for each one
                for (int i = 0; i < components.Length; i++)
                {
                    // Get the component price from the database based on the component name
                    decimal componentPrice = GetComponentPrice(components[i]);

                    // Insert order details into the database using the component name
                    InsertOrderDetail(newOrderID, computerId, components[i], componentPrice);
                }

                Response.Redirect("Orders.aspx");
            }
            else
            {
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

        private decimal GetComponentPrice(string componentName)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;

            string query = "SELECT Price FROM components WHERE ComponentName = @ComponentName";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ComponentName", componentName);

                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToDecimal(result);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        private int InsertOrder(int customerId, decimal totalPrice)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string insertOrderSql = "INSERT INTO orders (CustomerID, OrderDate, TotalPrice) VALUES (@CustomerID, NOW(), @TotalPrice); SELECT LAST_INSERT_ID();";

                using (MySqlCommand cmd = new MySqlCommand(insertOrderSql, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);

                    int newOrderID = Convert.ToInt32(cmd.ExecuteScalar());

                    return newOrderID;
                }
            }
        }

        private void InsertOrderDetail(int orderID, int computerId, string componentName, decimal componentPrice)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Modify the query to retrieve the component ID based on the component name
                string componentIdQuery = "SELECT ComponentID, ComponentType FROM components WHERE ComponentName = @ComponentName";

                using (MySqlCommand componentIdCommand = new MySqlCommand(componentIdQuery, connection))
                {
                    componentIdCommand.Parameters.AddWithValue("@ComponentName", componentName);
                    int componentID = 0;
                    int componentType = 0;
                    using (MySqlDataReader reader = componentIdCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            componentID = Convert.ToInt32(reader["ComponentID"]);
                            componentType = Convert.ToInt32(reader["ComponentType"]);
                        }
                    }

                    // Insert order details into the database
                    string insertOrderDetailSql = "INSERT INTO orderdetails (OrderID, ComputerID, ComponentID, ComponentType, ComponentPrice) VALUES (@OrderID, @ComputerID, @ComponentID, @ComponentType, @ComponentPrice);";
                    using (MySqlCommand cmd = new MySqlCommand(insertOrderDetailSql, connection))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                        cmd.Parameters.AddWithValue("@ComputerID", computerId);
                        cmd.Parameters.AddWithValue("@ComponentID", componentID); // Use the retrieved component ID
                        cmd.Parameters.AddWithValue("@ComponentType", componentType);
                        cmd.Parameters.AddWithValue("@ComponentPrice", componentPrice);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected Computer GetComputerById(int computerId)
        {
            List<Computer> computers = GetComputerData();

            // Iterate through the list of computers and find the one with the matching ID
            foreach (Computer computer in computers)
            {
                if (computer.Id == computerId)
                {
                    return computer; // Found the matching computer
                }
            }

            return null; // No computer found with the specified ID
        }

        protected List<Computer> GetComputerData()
        {
            List<Computer> computers = new List<Computer>();
            string filePath = Server.MapPath("~/App_Data/ComputerData.txt");

            string[] lines = System.IO.File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (parts.Length >= 5)
                {
                    int id = Convert.ToInt32(parts[0]);
                    string model = parts[1];
                    string description = parts[2];
                    int price = Convert.ToInt32(parts[3]);
                    string imagePath = parts[4];

                    Computer computer = new Computer
                    {
                        Id = id,
                        Model = model,
                        Description = description,
                        Price = price,
                        ImagePath = imagePath
                    };
                    computers.Add(computer);
                }
            }

            return computers;
        }
    }
    }
