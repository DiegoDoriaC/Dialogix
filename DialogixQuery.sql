use master
create database Dialogix
use Dialogix

set dateformat ymd
go

create table Usuarios
(
	IdUsuario int identity primary key,
	Nombre varchar(50),
	Apellido varchar(50),
	FechaNacimiento datetime,
	Rol varchar(10),
	Estado varchar(10),
	Usuario varchar(30) unique,
	Contraseña varchar(300)
)

create table PreguntasFrecuentes
(
	IdPreguntaFrecuente int identity primary key,
	Descripcion varchar(50),
	Estado varchar(10),
	Orden int unique
)

create table MetricaUso
(
	IdMetrica int identity primary key,
	Fecha date,
	TotalConversaciones int
)

create table Convesaciones
(
	IdConversacion int identity primary key,
	DniUsuario int,
	FechaInicio datetime,
	FechaFin datetime,
	Canal varchar(10),
	Estado varchar(10)
)

create table Mensajes
(
	IdMensaje int identity primary key,
	IdConversacion int references Convesaciones,
	Texto varchar(300),
	Respuesta varchar(300),
	Fecha datetime
)

create table Feedback
(
	IdFeedback int identity primary key,
	idConversacion int references Convesaciones,
	Calificacion int,
	Fecha datetime
)

insert into Usuarios ([Nombre], [Apellido], [FechaNacimiento], [Rol], [Estado], [Usuario], [Contraseña])
values ('Diego', 'Doria', '2002-04-06', 'ADMIN', 'ACT', 'admin', 'admin123') --luego se codificará la contraseña

----------------------------------------	USUARIO	  ----------------------------------------
create procedure pr_iniciar_sesion
	@Usuario varchar(30),
	@Contraseña varchar(300)
as
begin
	select IdUsuario, Nombre, Apellido, Rol 
	from Usuarios where Usuario = @Usuario
	AND Contraseña = @Contraseña
	AND Estado = 'ACT'
end
go


----------------------------------------	METRICA	  ----------------------------------------
create procedure pr_registrar_metrica
	@Fecha datetime
as
begin
	update MetricaUso set TotalConversaciones = (TotalConversaciones + 1) where fecha = CONVERT(date, @Fecha)

	if(@@ROWCOUNT = 0)
	begin
		insert into MetricaUso ([Fecha], [TotalConversaciones]) values (@Fecha, 1)
	end
end
go

exec pr_registrar_metrica @Fecha = '2025-01-01'
exec pr_registrar_metrica @Fecha = '2025-02-01'
exec pr_registrar_metrica @Fecha = '2025-03-01'
exec pr_registrar_metrica @Fecha = '2025-03-05'
exec pr_registrar_metrica @Fecha = '2025-03-13'
exec pr_registrar_metrica @Fecha = '2025-04-30'
exec pr_registrar_metrica @Fecha = '2025-05-12'
exec pr_registrar_metrica @Fecha = '2025-06-05'
exec pr_registrar_metrica @Fecha = '2025-07-06'
exec pr_registrar_metrica @Fecha = '2025-08-09'
exec pr_registrar_metrica @Fecha = '2025-09-21'
exec pr_registrar_metrica @Fecha = '2025-10-06'
exec pr_registrar_metrica @Fecha = '2025-11-01'
exec pr_registrar_metrica @Fecha = '2025-11-01'
exec pr_registrar_metrica @Fecha = '2025-02-01'


alter procedure pr_filtrar_metrica_uso
	@FechaInicio datetime,
	@FechaFin datetime
as
begin
	select sum(TotalConversaciones) as consultasTotales from MetricaUso
	where Fecha >= @FechaInicio
	AND Fecha <= @FechaFin
end
go

alter procedure pr_filtrar_metrica_uso_por_dia
	@FechaInicio datetime,
	@FechaFin datetime
as
begin
	select Fecha, SUM(TotalConversaciones) as RegPorDia from MetricaUso
	where Fecha >= @FechaInicio
	AND Fecha <= @FechaFin
	group by Fecha
	order by Fecha
end
go

alter procedure pr_filtrar_metrica_uso_por_mes
	@FechaInicio datetime,
	@FechaFin datetime
as
begin
	select month(Fecha) as NumeroMes, 
	SUM(TotalConversaciones) as RegPorMes from MetricaUso
	where Fecha >= @FechaInicio
	AND Fecha <= @FechaFin
	group by month(Fecha)
	order by month(Fecha)
end
go


----------------------------------------	PREGUNTAS FRECUENTES	  ----------------------------------------
create procedure pr_listar_preguntas_frecuentes
as
begin
	select IdPreguntaFrecuente, Descripcion, Orden from PreguntasFrecuentes
	order by Orden
