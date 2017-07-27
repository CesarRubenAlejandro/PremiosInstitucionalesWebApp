<%@ Page Title="" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="AdministraGanadores.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraGanadores" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container fadeView">

        <asp:Button type="button" runat="server" OnClick="BackBtn_Click" class="backBtn"/>

        <!-- Header -->
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <h3 class="section-heading text-center">Asignar ganador</h3>
                    <ul class="nav nav-tabs">
                        <li class="active li-center"><a data-toggle="tab" href="#Pendientes"><strong>Pendientes</strong></a></li>
                        <li class="li-center"><a data-toggle="tab" href="#Terminados"><strong>Terminados</strong></a></li>
                    </ul>
                </div>
            </div>
        </div>

        <!-- Contenido Tabs -->
        <div class="tab-content">

            <!-- Pendientes -->
            <div id="Pendientes" class="tab-pane fade in active">
                <div class="container">
                    <div class="row" runat="server" id="PanelCategoriasPendientes"></div>
                </div>
            </div>

            <!-- Pendientes -->
            <div id="Terminados" class="tab-pane fade in active">
                <div class="container">
                    <div class="row" runat="server" id="PanelCategoriasTerminadas"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
