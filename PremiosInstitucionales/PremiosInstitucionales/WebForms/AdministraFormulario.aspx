<%@ Page Title="" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="AdministraFormulario.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraFormulario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">


    <script src="http://rubaxa.github.io/Sortable/Sortable.js"></script>
    <script src="/scripts/adminQ.js"></script>
    <script src="../Resources/js/jquery-ui.js" defer="defer"></script>
    <script src="../Resources/js/jquery.dataTables.js" type="text/javascript" defer="defer"></script>
    <script src="../Resources/js/AsignarJuez.js" type="text/javascript" defer="defer"></script>
    <link   href="../Resources/css/dataTables.css" rel="stylesheet" type="text/css" />

    <form runat="server" id="FormularioPreguntas">

    <!-- Contenido -->
    <div class="container fadeView">

    <div id="nombrePremioCategoria" runat="server"></div>
    
    <div id="centerDiv">
        <ul class="nav nav-tabs">
            <li class="active li-center"><a data-toggle="tab" href="#Formulario"><strong>Formulario</strong></a></li>
            <li class="li-center"><a data-toggle="tab" href="#Jueces"><strong>Asignar Jueces</strong></a></li>
        </ul>
    </div>

    <div class="tab-content">
        <div id="Formulario" class="tab-pane fade in active">
            <!-- Frutos -->
            <div class="container">
	            <div class="row">
		            <div id="PreguntaPadre" class="wrapper" runat="server">
			            <button class="add_button btn">Agrega Pregunta</button><br/><br/>
			            <div id="simpleList" class="list-group" runat="server" ClientIDMode="Static">
			            </div>
		            </div>
                    <div style="width: 100%; text-align: right;">
                        <asp:Button class="btn btn-primary" ID="Button1" Text="Guardar Cambios" OnClick="SaveChanges" runat="server"/>
		            </div>
	            </div>
            </div>
        </div>

        <div id="Jueces" class="tab-pane fade">
            <input id="hiddenControl" type="hidden" runat="server"/>

            <div class="row">
		        <div class="container col-md-6">
                    <div class="judge-list">
                        <h5 style="text-align:center;">Banco de Jueces</h5>
					    <hr class="shorthr"/>
                        <table id="listaJuezTable" class="display" cellspacing="0" width="100%">
				            <thead>
					            <tr>
						            <th>IP</th>
						            <th>Nombres</th>
						            <th>Apellidos</th>
						            <th>Correo</th>
					            </tr>
				            </thead>
				            <tbody id="listaJuecesTableBody" runat="server">
				            </tbody>
			            </table>
                    </div>
		        </div>
                <div class="container col-md-6">
                    <div class="judge-list">
                        <h5 style="text-align:center;">Jueces Asignados</h5>
					    <hr class="shorthr"/>
                        <table id="listaJuezTableAsignados" class="display" cellspacing="0" width="100%">
				            <thead>
					            <tr>
						            <th>IP</th>
						            <th>Nombres</th>
						            <th>Apellidos</th>
						            <th>Correo</th>
					            </tr>
				            </thead>
				            <tbody id="listaJuezTableAsignadosBody" runat="server">
				            </tbody>
			            </table>
                    </div>
		        </div>
            </div>

            <div style="width: 100%; text-align: right;">
                <asp:Button class="btn btn-primary" ID="GuardarCambiosBttn" Text="Guardar Cambios" OnClick="SaveChanges" runat="server"/>
		    </div>
        </div>
    </div> 
    </div></form>
</asp:Content>
