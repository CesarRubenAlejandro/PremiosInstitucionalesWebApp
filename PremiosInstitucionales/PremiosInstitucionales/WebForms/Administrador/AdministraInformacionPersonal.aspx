﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="AdministraInformacionPersonal.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraInformacionPersonal" EnableEventValidation="false" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

        <div class="container fadeView">

            <asp:Button type="button" class="closeBtn" runat="server" OnClick="CloseBtn_Click"/>

            <div class="container">
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <h3 class="section-heading">Administración de información personal</h3>
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
                <!-- Forma para candidatos - form column -->
                <div class="col-md-8 col-sm-6 col-xs-12 personal-info" runat="server" id="controlFormCandidato">
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
                                <asp:TextBox ID="CorreoTextBox" runat="server" class="form-control" type="text" disabled="disabled" ReadOnly="true" />
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
                        <div class="form-group" runat="server" id="dataOptionsCandidato">
                            <div class="col-md-11">
                                <div class="btn-group-right">
                                    <a class="no-underline" data-toggle="modal" data-target="#modalChangePassword" onclick="openChangePasswordModal()">
                                        <button type="button" class="btn btn-default">
                                            Cambiar contraseña
                                        </button>
                                    </a>
                                    <button runat="server" id="guardarCambiosBtn" type="button" class="btn btn-primary" onclick="sendFormAux();">Guardar Cambios</button>

                                    <asp:Button Style="display: none;" ID="EnviarBtn" runat="server" Text="Guardar Cambios" OnClick="EnviarBtn_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Forma para jueces - form column -->
                <div class="col-md-8 col-sm-6 col-xs-12 personal-info" runat="server" id="controlFormJuez">
                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <label class="col-lg-3 control-label">Nombre(s):</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="jNombresTextBox" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 control-label">Apellido(s):</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="jApellidosTextBox" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 control-label">Correo eléctronico:</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="jCorreoTextBox" runat="server" class="form-control" type="text" disabled="disabled" ReadOnly="true"/>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-11">
                                <div class="btn-group-right">
                                    <a class="no-underline" data-toggle="modal" data-target="#modalChangePassword" onclick="openChangePasswordModal()">
                                        <button type="button" class="btn btn-default"> Cambiar contraseña
                                        </button>
                                    </a>
                                    <button type="button" class="btn btn-primary" onclick="sendFormAux();">Guardar Cambios</button>
                                    <asp:Button Style="display: none;" ID="Button2" runat="server" Text="Guardar Cambios" OnClick="EnviarBtn_Click" />
                                </div>
                            </div>
                        </div>
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
                    
                </div>
            </div>
        </div>
    <asp:Panel class="row" style="margin-top: 20px;" id="alertInfo" runat="server" Visible="false">
      <div class="col-md-6 col-md-offset-3">
      <div class="alert alert-info alert-dismissable">
        <a class="panel-close close" data-dismiss="alert">×</a> 
        <i class="fa fa-coffee"></i>
        <asp:Literal ID="litAlertMessage" runat="server" />
      </div>
    </div>
    </asp:Panel>
    
</asp:Content>
