<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ComputerStore.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h1 id="title"><%: Title %></h1>
        <div class="col-md-8">
        <h2>Dawson's Computer Solutions</h2>
            <address>
                Calgary, AB<br />
                403.846.7777
            </address>

            <address>
                <strong>Host:</strong>  <a href="https://dawsonisawesome.com/index.html">dawsonisawesome.com</a><br />
                <strong>Support:</strong>   <a href="mailto:dawsonmkaniusis@gmail.com">dawsonmkaniusis@gmail.com</a><br />
                <strong>Marketing:</strong> <a href="mailto:dkani421@mtroyal.ca">dkani421@mtroyal.ca</a>
            </address>
        </div>
        <div class="col-md-4">
            <h2>Customer Feedback</h2>
                <form action="SubmitFeedback.aspx" method="post">
                    <div>
                        <label for="name"><strong>Name:</strong></label>
                        <input type="text" id="name" name="name" required />
                    </div>
                    <div>
                        <label for="email"><strong>Email:</strong></label>
                        <input type="email" id="email" name="email" required />
                    </div>
                    <div>
                        <label for="subject"><strong>Subject:</strong></label>
                        <input type="text" id="subject" name="subject" required />
                    </div>
                    <div>
                        <label for="message"><strong>Message:</strong></label>
                        <textarea id="message" name="message" rows="4" required></textarea>
                    </div>
                    <div>
                        <input type="submit" value="Submit" />
                    </div>
                </form>
        </div>
    </main>
</asp:Content>
