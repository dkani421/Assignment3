<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Timer ID="timer" runat="server" Interval="3000" OnTick="timer_Tick" Enabled="false" />
</asp:Content>
