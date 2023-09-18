using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;

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
                    // Handle the case where the parameters are missing or cannot be parsed
                }
            }
        }
        /*
        protected void OrderButton_Click(object sender, EventArgs e)
        {
            // Get the selected computer details and user information (you need to implement this part)
            //int computerId = GetSelectedComputerId(); // Implement this method to get the computer ID.
            //int customerId = GetCustomerId(); // Implement this method to get the customer ID.
            //decimal totalPrice = GetTotalPrice(); // Implement this method to get the total price.
            //List<Component> selectedComponents = GetSelectedComponents(); // Implement this method to get the selected components.

            // Create a new order
            Order newOrder = new Order
            {
                CustomerID = customerId,
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice
            };

            // Insert the order into the database and retrieve the generated OrderID
            int orderId = InsertOrder(newOrder);

            // Create order detail records for selected components
            foreach (Component component in selectedComponents)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    OrderID = orderId,
                    ComputerID = computerId,
                    ComponentID = component.ComponentID,
                    ComponentPrice = component.Price
                };

                // Insert the order detail into the database
                InsertOrderDetail(orderDetail);
            }

            // Redirect to the Orders.aspx page or show a confirmation message
            Response.Redirect("Orders.aspx");
        }

        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["YourConnectionStringName"].ConnectionString;

        public int InsertOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Insert query for Orders table
                string insertOrderQuery = "INSERT INTO Orders (CustomerID, OrderDate, TotalPrice) VALUES (@CustomerID, @OrderDate, @TotalPrice); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(insertOrderQuery, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);

                    // Execute the insert and retrieve the generated OrderID
                    int orderId = Convert.ToInt32(command.ExecuteScalar());
                    return orderId;
                }
            }
        }

        public void InsertOrderDetail(OrderDetail orderDetail)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Insert query for OrderDetails table
                string insertOrderDetailQuery = "INSERT INTO OrderDetails (OrderID, ComputerID, ComponentID, ComponentPrice) VALUES (@OrderID, @ComputerID, @ComponentID, @ComponentPrice);";

                using (SqlCommand command = new SqlCommand(insertOrderDetailQuery, connection))
                {
                    command.Parameters.AddWithValue("@OrderID", orderDetail.OrderID);
                    command.Parameters.AddWithValue("@ComputerID", orderDetail.ComputerID);
                    command.Parameters.AddWithValue("@ComponentID", orderDetail.ComponentID);
                    command.Parameters.AddWithValue("@ComponentPrice", orderDetail.ComponentPrice);

                    // Execute the insert
                    command.ExecuteNonQuery();
                }
            }
        }
        */

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
