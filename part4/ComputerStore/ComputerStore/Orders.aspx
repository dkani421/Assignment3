<%@ Page Title="Order Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="ComputerStore.Orders" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h1 id="title"><%: Title %></h1>
        
        <!-- List of Orders -->
        <h2>List of Orders</h2>
        <asp:GridView ID="gridViewOrders" runat="server" AutoGenerateColumns="true" OnRowCommand="GridViewOrders_RowCommand"></asp:GridView>
        
        <!-- View Order Details -->
        <asp:Panel ID="pnlViewOrder" runat="server" Visible="true">
            <h2>View Order Details</h2>
            <!-- Display order details here -->
            <asp:Button ID="btnBackToList" runat="server" Text="Back to List" OnClick="btnBackToList_Click" />
        </asp:Panel>
        
        <!-- Edit Order -->
        <asp:Panel ID="pnlEditOrder" runat="server" Visible="true">
            <h2>Edit Order</h2>
            <!-- Edit order details here -->
            <asp:Button ID="btnUpdateOrder" runat="server" Text="Update Order" OnClick="btnUpdateOrder_Click" />
            <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" OnClick="btnCancelEdit_Click" />
        </asp:Panel>
    </main>
</asp:Content>
