<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformacionPersonalCandidato.aspx.cs"
    Inherits="PremiosInstitucionales.WebForms.InformacionPersonalCandidato" MasterPageFile="~/mp-Candidato.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <form id="form1" runat="server" style="all: unset;">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="container fadeView">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <h3 class="section-heading">Información Personal</h3>
                        <hr class="shorthr" />
                    </div>
                </div>
            </div>
            <div class="row">
                <!-- left column -->
                <div class="col-md-4 col-sm-6 col-xs-12" style="height: 100%;">
                    <div class="text-center">
                        <img src="https://x1.xingassets.com/assets/frontend_minified/img/users/nobody_m.original.jpg" class="avatar img-circle img-thumbnail avatar-upload" alt="avatar" style="max-width: 264px;" onclick="uploadImage();">
                        
                        <asp:FileUpload ID="FileUploadImage" runat="server" class="text-center center-block well well-sm" style="display: none;"/>
                    </div>
                    <div class="form-group">
                        <div class="btn-group-mid">
                            <button onclick="uploadImage();" type="button" class="btn btn-default">Cambiar imagen de perfil</button>
                        </div>
                    </div>
                </div>
                <!-- edit form column -->
                <div class="col-md-8 col-sm-6 col-xs-12 personal-info">
                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <label class="col-lg-3 control-label">Nombre(s):</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="NombresTextBox" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 control-label">Apellido(s):</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="ApellidosTextBox" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 control-label">Correo eléctronico:</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="CorreoTextBox" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 control-label">Direccion:</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="DomicilioTextBox" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 control-label">Teléfono:</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="TelefonoTextBox" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 control-label">RFC:</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="RFCTextBox" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 control-label">Nacionalidad:</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="NacionalidadTextBox" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-11">
                                <div class="btn-group-right">
                                    <a data-toggle="modal" data-target="#modalChangePassword" onclick="openChangePasswordModal()">
                                        <button type="button" class="btn btn-default">Cambiar contraseña</button>
                                    </a>
                                    <asp:Button class="btn btn-primary" ID="EnviarBtn" runat="server" Text="Guardar Cambios" OnClick="EnviarBtn_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="changealert" style="margin-top: 20px; opacity: 0;">
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
                    <div class="form-horizontal" role="form" id="changepasswordform">
                        <div class="modal-body">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Contraseña actual:</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="currentPwdTextBox" runat="server" class="form-control" type="password" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Contraseña nueva:</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="newPwdTextBox" runat="server" class="form-control" type="password" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Confirmar contraseña:</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="confirmNewPwdTextBox" runat="server" class="form-control" type="password" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                            <!-- <button type="button" class="btn btn-primary" onclick="changePassword()">Cambiar</button> -->
                            <asp:Button ID="ButtonChangePassword" class="btn btn-primary" runat="server" Text="Cambiar" OnClick="CambiarContrasena_Click" />
                        </div>
                    </div>
                    <div class="sp sp-circle" id="loadingspinner"></div>
                    <img class="change-password-success" id="passwordcheckmark" src='<%= ResolveUrl("~/Resources/svg/checkmark.svg") %>'></img>
                </div>
            </div>
        </div>


        <h1>Mis datos personales</h1>
        <asp:Label class="appLabel" ID="Mensaje" runat="server"></asp:Label>
        <br />
        <asp:Button ID="EditarBtn" runat="server" Text="Editar datos" />
        <%-- 
    <asp:Button ID="EnviarBtn" runat="server" Enabled="false" Text="Enviar" OnClick="EnviarBtn_Click" />
        --%>
        <hr />


        
        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" />
    </form>


</asp:Content>
