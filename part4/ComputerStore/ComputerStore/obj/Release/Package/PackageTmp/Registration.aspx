<%@ Page Title="Registration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="ComputerStore.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Customer Registration</h1>
        <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name" /><br />
        <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name" /><br />
        <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" /><br />
        <asp:TextBox ID="txtUsername" runat="server" placeholder="Username" /><br />
        <br />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" /><br />
        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm Password" /><br />
        <br />
        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
        <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
    </div>
</asp:Content>
