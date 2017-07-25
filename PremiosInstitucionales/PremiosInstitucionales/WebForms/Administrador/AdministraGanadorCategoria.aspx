<%@ Page Title="" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="AdministraGanadorCategoria.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraGanadorCategoria" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <script src='<%= ResolveUrl("~/Resources/js/jquery.dataTables.js")%>' type="text/javascript" defer="defer"></script>
    <script src='<%= ResolveUrl("~/Resources/js/listaParticipantes.js")%>' type="text/javascript" defer="defer"></script>
    <link href='<%= ResolveUrl("~/Resources/css/dataTables.css")%>' rel="stylesheet" type="text/css" /> 

    <div class="container fadeView">

        <asp:Button type="button" runat="server" OnClick="BackBtn_Click" class="backBtn"/>

        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <h2 class="section-heading">Resultados de aplicaciones</h2>
                    <h3 class="section-heading">
                        <asp:Literal ID="litTituloPremio" runat="server" /></h3>
                    <h4>
                        <asp:Literal ID="litTituloCategoria" runat="server" /></h4>
                    <hr class="shorthr" />
                </div>
            </div>
        </div>

        <div class="container">
            <table id="listaParticipantesTable" class="display" cellspacing="0" width="100%">
                <thead>
                    <tr>
                        <th>IP</th>
                        <th>Nombres</th>
                        <th>Apellidos</th>
                        <th>Promedio</th>
                        <th>Jueces</th>
                        <th>Ganador</th>
                    </tr>
                </thead>
                <tbody id="listaParticipantesTableBody" runat="server">
                </tbody>
            </table>
        </div>

        <br />

         <!-- Boton Guardar -->
        <div style="width: 100%; text-align: right;">
            <asp:Button class="btn" ID="VeredictoFinal" Text="Veredicto Final" runat="server" style="margin-right: 10px;"/>
            <asp:Button class="btn btn-primary" ID="AsignarGanador" Text="Asignar Ganador" runat="server"/>
	    </div>

    </div>
</asp:Content>
