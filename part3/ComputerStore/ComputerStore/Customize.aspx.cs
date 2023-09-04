using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ComputerStore
{
    public partial class Customize : Page
    {
        private Computer _selectedComputer;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // Retrieve the selected computer's ID from the query string
                if (int.TryParse(Request.QueryString["computerId"], out int computerId))
                {
                    // Replace this with your data retrieval logic
                    _selectedComputer = GetComputerById(computerId);

                    if (_selectedComputer != null)
                    {
                        // Display the selected computer's information
                        SelectedComputerImage.ImageUrl = _selectedComputer.ImagePath;
                        SelectedComputerModel.Text = _selectedComputer.Model;
                        SelectedComputerDescription.Text = _selectedComputer.Description;
                        SelectedComputerPrice.Text = _selectedComputer.Price.ToString();

                        // Initialize the total price to the computer's price
                        decimal totalPrice = _selectedComputer.Price;
                        TotalPriceLabel.InnerText = totalPrice.ToString();

                        // Store the selected computer in ViewState
                        ViewState["SelectedComputer"] = _selectedComputer;
                    }
                    else
                    {
                        // Handle case when selected computer is not found
                    }
                }
                else
                {
                    // Handle case when computerId is not valid
                }
            }
            else
            {
                // Restore the selected computer from ViewState
                _selectedComputer = ViewState["SelectedComputer"] as Computer;
            }
        }

        protected void ComponentDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle the selected index change for each dropdown
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            // Calculate the total price based on selected dropdown values
            decimal totalPrice = _selectedComputer.Price;

            totalPrice += GetDropdownValue(RamDropDown);
            totalPrice += GetDropdownValue(HardDriveDropDown);
            totalPrice += GetDropdownValue(CpuDropDown);
            totalPrice += GetDropdownValue(DisplayDropDown);
            totalPrice += GetDropdownValue(OsDropDown);
            totalPrice += GetDropdownValue(SoundCardDropDown);

            TotalPriceLabel.InnerText = totalPrice.ToString();
        }

        private decimal GetDropdownValue(DropDownList dropdown)
        {
            decimal selectedValue = 0;
            if (dropdown.SelectedIndex > 0)
            {
                selectedValue = decimal.Parse(dropdown.SelectedValue);
            }
            return selectedValue;
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

        protected void AddToCartButton_Click(object sender, EventArgs e)
        {
            if (_selectedComputer != null)
            {
                // Construct the URL for the new window
                string url = "SelectedComputerDetails.aspx" +
                             "?computerId=" + _selectedComputer.Id +
                             "&totalPrice=" + _selectedComputer.GetTotalPrice() +
                             "&TotalPriceLabel=" + TotalPriceLabel.InnerText;

                url += "&Ram=" + RamDropDown.SelectedItem.Text;
                url += "&HardDrive=" + HardDriveDropDown.SelectedItem.Text;
                url += "&CPU=" + CpuDropDown.SelectedItem.Text;
                url += "&Display=" + DisplayDropDown.SelectedItem.Text;
                url += "&OS=" + OsDropDown.SelectedItem.Text;
                url += "&SoundCard=" + SoundCardDropDown.SelectedItem.Text;

                // Redirect to the cart page
                Response.Redirect(url);
            }
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
