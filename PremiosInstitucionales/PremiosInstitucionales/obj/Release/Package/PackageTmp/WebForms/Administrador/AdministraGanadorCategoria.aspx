<%@ Page Title="" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="AdministraGanadorCategoria.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraGanadorCategoria" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <script src='<%= ResolveUrl("~/Resources/js/jquery.dataTables.js")%>' type="text/javascript" defer="defer"></script>
    <script src='<%= ResolveUrl("~/Resources/js/listaParticipantes.js")%>' type="text/javascript" defer="defer"></script>
    <script src='<%= ResolveUrl("~/Resources/js/adminGanadorCategoria.js")%>' type="text/javascript"></script>
    <link href='<%= ResolveUrl("~/Resources/css/dataTables.css")%>' rel="stylesheet" type="text/css" /> 

    <div class="container fadeView">

        <asp:Button type="button" runat="server" OnClick="BackBtn_Click" class="backBtn"/>

        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
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

         <!-- Botones -->
        <div style="width: 100%; text-align: right;">
            <a data-toggle="modal" data-target="#modalVeredicto" class="no-underline">
                <button class="btn" id="VeredictoFinal" style="margin-right: 10px; color: black; display: none;">Veredicto Final</button>
            </a>

            <asp:Button class="btn btn-primary" ID="AsignarGanador" Text="Asignar Ganador" OnClick="GanadorBtn_Click" runat="server" style="display: none"/>
	    </div>

        <!-- Var compartida (JS & CS) -->
        <input id="hiddenControl" type="hidden" runat="server"/>

        <!-- Modal para veredicto final -->
	    <div class="modal fade" id="modalVeredicto" tabindex="-1" role="dialog" aria-labelledby="myModalLabel2">
		    <div class="modal-dialog" role="document">
			    <div class="modal-content">

                    <!-- Encabezado del modal -->		
				    <div class="modal-header text-center">
					    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
					    <h3 class="modal-title" id="myModalLabel2">Veredicto Final</h3>
					    <hr class="shorthr"/>
				    </div>

                    <!-- Cuerpo del modal -->
				    <div class="modal-body">
                        ¿Estas seguro que quieres hacer el veredicto final? <br/>
                        Una vez realizada esta acción, se enviara un correo a todos los participantes de esta categoría,
                        informandoles que ya existe un ganador.
				    </div>

                    <!-- Pie del modal -->
				    <div class="modal-footer">
					    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
					    <asp:Button class="btn btn-primary" ID="BtnVeredictoFinal" Text="Enviar Correos" OnClick="VeredictoBtn_Click" runat="server"/>
				    </div>
			    </div>
		    </div>
	    </div>

    </div>
</asp:Content>
