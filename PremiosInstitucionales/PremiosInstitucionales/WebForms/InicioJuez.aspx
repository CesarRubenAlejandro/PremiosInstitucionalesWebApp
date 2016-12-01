<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioJuez.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.InicioJuez" MasterPageFile="~/MasterPage.Master"%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div>
        <asp:HiddenField runat="server" ID="CategoriasHidden"/>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
                
        </ajaxToolkit:TabContainer>
        <br />
        <asp:Button runat="server" ID="ExportarBttn" OnClick="ExportarBttn_Click" Text="Exportar"/>
    </div>
</asp:Content>
