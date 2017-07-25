<%@ Page Title="" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="EvaluaAplicacion.aspx.cs" Inherits="PremiosInstitucionales.WebForms.EvaluaAplicacion" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container fadeView">
        <div class="row">
            <div class="col-lg-12 text-center">
                <h2>Evaluación de Aplicación</h2>
                <h3 class="section-heading">
                    <asp:Literal ID="litTituloPremio" runat="server" /></h3>
                <h4>
                    <asp:Literal ID="litTituloCategoria" runat="server" /></h4>
                <hr class="shorthr" />
            </div>
        </div>

            <asp:Panel runat="server" ID="PanelArchivo" class="row question-form">
            </asp:Panel>

            <asp:Panel runat="server" ID="PanelFormulario" class="row question-form">
            </asp:Panel>

            <div class="row" runat="server" id="PanelEvaluacion">
                <hr />
                <div class="col-md-8 col-md-offset-2">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Evaluación de Alicación</h3>
                        </div>
                        <div class="panel-body">
                            <table class="table table-bordered">
                                <thead>
                                    <tr style="text-align: center;">
                                        <th>Quisque (15%)</th>
                                        <th>Sagittis (25%)</th>
                                        <th>Rhoncus (20%)</th>
                                        <th>Vulputate (40%)</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr style="text-align: center;">
                                        <td>Mauris nec quam consectetur, maximus mauris sit amet, molestie tellus.</td>
                                        <td>Praesent faucibus enim sed laoreet luctus.</td>
                                        <td>Proin at nibh a eros rutrum lobortis.</td>
                                        <td>Pellentesque risus nisl, feugiat at varius id, bibendum ac diam.</td>
                                    </tr>
                                </tbody>
                            </table>
                            <div style="text-align: center">
                                Calificacion final:
                                <asp:TextBox runat="server" ID="aplicationEvaluationNumber" type="number" min="0" max="100" value="0" size="3" Style="text-align: center;" />
                                %
                            </div>
                            <div class="btn-group-mid">
                                <a href="InicioJuez.aspx">
                                    <button type="button" class="btn btn-default">Cancelar</button>
                                </a>
                                <asp:Button ID="evaluateApplicationBtn" runat="server" OnClick="EvaluarAplicacion" Text="Enviar" class="btn btn-primary" />
                                <asp:Button ID="modifiyEvaluationBtn" runat="server" OnClick="ModificarAplicacion" Text="Guardar Cambios" class="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </div>
</asp:Content>
