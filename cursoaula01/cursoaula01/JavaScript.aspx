<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JavaScript.aspx.cs" Inherits="cursoaula01.JavaScript" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script type="text/javascript">
        function ExibirMensagem() {
            alert('Olá');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Image ID="Image1" runat="server" Height="400px" ImageUrl="~/Imagens/paisagem-ouro-preto-1008049370.jpg" Width="420px" 
          onMouseOver="ExibirMensagem()" />
    
        <br />
        <asp:Button ID="Button1" runat="server" Text="Clique" />
    
    </div>
    </form>
</body>
</html>
