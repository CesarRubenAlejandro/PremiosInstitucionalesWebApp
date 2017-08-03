// Ordenar por "Estado" las convocatorias
$(document).ready(function(){
    $('#listaConvocatorias').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": 0 }
        ],
        "aaSorting": [],
        "order": [[4, "desc"]],
        "language":
        {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }

        }
    });


});

function updateCharactersLeft2(textarea) {
    var charactersMessage = textarea.parentElement.getElementsByTagName("p")[0];
    var actualCharacters = textarea.value.length;
    var maxCharacters = 500;

    var result = maxCharacters - actualCharacters;

    charactersMessage.innerHTML = result + ' caracteres restantes.';
}

function printDates(date) {
            
    var dates = $('#datepicker').multiDatesPicker('getDates');

    resetDateInputs(dates);
}

function resetDateInputs(dates){
    // Inputs IDs
    var dateInputs = ['#FechaInicioNuevaConvo', '#FechaFinNuevaConvo', '#FechaVeredicto'];

    // Reset inputs
    for (var i = 0; i < dateInputs.length; i++){
        $(dateInputs[i]).val("");
    }

    // New value to inputs
    for (var i = 0; i < dates.length; i++){
        $(dateInputs[i]).val(dates[i]);
    }
}

// InitDatePicker
$(function () {
    $('#datepicker').multiDatesPicker({
        onSelect: function (date) { printDates(date); },
        pickableRange: 730,
        maxPicks: 3,
        minDate: 0
    });
});