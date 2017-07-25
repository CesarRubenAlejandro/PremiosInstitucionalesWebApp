<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="AdministraCategorias.aspx.cs" Inherits="PremiosInstitucionales.WebForms.AdministraCategorias"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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

            <div class="row">
                <div class="col-lg-12">
                    <asp:Literal ID="litTituloPremio" runat="server"></asp:Literal>
                    <asp:Literal ID="litTituloConvocatoria" runat="server"></asp:Literal>
                </div>
            </div>

        </div>

        <div class="container">
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
