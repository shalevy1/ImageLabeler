<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ImageLabeller._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Image Labeller</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        
    <div style="text-align:center">
       <h4 style="color:orangered">Image Labeller</h4>
        <asp:Image ID="image" Width="400px" runat="server" />

        <asp:RadioButtonList runat="server" ID="radioButtonList" TextAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="radioButtonList_SelectedIndexChanged" align="center">
            <asp:ListItem Text="People" Value="People"></asp:ListItem>
            <asp:ListItem Text="Animal" Value="Animal"></asp:ListItem>
            <asp:ListItem Text="Plant" Value="Plant"></asp:ListItem>
            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:TextBox runat="server" ID="otherTextBox" Visible="false" />
        <br /><br />
        <asp:Button ID="btn" Text="Save" runat="server" OnClick="btn_Click" CssClass="btn btn-primary"/>
        <asp:Button Text="Remove" ID="skipBtn" OnClick="skipBtn_Click" runat="server" CssClass="btn btn-danger" />
    </div>
        
        <asp:Label runat="server" ID="label1"></asp:Label>
    </form>
</body>
</html>
