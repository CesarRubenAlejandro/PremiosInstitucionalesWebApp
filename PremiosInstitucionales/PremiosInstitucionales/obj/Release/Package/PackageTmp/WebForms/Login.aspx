<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.Login" MasterPageFile="~/MP-Login.Master" EnableEventValidation="false"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
	<div class="paginasIniciales">
		<div id="vidtop-content">
			<div class="login-wrap">
				<div class="login-html no-enter-key">
					<img id="logoTec" src='<%= ResolveUrl("~/Resources/img/logotec.png")%>' alt="Logo Tec"/>
					<div class="option-tabs">
						<input id="tab-1" type="radio" name="tab" class="sign-in" onchange="changeTab(true)" checked="checked"/><label for="tab-1" class="tab">Inicio</label>
						<input id="tab-2" type="radio" name="tab" class="sign-up" onchange="changeTab(false)"/><label for="tab-2" class="tab">Registro</label>
					</div>
					<div class="login-form">
						<div class="sign-in-htm">
							<div class="int-group">
								<label for="user" class="int-label">Correo Electrónico<span class="req">*</span></label>
								<asp:TextBox class="int-input" ID="user" ClientIDMode="Static" runat="server" autocomplete="new-password" spellcheck="false"></asp:TextBox>
							</div>
							<div class="int-group">
								<label for="passlogin" class="int-label">Contraseña<span class="req">*</span></label>
								<asp:TextBox class="int-input" ID="passlogin" runat="server" type="password" autocomplete="new-password" data-type="password" spellcheck="false"></asp:TextBox>
							</div>
							<div class="int-group">
								<input id="checkBox" type="checkbox" class="check"/>
								<label for="check"><span class="icon"></span> Recordarme</label>
							</div>
							<div class="int-group">
								<asp:Button class="btn btn-primary" ID="Button1" runat="server" OnClick="Button1_Click" Text="Iniciar Sesión" style="width:100%;"/>
							</div>
							<hr class="custom-hr"/>
							<div class="foot-lnk">
								<a href="#" onclick="forgotPassword(true)">¿Olvidaste tu contraseña?</a>
							</div>
						</div>
						<div class="sign-up-htm">
							<div class="int-group">
								<label for="name" class="int-label">Nombre(s)<span class="req">*</span></label>
								<asp:TextBox ID="name" runat="server" class="int-input" type="text" autocomplete="off" spellcheck="false"></asp:TextBox>
							</div>
							<div class="int-group">
								<label for="lname" class="int-label">Apellido(s)<span class="req">*</span></label>
								<asp:TextBox ID="lname" runat="server" class="int-input" type="text" autocomplete="off" spellcheck="false"></asp:TextBox>
							</div>
							<div class="int-group">
								<label for="email" class="int-label">Correo Electrónico<span class="req">*</span></label>
								<asp:TextBox class="int-input" ID="email" runat="server" ClientIDMode="Static" type="text" autocomplete="new-password" spellcheck="false"></asp:TextBox>
							</div>
							<div class="int-group">
								<label for="passreg" class="int-label">Contraseña<span class="req">*</span></label>
								<asp:TextBox class="int-input" ID="passreg" runat="server" ClientIDMode="Static" TextMode="Password" autocomplete="new-password" spellcheck="false"></asp:TextBox>
							</div>
							<div class="int-group">
								<label for="passreg2" class="int-label">Confirmar constraseña<span class="req">*</span></label>
								<asp:TextBox class="int-input" ID="passreg2" runat="server" type="password" ClientIDMode="Static" data-type="password" TextMode="Password"  autocomplete="new-password" spellcheck="false"></asp:TextBox>
							</div>
							<div class="int-group">
								<asp:Button class="btn btn-primary" ID="RegisterButton" runat="server" OnClick="Registro_Click" Text="Regístrate" style="width:100%;"/>
							</div>
							<hr class="custom-hr"/>
							<div class="foot-lnk">
								<label for="tab-1" style="cursor:pointer">¿Ya eres miembro?</label>
							</div>
						</div>
						<div class="forgot-htm">
							<div class="hide-content">
								<div class="option-tabs">
									<label id="tab-3" class="tab">Recuperar Contraseña</label>
								</div>
								<div class="int-group">
									<label for="userforgot" class="int-label">Correo Electrónico<span class="req">*</span></label>
									<asp:TextBox class="int-input" runat="server" ID="userforgot" ClientIDMode="Static" autocomplete="new-password" spellcheck="false"></asp:TextBox>
								</div>
								<div class="int-group">
									<asp:Button class="btn btn-primary" ID="EnviarBoton" runat="server" Text="Enviar" OnClick="Recover_Click" style="width:100%; margin-top:15px"/>
								</div>
							</div>
							<hr class="custom-hr"/>
							<div class="foot-lnk">
								<a href="#" onclick="forgotPassword(false);">Volver al Inicio</a>
							</div>
						</div>
					</div>
				</div>
			</div>
            <img id="loader" src='<%= ResolveUrl("~/Resources/img/loader.gif")%>' alt="Cargando" style="display: none;"/>
		</div>
	</div>
</asp:Content>
