
//Variables Globales:
var ModalWindow = $("#ClientesModal");
var encabezado = $("#modaltitle");
var cuerpo = $("#modalBody");
var footerModal = $("#PieModal");
var Aceptar = $("#btnprimary");
var cancelar = $("#btnsecundary");
var ModalIcon = $("#IconTitle");
var Table = $("#Tclientes");

function InitializeTable(DataUrl) {

    $(document).ready(function () {

        Table = Table.DataTable({
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
                "type": "POST",
                "datatype": "json",
                "url": DataUrl+"/"
            },
            "info": true,
            "columns": [
                { title: "ID","searchable": false, "data": "id" },
                { title: "Nombre","data": "nombre" },
                { title: "Apellido","searchable": false, "data": "apellido" },
                { title: "TipoCliente", "searchable": false,"data": "TipoCliente" },
                { title: "DNI","data":"DNI"},
                { title: "Dirección", "searchable": false,"data": "direccion" },
                { title: "Telefono", "searchable": false,"data": "telefono" },
                { title: "Correo", "searchable": false,"data": "email" },
                { title: "Acciones", "searchable": false,"data": null }
            ],
            "columnDefs": [{
                "targets": -1,
                "data": null,
                "defaultContent": "<a class='btn btn-secundary' data-toggle='modal' data-target='#ClientesModal' onclick='Editar()'><i class='fas fa-edit'></i></a>|" +
                "<button class='btn btn-danger' onclick='Eliminar()'><i class='fas fa-times-circle'></i></button>"

            }]


        });

    })

}
function Nuevo() {
    $(document).ready(function () {

        Aceptar.text("Agregar Cliente");
        encabezado.text("Nuevo Cliente");
        cancelar.text("Cancelar");
        //Peticion Ajax Al Servidor:
        $.get("/Clientes/Nuevo", function (back, status) {

            cuerpo.html(back);

        });

        Aceptar.on("click", function () {

            var form = $("#FormClientes");

            if (ValidarCliente(form)) {

                Aceptar.text("Procesando..");
                Aceptar.attr("disabled", true);
                var values = form.serializeArray();

                //Enviar Los Datos Al Servidor:
                $.post("/Clientes/Nuevo", values, function (back,status) {

                    if (back.Error == false) {

                        swal({
                            title: "Nuevo Cliente",
                            text: back.Mensaje,
                            type: "success"

                        });
                        Table.ajax.reload();
                        ModalClose();
                        
                    }
                    else {
                        swal({
                            title: "Nuevo Cliente",
                            text: back.Mensaje,
                            type: "error"
                        });
                        Aceptar.attr("disabled", false);
                        Aceptar.text("Agregar Cliente");
                    }

                });
            }
        });

    });

}
function Editar() {
    $(document).ready(function () {

        encabezado.text("Editar Cliente");
        Aceptar.text("Guardar Cambios");
        cancelar.addClass("btn btn-danger");

        $("#Tclientes tbody").on("click", "a", function () {

            var datos = Table.row($(this).parents("tr")).data();
            $.get("/Clientes/Editar/" + datos.id, function (backdata, status) {

                cuerpo.html(backdata);
            });
        });

        Aceptar.click(function () {

            var form = $("#FormClientes");

            if (ValidarServicio(form)) {

                Aceptar.text("Procesando..");
                Aceptar.attr("disable", true);

                var values = form.serializeArray();
                $.post("Clientes/Editar", values, function (callback, status) {

                    if (callback.Error == false) {

                        swal({
                            title: "Editar Cliente",
                            text: callback.Mensaje,
                            type: "success"
                        });
                        CloseModal();
                    }
                    else {
                        swal({
                            title: "Editar Cliente",
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
function Eliminar() {

    $(document).ready(function () {

        $("#Tclientes tbody").on("click", "button", function () {

            var datos = Table.row($(this).parents("tr")).data();

            //Confirmacion Para Eliminar los datos:
            var requets = new Request("/Clientes/Eliminar/" + datos.id, { method: "POST", mode: "cors", cache: "default" });
            swal({
                title: '¿Esta Seguro?',
                text: "Eliminar El Cliente: " + datos.nombre,
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

}
function ModalClose() {
    Aceptar.text("Agregar Cliente");
    Aceptar.attr("disable", false);
    cancelar.text("Cancelar");
    cuerpo.html("<h1 class='text-center'>Cargando...</h1>");
    ModalWindow.modal("hide");
}
function ValidarCliente(form) {

    var data = form.serializeArray();
    var validate = false;

    $(data).each(function (x) {

        if (data[x]["value"] == null || data[x]["value"] == "") {
            $("span[name='" + data[x]["name"] + "']").text("El Campo " + data[x]["name"] + "Es Requerido");
            validate = false;
        }
        else {
            validate = true;
        }

    });
    return validate;

}