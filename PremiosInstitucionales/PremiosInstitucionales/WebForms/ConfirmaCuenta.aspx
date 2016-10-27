<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmaCuenta.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.ConfirmaCuenta" MasterPageFile="~/MasterPage.Master"%>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <h2 runat="server" id="MensajeLbl" visible="false">Gracias por su confirmación. Ya puede iniciar sesión en el sistema.</h2>
    <asp:HyperLink id="LoginHL" Visible="false" runat="server" Text="Iniciar sesion" NavigateUrl="~/WebForms/Login.aspx"></asp:HyperLink>
</asp:Content>