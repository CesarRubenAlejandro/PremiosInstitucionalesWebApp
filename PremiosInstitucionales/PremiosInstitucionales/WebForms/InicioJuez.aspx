<%@ Page Title="Juez" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="PremiosJuez.aspx.cs" Inherits="PremiosInstitucionales.WebForms.InicioJuez" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href='<%= ResolveUrl("~/Resources/css/stylePremiosJuez.css")%>'  rel="stylesheet" type="text/css" />
    <div class="container fadeView">

        <div class="container">
		    <div class="row">
			    <div class="col-lg-12 text-center">
				    <h3 class="section-heading">Premios Institucionales</h3>
				    <hr class="shorthr">
			    </div>
		    </div>
	    </div>

        <div runat="server" id="premiosJuez"></div>
    </div>
</asp:Content>
