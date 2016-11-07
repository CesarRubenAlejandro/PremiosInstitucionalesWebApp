<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recuperar.aspx.cs" Inherits="PremiosInstitucionales.WebForms.Recuperar" MasterPageFile="~/MasterPage.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div style="text-align:center">
        <h3>Recuperar Contraseña</h3>
        <br />
        <asp:TextBox runat="server" placeholder="Email" ID="EmailTextBox"></asp:TextBox> <br /><br />
        Si tu cuenta de correo se encuentra registrada <br />
        en nuestro sistema, en poco tiempo recibirás un <br />
        correo con los pasos para reestablecer la contraseña. <br /><br />
        <asp:HyperLink ID="HyperLink1" NavigateUrl="~/WebForms/Login.aspx" runat="server">Volver a Inicio de Sesión</asp:HyperLink><br />
        <asp:HyperLink ID="HyperLink2" NavigateUrl="~/WebForms/Registro.aspx" runat="server">¿No tienes una cuenta? Regístrate</asp:HyperLink><br /><br />
        <asp:Button ID="EnviarBoton" runat="server" Text="Enviar" OnClick="Button1_Click" /> <br /><br />
        <asp:Label runat="server" ID="Mensaje" Visible="false"></asp:Label><br /><br />
     </div>
</asp:Content>