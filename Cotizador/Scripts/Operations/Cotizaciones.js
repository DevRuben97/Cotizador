
var table = $("#Coti")
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
} //Buscar Los Clientes Segun la Sugerencia de la Base De Datos
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
            select: function (event,ui) {

                $.ajax({
                    url: "/Cotizador/FillClientData",
                    dataType: "json",
                    data: { nombre: ui.item.value },
                    success: function (data) {

                        if (data.Error != true) {

                            $("#NumIdentidad").val(data.dni);
                            $("#Telefono").val(data.phone);
                            $("Direccion").val(data.direccion);
                            
                            switch (data.TipoCliente) {

                                case "Empresa":
                                    $("#TipoCliente option[value='Empresa']").prop("selected", true);
                                    break;
                                case "Persona":
                                    $("#TipoCliente option[value='Persona']").prop("selected", true);
                                    break;        
                            }
                        }
                        else {
                            console.log(data.Mensaje);
                        }
                    }
                });

            }

        });
   })
 
