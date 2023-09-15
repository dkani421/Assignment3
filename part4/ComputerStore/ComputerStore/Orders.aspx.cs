using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ComputerStore
{
    public partial class Orders : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int customerId = GetCurrentCustomerId(); // Implement a method to get the customer ID.
                //List<Order> orders = OrderRepository.GetOrdersByCustomer(customerId); // Implement this method.
                //gridViewOrders.DataSource = orders;
                gridViewOrders.DataBind();
            }
        }

        // Implement methods to get current customer ID and handle data retrieval from the database.

        protected void GridViewOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewOrder")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                // Implement logic to display order details in the pnlViewOrder panel.
                pnlViewOrder.Visible = true;
                pnlEditOrder.Visible = false;
            }
            else if (e.CommandName == "EditOrder")
            {
                int orderID = Convert.ToInt32(e.CommandArgument);
                // Implement logic to populate and display order details for editing in the pnlEditOrder panel.
                pnlEditOrder.Visible = true;
                pnlViewOrder.Visible = false;
            }
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            // Implement logic to navigate back to the list of orders.
            pnlViewOrder.Visible = false;
            pnlEditOrder.Visible = false;
        }

        protected void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            // Implement logic to update the order details in the database.
            pnlEditOrder.Visible = false;
            pnlViewOrder.Visible = false;
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            // Implement logic to cancel the order editing operation.
            pnlEditOrder.Visible = false;
            pnlViewOrder.Visible = false;
        }
    }
}
