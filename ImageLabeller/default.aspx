<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ImageLabeller._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resim Etiketleme</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="login" runat="server" visible="true">
        <div>
            <asp:Label>Şifre</asp:Label>
            <asp:TextBox ID="txtPass" TextMode="Password" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnPass" Text="Giriş" runat="server" OnClick="btnPass_Click" CssClass="btn btn-primary"/>
        </div>



    </form>
    <form id="form1" runat="server" visible="false">
        
    <div style="text-align:center">
       <h4 style="color:orangered">Image Labeller</h4>
        <asp:Image ID="image" Width="200px" runat="server" />

        <asp:RadioButtonList runat="server" ID="radioButtonList" TextAlign="Right" AutoPostBack="false" OnSelectedIndexChanged="radioButtonList_SelectedIndexChanged" align="center">
            <asp:ListItem Text="Yazı İçerir" Value="IncludeText"></asp:ListItem>
            <asp:ListItem Text="Yazı İçermez" Value="NotIncludeText"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:TextBox runat="server" ID="otherTextBox" Visible="false" />
        <br /><br />
        <asp:Button ID="btn" Text="Kaydet" runat="server" OnClick="btn_Click" CssClass="btn btn-primary"/>
        <asp:Button Text="Sil" ID="skipBtn" OnClick="skipBtn_Click" runat="server" CssClass="btn btn-danger" /><br />
        <br /><br />    
        <asp:Label runat="server" ID="lblCount"></asp:Label>
    </div>
        
        
    </form>
</body>
</html>
