<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="UploadArquivos.Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Upload de Arquivos"></asp:Label>
        <br />
        <asp:FileUpload ID="Arquivo" runat="server" />
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Nome do Arquivo"></asp:Label>
        <asp:TextBox ID="txbNome" runat="server" style="margin-left: 26px" Width="174px"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Tamanho do Arquivo"></asp:Label>
        <asp:TextBox ID="txbTamanho" runat="server" Width="175px"></asp:TextBox>
        <br />
        <asp:Button ID="BtEnviar" runat="server" OnClick="BtEnviar_Click" Text="Enviar" Width="281px" />
    
    </div>
    </form>
</body>
</html>
