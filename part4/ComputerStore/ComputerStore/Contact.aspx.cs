using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ComputerStore
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // Retrieve form values
                string name = Request.Form["name"];
                string email = Request.Form["email"];
                string subject = Request.Form["subject"];
                string message = Request.Form["message"];

                // Compose the email body
                string emailBody = $"Name: {name}\nDawson's Computer Solutions Feedback: {message}";

                try
                {
                    // Create a mailto URI
                    string mailtoUri = $"mailto:dawsonmkaniusis@gmail.com?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(emailBody)}";

                    // Redirect to the default email client with the pre-filled email
                    Response.Redirect(mailtoUri);
                }
                catch (Exception ex)
                {
                    // Handle any errors (e.g., log the error)
                    Response.Write($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}