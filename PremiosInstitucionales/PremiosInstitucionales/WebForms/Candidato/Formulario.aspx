<%@ Page Title="Formulario" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="PremiosInstitucionales.WebForms.Formulario" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <link href='<%= ResolveUrl("~/Resources/css/normalize.css")%>'  rel="stylesheet" type="text/css" />
    <link href='<%= ResolveUrl("~/Resources/css/archivoFormas.css")%>'  rel="stylesheet" type="text/css" />
	<!--[if IE]>
  	    <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
    <!-- remove this if you use Modernizr -->
	<script>
		(function (e, t, n) { var r = e.querySelectorAll("html")[0]; r.className = r.className.replace(/(^|\s)no-js(\s|$)/, "$1js$2") })(document, window, 0);
	</script>
    <script src='<%= ResolveUrl("~/Resources/js/custom-file-input.js") %>' defer="defer"></script>

    <div class="container fadeView">

        <a href="ListaPremios.aspx">
            <button type="button" class="backBtn"></button>
        </a>

        <div class="row">
            <div class="col-lg-12 text-center">
                <h3 class="section-heading">
                    <asp:Literal ID="litTituloPremio" runat="server" /></h3>
                <h4>
                    <asp:Literal ID="litTituloCategoria" runat="server" /></h4>
                <hr class="shorthr" />
            </div>
        </div>
        <div class="text-center">
            <h5 runat="server" id="alreadySubmittedLabel" visible="false">Ya se ha realizado una aplicación para esta categoría. Para conocer el estatus, por favor dirigirse a <a href="AplicacionesCandidato.aspx">mis aplicaciones vigentes </a></h5>
        </div>

        <div runat="server" id="uploadFile" visible="false" class="question-box">
            <h5>Selecciona un archivo:</h5>
			<div class="box">
				<input type="file" name="file" id="file" class="inputfile"/>
				<label for="file"><span></span> <strong><svg xmlns="http://www.w3.org/2000/svg" width="20" height="17" viewBox="0 0 20 17"><path d="M10 0l-5.2 4.9h3.3v5.1h3.8v-5.1h3.3l-5.2-4.9zm9.3 11.5l-3.2-2.1h-2l3.4 2.6h-3.5c-.1 0-.2.1-.2.1l-.8 2.3h-6l-.8-2.2c-.1-.1-.1-.2-.2-.2h-3.6l3.4-2.6h-2l-3.2 2.1c-.4.3-.7 1-.6 1.5l.6 3.1c.1.5.7.9 1.2.9h16.3c.6 0 1.1-.4 1.3-.9l.6-3.1c.1-.5-.2-1.2-.7-1.5z"/></svg> Escoge un archivo&hellip;</strong></label>
			</div>
        </div>

        <asp:Panel runat="server" ID="PanelFormulario" class="row question-form">
        </asp:Panel>

        <div class="btn-group-right" id="btnManager" runat="server">
            <a href="ListaPremios.aspx">
                <button type="button" class="btn btn-default">Cancelar</button>
            </a>
            <button type="button" class="btn btn-primary" onclick="sendFormAux()">Enviar</button>
            <asp:Button Style="display: none;" ID="EnviarBtn" runat="server" OnClick="EnviarAplicacion" Text="Enviar aplicación" />
        </div>
    </div>
</asp:Content>
