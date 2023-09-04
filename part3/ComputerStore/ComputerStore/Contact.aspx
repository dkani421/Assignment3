<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ComputerStore.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h1 id="title"><%: Title %>.</h1>
        <h2>Dawson's Computer Solutions</h2>
        <address>
            Calgary, AB<br />
            403.846.7777
        </address>

        <address>
            <strong>Host:</strong>  <a href="https://dawsonisawesome.com/index.html">dawsonisawesome.com</a><br />
            <strong>Support:</strong>   <a href="mailto:dawsonmkaniusis@gmail.com">dawsonmkaniusis@gmail.com</a><br />
            <strong>Marketing:</strong> <a href="mailto:dkani421@mtroyal.ca">dkani421@mtroyal.ca</a>
        </address>
    </main>
</asp:Content>
