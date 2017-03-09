var loadingSpinner, changePasswordForm, passwordCheckMark, changeAlert;
var svLoadingSpinner, svChangePasswordForm, svPasswordCheckMark;
var svHeight;

function getProfileReferences() {
    loadingSpinner = document.getElementById('loadingspinner');
    changePasswordForm = document.getElementById('changepasswordform');
    passwordCheckMark = document.getElementById('passwordcheckmark');
    changeAlert = document.getElementById('changealert');
    loadingSpinner.style.display = 'none';
    passwordCheckMark.style.display = 'none';

    svLoadingSpinner = loadingSpinner.innerHTML;
    svChangePasswordForm = changePasswordForm.innerHTML;
    svPasswordCheckMark = passwordCheckMark.innerHTML;
}

function openChangePasswordModal() {
    loadingSpinner.innerHTML = svLoadingSpinner;
    changePasswordForm.innerHTML = svChangePasswordForm;
    passwordCheckMark.innerHTML = svPasswordCheckMark;
    loadingSpinner.style.display = 'none';
    passwordCheckMark.style.display = 'none';
    changePasswordForm.style.height = '';
    changePasswordForm.style.display = 'block';
}

function changePassword() {
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
    setTimeout(function () {
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

function uploadImage() {
    var prefix = "ContentPlaceHolder_";
    var x = document.getElementById(prefix + "FileUploadImage").click();
}

function ShowImagePreview(input) {
    console.log('helo');
    if (input.files && input.files[0]) {
        console.log('helox2');
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#ContentPlaceHolder_avatarImage').css('background-image', 'url(' + e.target.result + ')');
        };
        reader.readAsDataURL(input.files[0]);
        
    }
}

/*
$(function () {
    $("[id*=EnviarBtn]").bind("click", function () {
        console.log('shiiit');
        var obj = {
            Nombre: $("[id*=NombresTextBox]").val(),
            Apellido: $("[id*=ApellidosTextBox]").val(),
            Direccion: $("[id*=DomicilioTextBox]").val(),
            Nacionalidad: $("[id*=NacionalidadTextBox]").val(),
            RFC: $("[id*=RFCTextBox]").val(),
            Telefono: $("[id*=TelefonoTextBox]").val()
        }
        console.log(obj);
        var jsonData = JSON.stringify(obj);
        $.ajax({
            type: "GET",
            url: "InformacionPersonalCandidato.aspx/MyMethod",
            data: jsonData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: "true",
            cache: "false",
            success: function (msg) {
                // On success
            },
            Error: function (x, e) {
                // On Error
            }
        });
        return false;
    });
});*/

//Called this method on any button click  event for Testing
function MyFunction() {
    console.log('shiiitx2');
    var obj = {
        Nombre: $("[id*=NombresTextBox]").val(),
        Apellido: $("[id*=ApellidosTextBox]").val(),
        Direccion: $("[id*=DomicilioTextBox]").val(),
        Nacionalidad: $("[id*=NacionalidadTextBox]").val(),
        RFC: $("[id*=RFCTextBox]").val(),
        Telefono: $("[id*=TelefonoTextBox]").val()
    }
    console.log(obj);
    var jsonData = JSON.stringify(obj);
    $.ajax({
        type: "GET",
        url: "InformacionPersonalCandidato.aspx/MyMethod",
        data: jsonData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: "true",
        cache: "false",
        success: function (msg) {
            // On success
        },
        Error: function (x, e) {
            // On Error
        }
    });
    return false;
    /*
    console.log('AJAX!');
    var obj = {
        Nombre: document.getElementById('<%=NombresTextBox=%>').value,
        Apellido: document.getElementById('<%=ApellidosTextBox=%>').value,
        Direccion: document.getElementById('<%=DomicilioTextBox=%>').value,
        Nacionalidad: document.getElementById('<%=NacionalidadTextBox=%>').value,
        RFC: document.getElementById('<%=RFCTextBox=%>').value,
        Telefono: document.getElementById('<%=TelefonoTextBox=%>').value
    }
    
    var jsonData = JSON.stringify(obj);
    $.ajax({
        type: "GET",
        url: "InformacionPersonalCandidato.aspx/MyMethod",
        data: jsonData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: "true",
        cache: "false",
        success: function (msg) {
            // On success
        },
        Error: function (x, e) {
            // On Error
        }
    });
    saveChanges();
    return false;*/
}  


function saveChanges() {
    changeAlert.style.transition = 'opacity .40s ease-in';
    changeAlert.style.opacity = '0';
    changeAlert.style.display = 'block';

    setTimeout(function () {
        changeAlert.style.opacity = '1';
    }, 50);
}

function closeAlert() {
    setTimeout(function () {
        changeAlert.style.opacity = '0';
    }, 150);
}
