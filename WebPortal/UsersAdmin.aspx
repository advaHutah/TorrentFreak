<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsersAdmin.aspx.cs" Inherits="WebPortal.UsersAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <img src="logo.png" alt="" />
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Username" DataSourceID="LinqDataSource1">
            <Columns>
                <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" ReadOnly="True" />
                <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                <asp:CheckBoxField DataField="isConnected" HeaderText="isConnected" SortExpression="isConnected" />
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="DatabaseLibrary.TorrentFreakDBLinkDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="Users">
        </asp:LinqDataSource>
        <br />
        <br />
        <asp:HyperLink ID="menu_HyperLink" runat="server" NavigateUrl="~/HomePage.html">Back To Main Menu</asp:HyperLink>
    </form>
</body>
</html>
