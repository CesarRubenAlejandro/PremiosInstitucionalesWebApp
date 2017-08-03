<%@ Page Title="Juez" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="InicioJuez.aspx.cs" Inherits="PremiosInstitucionales.WebForms.InicioJuez" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="fadeView">
        <!-- Welcome message -->
	    <div class="container welcome-box">
          <!-- Main component for a primary marketing message or call to action -->
          <div class="jumbotron">
            <asp:Literal ID="litBienvenidoUsuario" runat="server" /> <!-- Bienvenido -->
            <p>El Premio Eugenio Garza Sada fue instaurado con el fin de perpetuar los valores que en vida distinguieron a don Eugenio Garza Sada. FEMSA y el Tecnológico de Monterrey agradecen su contribución como miembro del Jurado, pues su labor permite que los ideales y valores de don Eugenio continúen vigentes.</p>
          </div>
        </div>

        <!-- Dividing line -->
	    <hr/>

        <!-- Home component -->
	    <div class="container">
		    <div class="row">
			    <div class="col-lg-6 col-md-6 text-center">
				    <div class="service-box">
					    <a href="PremiosInstitucionalesJuez.aspx"><img class="service-icons" src='<%= ResolveUrl("/Resources/svg/juez.svg") %>'/></a>
					    <h3>Premios Institucionales</h3>
					    <hr class="shorthr" style="margin-bottom:10px;"/>
					    <p class="text-muted">Seleccione el Premio en el que participa como Jurado e ingrese a los registros de los candidatos a evaluar.</p>
				    </div>
			    </div>
			    <div class="col-lg-6 col-md-6 text-center">
				    <div class="service-box">
					    <a href="InformacionPersonalJuez.aspx"><img class="service-icons" src='<%= ResolveUrl("/Resources/svg/learning.svg") %>'/></a>
					    <h3>Informacion Personal</h3>
					    <hr class="shorthr" style="margin-bottom:10px;"/>
					    <p class="text-muted">Información de la cuenta como miembro del Jurado. En esta sección puede actualizar los datos de contacto.</p>
				    </div>
			    </div>
		    </div>
	    </div>
    </div>
</asp:Content>
