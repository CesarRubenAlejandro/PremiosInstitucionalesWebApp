$(document).ready(function () {
    $('.fadeView').css('display', 'none');
    $('.fadeView').fadeIn(325);
    $('.link').click(function (event) {
        event.preventDefault();
        newLocation = this.href;
        $('.fadeView').fadeOut(325, newpage);
    });

    function newpage() {
        window.location = newLocation;
    }
});

function validateAnswerCharacters(e) {
    e.target.value = e.target.value.replace(/&/g, "y");
}

function updateCharactersLeft(textarea) {
    var charactersMessage = textarea.parentElement.getElementsByTagName("p")[0];
    var actualCharacters = textarea.value.length;
    var maxCharacters = textarea.maxLength;

    var result = maxCharacters - actualCharacters;

    charactersMessage.innerHTML = result + ' caracteres restantes.';
}

function sendFormAux() {
    var prefix = "ContentPlaceHolder_";
    var x = document.getElementById(prefix + "EnviarBtn").click();
}

function changePage(url) {
    window.open(url, '_blank');
}