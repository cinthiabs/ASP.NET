<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParouImpar.aspx.cs" Inherits="cursoaula01.ParouImpar" %>

<%@ Register src="WebUserControlMenu.ascx" tagname="WebUserControlMenu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 140px">
    <form id="form1" runat="server">
    <div>
    
        <uc1:WebUserControlMenu ID="WebUserControlMenu1" runat="server" />
    
        <asp:BulletedList ID="BlLista" runat="server" BorderStyle="None" BulletStyle="Numbered" OnClick="BlLista_Click" DisplayMode="LinkButton">
            <asp:ListItem>Par ou Impar</asp:ListItem>
            <asp:ListItem>Calcular o Fatorial</asp:ListItem>
        </asp:BulletedList>
        <br />
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
            <asp:ListItem>Par ou Impar</asp:ListItem>
            <asp:ListItem>Fatorial</asp:ListItem>
        </asp:RadioButtonList>
            <asp:Panel ID="pnParouImpar" runat="server" Visible="False">
                <br />
                Verifica se o numero informado é par ou impar<br />
                <asp:TextBox ID="txtValorpn1" runat="server"></asp:TextBox>
                <asp:Button ID="BtnVerifica" runat="server" OnClick="BtnVerifica_Click" Text="Verifica" />
                <br />
                <asp:Label ID="Lresp1" runat="server"></asp:Label>
            </asp:Panel>
         <br />
            <asp:Panel ID="PnFatorial" runat="server" Visible="False">
                <asp:Label ID="Label3" runat="server" Text="Calcula o fatorial de um numero"></asp:Label>
                <br />
                <asp:TextBox ID="txtValorpn2" runat="server"></asp:TextBox>
                <asp:Button ID="btnCalcular" runat="server" Text="Calcular" OnClick="btnCalcular_Click" />
                <br />
                <asp:Label ID="Lresp2" runat="server"></asp:Label>
            </asp:Panel>
        </div>
        </form>
       <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/PaginaInicial.aspx">Pagina Inicial</asp:HyperLink>
</body>
</html>
