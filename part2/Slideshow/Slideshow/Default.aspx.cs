using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;

public partial class _Default : Page
{
    private List<Picture> pictures;
    private int currentIndex = 0;
    private bool isPlaying = false;
    private bool isRandom = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPicturesData();
            Session["Pictures"] = pictures;

            // Initialize session variables
            Session["CurrentIndex"] = 0;
            Session["IsPlaying"] = false;
            Session["IsRandom"] = false;
        }
        else
        {
            pictures = (List<Picture>)Session["Pictures"];
            currentIndex = (int)Session["CurrentIndex"];
            isPlaying = (bool)Session["IsPlaying"];
            isRandom = (bool)Session["IsRandom"];
        }

        ShowSlide();
    }

    private void LoadPicturesData()
    {
        string filePath = Server.MapPath("~/App_Data/PicturesData.txt");
        var lines = File.ReadLines(filePath);

        pictures = lines.Select(line =>
        {
            var parts = line.Split(';');
            return new Picture
            {
                Name = parts[0],
                Location = parts[1],
                Description = parts[2]
            };
        }).ToList();
    }

    private void ShowSlide()
    {
        var currentPicture = pictures[currentIndex];
        imgPicture.ImageUrl = currentPicture.Location;
        lblCaption.Text = currentPicture.Description;
    }

    protected void btnStartStop_Click(object sender, EventArgs e)
    {
        isPlaying = !isPlaying;
        Session["IsPlaying"] = isPlaying;

        btnStartStop.Text = isPlaying ? "Stop" : "Start";

        if (isPlaying)
        {
            timer.Enabled = true;
            ShowSlide();
        }
        else
        {
            timer.Enabled = false;
        }
    }

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        currentIndex = (currentIndex - 1 + pictures.Count) % pictures.Count;
        Session["CurrentIndex"] = currentIndex;
        ShowSlide();
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        currentIndex = (currentIndex + 1) % pictures.Count;
        Session["CurrentIndex"] = currentIndex;
        ShowSlide();
    }

    protected void btnToggleMode_Click(object sender, EventArgs e)
    {
        isRandom = !isRandom;
        Session["IsRandom"] = isRandom;

        btnToggleMode.Text = isRandom ? "Sequential Mode" : "Random Mode";
    }

    protected void timer_Tick(object sender, EventArgs e)
    {
        if (!isPlaying)
        {
            return;
        }

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
    }
}
