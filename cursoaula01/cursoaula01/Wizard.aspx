<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wizard.aspx.cs" Inherits="cursoaula01.Wizard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="1" BackColor="#E6E2D8" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" FinishCompleteButtonText="Finalizar" FinishPreviousButtonText="Anterior" Font-Names="Verdana" Font-Size="0.8em" Height="578px" OnFinishButtonClick="Wizard1_FinishButtonClick" StartNextButtonText="Proximo" Width="587px">
            <HeaderStyle BackColor="#666666" BorderColor="#E6E2D8" BorderStyle="Solid" BorderWidth="2px" Font-Bold="True" Font-Size="0.9em" ForeColor="White" HorizontalAlign="Center" />
            <NavigationButtonStyle BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#1C5E55" />
            <SideBarButtonStyle ForeColor="White" />
            <SideBarStyle BackColor="#1C5E55" Font-Size="0.9em" VerticalAlign="Top" />
            <StepStyle BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderStyle="Solid" BorderWidth="2px" />
            <WizardSteps>
                <asp:WizardStep runat="server" title="Dados">
                    <asp:Label ID="Label1" runat="server" Text="Nome:"></asp:Label>
                    <br />
                    <asp:TextBox ID="tbNome" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="CPF:"></asp:Label>
                    <br />
                    <asp:TextBox ID="tbCPF" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="RG:"></asp:Label>
                    <br />
                    <asp:TextBox ID="TbRG" runat="server"></asp:TextBox>
                </asp:WizardStep>
                <asp:WizardStep runat="server" title="Endereço">
                    <asp:Label ID="Label4" runat="server" Text="CEP:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtCEP" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="Estado:"></asp:Label>
                    <br />
                    <asp:TextBox ID="TxtEstado" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label6" runat="server" Text="Cidade:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtCidade" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="Rua:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtRua" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="Bairro:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtBairro" runat="server"></asp:TextBox>
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
    
    </div>
    </form>
</body>
</html>
