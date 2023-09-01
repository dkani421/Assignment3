using System;
using System.Collections.Generic;

namespace ComputerStore
{
    public partial class SelectedComputerDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int computerId;
                int totalPrice;

                // Check if the computerId and totalPrice query string parameters are present and can be parsed
                if (int.TryParse(Request.QueryString["computerId"], out computerId) && int.TryParse(Request.QueryString["totalPrice"], out totalPrice))
                {
                    Computer selectedComputer = GetComputerById(computerId);

                    if (selectedComputer != null)
                    {
                        SelectedComputerImage.ImageUrl = selectedComputer.ImagePath;
                        SelectedComputerModel.Text = selectedComputer.Model;
                        SelectedComputerDescription.Text = selectedComputer.Description;
                        SelectedComputerPrice.Text = totalPrice.ToString();
                    }
                }
                else
                {
                    // Handle the case where the parameters are missing or cannot be parsed
                    // You can display an error message or redirect to an error page.
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
