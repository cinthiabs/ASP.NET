<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JavaScriptExercicio.aspx.cs" Inherits="cursoaula01.JavaScriptExercicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        var tam = 12;
        function IncText() {
            tam = tam + 1;
            document.getElementById("p1").style.fontSize = tam + "px";
            document.getElementById("p2").style.fontSize = tam + "px";
        }
        function Dectext() {
            tam = tam - 1;
            document.getElementById("p1").style.fontSize = tam + "px";
            document.getElementById("p2").style.fontSize = tam + "px";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:HyperLink ID="HyperLink1" runat="server" onClick="IncText()">Teste</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" onClick="Dectext()">Teste</asp:HyperLink>
         <br />
    </div>
    </form>
    <p id="p1">
        Olá, Primeiro paragrafo da pagina.
    </p>
     <p id="p2">
        Olá, Segundo paragrafo da pagina.
    </p>
</body>
</html>
