<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ComputerStore._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <section class="row" aria-labelledby="storeTitle">
            <h1 id="storeTitle">Welcome to Your Online Computer Store</h1>
            <p class="lead">Explore our wide range of custom computers and components.</p>
        </section>
        <p><a href="ProductListing.aspx" class="btn btn-primary btn-md">View Products &raquo;</a></p>
        <div class="row">
            <section class="col-md-4" aria-labelledby="aboutTitle">
                <h2 id="aboutTitle">About Us</h2>
                <p>
                    Discover the story behind our commitment to delivering top-notch computing solutions.
                </p>
                <p>
                    <a class="btn btn-default" href="About.aspx">Learn more &raquo;</a>
                </p>
            </section>
            <section class="col-md-4" aria-labelledby="contactTitle">
                <h2 id="contactTitle">Contact Us</h2>
                <p>
                    Have questions or need assistance? Feel free to reach out to our dedicated support team.
                </p>
                <p>
                    <a class="btn btn-default" href="ContactUs.aspx">Get in touch &raquo;</a>
                </p>
            </section>
            <section class="col-md-4" aria-labelledby="customizationTitle">
                <h2 id="customizationTitle">Customization Options</h2>
                <p>
                    Customize your dream computer with top-tier components to suit your needs.
                </p>
                <p>
                    <a class="btn btn-default" href="ProductListing.aspx">Explore options &raquo;</a>
                </p>
            </section>
        </div>
    </main>
</asp:Content>
