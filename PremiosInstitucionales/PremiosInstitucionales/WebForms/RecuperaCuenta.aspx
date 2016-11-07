<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperaCuenta.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.RecuperaCuenta" MasterPageFile="~/MasterPage.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div style="text-align:center">
        <h3>Restablece tu contraseña</h3>
        <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox><br /><br />
        <asp:TextBox ID="ConfirmPasswordTextBox" runat="server" TextMode="Password" placeholder="Confirma contraseña"></asp:TextBox><br /><br />
        <asp:Button ID="Boton" runat="server" OnClick="Button1_Click" Text="Cambiar contraseña" /><br /><br />
        <asp:Label runat="server" ID="Mensaje" Visible="false"></asp:Label><br /><br />
        <asp:HyperLink ID="HyperLinkRegresar" runat="server" Visible="false" NavigateUrl="~/WebForms/Login.aspx">Regresar a Inicio de Sesion</asp:HyperLink>
    </div>
</asp:Content>
