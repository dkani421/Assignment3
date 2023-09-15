<%@ Page Title="Selected Computer Details" Language="C#" AutoEventWireup="true" CodeBehind="SelectedComputerDetails.aspx.cs" Inherits="ComputerStore.SelectedComputerDetails" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h1>Customized Cart</h1>
        <div class="row">
            <div class="col-md-4">
                <div class="card">
                    <asp:Image ID="SelectedComputerImage" runat="server" CssClass="card-img-top" />
                    <div class="card-body">
                        <h5 class="card-title"><asp:Label ID="SelectedComputerModel" runat="server" /></h5>
                        <p class="card-text"><asp:Label ID="SelectedComputerDescription" runat="server" /></p>
                        <p class="card-text"><strong>Price: $<span><asp:Label ID="SelectedComputerPrice" runat="server" /></span></strong></p>
                    </div>
                </div>
            </div>
             <div class="col-md-8">
                <h3>Customization Details</h3>
                <p>RAM: <asp:Label ID="RamSelectionLabel" runat="server" /></p>
                <p>Hard Drive: <asp:Label ID="HardDriveSelectionLabel" runat="server" /></p>
                <p>CPU: <asp:Label ID="CpuSelectionLabel" runat="server" /></p>
                <p>Display: <asp:Label ID="DisplaySelectionLabel" runat="server" /></p>
                <p>OS: <asp:Label ID="OsSelectionLabel" runat="server" /></p>
                <p>Sound Card: <asp:Label ID="SoundCardSelectionLabel" runat="server" /></p>
                <p><strong>Total Price: $<asp:Label ID="TotalPriceLabel" runat="server" /></strong></p>
            </div>
            <div class="col-md-4">
                <h3>Customer Details</h3>
                <h5>Shipping Address:</h5>
                <div class="form-group">
                    <label for="FullName">Full Name</label>
                    <asp:TextBox ID="FullName" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="Address">Address</label>
                    <asp:TextBox ID="Address" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="City">City</label>
                    <asp:TextBox ID="City" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="State">State</label>
                    <asp:TextBox ID="State" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="ZipCode">Zip Code</label>
                    <asp:TextBox ID="ZipCode" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="col-md-4">
            <h3>Payment Information</h3>
            <h5>Card Details:</h5>
            <div class="form-group">
                <label for="CardNumber">Card Number</label>
                <asp:TextBox ID="CardNumber" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="ExpirationDate">Expiration Date</label>
                <asp:TextBox ID="ExpirationDate" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="CVV">CVV</label>
                <asp:TextBox ID="CVV" runat="server" CssClass="form-control" />
            </div>
            <asp:Button ID="Button1" runat="server" Text="Checkout" CssClass="btn btn-success" />
        </div>
        </div>
    </div>
</asp:Content>
