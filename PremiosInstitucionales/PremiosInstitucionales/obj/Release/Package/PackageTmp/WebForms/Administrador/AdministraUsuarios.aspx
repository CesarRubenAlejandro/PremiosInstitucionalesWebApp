<%@ Page Title="" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="AdministraUsuarios.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraUsuarios" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    
    <script src='<%= ResolveUrl("~/Resources/js/jquery.dataTables.js")%>' type="text/javascript" defer="defer"></script>
    <script src='<%= ResolveUrl("~/Resources/js/listaUsuariosCandidato.js")%>' type="text/javascript" defer="defer"></script>
    <link href='<%= ResolveUrl("~/Resources/css/dataTables.css")%>' rel="stylesheet" type="text/css" /> 

    <div class="container fadeView" style="position:relative;">
		
		<h3>Información personal</h3>
		<h4>
            <asp:Literal ID="litUsuarios" runat="server" />
        </h4>

        <div style="position: absolute; top: 50px; right: 0px;">
            <asp:Button class="btn" ID="Button1" Text="Descargar" OnClick="GetExcel_Click" runat="server"/>
		</div>

		<div class="container">
			<table id="listaCandidatoTable" class="display" cellspacing="0" width="100%">
				<thead>
					<tr>
						<th>IP</th>
						<th>Nombres</th>
						<th>Apellidos</th>
						<th>Correo</th>
                        <th>Telefono</th>
                        <th>Nacionalidad</th>
                        <th>RFC</th>
                        <th>Direccion</th>
                        <th>Confirmacion de cuenta</th>
                        <th>Aviso de privacidad</th>
					</tr>
				</thead>
				<tbody id="listaCandidatosTableBody" runat="server">
				</tbody>
			</table>
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
</asp:Content>