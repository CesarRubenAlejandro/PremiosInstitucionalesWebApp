<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PremioEspecificoAdmin.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.EditarConvocatoria" MasterPageFile="~/MasterPage.Master"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <script type="text/javascript">
        function ShowAcceptModalPopup(idApp) {
            $find("mpeAccept").show();
            document.getElementById('<%=IdAppHidden.ClientID%>').value = idApp;
            return false;
        }
        function HideAcceptModalPopup() {
            $find("mpeAccept").hide();
            return false;
        }

        function ShowModalPopup(idApp) {
            $find("mpe").show();
            document.getElementById('<%=IdAppHidden.ClientID%>').value = idApp;
            return false;
        }
        function HideModalPopup() {
            $find("mpe").hide();
            return false;
        }
    </script>
    <asp:hiddenfield id="IdAppHidden" value="" runat="server"/>
    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
            PopupControlID="pnlPopup" TargetControlID="lnkDummy" >
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" Style="display: none; border: 1px dashed">
            <div class="header">
                <asp:Label ID="headerRechazo" runat="server" Font-Bold="true">Rechazar Aplicación</asp:Label>
            </div>
            <div class="body">
                Motivo de rechazo
                <br />
                <asp:TextBox ID="razonTB" runat="server" TextMode="MultiLine"></asp:TextBox>
                <br />
                <asp:Button ID="btnHide" runat="server" Text="Cancelar" OnClientClick="return HideModalPopup()" />
                <asp:Button ID="bttnEnviarRechazo" text="Enviar" runat="server" onclick="bttnEnviarRechazo_Click"/>
            </div>
        </asp:Panel>

    <cc1:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpeAccept" runat="server"
            PopupControlID="pnlPopupAccept" TargetControlID="lnkDummy" >
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopupAccept" runat="server" Style="display: none; border: 1px dashed">
            <div class="header">
                <asp:Label ID="Label1" runat="server" Font-Bold="true">Aceptar Aplicación</asp:Label>
            </div>
            <div class="body">
                Se aceptará la aplicación. ¿Continuar?
                <br />
                <asp:Button ID="Button1" runat="server" Text="Cancelar" OnClientClick="return HideAcceptModalPopup()" />
                <asp:Button ID="bttnAceptarAplicacion" text="Aceptar" runat="server" onclick="bttnAceptarAplicacion_Click"/>
            </div>
        </asp:Panel>


        <asp:Image ID="ImageHeader" runat="server" CssClass="imgHeader"/>

        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
            <ajaxToolkit:TabPanel runat="server" ID="ConvocatoriaActualTab" HeaderText="Convocatoria actual">
                <ContentTemplate>
                    <asp:Label runat="server" ID="TituloConvocatoriaActualLbl" CssClass="convocatoriaTitle"></asp:Label>
                    <asp:Label runat="server" ID="TextoConvocatoriaActualLbl"></asp:Label>
                    <asp:Button runat="server" ID="EditarConvocatoriaActualBttn" Visible="false"
                        OnClick="EditarConvocatoriaActualBttn_Click" Text="Editar convocatoria"/>

                    <asp:TextBox runat="server" ID="TituloConvocatoriaActualTB" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="TextoConvocatoriaActualTB" TextMode="multiline" CssClass="htmlEditor"
                         Columns="100" Rows="30" runat="server" Visible="false"></asp:TextBox>
                    <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender2" runat="server" 
                        DisplaySourceTab="true" TargetControlID="TextoConvocatoriaActualTB" EnableSanitization="false">
                        <Toolbar> 
                            <ajaxToolkit:Undo /><ajaxToolkit:Redo /><ajaxToolkit:Bold /><ajaxToolkit:Italic /><ajaxToolkit:Underline />
                            <ajaxToolkit:StrikeThrough /><ajaxToolkit:Subscript /><ajaxToolkit:Superscript /><ajaxToolkit:JustifyLeft /><ajaxToolkit:JustifyCenter />
                            <ajaxToolkit:JustifyRight /><ajaxToolkit:JustifyFull /><ajaxToolkit:InsertOrderedList /><ajaxToolkit:InsertUnorderedList />
                            <ajaxToolkit:CreateLink /><ajaxToolkit:UnLink /><ajaxToolkit:RemoveFormat /><ajaxToolkit:SelectAll />
                            <ajaxToolkit:UnSelect /><ajaxToolkit:Delete /><ajaxToolkit:Cut /><ajaxToolkit:Copy /><ajaxToolkit:Paste />
                            <ajaxToolkit:BackgroundColorSelector /><ajaxToolkit:ForeColorSelector /><ajaxToolkit:FontNameSelector /><ajaxToolkit:FontSizeSelector />
                            <ajaxToolkit:Indent /><ajaxToolkit:Outdent /><ajaxToolkit:InsertHorizontalRule /><ajaxToolkit:HorizontalSeparator /><ajaxToolkit:InsertImage />
                        </Toolbar>
                    </ajaxToolkit:HtmlEditorExtender>
                    <asp:Button runat="server" ID="GuardarCambiosBttn" Text="Publicar cambios" Visible="false" OnClick="GuardarCambiosBttn_Click"/>
                    <asp:Button runat="server" ID="CancelarCambiosBttn" Text="Cancelar" Visible="false" OnClick="CancelarCambiosBttn_Click"/>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" ID="ConvocatoriaNuevaTab" HeaderText="Nueva convocatoria">
                <ContentTemplate>
                    <asp:Label runat="server" Text="Titulo"></asp:Label>
                    <asp:TextBox runat="server" ID="TituloNuevaConvocatoriaTB"></asp:TextBox>

                    <asp:TextBox ID="TextoNuevaConvocatoriaTB" TextMode="multiline" CssClass="htmlEditor" 
                        Columns="100" Rows="30" runat="server"></asp:TextBox>
                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" 
                        DisplaySourceTab="true" TargetControlID="TextoNuevaConvocatoriaTB" EnableSanitization="false">
                        <Toolbar> 
                            <ajaxToolkit:Undo /><ajaxToolkit:Redo /><ajaxToolkit:Bold /><ajaxToolkit:Italic /><ajaxToolkit:Underline />
                            <ajaxToolkit:StrikeThrough /><ajaxToolkit:Subscript /><ajaxToolkit:Superscript /><ajaxToolkit:JustifyLeft /><ajaxToolkit:JustifyCenter />
                            <ajaxToolkit:JustifyRight /><ajaxToolkit:JustifyFull /><ajaxToolkit:InsertOrderedList /><ajaxToolkit:InsertUnorderedList />
                            <ajaxToolkit:CreateLink /><ajaxToolkit:UnLink /><ajaxToolkit:RemoveFormat /><ajaxToolkit:SelectAll />
                            <ajaxToolkit:UnSelect /><ajaxToolkit:Delete /><ajaxToolkit:Cut /><ajaxToolkit:Copy /><ajaxToolkit:Paste />
                            <ajaxToolkit:BackgroundColorSelector /><ajaxToolkit:ForeColorSelector /><ajaxToolkit:FontNameSelector /><ajaxToolkit:FontSizeSelector />
                            <ajaxToolkit:Indent /><ajaxToolkit:Outdent /><ajaxToolkit:InsertHorizontalRule /><ajaxToolkit:HorizontalSeparator /><ajaxToolkit:InsertImage />
                        </Toolbar>
                    </ajaxToolkit:HtmlEditorExtender>
                    <div>
                        <asp:Calendar ID="FechaInicioNuevaConvo" runat="server" Caption="Fecha inicio"></asp:Calendar>
                    </div>
                    <div style="position: relative; left: 300px; margin-top: -163px;">
                        <asp:Calendar ID="FechaFinNuevaConvo" runat="server" Caption="Fecha fin"></asp:Calendar>
                    </div>
                    <div style="position: relative; left: 300px; margin-top: -163px;">
                        <asp:Calendar ID="FechaVeredicto" runat="server" Caption="Fecha veredicto"></asp:Calendar>
                    </div>
                    <asp:Button runat="server" ID="GuardarNuevaBttn" OnClick="GuardarNuevaBttn_Click" Text="Guardar nuevo"/>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" ID="AplicacionesTab" HeaderText="Gestión de aplicaciones de candidatos">
                <ContentTemplate>
                    <asp:Label runat="server" Text="Aplicaciones recibidas"></asp:Label>
                    <hr></hr>
                    <asp:DropDownList runat="server" ID="CategoriasDDL" AutoPostBack="true"
                        OnSelectedIndexChanged="CategoriasDDL_SelectedIndexChanged"></asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="ErrorLbl" runat="server" Visible="false"></asp:Label>
                    
                        <ajaxToolkit:Accordion
                        ID="MyAccordion"
                        runat="Server"
                        SelectedIndex="-1"
                        HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected"
                        ContentCssClass="accordionContent"
                        AutoSize="None"
                        FadeTransitions="true"
                        TransitionDuration="250"
                        FramesPerSecond="40"
                        RequireOpenedPane="false"
                        SuppressHeaderPostbacks="true">
         
                        <HeaderTemplate>...</HeaderTemplate>
                        <ContentTemplate>...</ContentTemplate>
                    </ajaxToolkit:Accordion>

                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
</asp:Content>