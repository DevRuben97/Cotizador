
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
                { title: "ID", "data": "id" },
                { title: "Nombre", "data": "nombre" },
                { title: "Apellido", "data": "apellido" },
                { title: "TipoCliente", "data": "TipoCliente" },
                { title: "DNI", "data":"DNI"},
                { title: "Dirección", "data": "direccion" },
                { title: "Telefono", "data": "telefono" },
                { title: "Correo", "data": "email" },
                { title: "Acciones", "data": null }
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


}
function Editar() {


}
function Eliminar() {


}
function ValidarCliente(form) {


}