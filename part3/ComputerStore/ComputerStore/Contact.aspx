<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ComputerStore.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>dawsonisawesome.com</h3>
        <address>
            Calgary, AB<br />
            <abbr title="Phone">P:</abbr>
            888.888.888
        </address>

        <address>
            <strong>Support:</strong>   <a href="mailto:dawsonmkaniusis@gmail.com">dawsonmkaniusis@gmail.com</a><br />
            <strong>Marketing:</strong> <a href="mailto:dkani421@mtroyal.ca">dkani421@mtroyal.ca</a>
        </address>
    </main>
</asp:Content>
