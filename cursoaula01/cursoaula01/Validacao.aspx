<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validacao.aspx.cs" Inherits="cursoaula01.Validacao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="Panel1" runat="server" BackColor="#009999" GroupingText="Validação de campo" Height="427px" Width="1021px">
            <asp:Label ID="Label1" runat="server" Text="Nome:"></asp:Label>
            <asp:TextBox ID="txtnome" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnome" ErrorMessage="O nome é Obrigatorio"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Senha:"></asp:Label>
            <asp:TextBox ID="txtsenha" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtsenha" ErrorMessage="Informe  uma Senha"></asp:RequiredFieldValidator>
            <br />
            <appSettings>
                <add key ="ValidationSettings:UnobtrusiveValidationMode" value="None"></add>
            </appSettings>
            <asp:Button ID="btEnviar" runat="server" Text="Enviar" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
