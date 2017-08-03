var changePasswordForm;
var svChangePasswordForm;

function getProfileReferences() {
    changePasswordForm = document.getElementById('changepasswordform');
    svChangePasswordForm = changePasswordForm.innerHTML;
}

function openChangePasswordModal() {
    changePasswordForm.innerHTML = svChangePasswordForm;
    changePasswordForm.style.height = '';
    changePasswordForm.style.display = 'block';
}

function uploadImage() {
    var prefix = "ContentPlaceHolder_";
    var x = document.getElementById(prefix + "FileUploadImage").click();
}

function ShowImagePreview(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#ContentPlaceHolder_avatarImage').css('background-image', 'url(' + e.target.result + ')');
        };
        reader.readAsDataURL(input.files[0]); 
    }
}
