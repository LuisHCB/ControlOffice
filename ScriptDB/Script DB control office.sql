use master;
go
create Database ControlOffice
go
use ControlOffice
go

/*Tabla se usuarios*/
Create table Usuarios 
(
 Usuario nvarchar(20) primary key,
 Pass nvarchar(20) not null,
 Nombre nvarchar (255)
)
go


Create table Tipo_electronico
(
 Id_tipo_electronico int not null identity(1,1) primary key,
 Nombre nvarchar(100)
)
go

Create table Marca_electronicos
(
 Id_marca int not null identity(1,1) primary key,
 Marca nvarchar(255) 
)
go

Create table Electronicos
(
 Id_electronico int not null identity(1,1) primary key,
 Id_tipo_electronico int not null,
 Cantidad int not null, 
 NoSerie nvarchar(50) not null,
 Id_marca int,
 FechaUso date,
 Reemplazo int,
 Imagen nvarchar(200),
 Usuario_registra nvarchar(20) not null,
 
 
 foreign key (Id_tipo_electronico) references Tipo_electronico(Id_tipo_electronico)
 on delete cascade on update no action,      
 foreign key (Id_marca) references Marca_electronicos(Id_marca)
 on delete cascade on update no action,
 foreign key (Usuario_registra)references Usuarios(Usuario)
 on update cascade on delete cascade
)
go


Create table Tipo_mobiliario
(
 Id_tipo_mobiliario int not null identity(1,1) primary key,
 Nombre nvarchar(100) 
)
go

Create table Marca_mobiliario
(
 Id_marca int not null identity(1,1) primary key,
 Marca nvarchar(255) 
)
go

Create table Mobiliarios
(
 Id_mobiliario int not null identity(1,1) primary key,
 Id_tipo_mobiliario int not null,
 Cantidad int not null, 
 NoSerie nvarchar(50) not null,
 Id_marca int,
 FechaUso date,
 Reemplazo int,
 Imagen nvarchar(200),
 Usuario_registra nvarchar(20) not null,
 
 
 foreign key (Id_tipo_mobiliario) references Tipo_mobiliario(Id_tipo_mobiliario)
 on delete cascade on update no action,      
 foreign key (Id_marca) references Marca_mobiliario(Id_marca)
 on delete cascade on update no action,
 foreign key (Usuario_registra)references Usuarios(Usuario)
  on update cascade on delete cascade
)
go


Create table Tipo_consumible
(
 Id_tipo_consumible int not null identity(1,1) primary key,
 Nombre nvarchar(100),
)
go


Create table Consumibles
(
 Id_consumible int not null identity(1,1) primary key,
 Id_tipo_consumible int not null,
 Cantidad int not null,
 Id_marca int,
 Clave nvarchar(50),
 Fecha_recepcion datetime2, 
 Entrega nvarchar(255),
 Usuario_recibe nvarchar(20) not null, 
 Imagen nvarchar(200),
 
 foreign key (Id_tipo_consumible) references Tipo_consumible(Id_tipo_consumible)
 on delete cascade on update no action,      
 foreign key (Usuario_recibe) references Usuarios(Usuario)
 on delete cascade on update no action,
)
go

create table Tipo_solicitudes
(
 Id_tipo_solicitud int not null identity(1,1) primary key,
 Tipo_solicitud nvarchar(100)
)
go

create table Solicitudes
(
  Id_solicitud int not null identity(1,1) primary key,
  Id_tipo_solicitud int not null,
  Destino nvarchar(255) not null,
  Descripcion nvarchar(1024) not null,
  Fecha_envio datetime2,
  Folio nvarchar(50),
  Imagen nvarchar(200),
  Usuario_registra nvarchar(20),
  
  
  foreign key (Id_tipo_solicitud) references Tipo_solicitudes(Id_tipo_solicitud)
  on delete cascade on update cascade,
  foreign key (Usuario_registra)references Usuarios(Usuario)
  on update cascade on delete cascade
)
 go
 
 
 create table Tipos_documento
 (
  Id_tipo_documento int not null identity(1,1) primary key,
  Tipo_documento nvarchar(150)
 )
 go
 
 create table Documentos_enviados
 (
  Id_documento int not null identity(1,1) primary key,
  Destino nvarchar(255) not null,
  Asunto nvarchar(500) not null,
  Id_tipo_documento int,
  Fecha_envio datetime2,
  Folio nvarchar(50),
  Imagen nvarchar(200),
  Usuario_registra nvarchar(20),  
  
  foreign key (Id_tipo_documento) references Tipos_documento(Id_tipo_documento)
  on update cascade on delete cascade,
  foreign key (Usuario_registra)references Usuarios(Usuario)
  on update cascade on delete cascade
  
 )
 go
 
  create table Documentos_recibidos
 (
  Id_documento int not null identity(1,1) primary key,
  Destino nvarchar(255) not null,
  Usuario_recibe nvarchar(20) not null,
  Asunto nvarchar(500) not null, 
  Fecha_recibe datetime2 not null default SYSDATETIME(), 
  Fecha_envio datetime2,
  Fecha_esperada datetime2,
  Folio nvarchar(50),
  Requiere_respuesta bit,
  Imagen nvarchar(200),  
  Fecha_respuesta datetime2,
  Respuesta nvarchar(500),
  
  foreign key (Usuario_recibe)references Usuarios(Usuario)
  on update cascade on delete cascade
 )
 go
 
 create table Tipos_documento_interno
 (
  Id_tipo_documento_interno int not null identity(1,1) primary key,
  Tipo_documento nvarchar(150)
 )
 go
 
 create table Documentos_internos
 (
  Id_documento int not null identity(1,1) primary key,
  Descripcion nvarchar(2048) not null,
  Usuario_registra nvarchar(20),
  Id_tipo_documento int,
  Folio nvarchar(50),
  Imagen nvarchar(200),  
  
  
  foreign key (Id_tipo_documento) references Tipos_documento_interno(Id_tipo_documento_interno)
  on update cascade on delete cascade,
  foreign key (Usuario_registra)references Usuarios(Usuario)
  on update cascade on delete cascade
  
 )
 go
 
/* Inserta datos necesarios */
 insert into dbo.Tipo_solicitudes (Tipo_solicitud)values
 ('Electronico'),/*1*/
 ('Mobiliario'),/*2*/
 ('Consumible')/*3*/
 go
 
 
 /* Inserta datos de prueba */
 insert into dbo.Usuarios (Usuario, Pass, Nombre) values('admin@gmail.com','1111','Administrador'),
 ('demo@gmail.com','demo','Usuario demo')
 go