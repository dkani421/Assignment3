using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace ComputerStore
{
    public partial class ProductListing : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindComputerData();
            }
        }

        protected void BindComputerData()
        {
            // Simulated data source, replace this with your actual data retrieval logic
            ComputerRepeater.DataSource = GetComputerDataFromDatabase();
            ComputerRepeater.DataBind();
        }

        protected List<Computer> GetComputerDataFromDatabase()
        {
            List<Computer> computers = new List<Computer>();

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM computers";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Computer computer = new Computer
                                {
                                    Id = Convert.ToInt32(reader["ComputerID"]),
                                    Model = reader["ModelName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    ImagePath = reader["ImageURL"].ToString()
                                };

                                computers.Add(computer);
                            }
                        }
                    }
                }
            }
            catch
            {
                // Handle any exceptions here, such as logging the error.
            }

            return computers;
        }

        private int ExtractPriceFromDescription(string priceStr)
        {
            // Convert the price string to an integer
            if (int.TryParse(priceStr, out int price))
            {
                return price;
            }

            // Return a default price if no valid price was found
            return 0; 
        }
    }
}
