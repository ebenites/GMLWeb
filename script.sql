USE [master]
GO
/****** Object:  Database [gml]    Script Date: 22/02/2016 06:02:34 p.m. ******/
CREATE DATABASE [gml]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'gml', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\gml.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'gml_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\gml_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [gml] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [gml].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [gml] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [gml] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [gml] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [gml] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [gml] SET ARITHABORT OFF 
GO
ALTER DATABASE [gml] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [gml] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [gml] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [gml] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [gml] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [gml] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [gml] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [gml] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [gml] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [gml] SET  DISABLE_BROKER 
GO
ALTER DATABASE [gml] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [gml] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [gml] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [gml] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [gml] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [gml] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [gml] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [gml] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [gml] SET  MULTI_USER 
GO
ALTER DATABASE [gml] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [gml] SET DB_CHAINING OFF 
GO
ALTER DATABASE [gml] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [gml] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [gml] SET DELAYED_DURABILITY = DISABLED 
GO
USE [gml]
GO
/****** Object:  Table [dbo].[Cronograma]    Script Date: 22/02/2016 06:02:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cronograma](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[anio] [int] NOT NULL,
	[codigo_equipo] [int] NOT NULL,
 CONSTRAINT [PK_Cronograma] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CronogramaDetalle]    Script Date: 22/02/2016 06:02:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CronogramaDetalle](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[codigo_cronograma] [int] NOT NULL,
	[semana] [int] NOT NULL,
	[actividad] [varchar](max) NOT NULL,
	[asignada] [char](1) NULL,
 CONSTRAINT [PK_CronogramaDetalle_1] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Disponibilidad]    Script Date: 22/02/2016 06:02:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disponibilidad](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[codigo_tecnico] [int] NOT NULL,
	[anio] [int] NOT NULL,
	[semana] [int] NOT NULL,
 CONSTRAINT [PK_Disponibilidad] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Equipo]    Script Date: 22/02/2016 06:02:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Equipo](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[serie] [varchar](50) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](max) NULL,
	[fabricante] [varchar](50) NULL,
	[fecha_compra] [date] NULL,
	[fecha_produccion] [date] NULL,
	[codigo_local] [int] NOT NULL,
 CONSTRAINT [PK_Equipo] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Local]    Script Date: 22/02/2016 06:02:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Local](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[ubicacion] [varchar](max) NULL,
 CONSTRAINT [PK_Local] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrdenServicio]    Script Date: 22/02/2016 06:02:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrdenServicio](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[numero] [varchar](50) NOT NULL,
	[observacion] [varchar](max) NULL,
	[fecha] [datetime] NOT NULL CONSTRAINT [DF_OrdenServicio_fecha]  DEFAULT (getdate()),
	[estado] [char](1) NOT NULL CONSTRAINT [DF_OrdenServicio_estado]  DEFAULT ('P'),
	[codigo_equipo] [int] NOT NULL,
	[codigo_tecnico] [int] NOT NULL,
	[codigo_solicitud] [int] NULL,
 CONSTRAINT [PK_OrdenServicio] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlanMantenimiento]    Script Date: 22/02/2016 06:02:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanMantenimiento](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[anio] [int] NOT NULL,
	[codigo_local] [int] NOT NULL,
 CONSTRAINT [PK_PlanMantenimiento_1] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PlanMantenimientoDetalle]    Script Date: 22/02/2016 06:02:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanMantenimientoDetalle](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[codigo_planmantenimiento] [int] NOT NULL,
	[codigo_cronogramadetalle] [int] NOT NULL,
	[codigo_disponibilidad] [int] NOT NULL,
 CONSTRAINT [PK_PlanMantenimientoDetalle] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Solicitud]    Script Date: 22/02/2016 06:02:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Solicitud](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[numero] [varchar](50) NOT NULL,
	[descripcion] [varchar](max) NOT NULL,
	[solicitante] [varchar](100) NOT NULL,
	[codigo_equipo] [int] NOT NULL,
	[fecha] [date] NOT NULL CONSTRAINT [DF_Solicitud_fecha]  DEFAULT (getdate()),
	[estado] [char](1) NOT NULL CONSTRAINT [DF_Solicitud_estado]  DEFAULT ('P'),
 CONSTRAINT [PK_Solicitud] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tecnico]    Script Date: 22/02/2016 06:02:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tecnico](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[dni] [varchar](12) NOT NULL,
	[nombres] [varchar](100) NOT NULL,
	[apellidos] [varchar](100) NULL,
	[especialidad] [varchar](100) NULL,
	[tipo] [char](1) NOT NULL CONSTRAINT [DF_Tecnico_tipo]  DEFAULT ('I'),
 CONSTRAINT [PK_Tecnico] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Cronograma] ON 

INSERT [dbo].[Cronograma] ([codigo], [anio], [codigo_equipo]) VALUES (1, 2015, 1)
INSERT [dbo].[Cronograma] ([codigo], [anio], [codigo_equipo]) VALUES (2, 2015, 2)
INSERT [dbo].[Cronograma] ([codigo], [anio], [codigo_equipo]) VALUES (3, 2016, 1)
INSERT [dbo].[Cronograma] ([codigo], [anio], [codigo_equipo]) VALUES (4, 2016, 2)
INSERT [dbo].[Cronograma] ([codigo], [anio], [codigo_equipo]) VALUES (5, 2017, 1)
INSERT [dbo].[Cronograma] ([codigo], [anio], [codigo_equipo]) VALUES (6, 2017, 2)
INSERT [dbo].[Cronograma] ([codigo], [anio], [codigo_equipo]) VALUES (7, 2017, 3)
INSERT [dbo].[Cronograma] ([codigo], [anio], [codigo_equipo]) VALUES (8, 2017, 4)
SET IDENTITY_INSERT [dbo].[Cronograma] OFF
SET IDENTITY_INSERT [dbo].[CronogramaDetalle] ON 

INSERT [dbo].[CronogramaDetalle] ([codigo], [codigo_cronograma], [semana], [actividad], [asignada]) VALUES (2, 1, 10, N'Cable de eléctrico deteriorados en el hogar que provocan corto circuito y destrucción del equipo.', NULL)
INSERT [dbo].[CronogramaDetalle] ([codigo], [codigo_cronograma], [semana], [actividad], [asignada]) VALUES (3, 1, 20, N'Interruptor o tomacorriente en estado defectuoso provoca falsos contacto y uso excesivo del corriente.', NULL)
INSERT [dbo].[CronogramaDetalle] ([codigo], [codigo_cronograma], [semana], [actividad], [asignada]) VALUES (6, 2, 40, N'Colocar más de un equipo eléctrico en fuente de corrientes no actas para ese fin este nos pude conducir a sobrecargas de cable de línea en hogar.', NULL)
INSERT [dbo].[CronogramaDetalle] ([codigo], [codigo_cronograma], [semana], [actividad], [asignada]) VALUES (7, 2, 60, N'Equipos con componentes externos oxidados provocando el mismo efecto en piezas internas del mismo.', NULL)
INSERT [dbo].[CronogramaDetalle] ([codigo], [codigo_cronograma], [semana], [actividad], [asignada]) VALUES (1004, 5, 10, N'Cable de eléctrico deteriorados en el hogar que provocan corto circuito y destrucción del equipo.', NULL)
INSERT [dbo].[CronogramaDetalle] ([codigo], [codigo_cronograma], [semana], [actividad], [asignada]) VALUES (1005, 5, 20, N'Interruptor o tomacorriente en estado defectuoso provoca falsos contacto y uso excesivo del corriente.', NULL)
INSERT [dbo].[CronogramaDetalle] ([codigo], [codigo_cronograma], [semana], [actividad], [asignada]) VALUES (1006, 5, 30, N'Colocar más de un equipo eléctrico en fuente de corrientes no actas para ese fin este nos pude conducir a sobrecargas de cable de línea en hogar.', NULL)
INSERT [dbo].[CronogramaDetalle] ([codigo], [codigo_cronograma], [semana], [actividad], [asignada]) VALUES (1007, 5, 40, N'Equipos con componentes externos oxidados provocando el mismo efecto en piezas internas del mismo.', NULL)
INSERT [dbo].[CronogramaDetalle] ([codigo], [codigo_cronograma], [semana], [actividad], [asignada]) VALUES (1009, 6, 15, N'Interruptor o tomacorriente en estado defectuoso provoca falsos contacto y uso excesivo del corriente.', NULL)
INSERT [dbo].[CronogramaDetalle] ([codigo], [codigo_cronograma], [semana], [actividad], [asignada]) VALUES (1010, 6, 25, N'Equipos con componentes externos oxidados provocando el mismo efecto en piezas internas del mismo.', NULL)
INSERT [dbo].[CronogramaDetalle] ([codigo], [codigo_cronograma], [semana], [actividad], [asignada]) VALUES (1011, 7, 35, N'Cable de eléctrico deteriorados en el hogar que provocan corto circuito y destrucción del equipo.', NULL)
INSERT [dbo].[CronogramaDetalle] ([codigo], [codigo_cronograma], [semana], [actividad], [asignada]) VALUES (1012, 8, 17, N'Interruptor o tomacorriente en estado defectuoso provoca falsos contacto y uso excesivo del corriente.', NULL)
INSERT [dbo].[CronogramaDetalle] ([codigo], [codigo_cronograma], [semana], [actividad], [asignada]) VALUES (1013, 8, 32, N'Colocar más de un equipo eléctrico en fuente de corrientes no actas para ese fin este nos pude conducir a sobrecargas de cable de línea en hogar.', NULL)
SET IDENTITY_INSERT [dbo].[CronogramaDetalle] OFF
SET IDENTITY_INSERT [dbo].[Disponibilidad] ON 

INSERT [dbo].[Disponibilidad] ([codigo], [codigo_tecnico], [anio], [semana]) VALUES (1, 2, 2015, 10)
INSERT [dbo].[Disponibilidad] ([codigo], [codigo_tecnico], [anio], [semana]) VALUES (2, 2, 2015, 20)
INSERT [dbo].[Disponibilidad] ([codigo], [codigo_tecnico], [anio], [semana]) VALUES (3, 2, 2015, 40)
INSERT [dbo].[Disponibilidad] ([codigo], [codigo_tecnico], [anio], [semana]) VALUES (4, 2, 2015, 60)
SET IDENTITY_INSERT [dbo].[Disponibilidad] OFF
SET IDENTITY_INSERT [dbo].[Equipo] ON 

INSERT [dbo].[Equipo] ([codigo], [serie], [nombre], [descripcion], [fabricante], [fecha_compra], [fecha_produccion], [codigo_local]) VALUES (1, N' LB01-LG0123', N'Congeladora LG 2033', N'Congeladora LG 2033 doble cabina', N'LG', CAST(N'2011-12-22' AS Date), CAST(N'2012-02-15' AS Date), 1)
INSERT [dbo].[Equipo] ([codigo], [serie], [nombre], [descripcion], [fabricante], [fecha_compra], [fecha_produccion], [codigo_local]) VALUES (2, N'LI03-CX0522', N'Cocina Coldex 312', N'Cocina Industrial Coldex a gas', N'Coldex', CAST(N'2013-02-14' AS Date), CAST(N'2013-04-03' AS Date), 2)
INSERT [dbo].[Equipo] ([codigo], [serie], [nombre], [descripcion], [fabricante], [fecha_compra], [fecha_produccion], [codigo_local]) VALUES (3, N'LGMD-29129', N'LG Licuadora x10', N'Licuadora industrial LG x10', N'LG', CAST(N'2013-02-11' AS Date), CAST(N'2013-04-12' AS Date), 1)
INSERT [dbo].[Equipo] ([codigo], [serie], [nombre], [descripcion], [fabricante], [fecha_compra], [fecha_produccion], [codigo_local]) VALUES (4, N'LTMX-29172', N'Cocina Coldex R200', N'Cocina industrial Coldex 200', N'Coldex', CAST(N'2014-11-12' AS Date), CAST(N'2015-02-12' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Equipo] OFF
SET IDENTITY_INSERT [dbo].[Local] ON 

INSERT [dbo].[Local] ([codigo], [nombre], [ubicacion]) VALUES (1, N'Plaza Vea Ate', N'Av. Nicolas Ayllon Sector B. Mz B Lte.4 Zona A - Baja Esq. Con Av. La Mar.')
INSERT [dbo].[Local] ([codigo], [nombre], [ubicacion]) VALUES (2, N'Plaza Vea La Molina', N'Av. Raúl Ferrero 1205 Urb. Remanso II Etapa')
INSERT [dbo].[Local] ([codigo], [nombre], [ubicacion]) VALUES (3, N'Plaza Vea Risso
', N'Av. Arequipa No 2250.')
INSERT [dbo].[Local] ([codigo], [nombre], [ubicacion]) VALUES (4, N'Plaza Vea Primavera
', N'Av. Angamos Este 2337.')
INSERT [dbo].[Local] ([codigo], [nombre], [ubicacion]) VALUES (5, N'Plaza Vea Miraflores
', N'4651 Avenida Arequipa, Miraflores, Lima, Perú')
SET IDENTITY_INSERT [dbo].[Local] OFF
SET IDENTITY_INSERT [dbo].[OrdenServicio] ON 

INSERT [dbo].[OrdenServicio] ([codigo], [numero], [observacion], [fecha], [estado], [codigo_equipo], [codigo_tecnico], [codigo_solicitud]) VALUES (1, N'OS-2016-02-0001', NULL, CAST(N'2016-02-10 11:54:00.000' AS DateTime), N'P', 1, 1, 1)
INSERT [dbo].[OrdenServicio] ([codigo], [numero], [observacion], [fecha], [estado], [codigo_equipo], [codigo_tecnico], [codigo_solicitud]) VALUES (2, N'OS-2016-02-0002', NULL, CAST(N'2016-02-24 13:24:00.000' AS DateTime), N'P', 1, 2, 7)
SET IDENTITY_INSERT [dbo].[OrdenServicio] OFF
SET IDENTITY_INSERT [dbo].[PlanMantenimiento] ON 

INSERT [dbo].[PlanMantenimiento] ([codigo], [anio], [codigo_local]) VALUES (1, 2015, 1)
INSERT [dbo].[PlanMantenimiento] ([codigo], [anio], [codigo_local]) VALUES (2, 2014, 2)
INSERT [dbo].[PlanMantenimiento] ([codigo], [anio], [codigo_local]) VALUES (3, 2014, 1)
SET IDENTITY_INSERT [dbo].[PlanMantenimiento] OFF
SET IDENTITY_INSERT [dbo].[Solicitud] ON 

INSERT [dbo].[Solicitud] ([codigo], [numero], [descripcion], [solicitante], [codigo_equipo], [fecha], [estado]) VALUES (1, N'GMLSOL-2016002', N'El sistema de aire no enciende y hace dmasiado ruido.', N'David Rodriguez', 1, CAST(N'2016-02-01' AS Date), N'G')
INSERT [dbo].[Solicitud] ([codigo], [numero], [descripcion], [solicitante], [codigo_equipo], [fecha], [estado]) VALUES (3, N'GMLSOL-2016010', N'Problemas con el medidor de temperatura', N'Victor Muchica', 2, CAST(N'2016-01-02' AS Date), N'G')
INSERT [dbo].[Solicitud] ([codigo], [numero], [descripcion], [solicitante], [codigo_equipo], [fecha], [estado]) VALUES (4, N'GMLSOL-2016007', N'Problemas con la fuente de poder', N'Melissa Salazar', 1, CAST(N'2016-03-01' AS Date), N'P')
INSERT [dbo].[Solicitud] ([codigo], [numero], [descripcion], [solicitante], [codigo_equipo], [fecha], [estado]) VALUES (5, N'SOL-2016-02-0005', N'Problemas con el encendido del equipo', N'Luis Quispe', 1, CAST(N'2016-02-11' AS Date), N'P')
INSERT [dbo].[Solicitud] ([codigo], [numero], [descripcion], [solicitante], [codigo_equipo], [fecha], [estado]) VALUES (6, N'SOL-2016-02-0006', N'Problemas con el refrigerante', N'Javier Perez', 2, CAST(N'2016-02-11' AS Date), N'P')
INSERT [dbo].[Solicitud] ([codigo], [numero], [descripcion], [solicitante], [codigo_equipo], [fecha], [estado]) VALUES (7, N'SOL-2016-02-0007', N'No responde los controles de cambio', N'Miguel Stella', 1, CAST(N'2016-02-11' AS Date), N'G')
SET IDENTITY_INSERT [dbo].[Solicitud] OFF
SET IDENTITY_INSERT [dbo].[Tecnico] ON 

INSERT [dbo].[Tecnico] ([codigo], [dni], [nombres], [apellidos], [especialidad], [tipo]) VALUES (1, N'42586542', N'Jose', N'Vega Lujan', N'Electrónico', N'I')
INSERT [dbo].[Tecnico] ([codigo], [dni], [nombres], [apellidos], [especialidad], [tipo]) VALUES (2, N'35965425', N'Manuel', N'Valencia', N'ELectrecista', N'I')
INSERT [dbo].[Tecnico] ([codigo], [dni], [nombres], [apellidos], [especialidad], [tipo]) VALUES (1002, N'26589545', N'Jorge', N'Mancilla', N'Electrónico', N'I')
INSERT [dbo].[Tecnico] ([codigo], [dni], [nombres], [apellidos], [especialidad], [tipo]) VALUES (1003, N'45854596', N'Samuel', N'Benites', N'Electromecánico', N'I')
INSERT [dbo].[Tecnico] ([codigo], [dni], [nombres], [apellidos], [especialidad], [tipo]) VALUES (1004, N'45868545', N'Miguel', N'Sanchez', N'Electricista', N'I')
INSERT [dbo].[Tecnico] ([codigo], [dni], [nombres], [apellidos], [especialidad], [tipo]) VALUES (1005, N'59875624', N'Nolberto', N'Solis', N'Electromecánico', N'I')
INSERT [dbo].[Tecnico] ([codigo], [dni], [nombres], [apellidos], [especialidad], [tipo]) VALUES (1006, N'10235459652', N'Julian', N'Perez', N'Electrónico', N'E')
INSERT [dbo].[Tecnico] ([codigo], [dni], [nombres], [apellidos], [especialidad], [tipo]) VALUES (1007, N'10456854352', N'Contratista', N'Anónimo SAC', NULL, N'E')
SET IDENTITY_INSERT [dbo].[Tecnico] OFF
ALTER TABLE [dbo].[Cronograma]  WITH CHECK ADD  CONSTRAINT [FK_Cronograma_Equipo] FOREIGN KEY([codigo_equipo])
REFERENCES [dbo].[Equipo] ([codigo])
GO
ALTER TABLE [dbo].[Cronograma] CHECK CONSTRAINT [FK_Cronograma_Equipo]
GO
ALTER TABLE [dbo].[CronogramaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_CronogramaDetalle_Cronograma] FOREIGN KEY([codigo_cronograma])
REFERENCES [dbo].[Cronograma] ([codigo])
GO
ALTER TABLE [dbo].[CronogramaDetalle] CHECK CONSTRAINT [FK_CronogramaDetalle_Cronograma]
GO
ALTER TABLE [dbo].[Disponibilidad]  WITH CHECK ADD  CONSTRAINT [FK_Disponibilidad_Tecnico] FOREIGN KEY([codigo_tecnico])
REFERENCES [dbo].[Tecnico] ([codigo])
GO
ALTER TABLE [dbo].[Disponibilidad] CHECK CONSTRAINT [FK_Disponibilidad_Tecnico]
GO
ALTER TABLE [dbo].[Equipo]  WITH CHECK ADD  CONSTRAINT [FK_Equipo_Local] FOREIGN KEY([codigo_local])
REFERENCES [dbo].[Local] ([codigo])
GO
ALTER TABLE [dbo].[Equipo] CHECK CONSTRAINT [FK_Equipo_Local]
GO
ALTER TABLE [dbo].[OrdenServicio]  WITH CHECK ADD  CONSTRAINT [FK_OrdenServicio_Equipo] FOREIGN KEY([codigo_equipo])
REFERENCES [dbo].[Equipo] ([codigo])
GO
ALTER TABLE [dbo].[OrdenServicio] CHECK CONSTRAINT [FK_OrdenServicio_Equipo]
GO
ALTER TABLE [dbo].[OrdenServicio]  WITH CHECK ADD  CONSTRAINT [FK_OrdenServicio_Solicitud] FOREIGN KEY([codigo_solicitud])
REFERENCES [dbo].[Solicitud] ([codigo])
GO
ALTER TABLE [dbo].[OrdenServicio] CHECK CONSTRAINT [FK_OrdenServicio_Solicitud]
GO
ALTER TABLE [dbo].[OrdenServicio]  WITH CHECK ADD  CONSTRAINT [FK_OrdenServicio_Tecnico] FOREIGN KEY([codigo_tecnico])
REFERENCES [dbo].[Tecnico] ([codigo])
GO
ALTER TABLE [dbo].[OrdenServicio] CHECK CONSTRAINT [FK_OrdenServicio_Tecnico]
GO
ALTER TABLE [dbo].[PlanMantenimiento]  WITH CHECK ADD  CONSTRAINT [FK_PlanMantenimiento_Local1] FOREIGN KEY([codigo_local])
REFERENCES [dbo].[Local] ([codigo])
GO
ALTER TABLE [dbo].[PlanMantenimiento] CHECK CONSTRAINT [FK_PlanMantenimiento_Local1]
GO
ALTER TABLE [dbo].[PlanMantenimientoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_PlanMantenimientoDetalle_CronogramaDetalle] FOREIGN KEY([codigo_cronogramadetalle])
REFERENCES [dbo].[CronogramaDetalle] ([codigo])
GO
ALTER TABLE [dbo].[PlanMantenimientoDetalle] CHECK CONSTRAINT [FK_PlanMantenimientoDetalle_CronogramaDetalle]
GO
ALTER TABLE [dbo].[PlanMantenimientoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_PlanMantenimientoDetalle_Disponibilidad] FOREIGN KEY([codigo_disponibilidad])
REFERENCES [dbo].[Disponibilidad] ([codigo])
GO
ALTER TABLE [dbo].[PlanMantenimientoDetalle] CHECK CONSTRAINT [FK_PlanMantenimientoDetalle_Disponibilidad]
GO
ALTER TABLE [dbo].[PlanMantenimientoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_PlanMantenimientoDetalle_PlanMantenimiento] FOREIGN KEY([codigo_planmantenimiento])
REFERENCES [dbo].[PlanMantenimiento] ([codigo])
GO
ALTER TABLE [dbo].[PlanMantenimientoDetalle] CHECK CONSTRAINT [FK_PlanMantenimientoDetalle_PlanMantenimiento]
GO
ALTER TABLE [dbo].[Solicitud]  WITH CHECK ADD  CONSTRAINT [FK_Solicitud_Equipo] FOREIGN KEY([codigo_equipo])
REFERENCES [dbo].[Equipo] ([codigo])
GO
ALTER TABLE [dbo].[Solicitud] CHECK CONSTRAINT [FK_Solicitud_Equipo]
GO
USE [master]
GO
ALTER DATABASE [gml] SET  READ_WRITE 
GO
