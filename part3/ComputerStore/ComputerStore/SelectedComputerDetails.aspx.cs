using System;

namespace ComputerStore
{
    public partial class SelectedComputerDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int computerId = Convert.ToInt32(Request.QueryString["computerId"]);
                int totalPrice = Convert.ToInt32(Request.QueryString["totalPrice"]);

                Computer selectedComputer = GetComputerById(computerId);

                if (selectedComputer != null)
                {
                    SelectedComputerImage.ImageUrl = selectedComputer.ImagePath;
                    SelectedComputerModel.Text = selectedComputer.Model;
                    SelectedComputerDescription.Text = selectedComputer.Description;
                    SelectedComputerPrice.Text = totalPrice.ToString();
                }
            }
        }

        private Computer GetComputerById(int computerId)
        {
            throw new NotImplementedException();
            // Implement the GetComputerById method similar to your Customize.aspx.cs page
            // ...
        }
    }
}
