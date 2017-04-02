$(document).ready(function(){
    $('#listaParticipantesTable').DataTable( {
        "columnDefs": [
            { "orderable": false, "targets": 0 }
        ],
        "aaSorting": [],
        "order": [[ 3, "asc" ]]
    });
    
    
});