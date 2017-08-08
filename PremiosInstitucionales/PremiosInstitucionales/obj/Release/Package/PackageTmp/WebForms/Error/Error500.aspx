<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error500.aspx.cs" Inherits="PremiosInstitucionales.WebForms.Error500" %>

<!DOCTYPE html>
<!--[if IE 8 ]><html class="no-js oldie ie8" lang="en"> <![endif]-->
<!--[if IE 9 ]><html class="no-js oldie ie9" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!--><html class="no-js"> <!--<![endif]-->
	<head>
	   <!--- basic page needs
	   ================================================== -->
        <meta charset="utf-8">
		<title>Premios Institucionales</title>
        <meta http-equiv="Refresh" content="5;url='<%= ResolveUrl("~/WebForms/Login.aspx")%>'" />

	   <!-- mobile specific metas
	   ================================================== -->
		<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">

		<!-- CSS
	   ================================================== -->
	   <link href='<%= ResolveUrl("~/Resources/css/Error.css")%>'  rel="stylesheet" type="text/css" />
        <style>
            .content-wrap {
                background-image: url('<%= ResolveUrl("~/Resources/img/itesm.jpg")%>');
            }
        </style>

	   <!-- favicons
		================================================== -->
		<link rel="icon" href="../favicon.ico">
	</head>

	<body>
		<!-- header 
	   ================================================== -->
	   <header class="main-header">
		<div class="row">
			<div class="logo">
				<img id="logoTec" src='<%= ResolveUrl("~/Resources/img/logotec2.png")%>' alt="Logo Tec" style="width: 64px; height: 64px;"/>
				<h1 style="margin-top: -60px; margin-left: 70px;">Premios TEC</h1>
			</div>   		
		</div>   
	   </header> <!-- /header -->

		<!-- main content
	   ================================================== -->
	   <main id="main-404-content" class="main-content-static">
		<div class="content-wrap">
		   <div class="shadow-overlay"></div>
			   <div class="main-content">
					<div class="row">
						<div class="col-twelve">
							<h1 class="kern-this">Error 500.</h1>
							<p style="color: white;">
								Error interno del servidor. Trataremos de redirigirte automáticamente a nuestra página de inicio en 5 segundos.
							</p>
						</div> <!-- /twelve --> 		   			
					</div> <!-- /row -->    		 		
			   </div> <!-- /main-content --> 
			</div> <!-- /content-wrap -->
	   </main> <!-- /main-404-content -->
	</body>
</html>
