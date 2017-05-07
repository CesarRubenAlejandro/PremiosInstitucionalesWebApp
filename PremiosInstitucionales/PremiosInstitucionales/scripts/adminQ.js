$(function()
{
    $(document).on('click', '.btn-add', function(e)
    {
        e.preventDefault();
       var numItems = $('.entry').length+1;
        var controlForm = $('.controls form:first'),
            currentEntry = $(this).parents('.entry:first'),
            newEntry = $(currentEntry.clone()).appendTo('ul');

        newEntry.find('input').val('');

        $(".form-control:last").attr("placeholder","Pregunta "+numItems);

        controlForm.find('.entry:not(:last) .btn-add')
            .removeClass('btn-add').addClass('btn-remove')
            .removeClass('btn-success').addClass('btn-danger')
            .html('<span class="glyphicon glyphicon-minus"></span>');
    }).on('click', '.btn-remove', function(e)
    {
		$(this).parents('.entry:first').remove();
        
		e.preventDefault();
		return false;
	});

});

 


$(document).ready(function(){
    $(".add_button").click(function(e){
        e.preventDefault();
        $('#PanelPreguntas').append('<div class="list-group-item"><input class="pregunta form-control" type="text" name="mytext[]" placeholder= "Pregunta"/><a href="#" class="remove">Eliminar</a></div>');
    });

    $('.wrapper').on("click",".remove", function(e){
        e.preventDefault();
        $(this).parent('div').remove();
    });

    $('#PanelPreguntas').sortable({
        stop: function(event,div){
            alert(div.item.index());
        }
    });
});
