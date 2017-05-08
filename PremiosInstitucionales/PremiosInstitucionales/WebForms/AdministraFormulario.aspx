<%@ Page Title="" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="AdministraFormulario.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraFormulario" %>

<asp:Content ID="Content" ContentPlaceHolderID="HeadContent" runat ="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
    <script src="http://rubaxa.github.io/Sortable/Sortable.js"></script>
    <script src="/scripts/adminQ.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Contenido -->
    <div class="container fadeView">

        	<div class="container">
		<div class="row">
			<div class="col-lg-6 text-center">
				<h3 id="nombrePremio" class="section-heading" runat="server">Premio 1</h3>
				<hr class="shorthr">
			</div>
            <div class="col-lg-6 text-center">
				<h3 id="nombreCategoria" class="section-heading" runat="server">Categoría</h3>
				<hr class="shorthr">
			</div>
		</div>
        <div class="row">
            <div class="col-lg-6 text-center">
				<h3 id="nombreConvocatoria" class="section-heading" runat="server">Convocatoria 2017</h3>
				<hr class="shorthr">
			</div>
        </div>
	</div>



<div class="container">
	<div class="row">
        <form runat="server" id="FormularioPreguntas">
		<div id="PreguntaPadre" class="wrapper" runat="server">
			<button class="add_button">Agrega Pregunta</button>
			<div id="simpleList" class="list-group" runat="server" ClientIDMode="Static">
			</div>
		</div>
        <div class="col-sm-offset-6 col-sm-3">
         <asp:Button class="btn btn-primary" ID="Button1" runat="server"  OnClick="Guarda_Formulario" Text="Guardar" style="width:100%;"/><br /> <br /> <br />
            </div>
            </form>
	</div>
</div>
    </div>
</asp:Content>
