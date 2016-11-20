<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformacionPersonalCandidato.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.InformacionPersonalCandidato" MasterPageFile="~/MasterPage.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <h3>Mis datos personales</h3>
    <asp:Label ID="Mensaje" runat="server"></asp:Label>
    <br /><br />
    <asp:Button ID="EditarBtn" runat="server" Text="Editar datos" OnClick="EditarBtn_Click" />
    <br /><br />
    <asp:Label ID="NombresLabel" runat="server" Text="Nombres: "></asp:Label>
    <asp:TextBox ID="NombresTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label ID="ApellidosLabel" runat="server" Text="Apellidos: "></asp:Label>
    <asp:TextBox ID="ApellidosTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label ID="CorreoLabel" runat="server" Text="Correo: "></asp:Label>
    <asp:TextBox ID="CorreoTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label ID="DomicilioLabel" runat="server" Text="Domicilio: "></asp:Label>
    <asp:TextBox ID="DomicilioTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label ID="CiudadLabel" runat="server" Text="Ciudad: "></asp:Label>
    <asp:TextBox ID="CiudadTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label ID="EstadoLabel" runat="server" Text="Estado: "></asp:Label>
    <asp:TextBox ID="EstadoTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label ID="CodigoPostalLabel" runat="server" Text="Código Postal: "></asp:Label>
    <asp:TextBox ID="CodigoPostalTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label ID="TelefonoLabel" runat="server" Text="Teléfono: "></asp:Label>
    <asp:TextBox ID="TelefonoTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label ID="PuestoLabel" runat="server" Text="Puesto: "></asp:Label>
    <asp:TextBox ID="PuestoTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label ID="InstitucionLabel" runat="server" Text="Institucion: "></asp:Label>
    <asp:TextBox ID="InstitucionTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Button ID="EnviarBtn" runat="server" Enabled="false" Text="Enviar" OnClick="EnviarBtn_Click" />
</asp:Content>