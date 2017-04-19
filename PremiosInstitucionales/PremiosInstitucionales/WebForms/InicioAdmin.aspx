<%@ Page Title="" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="InicioAdmin.aspx.cs" Inherits="PremiosInstitucionales.WebForms.InicioAdmin" %>

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
            <div class="col-lg-6">
                <div class="row service-list">
                    <div class="col-xs-4 text-center" style="height: 96px">
                        <img class="service-icon" src="../Resources/svg/clipboard.svg" />
                    </div>
                    <div class="col-xs-8">
                        <h5>Aplicaciones pendientes </h5>
                        <h6>Lista de aplicaciones que se deberán analizar para validar que cumplan con los requerimientos mínimos. </h6>
                    </div>
                </div>
            </div>
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
            <div class="col-lg-6">
                <div class="row service-list">
                    <div class="col-xs-4 text-center" style="height: 96px">
                        <img class="service-icon" src="../Resources/svg/proponer.svg" />
                    </div>
                    <div class="col-xs-8">
                        <h5>Proponer candidato </h5>
                        <h6>Proponer candidato a registrarse en el concurso de Premios Institucionales. </h6>
                    </div>
                </div>
            </div>
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
            <div class="col-lg-6">
                <div class="row service-list">
                    <div class="col-xs-4 text-center" style="height: 96px">
                        <img class="service-icon" src="../Resources/svg/teacher.svg" />
                    </div>
                    <div class="col-xs-8">
                        <h5>Crear juez </h5>
                        <h6>Crea cuenta de juez que permite tener acceso a las aplicaciones de las categorías que se le otorguen. </h6>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="row service-list">
                    <div class="col-xs-4 text-center" style="height: 96px">
                        <img class="service-icon" src="../Resources/svg/team.svg" />
                    </div>
                    <div class="col-xs-8">
                        <h5>Administrar jueces a categorías </h5>
                        <h6>En esta sección se otorgan permisos a cada juez para poder asignar su categoría a evaluar. </h6>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="row service-list">
                    <div class="col-xs-4 text-center" style="height: 96px">
                        <img class="service-icon" src="http://inspired-bd.com/wp-content/uploads/2016/07/trophy200.png" />
                    </div>
                    <div class="col-xs-8">
                        <h5>Elegir ganador </h5>
                        <h6>Selección de ganador en base a las evaluaciones de los jueces. </h6>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
