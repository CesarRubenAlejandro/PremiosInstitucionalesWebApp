<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorrigeAplicacion.aspx.cs" 
    MasterPageFile="~/mp-Candidato.Master" Inherits="PremiosInstitucionales.WebForms.CorrigeAplicacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <script type="text/javascript">
    </script>
    <h1>Modificación de solicitud de registro</h1>
    <asp:Label class="appLabel" ID="ErrorLbl" runat="server" Visible="false" Text="Esta aplicación no requiere cambios."></asp:Label>
    <form runat="server">
    <div runat="server" id="PanelFormulario">

    </div>
    </form>

    <table style="width:100%">
        <tr>
            <td style="width: 100%" align="center">
                <asp:Button id="EnviarBttn" runat="server" onclick="EnviarBttn_Click" 
                    CssClass="ApplicationButton" Text="Enviar aplicación" Visible="false"/>
            </td>
        </tr>
    </table>
    
</asp:Content>
