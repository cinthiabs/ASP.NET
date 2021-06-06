<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SitesParceiros.aspx.cs" Inherits="cursoaula01.SitesParceiros" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ImageMap ID="ImageMap1" runat="server" ImageUrl="~/Imagens/a22e4e8357966d19480422930be3c6aa.jpg">
            <asp:RectangleHotSpot AlternateText="Paisagem" Right="400" NavigateUrl="http://www.google.com"  Target="_blank" Top="400" />
            <asp:RectangleHotSpot AlternateText="Nova Url" Bottom="200" Left="200" NavigateUrl="http://www.youtube.com" />
        </asp:ImageMap>
    
    </div>
    </form>
</body>
</html>
