$(document).ready(function () {
    $("input[name=ganador]").click(function () {
        $("#ContentPlaceHolder_AsignarGanador").show();
        var hiddenValue = document.getElementById("ContentPlaceHolder_hiddenControl");
        hiddenValue.value = $("input[name=ganador]:checked").val();
    });
});

function showVeredictoFinal() {
    $("#VeredictoFinal").show();
}

function showAsignarGanador() {
    $("#ContentPlaceHolder_AsignarGanador").show();
}

function selectGanador(ganador) {
    $("input[name=ganador][value='"+ ganador +"']").prop("checked", true);
}