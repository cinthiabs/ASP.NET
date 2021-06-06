<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlMenu.ascx.cs" Inherits="cursoaula01.WebUserControlMenu" %>
<asp:Menu ID="Menu1" runat="server" BackColor="#FFFBD6" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#990000" Orientation="Horizontal" StaticSubMenuIndent="10px">
    <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <DynamicMenuStyle BackColor="#FFFBD6" />
    <DynamicSelectedStyle BackColor="#FFCC66" />
    <Items>
        <asp:MenuItem NavigateUrl="~/Cursos.aspx" Text="Cursos" Value="Cursos"></asp:MenuItem>
        <asp:MenuItem NavigateUrl="~/Tabuada.aspx" Text="Tabuada" Value="Tabuada"></asp:MenuItem>
        <asp:MenuItem NavigateUrl="~/ParouImpar.aspx" Text="ParOuImpar" Value="ParOuImpar"></asp:MenuItem>
    </Items>
    <StaticHoverStyle BackColor="#990000" ForeColor="White" />
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticSelectedStyle BackColor="#FFCC66" />
</asp:Menu>

