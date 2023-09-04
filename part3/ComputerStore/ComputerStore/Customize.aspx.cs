using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace ComputerStore
{
    public partial class Customize : Page
    {
        private List<ComputerComponent> _components; // List of computer components
        private Computer _selectedComputer;

        protected void Page_Load(object sender, EventArgs e)
        {
            _components = GetHardcodedComputerComponents();

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

                        // Bind the component dropdown
                        ComponentDropDown.DataSource = _components;
                        ComponentDropDown.DataTextField = "Name";
                        ComponentDropDown.DataValueField = "Name";
                        ComponentDropDown.DataBind();

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
            // Only update the PricingOptionDropDown when a component is selected
            string selectedComponentName = ComponentDropDown.SelectedValue;
            var selectedComponent = _components.FirstOrDefault(c => c.Name == selectedComponentName);

            if (selectedComponent != null)
            {
                // Populate the PricingOptionDropDown with pricing options for the selected component
                PricingOptionDropDown.DataSource = selectedComponent.PriceOptions;
                PricingOptionDropDown.DataTextField = "OptionName";
                PricingOptionDropDown.DataValueField = "Price";
                PricingOptionDropDown.DataBind();

                // Select the first pricing option by default
                PricingOptionDropDown.SelectedIndex = 0;
            }
        }

        protected void PricingOptionDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the selected pricing option for the component
            string selectedComponentName = ComponentDropDown.SelectedValue;
            var selectedComponent = _selectedComputer.Components.FirstOrDefault(c => c.Name == selectedComponentName);

            if (selectedComponent != null)
            {
                string selectedOptionName = PricingOptionDropDown.SelectedValue;
                var selectedOption = selectedComponent.PriceOptions.FirstOrDefault(option => option.OptionName == selectedOptionName);

                if (selectedOption != null)
                {
                    // Mark the selected option as "Selected"
                    selectedOption.Selected = true;
                }
            }

            // Update the total price
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            decimal totalPrice = _selectedComputer.Price;

            // Calculate the total price based on selected components and options
            foreach (var component in _selectedComputer.Components)
            {
                var selectedOption = component.PriceOptions.FirstOrDefault(option => option.Selected);
                if (selectedOption != null)
                {
                    totalPrice += selectedOption.Price;
                }
            }

            // Set the total price label
            TotalPriceLabel.InnerText = totalPrice.ToString();
        }

        protected List<ComputerComponent> GetHardcodedComputerComponents()
        {
            List<ComputerComponent> components = new List<ComputerComponent>
        {
            new ComputerComponent
            {
                Id = 1,
                Name = "RAM",
                PriceOptions = new List<PriceOption>
                {
                    new PriceOption { OptionName = "Basic", Price = 100 },
                    new PriceOption { OptionName = "Standard", Price = 150 },
                    new PriceOption { OptionName = "Advanced", Price = 200 }
                }
            },
            new ComputerComponent
            {
                Id = 2,
                Name = "Hard Drive",
                PriceOptions = new List<PriceOption>
                {
                    new PriceOption { OptionName = "Basic", Price = 120 },
                    new PriceOption { OptionName = "Standard", Price = 180 },
                    new PriceOption { OptionName = "Advanced", Price = 250 }
                }
            },
            new ComputerComponent
            {
                Id = 3,
                Name = "CPU",
                PriceOptions = new List<PriceOption>
                {
                    new PriceOption { OptionName = "Basic", Price = 150 },
                    new PriceOption { OptionName = "Standard", Price = 250 },
                    new PriceOption { OptionName = "Advanced", Price = 350 }
                }
            },
            new ComputerComponent
            {
                Id = 4,
                Name = "Display",
                PriceOptions = new List<PriceOption>
                {
                    new PriceOption { OptionName = "Basic", Price = 200 },
                    new PriceOption { OptionName = "Standard", Price = 350 },
                    new PriceOption { OptionName = "Advanced", Price = 500 }
                }
            },
            new ComputerComponent
            {
                Id = 5,
                Name = "OS",
                PriceOptions = new List<PriceOption>
                {
                    new PriceOption { OptionName = "Basic", Price = 50 },
                    new PriceOption { OptionName = "Standard", Price = 80 },
                    new PriceOption { OptionName = "Advanced", Price = 120 }
                }
            },
            new ComputerComponent
            {
                Id = 6,
                Name = "Sound Card",
                PriceOptions = new List<PriceOption>
                {
                    new PriceOption { OptionName = "Basic", Price = 30 },
                    new PriceOption { OptionName = "Standard", Price = 50 },
                    new PriceOption { OptionName = "Advanced", Price = 80 }
                }
            },
        };

            return components;
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
                             "&totalPrice=" + _selectedComputer.GetTotalPrice();

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
