<%@ Page Title="Selected Computer Details" Language="C#" AutoEventWireup="true" CodeBehind="SelectedComputerDetails.aspx.cs" Inherits="ComputerStore.SelectedComputerDetails" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Selected Computer Details</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <h2>Selected Computer Details</h2>
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
            </div>
        </div>
    </form>
</body>
</html>
