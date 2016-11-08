<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PremioEspecificoAdmin.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.EditarConvocatoria" MasterPageFile="~/MasterPage.Master"%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <style>
        .imgHeader {
            width:100%;
            height: 180px;
        }
        .htmlEditor {
            padding-bottom:15px;
        }
        .convocatoriaTitle {
            display:block;
            font: bold;
            font-size:large;
        }

        .accordionContent {
            background-color: rgb(180,180,180);
        }

        .accordionHeader {
            border: 1px solid #dddddd;
            background-color: rgb(180,180,180);
            padding: 3px 3px 3px 18px;
            margin-top: 5px;
            cursor: pointer;
        }

        .accordionHeaderSelected {
            background-color: rgb(180,180,180);
            padding: 3px 3px 3px 18px;
            margin-top: 5px;
            cursor: pointer;
        }
    </style>
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
                    <asp:Calendar ID="FechaInicioNuevaConvo" runat="server" Caption="Fecha inicio"></asp:Calendar>
                    <asp:Calendar ID="FechaFinNuevaConvo" runat="server" Caption="Fecha fin"></asp:Calendar>
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