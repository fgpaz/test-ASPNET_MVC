// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

let dtlenguage = {
    "search": "Buscar: ",
    "lengthMenu": "Mostrar _MENU_ registros por página",
    "zeroRecords": "No se encontraron registros",
    "info": "Mostrando _PAGE_ de _PAGES_ páginas",
    "paginate": {
        "previous": "Anterior",
        "next": "Siguiente"
    },
    "select": {
        rows: {
            _: "%d filas seleccionadas",
            1: "1 fila seleccionada"
        }
    }
};


function alert(message, type, container) {
    if (!container) container = alertPlaceholder;
    let wrapper = document.createElement('div');
    wrapper.innerHTML = '<div class="alert alert-' + type + ' alert-dismissible" role="alert">'
        + message + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>'

    container.append(wrapper);
}

function showAlert(message, type, container) {
    $(".alert").remove();
    alert(message, type, container);

    setTimeout(function () {
        $(".alert").fadeTo(500, 0).slideUp(500, function () {
            $(this).remove();
        });
    }, 6000)
}