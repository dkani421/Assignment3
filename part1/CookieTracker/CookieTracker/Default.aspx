<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CookieTracker.Default" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Welcome to CookieTracker</h1>
    <div>
        <asp:Label ID="VisitCountLabel" runat="server" CssClass="mt-3"></asp:Label><br />
        <asp:Label ID="IPLabel" runat="server" CssClass="mt-3"></asp:Label><br />
        <asp:Label ID="TimeZoneLabel" runat="server" CssClass="mt-3"></asp:Label>
    </div>
</asp:Content>
