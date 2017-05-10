<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="AdministraFormulario.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraFormulario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <!-- Scripts -->
    <script src="../Resources/js/adminQ.js" type="text/javascript" defer="defer"></script>
    <script src="../Resources/js/jquery-ui.js" type="text/javascript" defer="defer"></script>
    <script src="../Resources/js/jquery.dataTables.js" type="text/javascript" defer="defer"></script>
    <script src="../Resources/js/AsignarJuez.js" type="text/javascript" defer="defer"></script>

    <!-- CSS -->
    <link   href="../Resources/css/dataTables.css" rel="stylesheet" type="text/css" />

    <!-- Contenido -->
        <div class="container fadeView">

            <!-- Premio & Categoria (Nombres) -->
            <div id="nombrePremioCategoria" runat="server"></div>
            
            <!-- Tabs -->
            <div id="centerDiv">
                <ul class="nav nav-tabs">
                    <li class="active li-center"><a data-toggle="tab" href="#Formulario"><strong>Formulario</strong></a></li>
                    <li class="li-center"><a data-toggle="tab" href="#Jueces"><strong>Asignar Jueces</strong></a></li>
                </ul>
            </div>

            <!-- Contenido Tabs -->
            <div class="tab-content">

                <!-- Formulario -->
                <div id="Formulario" class="tab-pane fade in active">
                    <div class="container">
	                    <div class="row">
		                    <div id="PreguntaPadre" class="wrapper" runat="server">
			                    <button class="add_button btn">Agrega Pregunta</button><br/><br/>
			                    <div id="simpleList" class="list-group" runat="server" ClientIDMode="Static">
			                    </div>
		                    </div>

                            <!-- Boton Guardar -->
                            <div style="width: 100%; text-align: right;">
                                <asp:Button class="btn btn-primary" ID="Button1" Text="Guardar Cambios" OnClick="SaveChanges" runat="server"/>
		                    </div>
	                    </div>
                    </div>
                </div>

                <!-- Asignar Jueces -->
                <div id="Jueces" class="tab-pane fade">
                    
                    <!-- Var compartida (JS & CS) -->
                    <input id="hiddenControl" type="hidden" runat="server"/>

                    <!-- Banco de Jueces -->
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

                        <!-- Jueces Asignados -->
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

                    <!-- Boton Guardar -->
                    <div style="width: 100%; text-align: right;">
                        <asp:Button class="btn btn-primary" ID="GuardarCambiosBttn" Text="Guardar Cambios" OnClick="SaveChanges" runat="server"/>
		            </div>

                </div>
            </div> 
        </div>
</asp:Content>
