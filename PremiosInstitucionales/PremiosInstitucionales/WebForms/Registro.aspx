<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="PremiosInstitucionales.WebForms.WebForm1" 
    MasterPageFile="~/PaginasIniciales.Master" EnableEventValidation="false"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div class="paginasIniciales" style="text-align:center">
        <h3>Regístrate</h3>
        <br />
        
        <asp:TextBox class="textboxiniciales" ID="PasswordTextBox" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox><br />
        <asp:TextBox class="textboxiniciales" ID="ConfirmPasswordTextBox" runat="server" TextMode="Password" placeholder="Confirma contraseña"></asp:TextBox><br />
        
        <asp:Button class="buttoniniciales" ID="RegisterButton" runat="server" OnClick="Button1_Click" Text="Regístrate" /><br /><br /><br />
        <asp:HyperLink class="linkiniciales" ID="HyperLink1" NavigateUrl="~/WebForms/Login.aspx" runat="server" >¿Ya tienes cuenta? Inicia sesión</asp:HyperLink>
        <br />
        </div>
</asp:Content>
