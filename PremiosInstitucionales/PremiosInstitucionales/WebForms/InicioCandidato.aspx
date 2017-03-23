<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioCandidato.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.InicioCandidato" MasterPageFile="~/mp-Candidato.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div>
        <!--
        <h1>Mis aplicaciones vigentes</h1>
        <div runat="server" id="contenidoiniciocandidato">

        </div> -->
        <div>
		    <!-- Welcome message -->
		    <div class="container welcome-box">
		    <!-- Main component for a primary marketing message or call to action -->
		    <div class="jumbotron">
			    <h1>Bienvenido, Antonio.</h1>
			    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut vitae feugiat ipsum, a finibus ex. Donec non ante erat. Vivamus aliquet fermentum sodales. In finibus porttitor odio, quis rutrum quam commodo quis. Cras ut vestibulum risus, et mattis nibh. Vestibulum placerat nisl quis nunc molli</p>
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
						    <p class="text-muted">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut vitae feugiat ipsum, a finibus ex. Donec non ante erat.</p>
					    </div>
				    </div>
				    <div class="col-lg-3 col-md-6 text-center">
					    <div class="service-box">
						    <a href="AplicacionesCandidato.aspx"><img class="service-icons" src='<%= ResolveUrl("/Resources/svg/clipboard.svg") %>'/></a>
						    <h3>Mis Aplicaciones Vigentes</h3>
						    <hr class="shorthr" style="margin-bottom:10px;"/>
						    <p class="text-muted">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut vitae feugiat ipsum, a finibus ex. Donec non ante erat. Lorem ipsum dolor sit amet.</p>
					    </div>
				    </div>
				    <div class="col-lg-3 col-md-6 text-center">
					    <div class="service-box">
						    <a href="InformacionPersonalCandidato.aspx"><img class="service-icons" src='<%= ResolveUrl("/Resources/svg/learning.svg") %>'/></a>
						    <h3>Informacion Personal</h3>
						    <hr class="shorthr" style="margin-bottom:10px;"/>
						    <p class="text-muted">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut vitae feugiat ipsum, a finibus ex. Donec non ante erat. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
					    </div>
				    </div>
				    <div class="col-lg-3 col-md-6 text-center">
					    <div class="service-box">
						    <a data-toggle="modal" data-target="#modalInvite"><img class="service-icons" src='<%= ResolveUrl("/Resources/svg/proponer.svg") %>'/></a>
						    <h3>Proponer Candidato</h3>
						    <hr class="shorthr" style="margin-bottom:10px;"/>
						    <p class="text-muted">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut vitae feugiat ipsum.</p>
					    </div>
				    </div>
			    </div>
		    </div>
	    </div>
    </div>
</asp:Content>
