<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dados.aspx.cs" Inherits="cursoaula01.Dados" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Nome:"></asp:Label>
        <br />
        <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="E-mail:"></asp:Label>
        <br />
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <asp:Button ID="BtCadastrar" runat="server" OnClick="BtCadastrar_Click" Text="Cadastrar" />
        <br />
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Lista de Contatos"></asp:Label>
        <asp:DataList ID="DataList1" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyField="Id" DataSourceID="SqlDataSource1" GridLines="Horizontal" Width="393px">
            <AlternatingItemStyle BackColor="#F7F7F7" />
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <ItemTemplate>
                Id:
                <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                <br />
                Nome:
                <asp:Label ID="NomeLabel" runat="server" Text='<%# Eval("Nome") %>' />
                <br />
                Email:
                <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
                <br />
<br />
            </ItemTemplate>
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        </asp:DataList>
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Id], [Nome], [Email] FROM [Contato]"></asp:SqlDataSource>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" />
    
    </div>
    </form>
</body>
</html>
