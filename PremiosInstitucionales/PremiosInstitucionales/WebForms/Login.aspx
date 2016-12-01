<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.Login" MasterPageFile="~/PaginasIniciales.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
      
    <div class="paginasIniciales" style="text-align:center">
        <h3>Inicia sesión</h3>
        <asp:Label class="labeliniciales" ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label><br />
        <asp:TextBox class="textboxiniciales" ID="TextBox1" runat="server" placeholder="Correo electrónico"></asp:TextBox><br />
        <asp:TextBox class="textboxiniciales" ID="TextBox2" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox><br />
        <asp:Button class="buttoniniciales" ID="Button1" runat="server" OnClick="Button1_Click" Text="Inicia Sesión" /><br /> <br /> <br />
        <asp:HyperLink class="linkiniciales" ID="HyperLink2" NavigateUrl="~/WebForms/Recuperar.aspx" runat="server">¿Olvidaste tu contraseña?</asp:HyperLink><br />
        <asp:HyperLink class="linkiniciales" ID="HyperLink1" NavigateUrl="~/WebForms/Registro.aspx" runat="server">¿No tienes cuenta? Regístrate</asp:HyperLink>
    </div>
</asp:Content>
