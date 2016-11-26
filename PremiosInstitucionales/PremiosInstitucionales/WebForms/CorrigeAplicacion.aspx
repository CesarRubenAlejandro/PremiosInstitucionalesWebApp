<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorrigeAplicacion.aspx.cs" 
    MasterPageFile="~/MasterPage.Master" Inherits="PremiosInstitucionales.WebForms.CorrigeAplicacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <script type="text/javascript">
    </script>

    <asp:Label ID="ErrorLbl" runat="server" Visible="false" Text="Esta aplicación no ha sido rechazada."></asp:Label>
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
