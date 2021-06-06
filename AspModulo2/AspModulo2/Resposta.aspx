<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resposta.aspx.cs" Inherits="AspModulo2.Resposta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Calculo do Salario Minino</h1>
    <form id="form1" runat="server">
    <div>
    
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    
        <br />
        <asp:Button ID="Voltar" runat="server" PostBackUrl="~/Salario.aspx" Text="Voltar" />
    
    </div>
    </form>
</body>
</html>
