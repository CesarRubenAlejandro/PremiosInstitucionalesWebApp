<%@ Page Title="" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="ObservaAplicacion.aspx.cs" Inherits="PremiosInstitucionales.WebForms.ObservaAplicacion" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container fadeView">

        <asp:Button type="button" class="closeBtn" runat="server" OnClick="CloseBtn_Click"/>

        <div class="row">
            <div class="col-lg-12 text-center">
                <h3 class="section-heading">
                    <asp:Literal ID="litTituloPremio" runat="server" /></h3>
                <h4>
                    <asp:Literal ID="litTituloCategoria" runat="server" /></h4>
                <hr class="shorthr" />
            </div>
        </div>
            <asp:Panel runat="server" ID="PanelArchivo" class="row question-form"></asp:Panel>
            <asp:Panel runat="server" ID="PanelFormulario" class="row question-form"></asp:Panel>
            <br />
    </div>
</asp:Content>
