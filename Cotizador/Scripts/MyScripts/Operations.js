
//Variables Globales
var modal = $("#ModalServicios");
var encabezado = $("#modaltitle");
var cuerpo = $("#modalBody");
var footerModal = $("#PieModal");
var Aceptar = $("#btnprimary");
var cancelar = $("#btnsecundary");
var ModalIcon = $("#IconTitle");
var servicios = $("#Table");

function FormServicio(url) {
    
    $(document).ready(function () {

        encabezado.text(" Agregar Servicio");
        Aceptar.text("Crear Servicio").show();
        cancelar.addClass("btn btn-danger").text("Cancelar");
        

        //Obtener Los El Partial View
        $.get(url, function (data, status) {

            cuerpo.html(data);
        });

        Aceptar.click(function () {

            var form = $("#Servicios");
            values = form.serializeArray();

            $.post(url, values, function (callback, status) {

                if (callback.Error == false) {
                    swal({
                        title: "Crear Servicio",
                        text: callback.Mensaje,
                        type: "success"
                    });
                    modal.modal("hide");
                    
                    
                    

                }
                else {
                    swal({
                        title: "Crear Servicio",
                        text: callback.Mensaje,
                        type: "warning"
                    });
                }
            })
        });
    });
}
function CloseModal() {
    Aceptar.show();
    cuerpo.html("<h1 class='text-center'>Cargando...</h1>");
}
function SetTable(ajaxUrl) {

    $(document).ready(function () {
        servicios.DataTable({
            "language": {
                "search": "Buscar",
                "searchPlaceholder": "Buscar Por Nombre",
                "lengthMenu": "Mostrando _MENU_ Por Pagina",
                "zeroRecords": "No Se Encontraron Registros",
                "infoEmpty": "No Hay Registros Para Mostrar",
                "info": "Mostrando Pagina _PAGE_ De _PAGES_",



            },
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": ajaxUrl + "/",
                "type": "POST",
                "datatype": "json",
            },
            "info": true,
            "columns": [
                { title: "Nombre", "data": "nombre" },
                { title: "Descripcición", "data": "Descripcion" },
                { title: "Costo", "data": "Costo" },
                { title: "Acciones", "data": null }

            ],
            "columnDefs": [{
                "targets": -1,
                "data": null,
                "defaultContent": "<button  class='btn btn-secondary' onclick='EditarServicio()'><i class='fas fa-edit'></i></button> " +
                "|<button class='btn btn-danger' onclick='EliminarServicio()'><i class='fas fa-times-circle'><i/></button>"
            }
            ]

        });
    });


}

function EliminarServicio() {
    swal("Una Prueba");
}
function EditarServicio() {


}