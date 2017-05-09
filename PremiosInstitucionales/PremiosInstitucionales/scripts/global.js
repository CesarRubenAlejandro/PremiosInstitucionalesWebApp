$(document).ready(function() {
	$('.fadeView').css('display', 'none');
	$('.fadeView').fadeIn(325);
	$('.link').click(function(event) {
		event.preventDefault();
		newLocation = this.href;
		$('.fadeView').fadeOut(325, newpage);
	});

	function newpage() {
		window.location = newLocation;
	}

});

function changeAnchor(s, premio) {
    var x = s[s.selectedIndex].id;
    document.getElementById(premio).href = "Formulario.aspx?c="+x;

}

var loadingSpinner, changePasswordForm, passwordCheckMark, changeAlert;
var svLoadingSpinner, svChangePasswordForm, svPasswordCheckMark;
var svHeight;

function getProfileReferences () {
	loadingSpinner = document.getElementById('loadingSpinner');
	changePasswordForm = document.getElementById('changePasswordForm');
	passwordCheckMark = document.getElementById('passwordCheckMark');
	changeAlert = document.getElementById('changeAlert');
	loadingSpinner.style.display = 'none';
	passwordCheckMark.style.display = 'none';
	changeAlert.style.display = 'none';

	svLoadingSpinner = loadingSpinner.innerHTML;
	svChangePasswordForm = changePasswordForm.innerHTML;
	svPasswordCheckMark = passwordCheckMark.innerHTML;
}

function openChangePasswordModal () {
	 loadingSpinner.innerHTML = svLoadingSpinner;
	 changePasswordForm.innerHTML = svChangePasswordForm;
	 passwordCheckMark.innerHTML = svPasswordCheckMark;
	 loadingSpinner.style.display = 'none';
	 passwordCheckMark.style.display = 'none';
	 changePasswordForm.style.height = '';
	 changePasswordForm.style.display = 'block';
}

function changePassword () {
	loadingSpinner.style.display = 'block';

	var saveChildren = changePasswordForm.innerHTML;
	var saveHeight = changePasswordForm.clientHeight;
	svHeight = saveHeight;
	var spinnerHeight = loadingSpinner.clientHeight;

	var normalizedHeight = saveHeight - spinnerHeight - 48;
	var expectedHeight = 0;

	changePasswordForm.style.height = normalizedHeight + 'px';
	loadingSpinner.style.opacity = '0%';
	passwordCheckMark.style.opacity = '0%';

	// transitions
	changePasswordForm.style.transition = "height .40s ease-in";
	loadingSpinner.style.transition = 'opacity .40s ease-in';
	passwordCheckMark.style.transition = 'opacity .40s ease-in';

	// cambios
	changePasswordForm.innerHTML = '';
	changePasswordForm.style.height = expectedHeight + 'px';
	loadingSpinner.style.opacity = '1';
	

	// Smooth transition
	setTimeout(function(){
		changePasswordForm.style.transition = 'height .20s ease-in';
	}, 250);

	setTimeout(function () {
		loadingSpinner.style.opacity = '0';
		loadingSpinner.style.transition = 'opacity .40s ease-in';
	}, 1250);

	setTimeout(function () {
		// loadingSpinner.remove();
		passwordCheckMark.style.display = 'block';
		passwordCheckMark.style.opacity = '0';
		passwordCheckMark.style.transition = 'opacity .40s ease-in';
		loadingSpinner.style.display = 'none';
	}, 1290);

	setTimeout(function () {
		passwordCheckMark.style.opacity = '1';
	}, 1340);

}

function saveChanges () {
	changeAlert.style.transition = 'opacity .40s ease-in';
	changeAlert.style.opacity = '0';
	changeAlert.style.display = 'block';

	setTimeout(function () {
		changeAlert.style.opacity = '1';
	}, 50);
}

function closeAlert () {
	setTimeout(function () {
		changeAlert.style.opacity = '0';
	}, 150);
}

function updateCharactersLeft (textarea) {
	var charactersMessage = textarea.parentElement.getElementsByTagName("p")[0];
	var actualCharacters = textarea.value.length;
	var maxCharacters = textarea.maxLength;

	var result = maxCharacters - actualCharacters;

	charactersMessage.innerHTML = result + ' caracteres restantes.';
}