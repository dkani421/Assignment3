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
                decimal totalPrice;
                string ram;
                string hardDrive;
                string cpu;
                string display;
                string os;
                string soundCard;
                string totalPriceLabel; // Add a variable to store the TotalPriceLabel value

                // Check if the computerId and other query string parameters are present and can be parsed
                if (int.TryParse(Request.QueryString["computerId"], out computerId) &&
                    decimal.TryParse(Request.QueryString["totalPrice"], out totalPrice) &&
                    !string.IsNullOrEmpty(Request.QueryString["Ram"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["HardDrive"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["CPU"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["Display"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["OS"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["SoundCard"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["TotalPriceLabel"])) // Retrieve TotalPriceLabel value
                {
                    Computer selectedComputer = GetComputerById(computerId);
                    ram = Request.QueryString["Ram"];
                    hardDrive = Request.QueryString["HardDrive"];
                    cpu = Request.QueryString["CPU"];
                    display = Request.QueryString["Display"];
                    os = Request.QueryString["OS"];
                    soundCard = Request.QueryString["SoundCard"];
                    totalPriceLabel = Request.QueryString["TotalPriceLabel"]; // Retrieve TotalPriceLabel value

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
