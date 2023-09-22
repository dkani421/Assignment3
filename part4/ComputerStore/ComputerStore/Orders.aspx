<%@ Page Title="Order Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="ComputerStore.Orders" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h1 id="title"><%: Title %></h1>

        <!-- List of Orders -->
        <h2>List of Orders</h2>
        <hr />
        <asp:GridView ID="gridViewOrders" runat="server" DataKeyNames="OrderID" AutoGenerateColumns="false" OnRowCommand="GridViewOrders_RowCommand">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:MM/dd/yyyy}" />
                <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" DataFormatString="{0:C}" />
                <asp:TemplateField HeaderText="View Order">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkViewOrder" runat="server" CommandName="ViewOrder" CommandArgument='<%# Eval("OrderID") %>'>View</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit Order">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditOrder" runat="server" CommandName="EditOrder" CommandArgument='<%# Eval("OrderID") %>'>Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete Order">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDeleteOrder" runat="server" CommandName="DeleteOrder" CommandArgument='<%# Eval("OrderID") %>'>Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblNoOrders" runat="server" Text="No orders made by this user." Visible="false"></asp:Label>

        <hr />

        <!-- View Order Details -->
        <asp:Panel ID="pnlViewOrder" runat="server" Visible="false">
            <h2>View Order Details</h2>
            <hr />
            <div>
                <strong>Order ID:</strong>
                <asp:Label ID="lblViewOrderID" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <strong>Order Date:</strong>
                <asp:Label ID="lblViewOrderDate" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <strong>Total Price:</strong>
                <asp:Label ID="lblViewTotalPrice" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <!-- GridView to display order details -->
            <asp:GridView ID="gridViewOrderDetails" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="ComponentName" HeaderText="Component Name" />
                    <asp:BoundField DataField="ComponentPrice" HeaderText="Component Price" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>

            <hr />
            <asp:Button ID="btnBackToList" runat="server" Text="Back to List" OnClick="btnBackToList_Click" />
            <hr />
        </asp:Panel>

        <!-- Edit Order -->
        <asp:Panel ID="pnlEditOrder" runat="server" Visible="false">
    <h2>Edit Order</h2>
    <hr />
    <div>
        <strong>Order ID:</strong>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
    <div>
        <strong>Order Date:</strong>
        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    </div>
    <div>
        <strong>Total Price:</strong>
        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <!-- Dropdown lists for component selection -->
<div style="margin-bottom: 10px;">
    <strong style="font-weight: bold;">Ram:</strong>
    <asp:DropDownList ID="ddlRam" runat="server" style="width: 100%;"></asp:DropDownList>
</div>
<div style="margin-bottom: 10px;">
    <strong style="font-weight: bold;">Hard Drive:</strong>
    <asp:DropDownList ID="ddlHardDrive" runat="server" style="width: 100%;"></asp:DropDownList>
</div>
<div style="margin-bottom: 10px;">
    <strong style="font-weight: bold;">CPU:</strong>
    <asp:DropDownList ID="ddlCPU" runat="server" style="width: 100%;"></asp:DropDownList>
</div>
<div style="margin-bottom: 10px;">
    <strong style="font-weight: bold;">Display:</strong>
    <asp:DropDownList ID="ddlDisplay" runat="server" style="width: 100%;"></asp:DropDownList>
</div>
<div style="margin-bottom: 10px;">
    <strong style="font-weight: bold;">OS:</strong>
    <asp:DropDownList ID="ddlOS" runat="server" style="width: 100%;"></asp:DropDownList>
</div>
<div style="margin-bottom: 10px;">
    <strong style="font-weight: bold;">Sound Card:</strong>
    <asp:DropDownList ID="ddlSoundCard" runat="server" style="width: 100%;"></asp:DropDownList>
</div>

    <hr />
    <asp:Button ID="btnUpdateOrder" runat="server" Text="Update Order" OnClick="btnUpdateOrder_Click" />
    <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" OnClick="btnCancelEdit_Click" />
    <div>
        <strong>Update Status:</strong>
        <asp:Label ID="lblUpdateStatus" runat="server" Text="" ForeColor="Green"></asp:Label>
    </div>
    <hr />
</asp:Panel>

    </main>
</asp:Content>
