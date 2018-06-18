create procedure MostrarCotizaciones
as
select top 100 Co.id as ID, (Ci.nombre + ' '+ Ci.apellido) as Cliente,
Co.Fecha as Fecha, Co.Total As Total, Co.Estado as Estado
from Cotizaciones Co inner join Clientes Ci 
on Ci.id= Co.idcliente
go
create procedure MostrarDetalle
@idcotizacion int
as
select top 100 De.id as ID, Se.nombre as Servicio, Se.Costo as Precio

go
