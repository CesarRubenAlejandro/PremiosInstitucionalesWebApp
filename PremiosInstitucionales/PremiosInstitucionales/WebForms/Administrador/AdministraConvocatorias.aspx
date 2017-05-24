<%@ Page Title="" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="AdministraConvocatorias.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.AdministrarConvocatoria" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <!-- CSS -->
    <link href='<%= ResolveUrl("~/Resources/css/dataTables.css")%>' rel="stylesheet" type="text/css"/>
    <link href='<%= ResolveUrl("~/Resources/css/jquery-ui.css")%>' rel="stylesheet" type="text/css"/>
    <!-- JS -->
    <script src='<%= ResolveUrl("~/Resources/js/jquery-ui.js")%>' defer="defer"></script>
    <script src='<%= ResolveUrl("~/Resources/js/jquery-ui.multidatespicker.js")%>' defer="defer"></script>
    <script src='<%= ResolveUrl("~/Resources/js/jquery.dataTables.js")%>' type="text/javascript" defer="defer"></script>
    <script src='<%= ResolveUrl("~/Resources/js/crearConvocatoria.js")%>' type="text/javascript" defer="defer"></script>
   
    <!-- Content -->
    <div class="container fadeView">

        <!-- Titulo -->
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <h3 class="section-heading"> Administrar Premio </h3>
                    <hr class="shorthr"/>
                </div>
            </div>
        </div>

        <!-- Gestionar info Premio -->
        <div class="row">
            <!-- left column -->
            <div class="col-md-4 col-sm-6 col-xs-12" style="height: 100%;">

                <div class="award-new-box">
                    <asp:Panel runat="server" class="award-thumbnail" ID="avatarImage" onclick="uploadImage();"></asp:Panel>
                </div>

                <asp:FileUpload ID="FileUploadImage" runat="server" class="text-center center-block well well-sm" Style="display: none;" onchange="ShowImagePreview(this);" />
                
                <div class="btn-group-mid">
                    <button type="button" class="btn btn-default" onclick="uploadImage();">Cambiar imagen del premio</button>
                </div>

            </div>
            <!-- edit form column -->
            <div class="col-md-8 col-sm-6 col-xs-12 personal-info">
                <div class="form-horizontal" role="form">
                    <div class="form-group">
                        <label class="col-lg-3 control-label">Nombre Premio:</label>
                        <div class="col-lg-8">
                            <asp:TextBox runat="server" class="form-control" type="text" ID="TituloPremioSeleccionado" />
                            <br />
                        </div>
                        <label class="col-lg-3 control-label">Descripción Premio:</label>
                        <div class="question-box col-lg-8">
                            <asp:TextBox runat="server" class="form-control form-text-area scrollbar-custom" Rows="5" onkeyup="updateCharactersLeft(this);" MaxLength="500" ID="DescripcionPremioSeleccionado" TextMode="MultiLine"></asp:TextBox>
                            <p> 500 caracteres restantes </p>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-11">
                            <div class="btn-group-right">
                                <asp:Button class="btn btn-primary" ID="GuardarCambiosBttn" Text="Guardar Cambios" OnClick="UpdateInfo" runat="server"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Tabla de Convocatorias -->
        <div class="container">
            <h4 class="section-heading"> Convocatorias </h4>
            <table id="listaConvocatorias" class="display" cellspacing="0" width="100%">
                <thead>
                    <tr>
                        <th>
                            <a data-toggle="modal" data-target="#modalCrearConvocatoria" style="cursor: pointer;">
                                <img src="/Resources/svg/plus.svg" class="avatar img-circle" alt="avatar" style="width: 28px; margin-left:-7px;"/>
                            </a>
                        </th>
                        <th>Nombre</th>
                        <th>Fecha Inicio</th>
                        <th>Fecha Fin</th>
                        <th>Fecha Veredicto</th>
                        <th>Estado</th>
                    </tr>
                </thead>
                <tbody runat="server" id="Prueba"></tbody>
            </table>
        </div>
    </div>
    <!-- Modal Crear Convocatoria  -->
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="modal fade" id="modalCrearConvocatoria" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h3 class="modal-title" id="myModalLabel">Nueva Convocatoria</h3>
                </div>
                <div class="form-horizontal" role="form" id="changePasswordForm">
                    <div class="modal-body">
                        <div class="form-group">
                            <label class="col-lg-4 control-label">Nombre Convocatoria:</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" ID="TituloNuevaConvocatoriaTB" class="form-control" type="text" style="width: 90%;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group" style="margin-top: 35px;">
                            <label class="col-lg-4 control-label"> Fechas: </label>
                            <div class="col-lg-8">
                                <div id="datepicker"></div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-4 control-label"> Fecha Inicio: </label>
                            <div class="col-lg-8">
                                <input id="FechaInicioNuevaConvo" name="FechaInicioNuevaConvo" class="form-control" type="text" style="width: 90%;" readonly="true"/>
                            </div>
                            <label class="col-lg-4 control-label"> Fecha Fin: </label>
                            <div class="col-lg-8">
                                <input id="FechaFinNuevaConvo" name="FechaFinNuevaConvo" class="form-control" type="text" style="width: 90%;" readonly="true"/>
                            </div>
                            <label class="col-lg-4 control-label"> Fecha Veredicto: </label>
                            <div class="col-lg-8">
                                <input id="FechaVeredicto" name="FechaVeredicto" class="form-control" type="text" style="width: 90%;" readonly="true"/>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                        <asp:Button runat="server" ID="GuardarNuevaBttn" class="btn btn-primary" OnClick="GuardarNuevaBttn_Click" Text="Crear"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>