<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="CookieTracker.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Cookie Tracker Web Application.</h3>
        <p>This web application uses persistent cookie to track client hits, IP address and time zone.</p>
    </main>
</asp:Content>
