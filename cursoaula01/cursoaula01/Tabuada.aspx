    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tabuada.aspx.cs" Inherits="cursoaula01.Tabuada" %>

<%@ Register src="WebUserControlMenu.ascx" tagname="WebUserControlMenu" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
    <div>
        <uc1:WebUserControlMenu ID="WebUserControlMenu1" runat="server" />
        <h1>Tabuada</h1>
    </div>
        <asp:DropDownList ID="dlNumeros" runat="server" Height="19px" Width="163px">
        </asp:DropDownList>
        <asp:Button ID="BtnExecutar" runat="server" OnClick="BtnExecutar_Click" Text="Exibir a Tabuada" />
        <br />
        <br />
        <asp:ListBox ID="LbDados" runat="server" Height="194px" Width="161px"></asp:ListBox>
        <br />
        <asp:Table ID="tbDados" runat="server" Height="216px" Width="190px" BackColor="#66FF99" CellPadding="3">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server">X</asp:TableCell>
                <asp:TableCell runat="server">0</asp:TableCell>
                <asp:TableCell runat="server">=</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server">X</asp:TableCell>
                <asp:TableCell runat="server">1</asp:TableCell>
                <asp:TableCell runat="server">=</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                 <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server">X</asp:TableCell>
                <asp:TableCell runat="server">2</asp:TableCell>
                <asp:TableCell runat="server">=</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                 <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server">X</asp:TableCell>
                <asp:TableCell runat="server">3</asp:TableCell>
                <asp:TableCell runat="server">=</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                 <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server">X</asp:TableCell>
                <asp:TableCell runat="server">4</asp:TableCell>
                <asp:TableCell runat="server">=</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                 <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server">X</asp:TableCell>
                <asp:TableCell runat="server">5</asp:TableCell>
                <asp:TableCell runat="server">=</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                 <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server">X</asp:TableCell>
                <asp:TableCell runat="server">6</asp:TableCell>
                <asp:TableCell runat="server">=</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                 <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server">X</asp:TableCell>
                <asp:TableCell runat="server">7</asp:TableCell>
                <asp:TableCell runat="server">=</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                 <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server">X</asp:TableCell>
                <asp:TableCell runat="server">8</asp:TableCell>
                <asp:TableCell runat="server">=</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                 <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server">X</asp:TableCell>
                <asp:TableCell runat="server">9</asp:TableCell>
                <asp:TableCell runat="server">=</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                 <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server">X</asp:TableCell>
                <asp:TableCell runat="server">10</asp:TableCell>
                <asp:TableCell runat="server">=</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </form>
       <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/PaginaInicial.aspx">Pagina Inicial</asp:HyperLink>
</body>
</html>
