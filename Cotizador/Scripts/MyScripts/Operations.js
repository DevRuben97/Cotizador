
//Variables Globales
var ModalWindow = $("#ModalServicios");
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
                { title: "ID", "data":"id" },
                { title: "Nombre", "data": "nombre" },
                { title: "Descripcición", "data": "Descripcion" },
                { title: "Costo", "data": "Costo" },
                { title: "Acciones", "data": null }

            ],
            "columnDefs": [{
                "targets": -1,
                "data": null,
                "defaultContent": "<button  class='btn btn-secondary' data-toggle='modal' data-target='#ModalServicios' data-placement='top' onclick='EditarServicio()'><i class='fas fa-edit'></i></button> " +
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

    this.encabezado.text("Editar Servicio");
    this.Aceptar.text("Editar Servicio");
    this.cancelar.addClass("btn btn-danger");

    $(document).ready(function () {
        $("#Table tbody").on("click", "button", function () {

            var data = servicios.row($(this).parent("tr")).data();
            $.get("/Servicios/Editar/" + data.id, function (datos, status) {

                this.cuerpo.html(datos);
            });
        });

    });

}