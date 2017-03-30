<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioCandidato.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.InicioCandidato" MasterPageFile="~/MasterPage.Master"%>

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
	<hr>

	<!-- Home component -->
	<div class="container">
		<div class="row">
			<div class="col-lg-3 col-md-6 text-center">
				<div class="service-box">
                    <asp:HyperLink class="linkiniciales" ID="HyperLink2" NavigateUrl="~/WebForms/ListaPremios.aspx" runat="server" ><img class="service-icons" src='<%= ResolveUrl("~/svg/badgebg.svg")%>'/></asp:HyperLink>
					<h3>Premios Institucionales</h3>
					<hr class="shorthr" style="margin-bottom:10px;">
					<p class="text-muted">We even changed some of the icons! We take the extra effort to make our designs truly original.</p>
				</div>
			</div>
			<div class="col-lg-3 col-md-6 text-center">
				<div class="service-box">
                    <asp:HyperLink class="linkiniciales" ID="HyperLink1" NavigateUrl="~/WebForms/Login.aspx" runat="server" ><a href="aplicaciones.html"><img class="service-icons" src='<%= ResolveUrl("~/svg/clipboard.svg")%>'/></a></asp:HyperLink>

					<h3>Mis Aplicaciones Vigentes</h3>
					<hr class="shorthr" style="margin-bottom:10px;">
					<p class="text-muted">Guaranteed to use the same fucking template that every other bootstrap website uses, downloaded straight from The Web™</p>
				</div>
			</div>
			<div class="col-lg-3 col-md-6 text-center">
				<div class="service-box">
					<a href="perfilCandidato.html"><img class="service-icons" src='<%= ResolveUrl("~/svg/learning.svg")%>'></a>
					<h3>Informacion Personal</h3>
					<hr class="shorthr" style="margin-bottom:10px;">
					<p class="text-muted">Look at this cool set of four icons describing different things about us! We use four, because it's the default.</p>
				</div>
			</div>
			<div class="col-lg-3 col-md-6 text-center">
				<div class="service-box">
					<a data-toggle="modal" data-target="#modalInvite"><img class="service-icons" src='<%= ResolveUrl("~/svg/proponer.svg")%>'/></a>
					<h3>Proponer Candidato</h3>
					<hr class="shorthr" style="margin-bottom:10px;">
					<p class="text-muted">Because nothing says hard work and talent like editing a few lines of text.</p>
				</div>
			</div>
		</div>
	</div>

	</div>
    </div>
</asp:Content>
