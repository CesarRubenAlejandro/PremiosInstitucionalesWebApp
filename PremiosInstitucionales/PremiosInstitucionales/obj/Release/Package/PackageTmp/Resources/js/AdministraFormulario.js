// Preguntas Formulario 
$(function () {
    $(document).on('click', '.btn-add', function (e) {
        e.preventDefault();
        var numItems = $('.entry').length + 1;
        var controlForm = $('.controls form:first'),
            currentEntry = $(this).parents('.entry:first'),
            newEntry = $(currentEntry.clone()).appendTo('ul');

        newEntry.find('input').val('');

        $(".form-control:last").attr("placeholder", "Pregunta " + numItems);

        controlForm.find('.entry:not(:last) .btn-add')
            .removeClass('btn-add').addClass('btn-remove')
            .removeClass('btn-success').addClass('btn-danger')
            .html('<span class="glyphicon glyphicon-minus"></span>');
    }).on('click', '.btn-remove', function (e) {
        $(this).parents('.entry:first').remove();

        e.preventDefault();
        return false;
    });

});

$(document).ready(function () {
    var num = 10000;
    $(".add_button").click(function (e) {
        e.preventDefault();
        $('#simpleList').append('<div class="list-group-item"><input class="pregunta form-control" id="pregunta_' + num + '" type="text" name="mytext" placeholder= "Pregunta" pos=""/><a href="#" class="remove">Eliminar</a></div>');
        num++;
    });

    $('.wrapper').on("click", ".remove", function (e) {
        e.preventDefault();
        $(this).parent('div').remove();
    });

    $('#simpleList').sortable({
        stop: function (event, div) {
        }
    });
});

// Asignar Juez a categoria
$(document).ready(function () {
    var stockTable = $("#listaJuezTable").DataTable({
        "columnDefs": [
            { "orderable": false, "targets": 0 }
        ],
        "aaSorting": [],
        "order": [[1, "asc"]],
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

    stockTable.on('click', 'tbody tr', function () {
        var $row = $(this);
        var addRow = stockTable.row($row);
        catalogTable.row.add(addRow.data()).draw();
        addRow.remove().draw();

        updateJueces();

    });

    var catalogTable = $('#listaJuezTableAsignados').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": 0 }
        ],
        "aaSorting": [],
        "order": [[1, "asc"]],
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

    catalogTable.on('click', 'tbody tr', function () {
        var $row = $(this);
        var addRow = catalogTable.row($row);
        stockTable.row.add(addRow.data()).draw();
        addRow.remove().draw();

        updateJueces();

    });

    updateJueces();
});


function updateJueces() {
    var idList = [];
    var lista = $("#ContentPlaceHolder_listaJuezTableAsignadosBody").find("span");

    for (var i = 0; i < lista.length; i++) {
        idList[i] = lista[i].id;
    }

    var hiddenValue = document.getElementById("ContentPlaceHolder_hiddenControl");
    hiddenValue.value = idList;
}