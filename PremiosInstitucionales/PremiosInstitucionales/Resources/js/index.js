function changeTab(isLoginSelected) {
	// Elementos
	var loginWrap = document.getElementsByClassName("login-wrap")[0];
	var signIn    = document.getElementsByClassName("sign-in-htm")[0];
	var signUp    = document.getElementsByClassName("sign-up-htm")[0];
	
	// Si esta activa la tab de Login
	if (isLoginSelected){
		
		// Cambio el tamaño del wrapper
		loginWrap.style.height = "535px";
		
		// Oculto las opciones de Registro
		signUp.style.transition = "opacity 0s";
		signUp.style.opacity = 0;
		signUp.style.zIndex = -1;
		
		// Muestro las opciones de Login
		signIn.style.transition = "opacity 1.5s";
		signIn.style.opacity = 1;
		signIn.style.zIndex = 1;
	}
	// Si esta activa la tab de Registro
	else{
		// Cambio el tamaño del wrapper
		loginWrap.style.height = "665px";
		
		// Oculto las opciones de Login
		signIn.style.transition = "opacity 0s";
		signIn.style.opacity = 0;
		signIn.style.zIndex = -1;
		
		// Muestro las opciones de Registro
		signUp.style.transition = "opacity 1.5s";
		signUp.style.opacity = 1;
		signUp.style.zIndex = 1;
	}
 }
 
 function forgotPassword(isForgotPasswordSelected) {
	// Elementos
	var loginWrap = document.getElementsByClassName("login-wrap")[0];
	var signIn    = document.getElementsByClassName("sign-in-htm")[0];
	var forgot    = document.getElementsByClassName("forgot-htm")[0];
	var tabs      = document.getElementsByClassName("option-tabs")[0];
	
	// Si se selecciono el recuperar contraseña
	if(isForgotPasswordSelected) {
		// Ocultar las pestañas de opciones (Login / Registro)
		tabs.style.display = "none";

		// Cambio el tamaño del wrapper
		loginWrap.style.height = "435px";
		
		// Oculto las opciones de Login
		signIn.style.transition = "opacity 0s";
		signIn.style.opacity = 0;
		signIn.style.zIndex = -1;
		
		// Muestro las opciones de Recuperar contraseña
		forgot.style.transition = "opacity 1s";
		forgot.style.opacity = 1;
		forgot.style.zIndex = 1;
	}
	// Si se selecciono el regresar a Login
	else {
		// Mostrar las pestañas de opciones (Login / Registro)
		tabs.style.display = "block";
		
		// Cambio el tamaño del wrapper
		loginWrap.style.height = "535px";
		
		// Oculto las opciones de Recuperar contraseña
		forgot.style.transition = "opacity 0s";
		forgot.style.opacity = 0;
		forgot.style.zIndex = -1;
		
		// Muestro las opciones de Login
		signIn.style.transition = "opacity 1s";
		signIn.style.opacity = 1;
		signIn.style.zIndex = 1;
	}
 }
 
 function mailSentSuccessfully() {
	// Elementos
	var loginWrap = document.getElementsByClassName("login-wrap")[0];
	var forgot    = document.getElementsByClassName("hide-content")[0];
			
	// Oculto las opciones de Recuperar contraseña
	forgot.style.transition = "opacity 0s";
	forgot.style.opacity = 0;
	forgot.style.zIndex = -1;
 }
 
 function transformToNavBar(){	
	var signIn    = document.getElementsByClassName("sign-in-htm")[0];
	var tabs      = document.getElementsByClassName("option-tabs")[0];
	var logoTec   = document.getElementById("logoTec");
	
	var disappearItems = [signIn, tabs, logoTec];
	for (i = 0; i < disappearItems.length; i++) {
		disappearItems[i].style.transition = "opacity .30s";
		disappearItems[i].style.opacity = 0;
		disappearItems[i].style.zIndex = -1;
	}
	
	var loginWrap = document.getElementsByClassName("login-wrap")[0];
	var loginHtml = document.getElementsByClassName("login-html")[0];
	
	// Def. Transiitons
	loginHtml.style.transition = "all .30s ease-in";
	loginWrap.style.transition = "height .40s ease-in, background-color .40s ease-in, box-shadow .40s ease-in";
	
	// Cambios
	loginHtml.style.padding = "0";
	loginWrap.style.height = "50px";
	loginWrap.style.boxShadow = "none";
	loginWrap.style.backgroundColor = "rgba(38, 50, 56, .95);";
	
	// Horizontal
	setTimeout(function(){
		loginWrap.style.transition = "width .40s ease-in, height .20s ease-in, background-color .20s ease-in, box-shadow .20s ease-in";
		loginWrap.style.width = "100%";
	}, 250);
	
	// Login
	setTimeout(function(){
		window.location.href = "inicioCandidato.html";
	}, 700);
 }
 
$('.int-group').find('.int-input, textarea').on('keyup blur focus', function (e) {
  var $this = $(this),
      label = $this.prev('.int-label');

	  if (e.type === 'keyup') {
			if ($this.val() === '') {
          label.removeClass('active highlight');
        } else {
          label.addClass('active highlight');
        }
    } else if (e.type === 'blur') {
    	if( $this.val() === '' ) {
    		label.removeClass('active highlight'); 
			} else {
		    label.removeClass('highlight');   
			}   
    } else if (e.type === 'focus') {
      
      if( $this.val() === '' ) {
    		label.removeClass('highlight'); 
			} 
      else if( $this.val() !== '' ) {
		    label.addClass('highlight');
			}
    }
});