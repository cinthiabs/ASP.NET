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
          
                <br />
                <asp:Label ID="sessionID" runat="server" Text="Session ID:"></asp:Label>
                <asp:TextBox ID="txtSession" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="Label1" runat="server" Text="Contador:"></asp:Label>
                <asp:TextBox ID="txtcontador" runat="server"></asp:TextBox>
                <asp:Button ID="Btlink" runat="server" OnClick="Btlink_Click" Text="Adicionar" />
                <asp:Button ID="btnRemove" runat="server" OnClick="btnRemove_Click" Text="Remover" />
                <br />
                <br />
                <asp:Label ID="Label2" runat="server" Text="Contador na Aplicação:"></asp:Label>
                <asp:TextBox ID="txtcontadorapp" runat="server"></asp:TextBox>
                <asp:Button ID="BtnAdicionar" runat="server" OnClick="BtnAdicionar_Click" Text="Adicionar" />
          
        </div>
    </form>
</body>
</html>
