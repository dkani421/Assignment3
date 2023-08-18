using System;
using System.Web;

namespace CookieTracker
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user has visited before
                if (Request.Cookies["VisitCount"] != null)
                {
                    int visitCount = int.Parse(Request.Cookies["VisitCount"].Value);
                    visitCount++;
                    Response.Cookies["VisitCount"].Value = visitCount.ToString();
                }
                else
                {
                    // First visit
                    Response.Cookies["VisitCount"].Value = "1";
                    Response.Cookies["VisitCount"].Expires = DateTime.Now.AddYears(1);
                }

                // Display IP address and time zone
                string ipAddress = HttpContext.Current.Request.UserHostAddress;
                string timeZone = TimeZoneInfo.Local.StandardName;

                VisitCountLabel.Text = $"You have visited {Request.Cookies["VisitCount"].Value} times.";
                IPLabel.Text = $"Your IP address: {ipAddress}";
                TimeZoneLabel.Text = $"Your time zone: {timeZone}";
            }
        }
    }
}
