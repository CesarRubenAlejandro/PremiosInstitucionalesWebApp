<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioJuez.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.InicioJuez" MasterPageFile="~/MasterPage.Master"%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

    <style>
        .accordionContent {
            background-color: rgb(180,180,180);
        }

        .accordionHeader {
            border: 1px solid #dddddd;
            background-color: rgb(180,180,180);
            padding: 3px 3px 3px 18px;
            margin-top: 5px;
            cursor: pointer;
        }

        .accordionHeaderSelected {
            background-color: rgb(180,180,180);
            padding: 3px 3px 3px 18px;
            margin-top: 5px;
            cursor: pointer;
        }
    </style>

    <div>
    <h1>Inicio Juez</h1>
        <asp:HiddenField runat="server" ID="CategoriasHidden"/>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
                
        </ajaxToolkit:TabContainer>
        <asp:Button runat="server" ID="ExportarBttn" OnClick="ExportarBttn_Click" Text="Exportar"/>
    </div>
</asp:Content>
