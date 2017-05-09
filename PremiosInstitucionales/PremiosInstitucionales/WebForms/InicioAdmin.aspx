<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="InicioAdmin.aspx.cs" Inherits="PremiosInstitucionales.WebForms.InicioAdmin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
        <!-- Welcome message -->
        <div class="container welcome-box">
            <!-- Main component for a primary marketing message or call to action -->
            <div class="jumbotron">
                <h1>Administrador de sitio</h1>
            </div>
        </div>

        <!-- Dividing line -->
        <hr>

        <div class="container" style="margin-bottom: 36px;">
            <div class="row" style="margin-left: 5px; margin-right: 5px">
                <a href="AdministraPremios.aspx" class="no-underline">
                    <div class="col-lg-6">
                        <div class="row service-list">
                            <div class="col-xs-4 text-center" style="height: 96px">
                                <img class="service-icon" src="../Resources/svg/badgebg.svg" />
                            </div>
                            <div class="col-xs-8">
                                <h5>Premios Institucionales </h5>
                                <h6>En esta sección, se podrán crear, editar y eliminar premios. Asímismo, se puede acceder a toda la
                            información de éste. </h6>
                            </div>
                        </div>
                    </div>
                </a>
                <a href="AdministraAplicacionesPendientes.aspx" class="no-underline">
                    <div class="col-lg-6">
                        <div class="row service-list">
                            <div class="col-xs-4 text-center" style="height: 96px">
                                <img class="service-icon" src="../Resources/svg/clipboard.svg" />
                            </div>
                            <div class="col-xs-8">
                                <h5>Registros pendientes </h5>
                                <h6>Lista de registros que se deberán analizar para validar que cumplan con los requisitos de la convocatoria.</h6>
                            </div>
                        </div>
                    </div>
                </a>
                <a href="AdministraUsuarios.aspx?t=candidato" class="no-underline">
                    <div class="col-lg-6">
                        <div class="row service-list">
                            <div class="col-xs-4 text-center" style="height: 96px">
                                <img class="service-icon" src="../Resources/svg/learning.svg" />
                            </div>
                            <div class="col-xs-8">
                                <h5>Información de candidatos </h5>
                                <h6>Sección que contiene una lista de todos los candidatos registrados. </h6>
                            </div>
                        </div>
                    </div>
                </a>
                <a data-toggle="modal" data-target="#modalInvite">
                <div class="col-lg-6">
                    <div class="row service-list">
                        <div class="col-xs-4 text-center" style="height: 96px">
                            <img class="service-icon" src="../Resources/svg/proponer.svg" />
                        </div>
                        <div class="col-xs-8">
                            <h5>Proponer candidato </h5>
                            <h6>Proponer candidato a registrarse en la convocatoria de Premios Institucionales. </h6>
                        </div>
                    </div>
                </div>
                    </a>
                <a href="AdministraUsuarios.aspx?t=juez" class="no-underline">
                    <div class="col-lg-6">
                        <div class="row service-list">
                            <div class="col-xs-4 text-center" style="height: 96px">
                                <img class="service-icon" src="../Resources/svg/juez.svg" />
                            </div>
                            <div class="col-xs-8">
                                <h5>Información de jueces </h5>
                                <h6>Sección que contiene una lista de todos los jueces registrados. </h6>
                            </div>
                        </div>
                    </div>
                </a>
                <a data-toggle="modal" data-target="#modalInviteJ">
                    <div class="col-lg-6">
                        <div class="row service-list">
                            <div class="col-xs-4 text-center" style="height: 96px">
                                <img class="service-icon" src="../Resources/svg/teacher.svg" />
                            </div>
                            <div class="col-xs-8">
                                <h5>Crear juez </h5>
                                <h6>Crea cuenta de juez para otorgarle acceso a los registros de candidatos de las categorías que le correspondan.</h6>
                            </div>
                        </div>
                    </div>
                </a>
                <a href="AdministraGanadores.aspx" class="no-underline">
                    <div class="col-lg-6">
                        <div class="row service-list">
                            <div class="col-xs-4 text-center" style="height: 96px">
                                <img class="service-icon" src="http://inspired-bd.com/wp-content/uploads/2016/07/trophy200.png" />
                            </div>
                            <div class="col-xs-8">
                                <h5>Elegir ganador </h5>
                                <h6>Selección de ganador con base en las evaluaciones de los jueces. </h6>
                            </div>
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="modal fade" id="modalInviteJ" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header text-center">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title" id="myModalLabel">Proponer Candidato</h3>
                        <hr class="shorthr">
                    </div>
                    <div class="modal-body">
                        <form class="form-horizontal" role="form">
                            <div class="form-group">
                                <label class="col-lg-3 control-label">Correo:</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="correoJuez" ClientIDMode="Static" runat="server" placeholder="Correo de Juez"></asp:TextBox><br />
                                </div>
                            </div>
                        </form>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                        <asp:Button class="btn btn-primary" ID="Button1" runat="server" OnClick="Registra_Juez" Text="Registra Juez" />
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
