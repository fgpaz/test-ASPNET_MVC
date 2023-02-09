$("#mi-lista").select2({
    theme: "bootstrap",
});

$(window).resize(function () {
    $('.select2').css('width', "100%");
});

$("#btnGuardar").click(function (e) {
    e.preventDefault();

    let nombreCategoria = $("#nombreCategoria").val();

    let form = $("#formEditCategoria");
    let botonGuardar = $("#btnGuardar");

    if (nombreCategoria === "") {
        showAlert("Debe completar todos los campos obligatorios", "warning");
        return;
    }

    botonGuardar.attr("value", "Guardando");

    $("#modalLoader").modal('show');
    form.submit();
});