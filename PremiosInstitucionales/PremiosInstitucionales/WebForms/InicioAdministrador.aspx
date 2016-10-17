<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioAdministrador.aspx.cs" 
    Inherits="PremiosInstitucionales.WebForms.InicioAdministrador" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <style>
        .premioPanel{
            width: 300px;
            height: 180px;
            display:inline-block;
            border-style:groove;
            border-width:3px;
            margin-right:10px;
            margin-bottom:10px;
        }
        .premioLabel{
            text-align:center;
        }
        .premioImgButton{
            width:100%;
            height:100%;
        }
    </style>
    <h1>Premios institucionales</h1>       
    <div runat="server" id="PanelesPremios">
    </div>
</asp:Content>
    