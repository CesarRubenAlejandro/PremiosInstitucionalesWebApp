<%@ Page Title="" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="AplicacionesCandidato.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AplicacionesCandidato" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href='<%= ResolveUrl("~/Resources/css/AplicacionesCandidato.css")%>'  rel="stylesheet" type="text/css" />
    <script src='<%= ResolveUrl("~/Resources/js/AplicacionesCandidato.js") %>'></script>
    <div class="container fadeView">

        <div class="container">
		    <div class="row">
			    <div class="col-lg-12 text-center">
				    <h3 class="section-heading">Mis aplicaciones vigentes</h3>
				    <hr class="shorthr"/>
			    </div>
		    </div>
	    </div>

        <div runat="server" id="estadosaplicaciones"></div>
    </div>
</asp:Content>
