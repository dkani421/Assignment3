using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace ComputerStore
{
    public partial class Customize : Page
    {
        private Computer _selectedComputer;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["computerId"], out int computerId))
                {
                    _selectedComputer = GetComputerById(computerId);

                    if (_selectedComputer != null)
                    {
                        SelectedComputerImage.ImageUrl = _selectedComputer.ImagePath;
                        SelectedComputerModel.Text = _selectedComputer.Model;
                        SelectedComputerDescription.Text = _selectedComputer.Description;
                        SelectedComputerPrice.Text = _selectedComputer.Price.ToString();

                        decimal totalPrice = _selectedComputer.Price;
                        TotalPriceLabel.InnerText = totalPrice.ToString();

                        ViewState["SelectedComputer"] = _selectedComputer;

                        BindDropdowns();
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
            else
            {
                _selectedComputer = ViewState["SelectedComputer"] as Computer;
            }
        }

        private void BindDropdowns()
        {
            RamDropDown.DataSource = GetComponentData("RAM");
            RamDropDown.DataTextField = "Name";
            RamDropDown.DataValueField = "Price";
            RamDropDown.DataBind();

            HardDriveDropDown.DataSource = GetComponentData("Hard Drive");
            HardDriveDropDown.DataTextField = "Name";
            HardDriveDropDown.DataValueField = "Price";
            HardDriveDropDown.DataBind();

            CpuDropDown.DataSource = GetComponentData("CPU");
            CpuDropDown.DataTextField = "Name";
            CpuDropDown.DataValueField = "Price";
            CpuDropDown.DataBind();

            DisplayDropDown.DataSource = GetComponentData("Display");
            DisplayDropDown.DataTextField = "Name";
            DisplayDropDown.DataValueField = "Price";
            DisplayDropDown.DataBind();

            OsDropDown.DataSource = GetComponentData("OS");
            OsDropDown.DataTextField = "Name";
            OsDropDown.DataValueField = "Price";
            OsDropDown.DataBind();

            SoundCardDropDown.DataSource = GetComponentData("Sound Card");
            SoundCardDropDown.DataTextField = "Name";
            SoundCardDropDown.DataValueField = "Price";
            SoundCardDropDown.DataBind();
        }

        protected List<Component> GetComponentData(string componentType)
        {
            List<Component> components = new List<Component>();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM components WHERE ComponentName LIKE @ComponentType";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ComponentType", componentType + "%");

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Component component = new Component
                            {
                                ComponentID = Convert.ToInt32(reader["ComponentID"]),
                                Name = reader["ComponentName"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                            };

                            components.Add(component);
                        }
                    }
                }
            }

            return components;
        }

        protected void ComponentDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
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
            Computer selectedComputer = null;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ComputerStoreDB"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM computers WHERE ComputerID = @ComputerId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ComputerId", computerId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            selectedComputer = new Computer
                            {
                                Id = Convert.ToInt32(reader["ComputerID"]),
                                Model = reader["ModelName"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                ImagePath = reader["ImageURL"].ToString(),
                            };
                        }
                    }
                }
            }

            return selectedComputer;
        }

        protected void AddToCartButton_Click(object sender, EventArgs e)
        {
            if (_selectedComputer != null)
            {
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

                Response.Redirect(url);
            }
        }
    }
}
