<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Customer Login</h1>
        <asp:TextBox ID="txtUsername" runat="server" placeholder="Username" /><br />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" /><br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
    </div>

    <!-- Password Recovery Section -->
    <hr />
    <div>
        <h3>Password Recovery</h3>
        <asp:TextBox ID="txtUsernameRecovery" runat="server" placeholder="Username" /><br />
        <asp:Button ID="btnRecoverPassword" runat="server" Text="Recover Password" OnClick="btnRecoverPassword_Click" />
    </div>
</asp:Content>
