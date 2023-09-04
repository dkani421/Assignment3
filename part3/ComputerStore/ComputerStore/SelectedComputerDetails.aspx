<%@ Page Title="Selected Computer Details" Language="C#" AutoEventWireup="true" CodeBehind="SelectedComputerDetails.aspx.cs" Inherits="ComputerStore.SelectedComputerDetails" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Customized Cart</h2>
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
                <h4>Customization Details</h4>
                <!-- Display customization details for RAM -->
                <p>RAM: <asp:Label ID="RamSelectionLabel" runat="server" /></p>
                
                <!-- Display customization details for Hard Drive -->
                <p>Hard Drive: <asp:Label ID="HardDriveSelectionLabel" runat="server" /></p>
                
                <!-- Display customization details for CPU -->
                <p>CPU: <asp:Label ID="CpuSelectionLabel" runat="server" /></p>
                
                <!-- Display customization details for Display -->
                <p>Display: <asp:Label ID="DisplaySelectionLabel" runat="server" /></p>
                
                <!-- Display customization details for OS -->
                <p>OS: <asp:Label ID="OsSelectionLabel" runat="server" /></p>
                
                <!-- Display customization details for Sound Card -->
                <p>Sound Card: <asp:Label ID="SoundCardSelectionLabel" runat="server" /></p>

                  <!-- Display total price -->
                <p><strong>Total Price: $<asp:Label ID="TotalPriceLabel" runat="server" /></strong></p>
            </div>
            <div class="col-md-4">
                <h4>Customer Details</h4>
                <h6>Shipping Address:</h6>
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
            <h4>Payment Information</h4>
            <h6>Card Details:</h6>
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
