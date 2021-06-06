<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Filmes.aspx.cs" Inherits="AspModulo2.Filmes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Filmes: Insira um novo filme digitando seu nome na caixa de texto."></asp:Label>
            <br />
            <asp:DropDownList ID="DDLFIlmes" runat="server">
            </asp:DropDownList>
            <asp:TextBox ID="txtvalor" runat="server" ToolTip="Digite o nome do FIlme"></asp:TextBox>
            <asp:Button ID="btnInserir" runat="server" Text="Inserir" OnClick="btnInserir_Click" />
            <br />
            <asp:Button ID="BtnEnviar" runat="server" Text="Enviar" OnClick="BtnEnviar_Click" PostBackUrl="~/RespostaFilmes.aspx" />
        </div>
    </form>
</body>
</html>
