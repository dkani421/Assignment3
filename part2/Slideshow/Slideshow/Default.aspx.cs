using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    private List<Picture> LoadPicturesData()
    {
        string filePath = Server.MapPath("~/App_Data/PicturesData.txt");
        var lines = File.ReadLines(filePath);

        var pictures = lines.Select(line =>
        {
            var parts = line.Split(';');
            return new Picture
            {
                Name = parts[0],
                Location = parts[1],
                Description = parts[2]
            };
        }).ToList();

        return pictures;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var pictures = LoadPicturesData();
            Session["Pictures"] = pictures;
            Session["CurrentIndex"] = 0;
            Session["IsPlaying"] = false;
            Session["IsRandom"] = false;

            ShowSlide();
        }
    }

    private void ShowSlide()
    {
        var pictures = (List<Picture>)Session["Pictures"];
        var currentIndex = (int)Session["CurrentIndex"];

        if (currentIndex >= 0 && currentIndex < pictures.Count)
        {
            var currentPicture = pictures[currentIndex];
            imgPicture.ImageUrl = currentPicture.Location;
            lblCaption.Text = currentPicture.Description;
        }
    }

    protected void btnStartStop_Click(object sender, EventArgs e)
    {
        bool isPlaying = (bool)Session["IsPlaying"];
        Session["IsPlaying"] = !isPlaying;

        btnStartStop.Text = isPlaying ? "Start" : "Stop";

        if ((bool)Session["IsPlaying"])
        {

            // Insert a debug break here
            //System.Diagnostics.Debugger.Break();
            StartSlideshow();
        }
    }

    private void StartSlideshow()
    {
        int currentIndex = (int)Session["CurrentIndex"];
        List<Picture> pictures = (List<Picture>)Session["Pictures"];
        bool isRandom = (bool)Session["IsRandom"];

        if (!isRandom)
        {
            currentIndex = (currentIndex + 1) % pictures.Count;
        }
        else
        {
            Random random = new Random();
            currentIndex = random.Next(pictures.Count);
        }

        Session["CurrentIndex"] = currentIndex;
        ShowSlide();

        if ((bool)Session["IsPlaying"])
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "StartSlideshow", "setTimeout('__doPostBack(\'btnStartStop\',\'\')', 3000);", true);
        }
    }

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        int currentIndex = (int)Session["CurrentIndex"];
        List<Picture> pictures = (List<Picture>)Session["Pictures"];

        currentIndex = (currentIndex - 1 + pictures.Count) % pictures.Count;

        Session["CurrentIndex"] = currentIndex;
        ShowSlide();
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        int currentIndex = (int)Session["CurrentIndex"];
        List<Picture> pictures = (List<Picture>)Session["Pictures"];

        currentIndex = (currentIndex + 1) % pictures.Count;

        Session["CurrentIndex"] = currentIndex;
        ShowSlide();
    }

    protected void btnToggleMode_Click(object sender, EventArgs e)
    {
        bool isRandom = (bool)Session["IsRandom"];
        Session["IsRandom"] = !isRandom;

        btnToggleMode.Text = isRandom ? "Sequential Mode" : "Random Mode";
    }
}
