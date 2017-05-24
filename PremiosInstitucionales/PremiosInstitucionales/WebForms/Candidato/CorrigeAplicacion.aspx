<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorrigeAplicacion.aspx.cs" 
    MasterPageFile="~/MP-Global.Master" Inherits="PremiosInstitucionales.WebForms.CorrigeAplicacion" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div class="container fadeView">	
		<div class="row">
			<div class="col-lg-12 text-center">
                <h2>Modificación de solicitud de registro</h2>
				<h3 class="section-heading"><asp:Literal ID="litTituloPremio" runat="server" /></h3>
				<h4> <asp:Literal ID="litTituloCategoria" runat="server" /></h4>
				<hr class="shorthr"/>
			</div>
		</div>
        <div class="text-center">
			<h5 runat="server" id="alreadySubmittedLabel" visible="false"> Ya se ha realizado una aplicación para esta categoría. Para conocer el estatus, por favor dirigirse a <a href="WebForms/AplicacionesCandidato.aspx"> mis aplicaciones vigentes </a></h5>
		</div>
            <asp:Panel runat="server" id="PanelFormulario" class="row question-form">
            </asp:Panel>
            <div class="btn-group-right">
                <a href="AplicacionesCandidato.aspx">
                    <button type="button" class="btn btn-default">Cancelar</button>
                </a>
                <button type="button" class="btn btn-primary" onclick="sendFormAux()">Enviar</button>
            </div>
            <asp:Button style="display: none;" id="EnviarBtn" runat="server" onclick="EnviarBttn_Click" 
                    CssClass="ApplicationButton" Text="Enviar aplicación" />
    </div>
</asp:Content>
