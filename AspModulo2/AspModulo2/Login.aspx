<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AspModulo2.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="Panel1" runat="server">
                <asp:Label ID="Label1" runat="server" Text="Login"></asp:Label>
                <br />
                <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Senha:"></asp:Label>
                <br />
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
                <br />
                <asp:Button ID="btExecutar" runat="server" OnClick="btExecutar_Click" Text="Login" />
            </asp:Panel>
        </div>
    </form>
</body>
</html>
