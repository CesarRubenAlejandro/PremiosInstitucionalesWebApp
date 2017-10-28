<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="InicioCandidato.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.InicioCandidato" MasterPageFile="~/MP-Global.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
     <script type="text/javascript">
        function openModal() {
            $('#modalAvisoPrivacidad').modal('show');
        }
    </script>
    <div class="fadeView">
		<!-- Welcome message -->
		<div class="container welcome-box">
		<!-- Main component for a primary marketing message or call to action -->
		<div class="jumbotron">
			<asp:Literal ID="litBienvenidoUsuario" runat="server" /> <!-- Bienvenido -->
			<p>El Premio Eugenio Garza Sada fue instaurado con el fin de perpetuar los valores que en vida distinguieron a don Eugenio Garza Sada. FEMSA y el Tecnológico de Monterrey agradecen su participación en la Convocatoria, pues con su ayuda se escribe una página más en la historia del Premio.</p>
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
					    <p class="text-muted">Conoce los Premios Institucionales del Tecnológico de Monterrey en los que puedes participar como candidato.</p>
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
					    <p class="text-muted">¿Conoces a alguna persona, institución o grupo estudiantil que merece ser reconocido por su trascendencia? Invítalos a unirse a la plataforma.</p>
				    </div>
			    </div>
		    </div>
	    </div>
	</div>

        <!-- Modal aviso de privacidad -->
        <div class="modal fade" id="modalAvisoPrivacidad" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header text-center">
                        <h3 class="modal-title">Aviso de privacidad</h3>
                        <hr class="shorthr"/>
                    </div>
                    <div class="modal-body">
                        <!-- Imagen de escudo -->
                        <div class="text-center" style="margin-bottom: 16px;">
                            <img src='<%= ResolveUrl("~/Resources/svg/shield.svg") %>' style="width: 96px; height: 96px;" />
                        </div>
                        <!-- Texto Corto -->
                        <p>
                            En el supuesto de que por este medio usted proporcione datos personales sujetos a la normatividad vigente, le informamos que éstos podrían ser tratados por el Instituto Tecnológico y de Estudios Superiores de Monterrey (en adelante ITESM) con domicilio ubicado en Av. Eugenio Garza Sada Sur No. 2501, colonia Tecnológico en Monterrey, Nuevo León. C.P. 64849, en caso de que fuera necesario para cumplir con la finalidad para la cual usted nos ha enviado dicha información.
                        </p>
                        <!-- Ver PDF -->
                    <input type="checkbox" onchange="document.getElementById('ContentPlaceHolder_toggleCheckboxButton').style.display = this.checked ? '' : 'none';"/>
                        Al realizar clic en el botón de aceptar, usted está de acuerdo con compartir su información de acuerdo a las
                            <a target="_blank" type="application/pdf" href='<%= ResolveUrl("~/Document/PoliticasDePrivacidadTec.pdf") %>'> políticas de privacidad </a>
                        para el uso del sistema.
                    </div>
                    <div class="modal-footer">
                          <asp:Button id="cancelarButton" runat="server" onclick="CancelarBtn_Click" CssClass="btn" Text="Cancelar" />
                          <asp:Button style="display: none;" id="toggleCheckboxButton" runat="server" onclick="EnviarBtn_Click" CssClass="btn btn-primary" Text="Aceptar" />
                    </div>
                </div>
            </div>
        </div>
</asp:Content>