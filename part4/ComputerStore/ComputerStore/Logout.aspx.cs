using System;
using System.Web.Security;

namespace ComputerStore
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Log the user out and redirect to the login page
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }
    }
}
