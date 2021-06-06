    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="triangulo.aspx.cs" Inherits="Web.triangulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Lado A"></asp:Label>
        <br />
        <asp:TextBox ID="txtladoA" runat="server" Text="10"></asp:TextBox>
        <br />
         <asp:Label ID="Label2" runat="server" Text="Lado B"></asp:Label>
        <br />
         <asp:TextBox ID="txtladoB" runat="server" Text="10"></asp:TextBox>
        <br />
          <asp:Label ID="Label3" runat="server" Text="Lado C"></asp:Label>
        <br />
         <asp:TextBox ID="txtladoC" runat="server" Text="10"></asp:TextBox>
        <br />
        <asp:Button ID="BtnVerificar" runat="server" Text="Verificar" OnClick="BtnVerificar_Click" />
        <br />
          <asp:Label ID="Resposta" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
