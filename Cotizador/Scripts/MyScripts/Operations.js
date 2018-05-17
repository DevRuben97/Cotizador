
//Variables Globales
var encabezado = $("#modaltitle");
var cuerpo = $("#modalBody");
var footerModal = $("#PieModal");
var Aceptar = $("#btnprimary");
var cancelar = $("#btnsecundary");
var ModalIcon = $("#IconTitle");

function FormServicio(url) {
    
    $(document).ready(function () {

        encabezado.text(" Agregar Servicio");
        ModalIcon.addClass("fas fa-wrench");
        encabezado.addClass("NewModel");
        Aceptar.text("Crear Servicio").show();
        $("#btnprimary").hide();
        

        //Obtener Los El Partial View
        $.get(url, function (data, status) {

            cuerpo.html(data);
        });

    });
}
function CloseModal() {
    Aceptar.show();
    cuerpo.html("<h1 class='text-center'>Cargando...</h1>");
}