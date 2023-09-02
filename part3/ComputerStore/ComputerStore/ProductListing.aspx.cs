using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

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

        // Inside the BindComputerData method
        protected void BindComputerData()
        {
            // Simulated data source, replace this with your actual data retrieval logic
            ComputerRepeater.DataSource = GetComputerData();
            ComputerRepeater.DataBind();
        }

        // Inside the GetComputerData method
        protected List<Computer> GetComputerData()
        {
            List<Computer> computers = new List<Computer>();

            // Read lines from the ComputerData.txt file
            string[] lines = System.IO.File.ReadAllLines(Server.MapPath("~/App_Data/ComputerData.txt"));

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (parts.Length >= 5) // Make sure to adjust this based on the actual number of parts
                {
                    string id = parts[0];
                    string model = parts[1];
                    string description = parts[2];
                    string priceStr = parts[3];
                    string imagePath = parts[4];

                    // Assume you have a method to extract the price from the description
                    int price = ExtractPriceFromDescription(priceStr);

                    // Create a new Computer object and add it to the list
                    Computer computer = new Computer
                    {
                        Id = Convert.ToInt32(id), // Convert id to an integer
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

        // Inside the ExtractPriceFromDescription method
        private int ExtractPriceFromDescription(string priceStr)
        {
            // Convert the price string to an integer
            if (int.TryParse(priceStr, out int price))
            {
                return price;
            }

            // Return a default price if no valid price was found
            return 0; // You can choose an appropriate default value
        }
    }
}
