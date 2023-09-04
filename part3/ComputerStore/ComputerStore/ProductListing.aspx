<%@ Page Title="Product Listing" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductListing.aspx.cs" Inherits="ComputerStore.ProductListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h1>Product Listing</h1>
        <div class="row">
            <asp:Repeater ID="ComputerRepeater" runat="server">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card">
                            <img src='<%# Eval("ImagePath") %>' alt='<%# Eval("Model") %>' class="card-img-top">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Model") %></h5>
                                <p class="card-text"><%# Eval("Description") %></p>
                                <p class="card-text"><strong>Price: $<span id="price_<%# Container.ItemIndex %>"><%# Eval("Price") %></span></strong></p>
                                <a href='<%# "Customize.aspx?computerId=" + Eval("Id") %>' class="btn btn-primary">Customize</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
