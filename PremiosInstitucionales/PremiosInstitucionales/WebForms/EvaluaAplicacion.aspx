<%@ Page Title="" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="EvaluaAplicacion.aspx.cs" Inherits="PremiosInstitucionales.WebForms.EvaluaAplicacion" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container fadeView">	
		<div class="row">
			<div class="col-lg-12 text-center">
                <h2>Evaluación de Aplicación</h2>
				<h3 class="section-heading"><asp:Literal ID="litTituloPremio" runat="server" /></h3>
				<h4> <asp:Literal ID="litTituloCategoria" runat="server" /></h4>
				<hr class="shorthr"/>
			</div>
		</div>
        <form runat="server" id="FormFormulario">
            <asp:Panel runat="server" id="PanelFormulario" class="row question-form">
            </asp:Panel>
        </form>
    </div>
</asp:Content>
