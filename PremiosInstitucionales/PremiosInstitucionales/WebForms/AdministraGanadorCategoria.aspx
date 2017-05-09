<%@ Page Title="" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="AdministraGanadorCategoria.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraGanadorCategoria" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script src="../Resources/js/jquery.dataTables.js" type="text/javascript" defer="defer"></script>
    <script src="../Resources/js/listaParticipantes.js" type="text/javascript" defer="defer"></script>
    <link href='../Resources/css/dataTables.css' rel="stylesheet" type="text/css" />

    <div class="container fadeView">
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
                    </tr>
                </thead>
                <tbody id="listaParticipantesTableBody" runat="server">
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
