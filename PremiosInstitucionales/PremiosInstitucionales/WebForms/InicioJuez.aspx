<%@ Page Title="Juez" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="InicioJuez.aspx.cs" Inherits="PremiosInstitucionales.WebForms.InicioJuez" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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
							    <input class="form-control" value="Rubén Eugenio" type="text"/>
						    </div>
						    </div>
						    <div class="form-group">
						    <label class="col-lg-3 control-label">Apellido(s):</label>
						    <div class="col-lg-8">
							    <input class="form-control" value="Cantú Vota" type="text"/>
						    </div>
						    </div>
						    <div class="form-group">
						    <label class="col-lg-3 control-label">Correo eléctronico:</label>
						    <div class="col-lg-8">
							    <input class="form-control" value="rubencv@hollowlife.com" type="text"/>
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
			    <div class="col-lg-4 col-md-6 text-center">
				    <div class="service-box">
					    <a href="/WebForms/PremiosInstitucionalesJuez.aspx"><img class="service-icons" src='<%= ResolveUrl("/Resources/svg/juez.svg") %>'/></a>
					    <h3>Premios Institucionales</h3>
					    <hr class="shorthr" style="margin-bottom:10px;"/>
					    <p class="text-muted">We even changed some of the icons! We take the extra effort to make our designs truly original.</p>
				    </div>
			    </div>
			    <div class="col-lg-4 col-md-6 text-center">
				    <div class="service-box">
					    <a href="perfilCandidato.html"><img class="service-icons" src='<%= ResolveUrl("/Resources/svg/learning.svg") %>'/></a>
					    <h3>Informacion Personal</h3>
					    <hr class="shorthr" style="margin-bottom:10px;"/>
					    <p class="text-muted">Look at this cool set of four icons describing different things about us! We use four, because it's the default.</p>
				    </div>
			    </div>
			    <div class="col-lg-4 col-md-6 text-center">
				    <div class="service-box">
					    <a data-toggle="modal" data-target="#modalInvite"><img class="service-icons" src='<%= ResolveUrl("/Resources/svg/proponer.svg") %>'/></a>
					    <h3>Proponer Candidato</h3>
					    <hr class="shorthr" style="margin-bottom:10px;"/>
					    <p class="text-muted">Because nothing says hard work and talent like editing a few lines of text.</p>
				    </div>
			    </div>
		    </div>
	    </div>
    </div>
</asp:Content>
