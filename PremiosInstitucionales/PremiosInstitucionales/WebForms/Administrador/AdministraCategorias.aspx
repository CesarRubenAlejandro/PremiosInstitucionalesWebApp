<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="AdministraCategorias.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraCategorias"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
        
    <!-- CSS -->
    <link href='<%= ResolveUrl("~/Resources/css/jquery-ui.css")%>' rel="stylesheet" type="text/css"/>

    <!-- JS -->
    <script src='<%= ResolveUrl("~/Resources/js/jquery-ui.js")%>' defer="defer"></script>
    <script src='<%= ResolveUrl("~/Resources/js/jquery-ui.multidatespicker.js")%>' defer="defer"></script>
    <script src='<%= ResolveUrl("~/Resources/js/editarConvocatoria.js")%>' type="text/javascript"></script>

    <div class="modal fade" id="modalNewCategory" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h3 class="modal-title" id="myModalLabel">Agregar categoría</h3>
                    <hr class="shorthr"/>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div>
                                <label class="col-lg-12 control-label control-title" style="margin-top: 0px">Nombre:</label>
                                <asp:TextBox runat="server" class="form-control" placeholder="Alumnos" type="text" ID="tbCategoryTitle" />
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-primary" onclick="sendFormAux();">Agregar</button>
                    <asp:Button Style="display: none;" ID="EnviarBtn" runat="server" OnClick="EnviarBtn_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Contenido -->
    <div class="container fadeView">

        <asp:Button type="button" runat="server" OnClick="BackBtn_Click" class="backBtn"/>

        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <h3 class="section-heading text-center">Administrador de categorías</h3>
                    <hr class="shorthr"/>
                </div>
            </div>
        </div>

        <!-- Gestionar info Convocatoria -->
        <div class="row" style="position:relative; right:-15px;">
            <!-- left column -->
            <div class="col-md-4 col-sm-6 col-xs-12" style="height: 100%; margin-top: 20px;">
                    <div id="datepicker" class="pull-right"></div>
            </div>
            <!-- edit form column -->
            <div class="col-md-8 col-sm-6 col-xs-12 personal-info">
                <div class="form-horizontal" role="form">
                    <div class="form-group">
                        <h4><label class="col-lg-4 control-label"><strong>Premio:</strong></label></h4>
                        <div class="col-lg-7">
                            <h4 style="margin-top: 7px;"><asp:Literal ID="litTituloPremio" runat="server"></asp:Literal></h4>
                        </div>

                        <h5><label class="col-lg-4 control-label"><strong>Convocatoria:</strong></label></h5>
                        <div class="col-lg-7">
                            <asp:TextBox runat="server" class="form-control" type="text" ID="TituloNuevaConvocatoriaTB" style="display: inline-block; font-size: 20px; width: 225px;"/>
                            <br />
                        </div>
                        <h5><label class="col-lg-4 control-label">Fecha Inicio:</label></h5>
                        <div class="col-lg-7">
                            <input id="FechaInicioNuevaConvo" name="FechaInicioNuevaConvo" class="form-control" type="text" readonly="true" style="display: inline-block; font-size: 20px; width: 225px;"/>
                            <br />
                        </div>
                        <h5><label class="col-lg-4 control-label">Fecha Fin:</label></h5>
                        <div class="col-lg-7">
                            <input id="FechaFinNuevaConvo" name="FechaFinNuevaConvo" class="form-control" type="text" readonly="true" style="display: inline-block; font-size: 20px; width: 225px;"/>
                            <br />
                        </div>
                        <h5><label class="col-lg-4 control-label">Fecha Veredicto:</label></h5>
                        <div class="col-lg-7">
                            <input id="FechaVeredicto" name="FechaVeredicto" class="form-control" type="text" readonly="true" style="display: inline-block; font-size: 20px; width: 225px;"/>
                            <br />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-11">
                            <div class="btn-group-right">
                                <asp:Button class="btn btn-primary" ID="GuardarCambiosBttn" Text="Guardar Cambios" runat="server" OnClick="GuardarBttn_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
 
        <hr />

        <div class="container" style="margin-top: -30px;">
            <div class="row" runat="server" id="categoryList">
                <div class="col-md-4 item-list">
                    <a data-toggle="modal" data-target="#modalNewCategory" style="text-decoration: none">
                        <div class="create-item">
                            <img class="item-add" src="/Resources/svg/add.svg"/>
                            <h5 class="text-center" style="margin-top: 40px; font-weight: bold;">Agregar categoría </h5>
                        </div>
                    </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
