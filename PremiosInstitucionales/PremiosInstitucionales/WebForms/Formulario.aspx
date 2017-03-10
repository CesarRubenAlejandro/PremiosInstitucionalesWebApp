<%@ Page Title="" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="PremiosInstitucionales.WebForms.Formulario" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    
    <div class="container fadeView">	
		<div class="row">
			<div class="col-lg-12 text-center">
				<h3 class="section-heading"><asp:Literal ID="litTituloPremio" runat="server" /></h3>
				<h4> <asp:Literal ID="litTituloCategoria" runat="server" /></h4>
				<hr class="shorthr"/>
			</div>
		</div>
        <form runat="server">
        <asp:Panel runat="server" id="PanelFormulario" class="row question-form">
        </asp:Panel>
        </form>
    </div>
    

</asp:Content>
