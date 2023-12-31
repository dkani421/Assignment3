﻿<%@ Page Title="Customize Computer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customize.aspx.cs" Inherits="ComputerStore.Customize" %>

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
                </asp:DropDownList>
                <br /> 

                <!-- Dropdown for Hard Drive -->
                <asp:DropDownList ID="HardDriveDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComponentDropDown_SelectedIndexChanged">
                </asp:DropDownList>
                <br />

                <!-- Dropdown for CPU -->
                <asp:DropDownList ID="CpuDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComponentDropDown_SelectedIndexChanged">
                </asp:DropDownList>
                <br />

                <!-- Dropdown for Display -->
                <asp:DropDownList ID="DisplayDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComponentDropDown_SelectedIndexChanged">
                </asp:DropDownList>
                <br />

                <!-- Dropdown for OS -->
                <asp:DropDownList ID="OsDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComponentDropDown_SelectedIndexChanged">
                </asp:DropDownList>
                <br />

                <!-- Dropdown for Sound Card -->
                <asp:DropDownList ID="SoundCardDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ComponentDropDown_SelectedIndexChanged">
                </asp:DropDownList>

                <p class="mt-2" id="ComponentPriceLabel" runat="server"></p>
                <p><strong>Total Price: $<span id="TotalPriceLabel" runat="server"></span></strong></p>
                <asp:Button ID="AddToCartButton" runat="server" Text="Add to Cart" OnClick="AddToCartButton_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
