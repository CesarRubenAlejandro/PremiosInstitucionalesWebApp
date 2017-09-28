function printDates(date) {
    var dates = $('#datepicker').multiDatesPicker('getDates');

    resetDateInputs(dates);
}

function resetDateInputs(dates) {
    // Inputs IDs
    var dateInputs = ['#FechaInicioNuevaConvo', '#FechaFinNuevaConvo', '#FechaVeredicto'];

    // Reset inputs
    for (var i = 0; i < dateInputs.length; i++) {
        $(dateInputs[i]).val("");
    }

    // New value to inputs
    for (var i = 0; i < dates.length; i++) {
        var formattedDate = dates[i].substr(3, 2) + "-" + dates[i].substr(0, 2) + "-" + dates[i].substr(6, 4);
        $(dateInputs[i]).val(formattedDate);
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
    $("#FechaInicioNuevaConvo").val(fInicio.replace(/\//g, "-"));
    $("#FechaFinNuevaConvo").val(fFin.replace(/\//g, "-"));
    $("#FechaVeredicto").val(fVeredicto.replace(/\//g, "-"));

    // Create Date Objects
    var dInicio = new Date(fInicio.substr(3, 2) + "/" + fInicio.substr(0, 2) + "/" + fInicio.substr(6, 4));
    var dFin = new Date(fFin.substr(3, 2) + "/" + fFin.substr(0, 2) + "/" + fFin.substr(6, 4));
    var dVeredicto = new Date(fVeredicto.substr(3, 2) + "/" + fVeredicto.substr(0, 2) + "/" + fVeredicto.substr(6, 4));

    // Select Dates 
    $('#datepicker').multiDatesPicker('addDates', [dInicio, dFin, dVeredicto]);
});