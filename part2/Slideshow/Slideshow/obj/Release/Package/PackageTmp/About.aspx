<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Slideshow.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>
        <h3>Slideshow Web Application</h3>
        <p>This web application utilizes ASP.NET C# to showcase a dynamic slideshow of images accompanied by their respective captions. Users have the ability to control the slideshow's progression and mode, enhancing the interactive experience.</p>
        <p>The application stores image information in a text file and provides seamless navigation through a variety of high-quality pictures. Whether in sequential or random mode, the slideshow offers an engaging visual journey with more than 20 captivating images.</p>
    </main>
</asp:Content>


