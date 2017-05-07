<%@ Page Title="" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="CreaJuez.aspx.cs" Inherits="PremiosInstitucionales.WebForms.CreaJuez" %>


<asp:Content ID="Content" ContentPlaceHolderID="HeadContent" runat ="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
    <script src="http://rubaxa.github.io/Sortable/Sortable.js"></script>
    <script src="../scripts/adminQ.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Contenido -->
    <div class="container fadeView">

        	<div class="container">
		<div class="row">
			<div class="col-lg-offset-3 col-lg-6 text-center">
				<h3  class="section-heading">Crea Juez</h3>
				<hr class="shorthr">
			</div>
		</div>
	</div>
        </div>



<div class="container">
	<div class="row">
        <form runat="server" id="FormularioPreguntas">
            <asp:TextBox ID="correoJuez" ClientIDMode="Static" runat="server" placeholder="Correo de Juez"></asp:TextBox><br />
            <div class="int-group">
                <asp:Button class="btn btn-primary" ID="Button1" runat="server" OnClick="Registra_Juez" Text="Registra Juez" style="width:100%;" />
            </div>
        </form>
	</div>
</div>
</asp:Content>
