<%@ Page Title="Registration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="ComputerStore.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>New Customer Registration</h1>
        <asp:TextBox ID="txtUsername" runat="server" placeholder="Username" /><br />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" /><br />
        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
        <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
    </div>
</asp:Content>
