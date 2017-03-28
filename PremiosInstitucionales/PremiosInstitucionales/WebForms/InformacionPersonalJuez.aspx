<%@ Page Title="" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="InformacionPersonalJuez.aspx.cs" Inherits="PremiosInstitucionales.WebForms.InformacionPersonalJuez" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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
                    <div class="userHeader avatar img-circle img-thumbnail">
                        <asp:Panel runat="server" class="userClass" ID="avatarImage" onclick="uploadImage();"></asp:Panel>
                    </div>
                    <div class="text-center">
                        <asp:FileUpload ID="FileUploadImage" runat="server" class="text-center center-block well well-sm" Style="display: none;" onchange="ShowImagePreview(this);" />
                    </div>
                    <div class="form-group">
                        <div class="btn-group-mid">
                            <button type="button" id="Button1" class="btn btn-default" text="Cambiar imagen de perfil" onclick="uploadImage();">Cambiar imagen de perfil</button>
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
                                <asp:TextBox ID="CorreoTextBox" runat="server" class="form-control" type="text" disabled="disabled" ReadOnly="true"/>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-11">
                                <div class="btn-group-right">
                                    <a href="InicioJuez.aspx" class="no-underline">
                                        <button type="button" class="btn btn-default"> Cancelar
                                        </button>
                                    </a>
                                    <a class="no-underline" data-toggle="modal" data-target="#modalChangePassword" onclick="openChangePasswordModal()">
                                        <button type="button" class="btn btn-default"> Cambiar contraseña
                                        </button>
                                    </a>
                                    <button type="button" class="btn btn-primary" onclick="sendFormAux();">Guardar Cambios</button>
                                    <asp:Button Style="display: none;" ID="EnviarBtn" runat="server" Text="Guardar Cambios" OnClick="EnviarBtn_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Alerta de cambios guardados... -->
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
                            <asp:Button ID="ButtonChangePassword" class="btn btn-primary" runat="server" Text="Cambiar" OnClick="CambiarContrasena_Click" />
                        </div>
                    </div>
                    <div class="sp sp-circle" id="loadingspinner"></div>
                    <img class="change-password-success" id="passwordcheckmark" src='<%= ResolveUrl("~/Resources/svg/checkmark.svg") %>'></img>
                </div>
            </div>
        </div>

    </form>
</asp:Content>
