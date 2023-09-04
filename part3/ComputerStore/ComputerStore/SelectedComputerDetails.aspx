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
                        <p class="card-text">Price: $<span><asp:Label ID="SelectedComputerPrice" runat="server" /></span></p>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <h4>Customer Details</h4>
                <h6>Shipping Address</h6>
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
                <asp:Button ID="CheckoutButton" runat="server" Text="Checkout" CssClass="btn btn-success" />
            </div>
        </div>
    </div>
</asp:Content>
