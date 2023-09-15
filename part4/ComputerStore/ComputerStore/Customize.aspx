<%@ Page Title="Customize Computer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customize.aspx.cs" Inherits="ComputerStore.Customize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h1>Customize Computer</h1>
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
                <h3>Customize Components</h3>
                <!-- Dropdown for RAM -->
                <asp:DropDownList ID="RamDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComponentDropDown_SelectedIndexChanged">
                    <asp:ListItem Text="No RAM" Value="0" />
                    <asp:ListItem Text="Basic - $100" Value="100" />
                    <asp:ListItem Text="Standard - $150" Value="150" />
                    <asp:ListItem Text="Advanced - $200" Value="200" />
                </asp:DropDownList>
                <br /> 

                <!-- Dropdown for Hard Drive -->
                <asp:DropDownList ID="HardDriveDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComponentDropDown_SelectedIndexChanged">
                    <asp:ListItem Text="No Hard Drive" Value="0" />
                    <asp:ListItem Text="Basic - $120" Value="120" />
                    <asp:ListItem Text="Standard - $180" Value="180" />
                    <asp:ListItem Text="Advanced - $250" Value="250" />
                </asp:DropDownList>
                <br />

                <!-- Dropdown for CPU -->
                <asp:DropDownList ID="CpuDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComponentDropDown_SelectedIndexChanged">
                    <asp:ListItem Text="No CPU" Value="0" />
                    <asp:ListItem Text="Basic - $150" Value="150" />
                    <asp:ListItem Text="Standard - $250" Value="250" />
                    <asp:ListItem Text="Advanced - $350" Value="350" />
                </asp:DropDownList>
                <br />

                <!-- Dropdown for Display -->
                <asp:DropDownList ID="DisplayDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComponentDropDown_SelectedIndexChanged">
                    <asp:ListItem Text="No Display" Value="0" />
                    <asp:ListItem Text="Basic - $200" Value="200" />
                    <asp:ListItem Text="Standard - $350" Value="350" />
                    <asp:ListItem Text="Advanced - $500" Value="500" />
                </asp:DropDownList>
                <br />

                <!-- Dropdown for OS -->
                <asp:DropDownList ID="OsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComponentDropDown_SelectedIndexChanged">
                    <asp:ListItem Text="No OS" Value="0" />
                    <asp:ListItem Text="Basic - $50" Value="50" />
                    <asp:ListItem Text="Standard - $80" Value="80" />
                    <asp:ListItem Text="Advanced - $120" Value="120" />
                </asp:DropDownList>
                <br />

                <!-- Dropdown for Sound Card -->
                <asp:DropDownList ID="SoundCardDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComponentDropDown_SelectedIndexChanged">
                    <asp:ListItem Text="No Sound Card" Value="0" />
                    <asp:ListItem Text="Basic - $30" Value="30" />
                    <asp:ListItem Text="Standard - $50" Value="50" />
                    <asp:ListItem Text="Advanced - $80" Value="80" />
                </asp:DropDownList>

                <p class="mt-2" id="ComponentPriceLabel" runat="server"></p>
                <p><strong>Total Price: $<span id="TotalPriceLabel" runat="server"></span></strong></p>
                <asp:Button ID="AddToCartButton" runat="server" Text="Add to Cart" OnClick="AddToCartButton_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
