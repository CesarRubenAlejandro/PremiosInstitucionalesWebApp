<<<<<<< HEAD
﻿<%-- 

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioCandidato.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.InicioCandidato" MasterPageFile="~/mp-Candidato.Master"%>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">


 </asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="navBarIcons" Runat="Server">
    <asp:HyperLink class="naviconos" ID="HyperLink3" NavigateUrl="~/WebForms/ListaPremios.aspx" runat="server" ><img class="service-icons" style = " margin-left: 5%; padding-top:5px; width: 5%;" src='<%= ResolveUrl("~/svg/badgebg.svg")%>'/></asp:HyperLink>
     <asp:HyperLink class="naviconos" ID="HyperLink4" NavigateUrl="~/WebForms/Login.aspx" runat="server" ><a href="aplicaciones.html"><img class="service-icons" style = " margin-left: 5%; padding-top:5px; width: 5%;" src='<%= ResolveUrl("~/svg/clipboard.svg")%>'/></a></asp:HyperLink>
     <asp:HyperLink class="naviconos" ID="HyperLink5" NavigateUrl="~/WebForms/Login.aspx" runat="server" ><a href="aplicaciones.html"><img class="service-icons" style = " margin-left: 5%; padding-top:5px; width: 5%;" src='<%= ResolveUrl("~/svg/learning.svg")%>'/></a></asp:HyperLink>
     <asp:HyperLink class="naviconos" ID="HyperLink6" NavigateUrl="~/WebForms/Login.aspx" runat="server" ><a href="aplicaciones.html"><img class="service-icons" style = " margin-left: 5%; padding-top:5px; width: 5%;" src='<%= ResolveUrl("~/svg/proponer.svg")%>'/></a></asp:HyperLink>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div>
        <div runat="server" id="contenidoiniciocandidato">

        </div>
        	<div class="fadeView">
	
	<!-- Welcome message -->
	<div class="container welcome-box">
      <!-- Main component for a primary marketing message or call to action -->
      <div class="jumbotron">
        <h1>Bienvenido, Antonio.</h1>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut vitae feugiat ipsum, a finibus ex. Donec non ante erat. Vivamus aliquet fermentum sodales. In finibus porttitor odio, quis rutrum quam commodo quis. Cras ut vestibulum risus, et mattis nibh. Vestibulum placerat nisl quis nunc molli</p>
      </div>
    </div>

	<!-- Invite candidate -->
	<div class="modal fade" id="modalInvite" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">		
                <div class="modal-header text-center">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h3 class="modal-title" id="myModalLabel">Proponer Candidato</h3>
					<hr class="shorthr">
                </div>
                <div class="modal-body">
					<form class="form-horizontal" role="form">
						<div class="form-group">
						<label class="col-lg-3 control-label">Nombre(s):</label>
						<div class="col-lg-8">
							<input class="form-control" value="Rubén Eugenio" type="text">
						</div>
						</div>
						<div class="form-group">
						<label class="col-lg-3 control-label">Apellido(s):</label>
						<div class="col-lg-8">
							<input class="form-control" value="Cantú Vota" type="text">
						</div>
						</div>
						<div class="form-group">
						<label class="col-lg-3 control-label">Correo eléctronico:</label>
						<div class="col-lg-8">
							<input class="form-control" value="rubencv@hollowlife.com" type="text">
						</div>
						</div>
					</form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-primary">Invitar</button>
                </div>
            </div>
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
--%>


<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="InicioCandidato.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.InicioCandidato" MasterPageFile="~/mp-Candidato.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
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
    </div>
</asp:Content>