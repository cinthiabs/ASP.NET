<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="AspModulo2.Principal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
                <asp:Label ID="LbLogin" runat="server" Text="Label"></asp:Label>
          
                <br />
                <asp:Button ID="BtExecutar" runat="server" Text="Apagar Cookie" OnClick="BtExecutar_Click" />
          
                <asp:Button ID="BtListar" runat="server" Text="Listar Cookie" OnClick="BtListar_Click" />
          
        </div>
    </form>
</body>
</html>
