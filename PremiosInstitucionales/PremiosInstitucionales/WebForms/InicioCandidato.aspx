<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="InicioCandidato.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.InicioCandidato" MasterPageFile="~/mp-Candidato.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div>
		<!-- Welcome message -->
		<div class="container welcome-box">
		<!-- Main component for a primary marketing message or call to action -->
		<div class="jumbotron">
			<asp:Literal ID="litBienvenidoUsuario" runat="server" /> <!-- Bienvenido -->
			<p>FEMSA y el Tecnológico de Monterrey agradecen su participación, pues con su ayuda se escribe una página más en la historia del Premio Eugenio Gaza Sada.</p>
		</div>
		</div>

		<!-- Dividing line -->
		<hr/>

		<!-- Home component -->
		<div class="container">
			<div class="row">
				<div class="col-lg-3 col-md-6 text-center">
					<div class="service-box">
						<a href="ListaPremios.aspx"><img class="service-icons" src='<%= ResolveUrl("/Resources/svg/badgebg.svg") %>'/></a>
						<h3>Premios Institucionales</h3>
						<hr class="shorthr" style="margin-bottom:10px;"/>
						<p class="text-muted">Conoce los cuatro Premios Institucionales del Tecnológico de Monterrey en los que puedes participar como candidato o proponente.</p>
					</div>
				</div>
				<div class="col-lg-3 col-md-6 text-center">
					<div class="service-box">
						<a href="AplicacionesCandidato.aspx"><img class="service-icons" src='<%= ResolveUrl("/Resources/svg/clipboard.svg") %>'/></a>
						<h3>Mis Aplicaciones Vigentes</h3>
						<hr class="shorthr" style="margin-bottom:10px;"/>
						<p class="text-muted">Encuentra aquí la(s) convocatoria(s) en donde estás participando.</p>
					</div>
				</div>
				<div class="col-lg-3 col-md-6 text-center">
					<div class="service-box">
						<a href="InformacionPersonalCandidato.aspx"><img class="service-icons" src='<%= ResolveUrl("/Resources/svg/learning.svg") %>'/></a>
						<h3>Informacion Personal</h3>
						<hr class="shorthr" style="margin-bottom:10px;"/>
						<p class="text-muted">Información compartida para el registro a la convocatoria del Premio.</p>
					</div>
				</div>
				<div class="col-lg-3 col-md-6 text-center">
					<div class="service-box">
						<a data-toggle="modal" data-target="#modalInvite"><img class="service-icons" src='<%= ResolveUrl("/Resources/svg/proponer.svg") %>'/></a>
						<h3>Proponer Candidato</h3>
						<hr class="shorthr" style="margin-bottom:10px;"/>
						<p class="text-muted">¿Conoces a alguna persona, institución o grupo estudiantil que merece ser reconocido por su trascendencia? Comienza el proceso de registro aquí.</p>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>