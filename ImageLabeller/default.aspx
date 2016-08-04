<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ImageLabeller._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Image Labeller</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
        
        <asp:Image ID="image" Width="500px" runat="server" />

        <asp:RadioButtonList runat="server" ID="radioButtonList" TextAlign="Right"  align="center">
            <asp:ListItem Text="People" Value="People"></asp:ListItem>
            <asp:ListItem Text="Animal" Value="Animal"></asp:ListItem>
            <asp:ListItem Text="Plant" Value="Plant"></asp:ListItem>
            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
        </asp:RadioButtonList>
        <br /><br />
        <asp:Button ID="btn" Text="Save" runat="server" OnClick="btn_Click"/>
    </div>
        <asp:Label runat="server" ID="label1"></asp:Label>
    </form>
</body>
</html>
