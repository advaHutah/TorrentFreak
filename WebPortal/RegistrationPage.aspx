<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationPage.aspx.cs" Inherits="WebPortal.RegistrationPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <img src="logo.png" alt="" />
    <form id="form1" runat="server">
        Create New User:<br />
        <br />
        User Name:&nbsp;
        <asp:TextBox ID="userName_TextBox" runat="server"></asp:TextBox>
        <br />
        <br />Password:&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="password_TextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="register_Button" runat="server" OnClick="register_Button_Click" Text="Register" />
        <br />
        <br />
        <br />
        <asp:Label ID="statusRegestraionLable" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        <asp:HyperLink ID="menu_HyperLink" runat="server" NavigateUrl="~/HomePage.html">Back To Main Menu</asp:HyperLink>
        <br />
    </form>
</body>
</html>
