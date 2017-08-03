$(document).ready(function () {
    function newpage() {
        window.location = newLocation;
    }
});

function changeAnchor(s, premio) {
    var x = s[s.selectedIndex].id;
    document.getElementById(premio).href = "Formulario.aspx?c=" + x;
}

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

function loadPage(url) {
    location.href = url;
}