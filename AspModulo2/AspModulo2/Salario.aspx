<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Salario.aspx.cs" Inherits="AspModulo2.Salario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Informe o Salario Bruto:"></asp:Label>
        <br />
        <asp:TextBox ID="txtsalario" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Percentual de Desconto:"></asp:Label>
        <br />
        <asp:RadioButtonList ID="RBDesconto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="10">10%</asp:ListItem>
            <asp:ListItem Value="20">20%</asp:ListItem>
            <asp:ListItem Value="30">30%</asp:ListItem>
            <asp:ListItem>Outro</asp:ListItem>
        </asp:RadioButtonList>
        <asp:TextBox ID="txtdesconto" runat="server" Visible="False"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btn_Executar" runat="server" PostBackUrl="~/Resposta.aspx?Nome=Cinthia" Text="Executar" />
    
    </div>
    </form>
</body>
</html>
