<%@ Page Title="" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="AdministraPremios.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraPremios" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Crear premio - modal -->
        <div class="modal fade" id="modalNewAward" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog modal-big" role="document">
                <div class="modal-content">
                    <div class="modal-header text-center">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title" id="myModalLabel">Agregar premio</h3>
                        <hr class="shorthr">
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="award-new-box">
                                    <asp:Panel runat="server" class="js--image-preview" ID="avatarImage" onclick="uploadImage();"></asp:Panel>
                                </div>
                                <asp:FileUpload ID="FileUploadImage" runat="server" class="text-center center-block well well-sm" Style="display: none;" onchange="ShowImagePreview(this);" />
                                <div class="btn-group-mid">
                                    <button type="button" class="btn btn-default" onclick="uploadImage();">Carga una imagen</button>
                                </div>
                            </div>
                            <div class="col-md-6" style="padding-right: 35px; padding-left: 35px">
                                <div>
                                    <label class="col-lg-12 control-label control-title" style="margin-top: 0px">Nombre:</label>
                                    <asp:TextBox runat="server" class="form-control" placeholder="Eugenio Garza Sada" type="text" ID="tbAwardTitle" />
                                    <label class="col-lg-12 control-label control-title">Descripción:</label>
                                    <asp:TextBox runat="server" class="form-control form-text-area scrollbar-custom" maxlength="500" Rows="4" onkeyup="updateCharactersLeft(this);" ID="tbAwardDescription" TextMode="MultiLine"></asp:TextBox>

                                    <p>500 caracteres restantes </p>
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
                <div class="col-lg-12 text-center">
                    <h3 class="section-heading">Administrador de premios</h3>
                    <hr class="shorthr">
                </div>
            </div>
        </div>

        <div class="container">
            <div class="row" runat="server" id="awardList">
                <div class="col-md-4 item-list">
                    <a data-toggle="modal" data-target="#modalNewAward" style="text-decoration: none">
                        <div class="create-item">
                            <img class="item-add" src="/Resources/svg/add.svg"/>
                            <h5 class="text-center" style="margin-top: 40px; font-weight: bold;">Agregar premio </h5>
                        </div>
                    </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
