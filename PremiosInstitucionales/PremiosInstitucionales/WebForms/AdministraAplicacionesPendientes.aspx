<%@ Page Title="" Language="C#" MasterPageFile="~/mp-Candidato.Master" AutoEventWireup="true" CodeBehind="AdministraAplicacionesPendientes.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraAplicacionesPendientes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script type="text/javascript">
        function SetActualAppId(idApp) {
            document.getElementById('<%=IdAppHidden.ClientID%>').value = idApp;
            return true;
        }
    </script>

    <form id="form1" runat="server" style="all: unset;">
        <asp:hiddenfield id="IdAppHidden" value="" runat="server"/>
        <div class="container fadeView">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <h3 class="section-heading">Registros pendientes</h3>
                        <hr class="shorthr">
                    </div>
                </div>
            </div>

            <div class="container">
                <div class="panel-group" id="accordion" runat="server" role="tablist" aria-multiselectable="true">
                </div>
            </div>

            <div class="modal fade" id="modalRechazApp" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header text-center">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h3 class="modal-title" id="myModalLabel">Rechazo de aplicación</h3>
                            <hr class="shorthr">
                        </div>
                        <div class="modal-body">
                            <h5>Estas por rechazar una solicitud. </h5>
                            <h6>Un correo de notificación será enviado al aplicante, ¿cuál es el motivo del rechazo? </h6>
                            <asp:TextBox class="form-control form-text-area scrollbar-custom" rows="3" runat="server" TextMode="MultiLine"  ID="razonTB"></asp:TextBox>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                            <button type="button" class="btn btn-primary" onclick="return document.getElementById('ContentPlaceHolder_btnRechaz').click()">Enviar</button>
                            <asp:Button Style="display: none;" ID="btnRechaz" runat="server" Text="Guardar Cambios" OnClick="bttnEnviarRechazo_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="modalAcceptApp" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header text-center">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h3 class="modal-title" id="myModalLabel">Aceptar aplicación</h3>
                            <hr class="shorthr">
                        </div>
                        <div class="modal-body">
                            <h5>Estas por aceptar una solicitud. </h5>
                            <h6>Se aceptará la solicitud de registro. ¿Desea continuar? </h6>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                            <button type="button" class="btn btn-primary" onclick="return document.getElementById('ContentPlaceHolder_btnAccept').click()"" >Aceptar</button>
                            <asp:Button Style="display: none;" ID="btnAccept" runat="server" OnClick="bttnAceptarAplicacion_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
