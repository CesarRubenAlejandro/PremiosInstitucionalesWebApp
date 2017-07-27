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

// Globals
var fInicio = null;
var fFin = null;
var fVeredicto = null;

function setDates(fIniciop, fFinp, fVeredictop) {
    fInicio = fIniciop;
    fFin = fFinp;
    fVeredicto = fVeredictop;
}

$(document).ready(function () {
    // InitDatePicker
    $('#datepicker').multiDatesPicker({
        onSelect: function (date) { printDates(date); },
        pickableRange: 730,
        maxPicks: 3,
        //minDate: 0
    });

    // InitLabels
    $("#FechaInicioNuevaConvo").val(fInicio.substr(3, 2)+"/"+fInicio.substr(0, 2)+"/"+fInicio.substr(6, 4));
    $("#FechaFinNuevaConvo").val(fFin.substr(3, 2)+"/"+fFin.substr(0, 2)+"/"+fFin.substr(6, 4));
    $("#FechaVeredicto").val(fVeredicto.substr(3, 2)+"/"+fVeredicto.substr(0, 2)+"/"+fVeredicto.substr(6, 4));

    // Create Date Objects
    fInicio = fInicio.split("/");
    var dInicio = new Date(fInicio[2], parseInt(fInicio[1], 10) - 1, fInicio[0]);

    fFin = fFin.split("/");
    var dFin = new Date(fFin[2], parseInt(fFin[1], 10) - 1, fFin[0]);

    fVeredicto = fVeredicto.split("/");
    var dVeredicto = new Date(fVeredicto[2], parseInt(fVeredicto[1], 10) - 1, fVeredicto[0]);
    
    // Select Dates 
    $('#datepicker').multiDatesPicker('addDates', [dInicio, dFin, dVeredicto]);
});