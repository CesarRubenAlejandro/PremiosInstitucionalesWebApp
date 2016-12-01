<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recuperar.aspx.cs" Inherits="PremiosInstitucionales.WebForms.Recuperar" MasterPageFile="~/PaginasIniciales.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div style="text-align:center">
        <h3>Recuperar contraseña</h3>
        <br />
        <asp:TextBox class="textboxiniciales" runat="server" placeholder="Email" ID="EmailTextBox"></asp:TextBox> <br /><br />
        <asp:Label class="labeliniciales" runat="server" ID="Info">Si tu cuenta de correo se encuentra registrada <br />
        en nuestro sistema, en poco tiempo recibirás un <br />
        correo con los pasos para reestablecer la contraseña.</asp:Label>
        <br /><br />
        <asp:Button class="buttoniniciales" ID="EnviarBoton" runat="server" Text="Enviar" OnClick="Button1_Click" /> <br /><br />
        <asp:HyperLink class="linkiniciales" ID="HyperLink1" NavigateUrl="~/WebForms/Login.aspx" runat="server">Volver a Inicio de Sesión</asp:HyperLink><br />
        <asp:HyperLink class="linkiniciales" ID="HyperLink2" NavigateUrl="~/WebForms/Registro.aspx" runat="server">¿No tienes una cuenta? Regístrate</asp:HyperLink><br /><br />
     </div>
</asp:Content>