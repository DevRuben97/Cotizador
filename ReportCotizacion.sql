create procedure ReportCotizacion
@idcotizacion int
as
select C.Total as Total, C.Fecha as Fecha, (E.nombre+' '+ E.apellido) as Cliente, E.direccion as Dirección,
E.telefono as Telefono, E.email as Correo, E.DNI as Cedula,
Dc.id as Codigo,S.nombre as Servicio, S.Descripcion as Descripción,
Dc.cantidad as Cantidad, Dc.PrecioCotizacion as Precio
from Cotizaciones C inner join DetalleCotizacions Dc on c.id= Dc.idcotizacion
inner join Clientes E on C.idcliente= E.id
inner join Servicios S on Dc.idservicio= S.id
where C.id= @idcotizacion
go