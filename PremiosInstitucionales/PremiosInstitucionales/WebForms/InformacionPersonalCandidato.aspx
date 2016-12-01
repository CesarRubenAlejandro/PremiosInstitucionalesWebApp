<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformacionPersonalCandidato.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.InformacionPersonalCandidato" MasterPageFile="~/MasterPage.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <h1>Mis datos personales</h1>
    <asp:Label class="appLabel" ID="Mensaje" runat="server"></asp:Label>
    <br />
    <asp:Button ID="EditarBtn" runat="server" Text="Editar datos" OnClick="EditarBtn_Click" />
    
    <br /><br />
    <asp:Label class="appLabel" ID="NombresLabel" runat="server" Text="Nombres: "></asp:Label>
    <asp:TextBox ID="NombresTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label class="appLabel" ID="ApellidosLabel" runat="server" Text="Apellidos: "></asp:Label>
    <asp:TextBox ID="ApellidosTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label class="appLabel" ID="CorreoLabel" runat="server" Text="Correo: "></asp:Label>
    <asp:TextBox ID="CorreoTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label class="appLabel" ID="DomicilioLabel" runat="server" Text="Domicilio: "></asp:Label>
    <asp:TextBox ID="DomicilioTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label class="appLabel" ID="TelefonoLabel" runat="server" Text="Teléfono: "></asp:Label>
    <asp:TextBox ID="TelefonoTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label class="appLabel" ID="Label1" runat="server" Text="RFC/Clave organización: "></asp:Label>
    <asp:TextBox ID="RFCTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label class="appLabel" ID="Label2" runat="server" Text="Nacionalidad: "></asp:Label>
    <asp:TextBox ID="NacionalidadTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    
    <asp:Button ID="EnviarBtn" runat="server" Enabled="false" Text="Enviar" OnClick="EnviarBtn_Click" />
</asp:Content>