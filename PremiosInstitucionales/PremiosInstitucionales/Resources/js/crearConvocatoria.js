// Ordenar por "Estado" las convocatorias
$(document).ready(function(){
    $('#listaConvocatorias').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": 0 }
        ],
        "aaSorting": [],
        "order": [[ 4, "des" ]]
    });
});

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