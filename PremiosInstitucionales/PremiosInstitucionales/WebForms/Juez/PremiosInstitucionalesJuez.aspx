﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MP-Global.Master" AutoEventWireup="true" CodeBehind="PremiosInstitucionalesJuez.aspx.cs" Inherits="PremiosInstitucionales.WebForms.PremiosInstitucionalesJuez" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href='<%= ResolveUrl("~/Resources/css/PremiosInstitucionalesJuez.css")%>'  rel="stylesheet" type="text/css" />
    <link href='<%= ResolveUrl("~/Resources/css/sb-admin-2.css")%>'  rel="stylesheet" type="text/css" />
    <div class="container fadeView">

        <asp:Button type="button" runat="server" OnClick="BackBtn_Click" class="backBtn"/>

        <div class="container">
		    <div class="row">
			    <div class="col-lg-12 text-center">
				    <h3 class="section-heading">Premios Institucionales</h3>
				    <hr class="shorthr"/>
			    </div>
		    </div>
	    </div>

        <div runat="server" id="premiosJuez"></div>
    </div>
</asp:Content>
