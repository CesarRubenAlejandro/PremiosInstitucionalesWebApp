<%@ Page Title="" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="AdministraGanadores.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraGanadores" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container fadeView">

        <!-- Header -->
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <h3 class="section-heading text-center">Asignar ganador</h3>
                    <hr class="shorthr"/>
                </div>
            </div>
        </div>

        <!-- content -->
        <div class="container">
            <div class="row" runat="server" id="PanelCategoriasPorPremio">
            </div>
        </div>
    </div>
</asp:Content>
