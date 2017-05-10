<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmaCuenta.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.ConfirmaCuenta" MasterPageFile="~/PaginasIniciales.Master" EnableEventValidation="false"%>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <h3 runat="server" id="MensajeLbl" visible="false">Gracias por su confirmación. Ya se inició sesión. Será redireccionado a su página de información.</h3>
    <asp:HyperLink class="linkiniciales" id="LoginHL" Visible="false" runat="server" Text="Iniciar sesion" NavigateUrl="~/WebForms/Login.aspx"></asp:HyperLink>
</asp:Content>