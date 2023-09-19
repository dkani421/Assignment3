<%@ Page Title="Order Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="ComputerStore.Orders" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h1 id="title"><%: Title %></h1>

        <!-- List of Orders -->
        <h2>List of Orders</h2>
        <hr />
        <asp:GridView ID="gridViewOrders" runat="server" AutoGenerateColumns="false" OnRowCommand="GridViewOrders_RowCommand">
        <Columns>
            <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
            <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" DataFormatString="{0:C}" />
            <asp:ButtonField ButtonType="Button" CommandName="ViewOrder" Text="View" HeaderText="View Order" />
            <asp:ButtonField ButtonType="Button" CommandName="EditOrder" Text="Edit" HeaderText="Edit Order" />
        </Columns>
    </asp:GridView>

        <hr />

        <!-- View Order Details -->
        <asp:Panel ID="pnlViewOrder" runat="server" Visible="false">
            <h2>View Order Details</h2>
            <hr />
            <!-- Display order details here -->
            <asp:Button ID="btnBackToList" runat="server" Text="Back to List" OnClick="btnBackToList_Click" />
            <hr />
        </asp:Panel>

        <!-- Edit Order -->
        <asp:Panel ID="pnlEditOrder" runat="server" Visible="false">
            <h2>Edit Order</h2>
            <hr />
            <!-- Edit order details here -->
            <asp:Button ID="btnUpdateOrder" runat="server" Text="Update Order" OnClick="btnUpdateOrder_Click" />
            <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" OnClick="btnCancelEdit_Click" />
            <hr />
        </asp:Panel>
    </main>
</asp:Content>
