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
                <asp:DropDownList ID="ComponentDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComponentDropDown_SelectedIndexChanged">
                    <asp:ListItem Text="Select Component" Value="" />
                    <asp:ListItem Text="RAM" Value="RAM" />
                    <asp:ListItem Text="Hard Drive" Value="HardDrive" />
                    <asp:ListItem Text="CPU" Value="CPU" />
                    <asp:ListItem Text="Display" Value="Display" />
                    <asp:ListItem Text="OS" Value="OS" />
                    <asp:ListItem Text="Sound Card" Value="SoundCard" />
                </asp:DropDownList>
                <p class="mt-2" id="ComponentPriceLabel" runat="server"></p>
                <p>Total Price: $<span id="TotalPriceLabel" runat="server"></span></p>
            </div>
        </div>
    </div>
</asp:Content>
