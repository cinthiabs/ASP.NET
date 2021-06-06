<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="cursoaula01.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Curso Aula01</title>
    <style type="text/css">
        .auto-style1 {
            width: 411px;
        }
        .auto-style2 {
            width: 328px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lmsg" runat="server" Text="Escreva seu nome:"></asp:Label>
        <asp:TextBox ID="txtMensagem" runat="server" Width="549px"></asp:TextBox>
        <asp:Button ID="Executar" runat="server" OnClick="Executar_Click" style="height: 29px" Text="Executar" />
        <br />
        <asp:Label ID="txtvisivel" runat="server"></asp:Label>
        <br />
    
    </div>
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">Site</td>
                <td class="auto-style2">Endereço</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:TextBox ID="Txtsite" runat="server" Width="410px"></asp:TextBox>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="Txtendereco" runat="server" Width="410px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:ListBox ID="ListBoxLink" runat="server" Width="407px" SelectionMode="Multiple">
                    </asp:ListBox>
                </td>
                <td class="auto-style2">
                    Opções<br />
                    <asp:Button ID="btnInserir" runat="server" Text="Inserir" OnClick="btnInserir_Click" style="width: 69px" />
                    <asp:Button ID="btnInserirList" runat="server" Text="Selecionar Site" OnClick="btnInserir_Click" Width="187px" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:DropDownList ID="ListSite" runat="server" Height="16px" Width="419px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
