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

         <!-- Botones -->
        <div style="width: 100%; text-align: right;">
            <a data-toggle="modal" data-target="#modalVeredicto">
                <button class="btn" id="VeredictoFinal" style="margin-right: 10px; color: black;">Veredicto Final</button>
            </a>
            <a data-toggle="modal" data-target="#modalGanador">
                <button class="btn btn-primary" id="AsignarGanador">Asignar Ganador</button>
            </a>
	    </div>

        <!-- Modal para asignar ganador -->
	    <div class="modal fade" id="modalGanador" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
		    <div class="modal-dialog" role="document">
			    <div class="modal-content">

                    <!-- Encabezado del modal -->		
				    <div class="modal-header text-center">
					    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
					    <h3 class="modal-title" id="myModalLabel">Asignar Ganador</h3>
					    <hr class="shorthr"/>
				    </div>

                    <!-- Cuerpo del modal -->
				    <div class="modal-body">
                         Lorem ipsum...................
				    </div>

                    <!-- Pie del modal -->
				    <div class="modal-footer">
					    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
					    <asp:Button class="btn btn-primary" id="BtnAsginarGanador" runat="server" Text="Asignar"/>
				    </div>
			    </div>
		    </div>
	    </div>

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
                         Correo
				    </div>

                    <!-- Pie del modal -->
				    <div class="modal-footer">
					    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
					    <asp:Button class="btn btn-primary" id="BtnVeredictoFinal" runat="server" Text="Enviar Correos"/>
				    </div>
			    </div>
		    </div>
	    </div>

    </div>
</asp:Content>
