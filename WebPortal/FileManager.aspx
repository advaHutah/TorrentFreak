<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileManager.aspx.cs" Inherits="WebPortal.FileManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <img src="logo.png" alt="" />
    <form id="form1" runat="server">
    <div>
    
        Files List:</div>
        <asp:GridView ID="FilesGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
                <asp:BoundField DataField="FileName" HeaderText="FileName" SortExpression="FileName" />
                <asp:BoundField DataField="FileSize" HeaderText="FileSize" SortExpression="FileSize" />
                <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnectionString %>" SelectCommand="SELECT * FROM [Files]"></asp:SqlDataSource>
        <br />
        <br />
      Active Users:&nbsp;&nbsp;
        <asp:TextBox ID="activeUsersText" runat="server" Enabled="False"></asp:TextBox>
        <br />
        <br />
        Total Users:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="totalUsersText0" runat="server" Enabled="False"></asp:TextBox>
    
        <p>
            Total Files:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="totalFilestext" runat="server" Enabled="False"></asp:TextBox>
    
        </p>
        <p>
            Search File&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="searchTxt" runat="server"></asp:TextBox>
    
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
        </p>
        <p>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </p>
        <p>
            <asp:HyperLink ID="menu_HyperLink" runat="server" NavigateUrl="~/HomePage.html">Back To Main Menu</asp:HyperLink>
        </p>
    
    </form>
</body>
</html>
