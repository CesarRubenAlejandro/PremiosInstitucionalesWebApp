<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="PremiosInstitucionales.WebForms.WebForm1" 
    MasterPageFile="~/MasterPage.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div style="text-align:center">
        <h3>Regístrate</h3>
        <asp:TextBox ID="EmailTextBox" runat="server" placeholder="E-mail"></asp:TextBox><br /><br />
        <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox><br /><br />
        <asp:TextBox ID="ConfirmPasswordTextBox" runat="server" TextMode="Password" placeholder="Confirma contraseña"></asp:TextBox><br /><br />
        <asp:Button ID="RegisterButton" runat="server" OnClick="Button1_Click" Text="Regístrate" /><br /><br />
        <asp:Label runat="server" ID="FracasoLbl" Text="Ya existe un usuario con ese correo" Visible="false"></asp:Label><br /><br />
        <asp:HyperLink ID="HyperLink1" NavigateUrl="~/WebForms/Login.aspx" runat="server" >¿Ya tienes cuenta? Inicia sesión</asp:HyperLink>
        <br />
        
        <asp:Label runat="server" ID="ConfirmacionLbl" 
            Text="Se envió un mail al correo electronico registrado. Favor de confirmar cuenta." Visible="false"></asp:Label><br /><br />
    </div>
</asp:Content>
