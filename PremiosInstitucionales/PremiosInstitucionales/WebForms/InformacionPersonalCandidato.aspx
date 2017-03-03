<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformacionPersonalCandidato.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.InformacionPersonalCandidato" MasterPageFile="~/mp-Candidato.Master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <form id="form1" runat="server" style="all: unset;">
    <div class="container fadeView">
    <div class="container">
        <div class="row">
            <div class="col-lg-12 text-center">
                <h3 class="section-heading">Información Personal</h3>
                <hr class="shorthr">
            </div>
        </div>
    </div>
    <div class="row">
      <!-- left column -->
      <div class="col-md-4 col-sm-6 col-xs-12" style="height: 100%;">
        <div class="text-center">
          <img src="https://x1.xingassets.com/assets/frontend_minified/img/users/nobody_m.original.jpg" class="avatar img-circle img-thumbnail avatar-upload" alt="avatar" style="max-width: 264px;" onClick="uploadImage();">
          <input type="file" id ="upImage" class="text-center center-block well well-sm" style="display: none;">
        </div>
        <div class="form-group">
              <div class="btn-group-mid">
                <button onClick="uploadImage();" type="button" class="btn btn-default">Cambiar imagen de perfil</button>
            </div>
          </div>
      </div>
      <!-- edit form column -->
      <div class="col-md-8 col-sm-6 col-xs-12 personal-info">
        <form class="form-horizontal" role="form">
          <div class="form-group">
            <label class="col-lg-3 control-label">Nombre(s):</label>
            <div class="col-lg-8">
              <asp:TextBox ID="NombresTextBox" runat="server" class="form-control" value="Rubén Eugenio" type="text"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-lg-3 control-label">Apellido(s):</label>
            <div class="col-lg-8">
              <input class="form-control" value="Cantú Vota" type="text"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-lg-3 control-label">Correo eléctronico:</label>
            <div class="col-lg-8">
              <input class="form-control" value="rubencv@hollowlife.com" type="text"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-lg-3 control-label">Direccion:</label>
            <div class="col-lg-8">
              <input class="form-control" value="Garza Sada 64989" type="text"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-lg-3 control-label">Teléfono:</label>
            <div class="col-lg-8">
              <input class="form-control" value="XXX-XXXX-XXXX" type="text"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-lg-3 control-label">RFC:</label>
            <div class="col-lg-8">
              <input class="form-control" value="AJUA102343" type="text"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-lg-3 control-label">Nacionalidad:</label>
            <div class="col-lg-8">
              <input class="form-control" value="Mexicana" type="text"/>
            </div>
          </div>
          <div class="form-group">
            <div class="col-md-11">
              <div class="btn-group-right">
                <a data-toggle="modal" data-target="#modalChangePassword" onclick="openChangePasswordModal()"><button type="button" class="btn btn-default">Cambiar contraseña</button></a>
                <button type="button" class="btn btn-primary" onclick="saveChanges()">Guardar Cambios</button>
              </div>
            </div>
          </div>
        </form>
      </div>
    </div>
    <div class="row" style="margin-top: 20px;" id="changeAlert">
      <div class="col-md-6 col-md-offset-3">
      <div class="alert alert-info alert-dismissable">
        <a class="panel-close close" onclick="closeAlert()">×</a> 
        <i class="fa fa-coffee"></i>
        Los <strong>cambios</strong> han sido guardados.
      </div>
    </div>
    </div>
  </div>

  <!-- Modal cambiar password -->
	<div class="modal fade" id="modalChangePassword" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">		
            <div class="modal-header text-center">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h3 class="modal-title" id="myModalLabel">Cambiar contraseña</h3>
            </div>
            <form class="form-horizontal" role="form" id="changePasswordForm">
              <div class="modal-body">
                <div class="form-group">
                  <label class="col-lg-4 control-label">Contraseña actual:</label>
                  <div class="col-lg-8">
                    <input class="form-control" type="password">
                  </div>
                </div>
                <div class="form-group">
                  <label class="col-lg-4 control-label">Contraseña nueva:</label>
                  <div class="col-lg-8">
                    <input class="form-control" type="password">
                  </div>
                </div>
                <div class="form-group">
                  <label class="col-lg-4 control-label">Confirmar contraseña:</label>
                  <div class="col-lg-8">
                    <input class="form-control" type="password">
                  </div>
                </div>
              </div>
              <div class="modal-footer">
                  <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                  <button type="button" class="btn btn-primary" onclick="changePassword()">Cambiar</button>
              </div>
            </form>
            <div class="sp sp-circle" id="loadingSpinner"></div>
            <img class="change-password-success" id="passwordCheckMark" src='<%= ResolveUrl("~/Resources/svg/checkmark.svg") %>'></img>
        </div>
    </div>
	</div>

    
    <h1>Mis datos personales</h1>
    <asp:Label class="appLabel" ID="Mensaje" runat="server"></asp:Label>
    <br />
    <asp:Button ID="EditarBtn" runat="server" Text="Editar datos" />
    
    <br /><br />
    <asp:Label class="appLabel" ID="NombresLabel" runat="server" Text="Nombres: "></asp:Label>
    <br /><br />
    <asp:Label class="appLabel" ID="ApellidosLabel" runat="server" Text="Apellidos: "></asp:Label>
    <asp:TextBox ID="ApellidosTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label class="appLabel" ID="CorreoLabel" runat="server" Text="Correo: "></asp:Label>
    <asp:TextBox ID="CorreoTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label class="appLabel" ID="DomicilioLabel" runat="server" Text="Domicilio: "></asp:Label>
    <asp:TextBox ID="DomicilioTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label class="appLabel" ID="TelefonoLabel" runat="server" Text="Teléfono: "></asp:Label>
    <asp:TextBox ID="TelefonoTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label class="appLabel" ID="Label1" runat="server" Text="RFC/Clave organización: "></asp:Label>
    <asp:TextBox ID="RFCTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    <asp:Label class="appLabel" ID="Label2" runat="server" Text="Nacionalidad: "></asp:Label>
    <asp:TextBox ID="NacionalidadTextBox" runat="server" Enabled="false"></asp:TextBox>
    <br /><br />
    
    <asp:Button ID="EnviarBtn" runat="server" Enabled="false" Text="Enviar" OnClick="EnviarBtn_Click" />

    <hr />
    

    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" />
        </form>

</asp:Content>