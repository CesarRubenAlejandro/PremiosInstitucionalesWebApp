<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperaCuenta.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.RecuperaCuenta" MasterPageFile="~/PaginasIniciales.Master" EnableEventValidation="false"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div class="paginasIniciales" style="text-align:center">
        <h3>Restablece tu contraseña</h3>
        <asp:TextBox class="textboxiniciales" ID="PasswordTextBox" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox><br /><br />
        <asp:TextBox class="textboxiniciales" ID="ConfirmPasswordTextBox" runat="server" TextMode="Password" placeholder="Confirma contraseña"></asp:TextBox><br /><br />
        <asp:Button class="buttoniniciales" ID="Boton" runat="server" OnClick="Button1_Click" Text="Cambiar contraseña" /><br /><br />
        <asp:Label class="labeliniciales" runat="server" ID="Mensaje" Visible="false"></asp:Label><br /><br />
        <asp:HyperLink class="linkiniciales" ID="HyperLinkRegresar" runat="server" Visible="false" NavigateUrl="~/WebForms/Login.aspx">Regresar a Inicio de Sesion</asp:HyperLink>
    </div>
</asp:Content>
