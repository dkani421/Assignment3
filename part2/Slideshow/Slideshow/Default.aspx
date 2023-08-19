<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Slideshow App</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <div class="container">
                <a class="navbar-brand" href="#">Slideshow App</a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item">
                            <a class="nav-link" href="Default.aspx">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="About.aspx">About</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="Contact.aspx">Contact</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
        <div class="container mt-4">
            <h1>Welcome to Slideshow App</h1>
            <div class="slideshow-container">
                <asp:Image ID="imgPicture" runat="server" CssClass="slide" />
                <div class="caption">
                    <asp:Label ID="lblCaption" runat="server"></asp:Label>
                </div>
            </div>
            <div class="controls">
                <asp:Button ID="btnStartStop" runat="server" Text="Start" OnClick="btnStartStop_Click" />
                <asp:Button ID="btnPrev" runat="server" Text="Previous" OnClick="btnPrev_Click" />
                <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
                <asp:Button ID="btnToggleMode" runat="server" Text="Sequential Mode" OnClick="btnToggleMode_Click" />
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="Scripts/script.js"></script>
</body>
</html>
