<%@ Page Title="" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="AsignarJuez.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AsignarJuez" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="form1" runat="server">
    <script src="../Resources/js/jquery.dataTables.js" type="text/javascript" defer="defer"></script>
    <script src="../Resources/js/AsignarJuez.js" type="text/javascript" defer="defer"></script>
    <link href='../Resources/css/dataTables.css' rel="stylesheet" type="text/css" />

    <input id="hiddenControl" type="hidden" runat="server"/>

    <div class="container fadeView">
		
        <div id="nombrePremioCategoria" runat="server"></div>

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
    </form>
</asp:Content>
