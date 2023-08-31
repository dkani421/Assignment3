using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace ComputerStore
{
    public partial class Customize : System.Web.UI.Page
    {
        private List<ComputerComponent> _components; // List of computer components
        private Computer _selectedComputer;
        protected Computer SelectedComputer
        {
            get { return _selectedComputer; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _components = GetComputerComponents();

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

                        // Bind the component dropdown
                        ComponentDropDown.DataSource = _components;
                        ComponentDropDown.DataTextField = "Name";
                        ComponentDropDown.DataValueField = "Name";
                        ComponentDropDown.DataBind();
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
        }

        protected void ComponentDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedComponentName = ComponentDropDown.SelectedValue;
            var selectedComponent = _components.FirstOrDefault(c => c.Name == selectedComponentName);

            if (selectedComponent != null)
            {
                _selectedComputer.SetComponent(selectedComponent);
                UpdateTotalPrice();
            }
        }

        private void UpdateTotalPrice()
        {
            int totalPrice = _selectedComputer.GetTotalPrice();
            TotalPriceLabel.InnerText = totalPrice.ToString();
        }

        protected List<ComputerComponent> GetComputerComponents()
        {
            // Simulated list of computer components with properties: Id, Name, Price
            // Replace this with your actual data retrieval logic
            return new List<ComputerComponent>
            {
                new ComputerComponent { Id = 1, Name = "RAM", Price = 100 },
                new ComputerComponent { Id = 2, Name = "HardDrive", Price = 150 },
                new ComputerComponent { Id = 3, Name = "CPU", Price = 200 },
                new ComputerComponent { Id = 4, Name = "Display", Price = 300 },
                new ComputerComponent { Id = 5, Name = "OS", Price = 50 },
                new ComputerComponent { Id = 6, Name = "SoundCard", Price = 30 }
                // Add more components here
            };
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
