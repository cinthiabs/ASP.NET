<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="cursoaula01.Cursos" %>

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
        <br />
    
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <asp:ImageButton ID="ImageButton1" runat="server" Height="332px" ImageUrl="~/Imagens/a22e4e8357966d19480422930be3c6aa.jpg" Width="515px" OnClick="ImageButton1_Click" />
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagens/paisagem-ouro-preto-1008049370.jpg"  Width="560px" Height="332px" OnClick="ImageButton2_Click" />
            </asp:View>
            <asp:View ID="View2" runat="server">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Paisagem Maresias"></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" Text="As praias de Maresias fica localizado no litoral norte de São Paulo, lugar turistico otima opção para as ferias."></asp:Label>
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://www.passaromarron.com.br/destino/maresias/">Compre passagens agora</asp:HyperLink>
                <br />
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Voltar</asp:LinkButton>
            </asp:View>
            <br />
            <asp:View ID="View3" runat="server">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Paisagem Rio Grande do Sul"></asp:Label>
                &nbsp;<br />
                <asp:Label ID="Label5" runat="server" Text="Rio Grande do Sul, um lugar turistico com paisagens maravilhosas."></asp:Label>
                <br />
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="https://www.expedia.com.br/Destinos-Rio-Grande-Do-Sul.d6052643.destinos-de-voo">Compre passagens agora</asp:HyperLink>
                <br />
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Voltar</asp:LinkButton>
            </asp:View>
            <br />
        </asp:MultiView>
    
    </div>
    </form>
</body>
</html>
