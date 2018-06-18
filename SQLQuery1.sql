create procedure DetalleCotizaciones
@idcotizacion int
as
select top 100 De.id as ID, Se.nombre as Servicio ,
De.cantidad as Cantidad, De.PrecioCotizacion as Cotizado
from DetalleCotizacions De inner join Servicios Se
on De.id= Se.id
where idcotizacion= @idcotizacion
go
