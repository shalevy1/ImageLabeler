<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ImageLabeller._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resim Etiketleme</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
</head>
<body>
	<div style="text-align:center">
    	<form id="login" runat="server" visible="true">
        	<div>
				<br />
				<br />
				<br />
				<br />
				
            	<asp:Label>Password : </asp:Label>
            	<asp:TextBox ID="txtPass" TextMode="Password" runat="server"></asp:TextBox>
           		<br />
				<br />
           		<asp:Button ID="btnPass" Text="Login" runat="server" OnClick="btnPass_Click" CssClass="btn btn-primary"/>
        	</div>
		</form>
	</div>
    <form id="form1" runat="server" visible="false">
        
    <div style="text-align:center">
       <h4 style="color:orangered">Image Labeller</h4>
        <asp:Image ID="image" Width="300px" runat="server" />
		<br />
		<br />
        <asp:RadioButtonList runat="server" ID="radioButtonList" TextAlign="Right" AutoPostBack="false" OnSelectedIndexChanged="radioButtonList_SelectedIndexChanged" align="center">
            <asp:ListItem Text="Include Text" Value="IncludeText"></asp:ListItem>
			
            <asp:ListItem Text="Not Include Text" Value="NotIncludeText"></asp:ListItem>

            <asp:ListItem Text="Adult Content" Value="AdultContent"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:TextBox runat="server" ID="otherTextBox" Visible="false" />
        <br /><br />
        <asp:Button ID="btn" Text="Save" runat="server" OnClick="btn_Click" CssClass="btn btn-primary"/>
        <asp:Button Text="Delete" ID="skipBtn" OnClick="skipBtn_Click" runat="server" CssClass="btn btn-danger" /><br />
        <br />    
        <asp:LinkButton ID="reportImage" Text="Report Image" OnClick="reportImage_Click" runat="server"></asp:LinkButton>
        <br />
        <br />
        <asp:Label runat="server" ID="lblCount"></asp:Label>
    </div>
        
        
    </form>
</body>
</html>
