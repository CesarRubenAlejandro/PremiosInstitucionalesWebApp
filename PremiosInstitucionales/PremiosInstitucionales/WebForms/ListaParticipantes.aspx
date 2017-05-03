<%@ Page Title="" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="ListaParticipantes.aspx.cs" Inherits="PremiosInstitucionales.WebForms.ListaParticipantes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script src="../Resources/js/jquery.dataTables.js" type="text/javascript" defer="defer"></script>
    <script src="../Resources/js/listaParticipantes.js" type="text/javascript" defer="defer"></script>
    <link href='../Resources/css/dataTables.css' rel="stylesheet" type="text/css" />
    <div class="container fadeView">

        <h3>
            <asp:Literal ID="litTituloPremio" runat="server" /></h3>
        <h4>
            <asp:Literal ID="litTituloCategoria" runat="server" /></h4>

        <div class="container">
            <table id="listaParticipantesTable" class="display" cellspacing="0" width="100%">
                <thead>
                    <tr>
                        <th>IP</th>
                        <th>Nombres</th>
                        <th>Apellidos</th>
                        <th>Estado</th>
                    </tr>
                </thead>
                <tbody id="listaParticipantesTableBody" runat="server">
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
