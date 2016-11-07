<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.Login" MasterPageFile="~/MasterPage.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
      
    <div style="text-align:center">
        <h3>Login</h3>
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label><br />
        <asp:TextBox ID="TextBox1" runat="server" placeholder="E-mail"></asp:TextBox><br /><br />
        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox><br /><br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Inicia Sesion" /><br /><br />
        <asp:HyperLink ID="HyperLink2" NavigateUrl="~/WebForms/Recuperar.aspx" runat="server">¿Olvidaste tu contraseña?</asp:HyperLink><br /><br />
        <asp:HyperLink ID="HyperLink1" NavigateUrl="~/WebForms/Registro.aspx" runat="server">¿No tienes cuenta? Registrate</asp:HyperLink>
    </div>
</asp:Content>
