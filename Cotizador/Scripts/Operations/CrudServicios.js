
//Variables Globales
var ModalWindow = $("#ModalServicios");
var encabezado = $("#modaltitle");
var cuerpo = $("#modalBody");
var footerModal = $("#PieModal");
var Aceptar = $("#btnprimary");
var cancelar = $("#btnsecundary");
var ModalIcon = $("#IconTitle");
var servicios = $("#Table");

function NuevoServicio(url) {
    $(document).ready(function () {

        encabezado.text("Agregar Servicio");
        Aceptar.text("Crear Servicio").show();
        cancelar.addClass("btn btn-danger").text("Cancelar");
        

        //Obtener Los El Partial View
        $.get(url, function (data, status) {

            cuerpo.html(data);
        });

        Aceptar.click(function () {

            var form = $("#Servicios");
            if (ValidarServicio(form)) {

                Aceptar.text("Procesando..");
                Aceptar.attr("disabled", true);
                

                values = form.serializeArray();


                $.post(url, values, function (callback, status) {

                    if (callback.Error == false) { // Controlar Los Errores del Servidor Error= false Ejecucion exitosa, sino Error.
                        swal({
                            title: "Crear Servicio",
                            text: callback.Mensaje,
                            type: "success"
                        });
                        CloseModal();
                    }
                    else {
                        swal({
                            title: "Crear Servicio",
                            text: callback.Mensaje,
                            type: "warning"
                        });
                        Aceptar.text("Agregar Servicio");
                        Aceptar.attr("disabled", false);
                    }
                });
            }
            
        });
    });
}
function CloseModal() {

    Aceptar.show();
    Aceptar.text("Agregar Servicio");
    Aceptar.attr("disabled", false);
    cuerpo.html("<h1 class='text-center'>Cargando...</h1>");
    ModalWindow.modal("hide");
}
function SetTable(ajaxUrl) {

    $(document).ready(function () {

        servicios = servicios.DataTable({
            "language": {
                "search": "Buscar",
                "searchPlaceholder": "Buscar Por Nombre",
                "lengthMenu": "Mostrando _MENU_ Por Pagina",
                "zeroRecords": "No Se Encontraron Registros",
                "infoEmpty": "No Hay Registros Para Mostrar",
                "info": "Mostrando Pagina _PAGE_ De _PAGES_",
                "processing": "Cargando Datos, Espere..",
                "paginate": {
                    "first": "Primero",
                    "last": "Ultimo",
                    "next": "Siguiente",
                    "previous": "Anteriror"
                },
                "infoFiltered": "Filtrado Desde _MAX_ Registros Totales"



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
                { title: "ID", "data": "id" },
                { title: "Nombre", "data": "nombre" },
                { title: "Descripcición", "data": "Descripcion" },
                { title: "Costo", "data": "Costo" },
                { title: "Acciones", "data": null }

            ],
            "columnDefs": [{
                "targets": -1,
                "data": null,
                "defaultContent": "<a  class='btn btn-secondary' data-toggle='modal' data-target='#ModalServicios' data-placement='top' onclick='EditarServicio()'><i class='fas fa-edit'></i></a> " +
                "|<button class='btn btn-danger' onclick='EliminarServicio()'><i class='fas fa-times-circle'><i/></button>"
            }
            ]

        });
    });


}

function EliminarServicio() {

    $(document).ready(function () {

        $("#Table tbody").on("click", "button", function () {

            var datos = servicios.row($(this).parents("tr")).data();

            //Confirmacion Para Eliminar los datos:
            var requets = new Request("/Servicios/Eliminar/" + datos.id, { method: "POST", mode: "cors", cache: "default" });
            swal({
                title: '¿Esta Seguro?',
                text: "Eliminar El Servicio: "+ datos.nombre,
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                showLoaderOnConfirm: true,
                cancelButtonColor: '#d33',
                cancelButtonText: "Cancelar",
                confirmButtonText: 'Si, Eliminarlo',
                preConfirm: function () {
                    return fetch(requets).then(function (Response) {
                        if (Response.ok) {
                            return Response.json();
                        }

                    });
                },
                allowOutsideClick: () => !swal.isLoading()
            }).then((result) => {
                if (result.value) {

                    if (result.value.Status) {
                        swal({
                            title: "Elimnado!",
                            text: "" + result.value.Mensaje,
                            type: "success"
                        });
                    }
                    else {
                        swal({
                            title: "Error Encontrado!",
                            text: "" + result.value.Mensaje,
                            type: "error"
                        });
                    }
                }
            })

        });

    });

    //Venta De Confirmacion:
   
}
function EditarServicio() {

    $(document).ready(function () {

        encabezado.text("Editar Servicio");
        Aceptar.text("Guardar Cambios");
        cancelar.addClass("btn btn-danger");

        $("#Table tbody").on("click", "a", function () {

            var datos = servicios.row($(this).parents("tr")).data();
            $.get("/Servicios/Editar/" + datos.id, function (backdata, status) {

                cuerpo.html(backdata);
            });
        });

        Aceptar.click(function () {

            var form = $("#Servicios");

            if (ValidarServicio(form)) {

                Aceptar.text("Procesando..");
                Aceptar.attr("disable", true);

                var values = form.serializeArray();
                $.post("Servicios/Editar", values, function (callback, status) {

                    if (callback.Error == false) {

                        swal({
                            title: "Editar Servicio",
                            text: callback.Mensaje,
                            type: "success"
                        });
                        CloseModal();
                    }
                    else {
                        swal({
                            title: "Editar Servicio",
                            text: callback.Mensaje,
                            type: "error"
                        });
                        Aceptar.attr("disable", false);
                        Aceptar.text("Guardar Cambios");
                    }

                });
            }

        })
        

    });

}

function ValidarServicio(form) {// Validar los Campos de Texto 

    var data = form.serializeArray();
    var validate = false;
    $(data).each(function (x) {

        if (data[x]["value"] == "") {
            $("span[name='" + data[x]["name"] + "']").text("El Campo " + data[x]["name"] + " es Requerido");
            validate = false;
        }
        else {
            validate = true;
        }
    });

    return validate;

}