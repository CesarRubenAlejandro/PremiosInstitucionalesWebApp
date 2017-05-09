<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PremioEspecificoCandidato.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.PremioEspecificoCandidato" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    
    <h1>Formulario de aplicación</h1>
    <asp:Label class="appLabel" runat="server" Text="Seleccione una categoría"></asp:Label>
    <asp:DropDownList runat="server" ID="CategoriasDDL" AutoPostBack="true"
        OnSelectedIndexChanged="CategoriasDDL_SelectedIndexChanged"></asp:DropDownList>
    <br /><br />
    <asp:Label class="appLabel" ID="ErrorLbl" runat="server" Visible="false"></asp:Label>
    <div runat="server" id="PanelFormulario">

    </div>
        
    <table style="width:100%">
        <tr>
            <td style="width: 100%" align="center">
                <asp:Button id="EnviarBttn" runat="server" onclick="EnviarBttn_Click" 
                    CssClass="ApplicationButton" Text="Enviar aplicación" Visible="false"/>
            </td>
        </tr>
    </table>
</asp:Content>
