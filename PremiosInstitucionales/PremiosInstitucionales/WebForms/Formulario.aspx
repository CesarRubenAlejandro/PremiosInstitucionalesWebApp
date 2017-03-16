<%@ Page Title="Formulario" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="PremiosInstitucionales.WebForms.Formulario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="container fadeView">
        <div class="row">
            <div class="col-lg-12 text-center">
                <h3 class="section-heading">
                    <asp:Literal ID="litTituloPremio" runat="server" /></h3>
                <h4>
                    <asp:Literal ID="litTituloCategoria" runat="server" /></h4>
                <hr class="shorthr" />
            </div>
        </div>
        <div class="text-center">
            <h5 runat="server" id="alreadySubmittedLabel" visible="false">Ya se ha realizado una aplicación para esta categoría. Para conocer el estatus, por favor dirigirse a <a href="WebForms/AplicacionesCandidato.aspx">mis aplicaciones vigentes </a></h5>
        </div>
        <form runat="server" id="FormFormulario">
            <asp:Panel runat="server" ID="PanelFormulario" class="row question-form">
            </asp:Panel>

            <div class="btn-group-right">
                <a href="/WebForms/InicioCandidato.aspx">
                    <button type="button" class="btn btn-default">Cancelar</button>
                </a>
                <button type="button" class="btn btn-primary" onclick="sendFormAux()">Enviar</button>
            </div>
            <asp:Button Style="display: none;" ID="EnviarBtn" runat="server" OnClick="EnviarAplicacion" Text="Enviar aplicación" />
        </form>

    </div>


</asp:Content>
