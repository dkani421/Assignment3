<%@ Page Title="Customize Computer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customize.aspx.cs" Inherits="ComputerStore.Customize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Customize Computer</h2>
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
                <h4>Customize Components</h4>
                <!-- Dropdown for RAM -->
                <asp:DropDownList ID="RamDropDown" runat="server">
                    <asp:ListItem Text="Select RAM" Value="Ram" />
                    <asp:ListItem Text="Basic - $100" Value="100" />
                    <asp:ListItem Text="Standard - $150" Value="150" />
                    <asp:ListItem Text="Advanced - $200" Value="200" />
                </asp:DropDownList>
                <br /> <!-- Add a line break after each component -->

                <!-- Dropdown for Hard Drive -->
                <asp:DropDownList ID="HardDriveDropDown" runat="server">
                    <asp:ListItem Text="Select Hard Drive" Value="Hard Drive" />
                    <asp:ListItem Text="Basic - $120" Value="120" />
                    <asp:ListItem Text="Standard - $180" Value="180" />
                    <asp:ListItem Text="Advanced - $250" Value="250" />
                </asp:DropDownList>
                <br />

                <!-- Dropdown for CPU -->
                <asp:DropDownList ID="CpuDropDown" runat="server">
                    <asp:ListItem Text="Select CPU" Value="CPU" />
                    <asp:ListItem Text="Basic - $150" Value="150" />
                    <asp:ListItem Text="Standard - $250" Value="250" />
                    <asp:ListItem Text="Advanced - $350" Value="350" />
                </asp:DropDownList>
                <br />

                <!-- Dropdown for Display -->
                <asp:DropDownList ID="DisplayDropDown" runat="server">
                    <asp:ListItem Text="Select Display" Value="Display" />
                    <asp:ListItem Text="Basic - $200" Value="200" />
                    <asp:ListItem Text="Standard - $350" Value="350" />
                    <asp:ListItem Text="Advanced - $500" Value="500" />
                </asp:DropDownList>
                <br />

                <!-- Dropdown for OS -->
                <asp:DropDownList ID="OsDropDown" runat="server">
                    <asp:ListItem Text="Select OS" Value="OS" />
                    <asp:ListItem Text="Basic - $50" Value="50" />
                    <asp:ListItem Text="Standard - $80" Value="80" />
                    <asp:ListItem Text="Advanced - $120" Value="120" />
                </asp:DropDownList>
                <br />

                <!-- Dropdown for Sound Card -->
                <asp:DropDownList ID="SoundCardDropDown" runat="server">
                    <asp:ListItem Text="Select Sound Card" Value="Sound Card" />
                    <asp:ListItem Text="Basic - $30" Value="30" />
                    <asp:ListItem Text="Standard - $50" Value="50" />
                    <asp:ListItem Text="Advanced - $80" Value="80" />
                </asp:DropDownList>
                <p class="mt-2" id="ComponentPriceLabel" runat="server"></p>
                <p>Total Price: $<span id="TotalPriceLabel" runat="server"></span></p>
                <asp:Button ID="AddToCartButton" runat="server" Text="Add to Cart" OnClick="AddToCartButton_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
