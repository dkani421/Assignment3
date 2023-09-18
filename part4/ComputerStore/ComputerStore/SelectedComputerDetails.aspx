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
                        
            <asp:Button ID="OrderButton" runat="server" Text="Order" CssClass="btn btn-success" OnClick="OrderButton_Click" />

            </div>
        </div>
    </div>
</asp:Content>
