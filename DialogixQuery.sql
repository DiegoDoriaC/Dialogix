use master
create database Dialogix
use Dialogix

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
	Fecha datetime,
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
