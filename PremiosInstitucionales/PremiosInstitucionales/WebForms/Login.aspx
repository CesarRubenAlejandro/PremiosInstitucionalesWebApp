<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.Login" MasterPageFile="~/PaginasIniciales.Master" EnableEventValidation="false"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
      
    <div class="paginasIniciales">
        <div id="vidtop-content">
		  <div class="login-wrap"> 
			<div class="login-html">
				<img id="logoTec" src='<%= ResolveUrl("~/img/logotec.png")%>' alt="Logo Tec">
				<div class="option-tabs">
					<input id="tab-1" type="radio" name="tab" class="sign-in" onchange="changeTab(true)" checked/><label for="tab-1" class="tab">Inicio</label>
					<input id="tab-2" type="radio" name="tab" class="sign-up" onchange="changeTab(false)"/><label for="tab-2" class="tab">Registro</label>
				</div>
				<div class="login-form">
					<div class="sign-in-htm">
						<div class="int-group">
							<label for="user" class="int-label">Correo Electrónico</label>
                            <asp:TextBox class="int-input" ID="user" ClientIDMode="Static" runat="server"></asp:TextBox><br />
						</div>
						<div class="int-group">
							<label for="passlogin" class="int-label">Contraseña</label>
                             <asp:TextBox class="int-input" ID="passlogin" runat="server" type="password"  autocomplete="off" data-type="password"></asp:TextBox><br />
						</div>
						<div class="int-group">
							<input id="check" type="checkbox" class="check" checked>
							<label for="check"><span class="icon"></span> Recordarme</label>
						</div>
						<div class="int-group">
                            <asp:Button class="btn btn-primary" ID="Button1" runat="server" OnClick="Button1_Click" Text="Inicia Sesión" style="width:100%;"/>
						</div>
						<hr class="custom-hr">
						<div class="foot-lnk">
							<a href="#" onclick="forgotPassword(true)">¿Olvidaste tu contraseña?</a>
						</div>
					</div>
					<div class="sign-up-htm">
					
					
						<div class="int-group">
							<label for="name" class="int-label">Nombre(s)</label>
							<input id="name" class="int-input" type="text"  autocomplete="off"/>
						</div>
						<div class="int-group">
							<label for="lname" class="int-label">Apellido(s)</label>
							<input id="lname" class="int-input" type="text"  autocomplete="off"/>
						</div>
						<div class="int-group">
							<label for="email" class="int-label">Correo Electrónico</label>
                            <asp:TextBox class="int-input" ID="email" runat="server" ClientIDMode="Static" type="text"></asp:TextBox><br />
						</div>
						<div class="int-group">
							<label for="passreg" class="int-label">Contraseña</label>
                             <asp:TextBox class="int-input" ID="passreg" runat="server" ClientIDMode="Static" TextMode="Password"></asp:TextBox><br />
						</div>
						<div class="int-group">
							<label for="passreg2" class="int-label">Confirmar constraseña</label>
                            <asp:TextBox class="int-input" ID="passreg2" runat="server" type="password" ClientIDMode="Static" data-type="password" TextMode="Password" ></asp:TextBox><br />
						</div>
						<div class="int-group">
                            <asp:Button class="btn btn-primary" ID="RegisterButton" runat="server" OnClick="Registro_Click" Text="Regístrate" style="width:100%;"/>
						</div>
						<hr class="custom-hr"/>
						<div class="foot-lnk">
                            <!-- ?? -->
							<label for="tab-1" style="cursor:pointer">¿Ya eres miembro?</label>
						</div>
					</div>
					<div class="forgot-htm">
						<div class="hide-content">
							<div class="option-tabs">
								<label class="tab">Recuperar Contraseña</label>
							</div>
							<div class="int-group">
								<label for="userforgot" class="int-label">Correo Electrónico</label>
                                <asp:TextBox class="int-input" runat="server" ID="userforgot" ClientIDMode="Static"></asp:TextBox> <br /><br />
							</div>
							<div class="int-group">
                                <asp:Button class="btn btn-primary" ID="EnviarBoton" runat="server" Text="Enviar" OnClick="Recover_Click" style="width:100%; margin-top:15px"/> <br /><br />
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
             <asp:Label class="labeliniciales" ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label><br />
        <asp:Label class="labeliniciales" runat="server" ID="Label3" Visible="false"></asp:Label> <br />
	</div>
    </div>
</asp:Content>
