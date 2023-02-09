$(function () {
    $("#btnbuscar").click(function (e) {
        e.preventDefault();
        input.click();
    });

})

$(document).ready(function () {
    $('#tablaCategorias').DataTable({
        language: dtlenguage,
        "columnDefs": [
            {"width": "10%", "targets": 2},
            {"width": "15%", "targets": 3}
        ],
        "iDisplayLength": 25
    });
});

