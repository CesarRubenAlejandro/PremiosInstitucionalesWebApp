<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PremioEspecificoCandidato.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.PremioEspecificoCandidato" MasterPageFile="~/MasterPage.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <style>
        .ApplicationButton{
            border-radius:5px;
        }
    </style>
    
    <h1>Formulario de aplicación</h1>
    <asp:Label runat="server" Text="Seleccione una categoría"></asp:Label>
    <asp:DropDownList runat="server" ID="CategoriasDDL" AutoPostBack="true"
        OnSelectedIndexChanged="CategoriasDDL_SelectedIndexChanged"></asp:DropDownList>
    <br /><br />
    <asp:Label ID="ErrorLbl" runat="server" Visible="false"></asp:Label>
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
