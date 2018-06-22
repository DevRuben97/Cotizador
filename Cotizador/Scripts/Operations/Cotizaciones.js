
var table = $("#Coti");
var serviciosTable;
var TotalCotizacion = 0;

function MostrarTabla(url) {

    $(document).ready(function () {
        table = table.DataTable({
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
            "searching": false,
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": url + "/",
                "type": "POST",
                "datatype": "json",
            },
            "info": true,
            "columns": [
                { title: "ID", "data": "id" },
                { title: "Cliente", "data": "Cliente" },
                { title: "Fecha", "data": "Fecha" },
                { title: "Total", "data": "Total" },
                { title: "Estado", "data": "Estado" },
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
function VerServicios() {
    $(document).ready(function () {

        $("#modaltitle").text("Seleccionar Servicios");
        $("#btnprimary").attr("disabled", true);

        $.ajax({
            url: "/Cotizador/ListaServicios/",
            type: "json",
            method: "get",
            success: (data) => {

                $("#modalBody").html(data);
                
            }
        });


    });

}
function CloseModal() {
    $("#ServiciosModal").modal("hide");
}
function SelectServicio(datos) {

    var itbis = datos.Costo * 0.18;
    

    $("#Cotizacion tbody").append("<tr><th>" + datos.id + "</th>" +
        "<th>1</th>" +
        "<th>" + datos.Descripcion + "</th>" +
        "<th>" +"$"+ datos.Costo + "</th>" +
        "<th>" + "$" + itbis + "</th >" +
        "<th>" + "$" + datos.Costo + "</th ></tr > ");

    TotalCotizacion = TotalCotizacion + datos.Costo;
    $("#Total").text("Total: RD: $" + TotalCotizacion);
    
   
}
function GuardarCotizacion() {

    var date = new Date();

    var fecha = date.getDate() + "/" + (date.getMonth()+1) + "/" + date.getFullYear();
    var estado = "REALIZADO";
    var idcliente = $("#IdCliente").val();


}
function EliminarFila() {

    $("#Cotizacion tbody tr").each(function (x) {

        if ($(this).hasClass("table-active")) {

            $(this).remove();
        }

    });

}
//Configuraciones Inicial al Cargar El View
//Buscar Los Clientes Segun la Sugerencia de la Base De Datos
$(document).ready(() => {

    $("#BuscarCliente").autocomplete({
        source: (request, response) => {
            $.ajax({
                url: "/Cotizador/BuscarClientes",
                dataType: "json",
                data: { search: request.term },
                success: (data) => {
                    response(data.Clientes);
                }
            });

        },
        minLegth: 1,
        select: function (event, ui) {

            $.ajax({
                url: "/Cotizador/DatosCliente",
                dataType: "json",
                data: { nombre: ui.item.value },
                method: "post",
                success: function (data) {

                    if (data.Error != true) {

                        $("#NumIdentidad").val(data.dni);
                        $("#Telefono").val(data.phone);
                        $("#Direccion").val(data.direccion);
                        $("#TipoCliente").val(data.TipoCliente);
                    }
                    else {
                        console.log(data.Mensaje);
                    }
                }
            });

        }

    });
    //Seleccionar filas de la tabla de Cotizacionea.
    $("#Cotizacion tbody").on("click", "tr", function () {

        $("#Cotizacion tbody tr").each(function () {

            if ($(this).hasClass("table-active")) {

                $(this).removeClass("table-active");
            }

        });

        $(this).addClass("table-active");
        
    });

});
 
