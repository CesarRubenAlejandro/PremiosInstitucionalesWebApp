<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PremioEspecificoCandidato.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.PremioEspecificoCandidato" MasterPageFile="~/MasterPage.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div>
        <h1>Premio especifico</h1>
        <asp:DropDownList runat="server" ID="CategoriasDDL" AutoPostBack="true"
            OnSelectedIndexChanged="CategoriasDDL_SelectedIndexChanged"></asp:DropDownList>
        <asp:Label ID="ErrorLbl" runat="server" Visible="false"></asp:Label>
        <div runat="server" id="PanelFormulario">

        </div>

    </div>
</asp:Content>
