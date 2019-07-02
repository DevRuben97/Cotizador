
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
                { title: "Cliente", "data": "cliente.nombre" },
                {
                    title: "Fecha", "data": "FormatedDate", "render": function (data) {
                        return data;
                    }
                },
                {
                    title: "Expiración", "data": "FormatedExpiracion", "render": function (data) {
                        return data;
                    }
                },
                {
                    title: "Total", "data": "Total", "render": function (data) {
                        return `$${Intl.NumberFormat().format(data)}`;
                    }
                },
                { title: "Estado", "data": "Estado" },
                { title: "Acciones", "data": null }

            ],
            "columnDefs": [{
                "targets": -1,
                "data": null,
                "defaultContent": "<a  class='btn btn-primary' id='btnPrint' onclick='PrintCotizacion()'><i class='fas fa-print'></i></a> " +
                "|<button class='btn btn-danger' onclick='AnularCotizacion()'><i class='fas fa-times-circle'></i></button>" +
                "| <button class='btn btn-info' id='btnDetalle' data-target='#CotizacionModal' data-toggle='modal' onclick='VerDetalle()'><i class='fas fa-info-circle'></i></button>"
            }
            ],
            "initComplete": function () {
                console.log("Los datos de las cotizaciones fueron cargadas correctamente");
            }
           

        });

        $("#Coti_filter").html("");
        $("#Date2").change(SearchByDate)
    });


}
function SearchByDate() {
    var date1 = $("#Date1").val();
    var date2 = $("#Date2").val();

    table.search(`${date1}/${date2}`).draw();
}
function VerServicios() { // Mostrar el Html Devuelto por el servidor en el modal
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
        "<th>" + Intl.NumberFormat().format(datos.Costo) + "</th>" +
        "<th> $" + Intl.NumberFormat().format(itbis) + "</th >" +
        "<th> $" + Intl.NumberFormat().format((datos.Costo + itbis)) + "</th ></tr > ");

    TotalCotizacion += (datos.Costo + itbis);
    $("#Total").text("Total: RD: $" + Intl.NumberFormat().format(TotalCotizacion));
    $("#CreateCoti").attr("disabled", false);
    
   
}
function GuardarCotizacion() {//Guardar la Cotizacion Hecha.

    var date = new Date();
    var Expiracion = $("#Expiracion").val();
    var Detalles = [];
    

    //Obtener todos los datos requeridos:

    var fecha = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
    var client = $("#BuscarCliente").val();

    $("#Cotizacion tbody tr").each(function (x) {

        var Items = $(this).children("th").toArray();

        var DetailItem = {};

        DetailItem.idservicio = Number.parseInt(Items[0].textContent);
        DetailItem.cantidad = Number.parseInt(Items[1].textContent);
        DetailItem.PrecioCotizacion = Number.parseFloat(Items[5].textContent);

        Detalles.push(DetailItem);

    });

    var data = { Ncliente: client, Fecha: fecha, Expiracion: Expiracion, Total: TotalCotizacion, Detalle: Detalles };
    
    
    //Enviar Los Datos Obtenidos

    $.ajax({
        url: "/Cotizador/Nuevo/",
        method: "post",
        dataType: "json",
        data: data,
        success: function (CallBack) {

            if (CallBack.Error === false) {
                swal({
                    title: "Crear Cotización",
                    text: CallBack.Mensaje,
                    type: "success"
                }).then(function () {
                    window.location.href = "/Cotizador/Lista";
                })
                

                
            }
            else {
                swal({
                    title: "Crear Cotización",
                    text: CallBack.Mensaje,
                    type: "error"
                });
            }

        }


    });




}
function EliminarFila() {


    $("#Cotizacion tbody tr").each(function (x) {

        if ($(this).hasClass("table-active")) {

            var Money = Number.parseInt($(this).children("th").toArray()[5].textContent);
            TotalCotizacion = TotalCotizacion- Money;
            $("#Total").text("Total: RD: $" + TotalCotizacion);
            $(this).remove();
            
        }
        
        
    });
    if ($("#Cotizacion tbody").children("tr").length <= 0) {

        $("#CreateCoti").attr("disabled", true);
    }

}
function VerDetalle() {

    $(document).ready(function () {

    //Obtener el id de la cotizacion

        $("#modaltitle").text("Detalle de la Cotización");
        $("#btnprimary").text("Aceptar");
        $("#btnsecundary").text("Cerrar");

    $("#Coti tbody").on("click", "#btnDetalle", function () {

        
        var datos = table.row($(this).parents("tr")).data();
        var idcot = datos.id;

        //Realizar la peticion Ajax al Servidor
        $.ajax({
            url: "/Cotizador/Detalle/",
            data: { CotID: idcot },
            success: function (data) {

                $("#modalBody").html(data);
            }
        });

    });

   

    });
}
function AnularCotizacion() {


}
function PrintCotizacion() {

    $(document).ready(function () {

        var idCotizacion = 0;
        $("#Coti tbody").on("click", "#btnPrint", function () {

            //Obtener el id de la tabla seleccionada.
            var datos = table.row($(this).parents("tr")).data();
            idCotizacion = datos.id;

            window.location.href = "/Cotizador/Report?id= " + datos.id;

        });

    });

}
function LinpiarForms() {//Linpiar el formulario por completo


    $("#NumIdentidad").clearFields();
    $("#Telefono").clearFields();
    $("#Direccion").clearFields();
    $("#TipoCliente").clearFields();
    $("#Cotizacion tbody tr").each((x) => {

        $(this).remove();
    });
    TotalCotizacion = 0;
    $("#Total").text("Total:");
    $("#DeleteRow").attr("disabled", true);
    $("#CreateCoti").attr("disabled", true);
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

                    if (data.Error !== true) {

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
 
