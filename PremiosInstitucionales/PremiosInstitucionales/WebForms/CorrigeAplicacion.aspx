<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorrigeAplicacion.aspx.cs" 
    MasterPageFile="~/MasterPage.Master" Inherits="PremiosInstitucionales.WebForms.CorrigeAplicacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <script type="text/javascript">
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


    <asp:Panel runat="server" ID="panel">
        <asp:hiddenfield id="IdAppHidden" value="" runat="server"/>

        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
        
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
            PopupControlID="pnlPopup" TargetControlID="lnkDummy" >
        </cc1:ModalPopupExtender>

        <asp:Panel ID="pnlPopup" runat="server" Style="display: none; border: 1px dashed">
            <div class="header">
                Modal Popup
            </div>
            <div class="body">
                This is a Modal Popup.
                <br />
                <asp:Button ID="btnHide" runat="server" Text="Hide Modal Popup" OnClientClick="return HideModalPopup()" />
                <asp:Button ID="bttnEnviarRechazo" text="prueba" runat="server" onclick="bttnEnviarRechazo_Click"/>
            </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
