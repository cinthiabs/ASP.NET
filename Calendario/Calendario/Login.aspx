<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Calendario.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link href="~/css.css" rel="stylesheet" type="text/css" />
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class ="principal">
    
        <asp:Label ID="Label1" runat="server" Text="E-mail"></asp:Label>
        <br />
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Senha"></asp:Label>
        <br />
        <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="btn_Logar" runat="server" OnClick="btn_Logar_Click" Text="Entrar" />
        <br />
        <asp:Label ID="msg" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>