end
go

create procedure pr_registrar_pregunta_frecuente
	@Descripcion varchar(50),
	@Orden int,
	@xIdPreFre int out
as
begin
	insert into PreguntasFrecuentes ([Descripcion], [Estado], [Orden]) 
	values (@Descripcion, 'ACT', @Orden)

	set @xIdPreFre = SCOPE_IDENTITY()
end
go

create procedure pr_modificar_pregunta_frecuente
	@IdPreguntaFrecuente int,
	@Descripcion varchar(50),
	@Orden int
as
begin
	update PreguntasFrecuentes set
	Descripcion = @Descripcion,
	Orden = @Orden
	where IdPreguntaFrecuente = @IdPreguntaFrecuente
end
go

create procedure pr_eliminar_preguntas_frecuentes
	@IdPreguntaFrecuente int
as
begin
	delete PreguntasFrecuentes where IdPreguntaFrecuente = @IdPreguntaFrecuente
end
go

----------------------------------------	CONVERSACIONES	 ----------------------------------------
create procedure pr_registrar_conversacion
	@DniUsuario int,
	@FechaInicio datetime,
	@Canal varchar(10),
	@IdConversacion INT OUTPUT
as
begin
	insert into Convesaciones ([DniUsuario], [FechaInicio], [Canal], [Estado])
	values (@DniUsuario, @FechaInicio, @Canal, 'ABIERTO')
	SET @IdConversacion = SCOPE_IDENTITY();
end
go

create procedure pr_actualizar_finalizar_conversacion
	@IdConversacion int,
	@IDFechaFin datetime
as
begin
	update Convesaciones set 
	FechaFin = @IDFechaFin,
	Estado = 'FINALIZADO'
	where IdConversacion = @IdConversacion
end
go

----------------------------------------	MENSAJES	----------------------------------------
create procedure pr_registrar_mensaje
	@IdConversacion int,
	@Texto varchar(300),
	@Respuesta varchar(300),
	@Fecha datetime
as
begin
	insert into Mensajes ([IdConversacion], [Texto], [Respuesta], [Fecha])
	values (@IdConversacion, @Texto, @Respuesta, @Fecha)
end
go

----------------------------------------	FEEDBACK	----------------------------------------
create procedure pr_registrar_feedback
	@IdConversacion int,
	@Calificacion int,
	@Fecha datetime
as
begin
	insert into Feedback ([idConversacion], [Calificacion], [Fecha])
	values (@IdConversacion, @Calificacion, @Fecha)
end
go

----------------------------------------	PROCEDURES ESPECIALES	----------------------------------------

--- Exportar Historial Conversaciones Pacientes - IA
create procedure pr_listar_conversaciones_mensajes
	@FechaInicio datetime,
	@FechaFin datetime,
	@DniUsuario int,
	@Estado varchar(10),
	@Calificacion int,
	@Canal varchar(10)
as
begin
	select con.IdConversacion, con.DniUsuario, con.FechaInicio, con.FechaFin, con.Canal, con.Estado,
	men.IdConversacion, men.Texto, men.Respuesta, men.Fecha, fee.Calificacion from Convesaciones con
	left join Mensajes men ON men.IdConversacion = con.IdConversacion
	left join Feedback fee ON fee.idConversacion = con.IdConversacion
	where (@FechaInicio = '' or @FechaInicio <= con.FechaInicio)
	AND (@FechaFin = '' or @FechaFin >= con.FechaInicio)
	AND (@DniUsuario = '' or @DniUsuario = con.DniUsuario)
	AND (@Estado = '' or @Estado = con.Estado)
	AND (@Calificacion = '' or @Calificacion = fee.Calificacion)
	AND (@Canal = '' or @Canal = con.Canal)
end
go

--- Exportar Feedback Segun Rango de Fechas
create procedure pr_listar_feedback_rango_fechas
	@FechaInicio datetime,
	@FechaFin datetime,
	@Calificacion int,
	@Canal varchar(10)	
as
begin
	select con.IdConversacion, con.Estado, fee.Fecha, 
	fee.Calificacion from Convesaciones con
	left join Feedback fee ON fee.idConversacion = con.IdConversacion
	where (@FechaInicio = '' or @FechaInicio <= con.FechaInicio)
	AND (@FechaFin = '' or @FechaFin >= con.FechaInicio)
	AND (@Calificacion = '' or @Calificacion = fee.Calificacion)
	AND (@Canal = '' or @Canal = con.Canal)
end
go