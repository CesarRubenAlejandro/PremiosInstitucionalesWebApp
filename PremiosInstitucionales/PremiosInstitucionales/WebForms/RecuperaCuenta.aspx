<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperaCuenta.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.RecuperaCuenta" MasterPageFile="~/PaginasIniciales.Master" EnableEventValidation="false"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div class="paginasIniciales">
		<div id="vidtop-content">
			<div class="login-wrap" style="height: 480px;">
				<div class="login-html">
					<img id="logoTec" src='<%= ResolveUrl("~/Resources/img/logotec.png")%>' alt="Logo Tec"/>
					<div class="option-tabs">
						<label class="tab">Recuperar Contraseña</label>
					</div>
					<div class="login-form">
						<div class="int-group">
							<label for="passreg" class="int-label">Contraseña<span class="req">*</span></label>
							<asp:TextBox class="int-input" ID="PasswordTextBox" runat="server" ClientIDMode="Static" TextMode="Password" autocomplete="new-password" spellcheck="false"></asp:TextBox>
						</div>
						<div class="int-group">
							<label for="passreg2" class="int-label">Confirmar constraseña<span class="req">*</span></label>
							<asp:TextBox class="int-input" ID="ConfirmPasswordTextBox" runat="server" type="password" ClientIDMode="Static" data-type="password" TextMode="Password"  autocomplete="new-password" spellcheck="false"></asp:TextBox>
						</div>
						<div class="int-group">
							<asp:Button class="btn btn-primary" ID="Boton" runat="server" OnClick="Button1_Click" Text="Cambiar contraseña" style="width:100%;"/>
						</div>
						<hr class="custom-hr"/>
						<div class="foot-lnk">
							<a href="Login.aspx"><label style="cursor:pointer">Regresar a Inicio de Sesión</label></a>
						</div>
					</div>
				</div>
			</div>

            <!-- Modal para dar un mensaje -->
            <div class="modal fade" id="modalMensaje" tabindex="-1" role="dialog" style="margin-top: 150px;">
                <div class="modal-dialog modal-sm">
                  <div class="modal-content">
                    <div class="modal-header text-center">
                      <span runat="server" id="modalMensajeTitulo"></span>
                    </div>
                    <div class="modal-body">
                        <asp:Label class="labeliniciales" runat="server" ID="Mensaje" style="color: black;"></asp:Label>
                    </div>
                    <div class="modal-footer">
                      <button type="button" class="btn btn-primary" data-dismiss="modal" style="width: 100%"> Aceptar </button>
                    </div>
                  </div>
                </div>
              </div>
		</div>
	</div>
</asp:Content>
