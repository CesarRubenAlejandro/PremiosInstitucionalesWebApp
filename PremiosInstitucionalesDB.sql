USE [wPremiosInstitucionalesdb]
GO
/****** Object:  Table [dbo].[PI_BA_Aplicacion]    Script Date: 08/08/2017 12:48:47 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_BA_Aplicacion](
	[cveAplicacion] [varchar](50) NOT NULL,
	[Status] [varchar](20) NULL,
	[cveCandidato] [varchar](50) NULL,
	[cveCategoria] [varchar](50) NULL,
	[NombreArchivo] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Aplicacion] PRIMARY KEY CLUSTERED 
(
	[cveAplicacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PI_BA_Candidato]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_BA_Candidato](
	[cveCandidato] [varchar](50) NOT NULL,
	[Password] [varchar](100) NULL,
	[Nombre] [varchar](100) NULL,
	[Apellido] [varchar](100) NULL,
	[Confirmado] [bit] NULL,
	[Correo] [varchar](100) NULL,
	[CodigoConfirmacion] [varchar](50) NULL,
	[Telefono] [varchar](50) NULL,
	[Nacionalidad] [varchar](50) NULL,
	[RFC] [varchar](50) NULL,
	[Direccion] [varchar](50) NULL,
	[NombreImagen] [varchar](50) NULL,
	[FechaPrivacidadDatos] [date] NULL,
 CONSTRAINT [PK_PI_BA_Candidato] PRIMARY KEY CLUSTERED 
(
	[cveCandidato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PI_BA_Categoria]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_BA_Categoria](
	[cveCategoria] [varchar](50) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[cveConvocatoria] [varchar](50) NULL,
	[cveAplicacionGanadora] [varchar](50) NULL,
	[FechaCreacion] [date] NULL,
	[UsuarioCreacion] [varchar](50) NULL,
	[FechaEdicion] [date] NULL,
	[UsuarioEdicion] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Categoria] PRIMARY KEY CLUSTERED 
(
	[cveCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PI_BA_Convocatoria]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_BA_Convocatoria](
	[cveConvocatoria] [varchar](50) NOT NULL,
	[FechaInicio] [date] NULL,
	[FechaFin] [date] NULL,
	[cvePremio] [varchar](50) NULL,
	[TituloConvocatoria] [varchar](100) NULL,
	[FechaVeredicto] [date] NULL,
	[FechaCreacion] [date] NULL,
	[UsuarioCreacion] [varchar](50) NULL,
	[FechaEdicion] [date] NULL,
	[UsuarioEdicion] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Convocatoria] PRIMARY KEY CLUSTERED 
(
	[cveConvocatoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PI_BA_Evaluacion]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_BA_Evaluacion](
	[cveEvaluacion] [varchar](50) NOT NULL,
	[Calificacion] [smallint] NULL,
	[cveAplicacion] [varchar](50) NULL,
	[cveJuez] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Evaluacion] PRIMARY KEY CLUSTERED 
(
	[cveEvaluacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PI_BA_Forma]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_BA_Forma](
	[cveForma] [varchar](50) NOT NULL,
	[cveCategoria] [varchar](50) NULL,
	[FechaCreacion] [date] NULL,
	[UsuarioCreacion] [varchar](50) NULL,
	[FechaEdicion] [date] NULL,
	[UsuarioEdicion] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Forma] PRIMARY KEY CLUSTERED 
(
	[cveForma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PI_BA_Juez]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_BA_Juez](
	[cveJuez] [varchar](50) NOT NULL,
	[Password] [varchar](100) NULL,
	[Nombre] [varchar](100) NULL,
	[Apellido] [varchar](100) NULL,
	[Correo] [varchar](100) NULL,
	[NombreImagen] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Juez] PRIMARY KEY CLUSTERED 
(
	[cveJuez] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PI_BA_JuezPorCategoria]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_BA_JuezPorCategoria](
	[cveJuezPorCategoria] [varchar](50) NOT NULL,
	[cveJuez] [varchar](50) NOT NULL,
	[cveCategoria] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PI_BA_JuezPorCategoria] PRIMARY KEY CLUSTERED 
(
	[cveJuezPorCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PI_BA_Pregunta]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_BA_Pregunta](
	[cvePregunta] [varchar](50) NOT NULL,
	[Texto] [varchar](100) NULL,
	[Orden] [int] NULL,
 CONSTRAINT [PK_PI_BA_Pregunta] PRIMARY KEY CLUSTERED 
(
	[cvePregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PI_BA_PreguntasPorForma]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_BA_PreguntasPorForma](
	[cvePreguntaPorForma] [varchar](50) NOT NULL,
	[cveForma] [varchar](50) NOT NULL,
	[cvePregunta] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PI_BA_PreguntasPorForma] PRIMARY KEY CLUSTERED 
(
	[cvePreguntaPorForma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PI_BA_Premio]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_BA_Premio](
	[cvePremio] [varchar](50) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[NombreImagen] [varchar](50) NULL,
	[Descripcion] [varchar](250) NULL,
	[FechaCreacion] [date] NULL,
	[UsuarioCreacion] [varchar](50) NULL,
	[FechaEdicion] [date] NULL,
	[UsuarioEdicion] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Premio] PRIMARY KEY CLUSTERED 
(
	[cvePremio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PI_BA_Respuesta]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_BA_Respuesta](
	[cveRespuesta] [varchar](50) NOT NULL,
	[Valor] [varchar](500) NULL,
	[cvePregunta] [varchar](50) NULL,
	[cveAplicacion] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Respuesta] PRIMARY KEY CLUSTERED 
(
	[cveRespuesta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PI_SE_Administrador]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_SE_Administrador](
	[cveAdministrador] [varchar](50) NOT NULL,
	[Password] [varchar](100) NULL,
	[Correo] [varchar](100) NULL,
 CONSTRAINT [PK_PI_SE_Administrador] PRIMARY KEY CLUSTERED 
(
	[cveAdministrador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PI_SE_Configuracion]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PI_SE_Configuracion](
	[cveConfiguracion] [varchar](50) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Host] [varchar](100) NOT NULL,
	[Puerto] [smallint] NOT NULL,
 CONSTRAINT [PK_PI_SE_Configuracion] PRIMARY KEY CLUSTERED 
(
	[cveConfiguracion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PI_BA_Aplicacion]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_Aplicacion_PI_BA_Candidato] FOREIGN KEY([cveCandidato])
REFERENCES [dbo].[PI_BA_Candidato] ([cveCandidato])
GO
ALTER TABLE [dbo].[PI_BA_Aplicacion] CHECK CONSTRAINT [FK_PI_BA_Aplicacion_PI_BA_Candidato]
GO
ALTER TABLE [dbo].[PI_BA_Aplicacion]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_Aplicacion_PI_BA_Categoria] FOREIGN KEY([cveCategoria])
REFERENCES [dbo].[PI_BA_Categoria] ([cveCategoria])
GO
ALTER TABLE [dbo].[PI_BA_Aplicacion] CHECK CONSTRAINT [FK_PI_BA_Aplicacion_PI_BA_Categoria]
GO
ALTER TABLE [dbo].[PI_BA_Categoria]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_Categoria_PI_BA_Aplicacion] FOREIGN KEY([cveAplicacionGanadora])
REFERENCES [dbo].[PI_BA_Aplicacion] ([cveAplicacion])
GO
ALTER TABLE [dbo].[PI_BA_Categoria] CHECK CONSTRAINT [FK_PI_BA_Categoria_PI_BA_Aplicacion]
GO
ALTER TABLE [dbo].[PI_BA_Categoria]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_Categoria_PI_BA_Convocatoria] FOREIGN KEY([cveConvocatoria])
REFERENCES [dbo].[PI_BA_Convocatoria] ([cveConvocatoria])
GO
ALTER TABLE [dbo].[PI_BA_Categoria] CHECK CONSTRAINT [FK_PI_BA_Categoria_PI_BA_Convocatoria]
GO
ALTER TABLE [dbo].[PI_BA_Convocatoria]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_Convocatoria_PI_BA_Premio] FOREIGN KEY([cvePremio])
REFERENCES [dbo].[PI_BA_Premio] ([cvePremio])
GO
ALTER TABLE [dbo].[PI_BA_Convocatoria] CHECK CONSTRAINT [FK_PI_BA_Convocatoria_PI_BA_Premio]
GO
ALTER TABLE [dbo].[PI_BA_Evaluacion]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_Evaluacion_PI_BA_Aplicacion] FOREIGN KEY([cveAplicacion])
REFERENCES [dbo].[PI_BA_Aplicacion] ([cveAplicacion])
GO
ALTER TABLE [dbo].[PI_BA_Evaluacion] CHECK CONSTRAINT [FK_PI_BA_Evaluacion_PI_BA_Aplicacion]
GO
ALTER TABLE [dbo].[PI_BA_Evaluacion]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_Evaluacion_PI_BA_Juez] FOREIGN KEY([cveJuez])
REFERENCES [dbo].[PI_BA_Juez] ([cveJuez])
GO
ALTER TABLE [dbo].[PI_BA_Evaluacion] CHECK CONSTRAINT [FK_PI_BA_Evaluacion_PI_BA_Juez]
GO
ALTER TABLE [dbo].[PI_BA_Forma]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_Forma_PI_BA_Categoria] FOREIGN KEY([cveCategoria])
REFERENCES [dbo].[PI_BA_Categoria] ([cveCategoria])
GO
ALTER TABLE [dbo].[PI_BA_Forma] CHECK CONSTRAINT [FK_PI_BA_Forma_PI_BA_Categoria]
GO
ALTER TABLE [dbo].[PI_BA_JuezPorCategoria]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_JuezPorCategoria_PI_BA_Categoria] FOREIGN KEY([cveCategoria])
REFERENCES [dbo].[PI_BA_Categoria] ([cveCategoria])
GO
ALTER TABLE [dbo].[PI_BA_JuezPorCategoria] CHECK CONSTRAINT [FK_PI_BA_JuezPorCategoria_PI_BA_Categoria]
GO
ALTER TABLE [dbo].[PI_BA_JuezPorCategoria]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_JuezPorCategoria_PI_BA_Juez] FOREIGN KEY([cveJuez])
REFERENCES [dbo].[PI_BA_Juez] ([cveJuez])
GO
ALTER TABLE [dbo].[PI_BA_JuezPorCategoria] CHECK CONSTRAINT [FK_PI_BA_JuezPorCategoria_PI_BA_Juez]
GO
ALTER TABLE [dbo].[PI_BA_PreguntasPorForma]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_PreguntasPorForma_PI_BA_Forma] FOREIGN KEY([cveForma])
REFERENCES [dbo].[PI_BA_Forma] ([cveForma])
GO
ALTER TABLE [dbo].[PI_BA_PreguntasPorForma] CHECK CONSTRAINT [FK_PI_BA_PreguntasPorForma_PI_BA_Forma]
GO
ALTER TABLE [dbo].[PI_BA_PreguntasPorForma]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_PreguntasPorForma_PI_BA_Pregunta] FOREIGN KEY([cvePregunta])
REFERENCES [dbo].[PI_BA_Pregunta] ([cvePregunta])
GO
ALTER TABLE [dbo].[PI_BA_PreguntasPorForma] CHECK CONSTRAINT [FK_PI_BA_PreguntasPorForma_PI_BA_Pregunta]
GO
ALTER TABLE [dbo].[PI_BA_Respuesta]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_Respuesta_PI_BA_Aplicacion] FOREIGN KEY([cveAplicacion])
REFERENCES [dbo].[PI_BA_Aplicacion] ([cveAplicacion])
GO
ALTER TABLE [dbo].[PI_BA_Respuesta] CHECK CONSTRAINT [FK_PI_BA_Respuesta_PI_BA_Aplicacion]
GO
ALTER TABLE [dbo].[PI_BA_Respuesta]  WITH NOCHECK ADD  CONSTRAINT [FK_PI_BA_Respuesta_PI_BA_Pregunta] FOREIGN KEY([cvePregunta])
REFERENCES [dbo].[PI_BA_Pregunta] ([cvePregunta])
GO
ALTER TABLE [dbo].[PI_BA_Respuesta] CHECK CONSTRAINT [FK_PI_BA_Respuesta_PI_BA_Pregunta]
GO
/****** Object:  StoredProcedure [dbo].[AddCandidato]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[AddCandidato]
	@cveCandidato			varchar(50)		= '',
	@Password				varchar(100)	= '',
	@Nombre					varchar(100)	= '',
	@Apellido				varchar(100)	= '',
	@Confirmado				bit				= null,
	@Correo					varchar(100)	= '',
	@CodigoConfirmacion		varchar(50)		= '',
	@Telefono				varchar(50)		= '',
	@Nacionalidad			varchar(50)		= '',
	@RFC					varchar(50)		= '',
	@Direccion				varchar(50)		= '',
	@NombreImagen			varchar(50)		= '',
	@FechaPrivacidadDatos	date			= null

AS
BEGIN
	INSERT INTO PI_BA_Candidato(cveCandidato,
								Password,
								Nombre,
								Apellido,
								Confirmado,
								Correo,
								CodigoConfirmacion,
								Telefono,
								Nacionalidad,
								RFC,
								Direccion,
								NombreImagen,
								FechaPrivacidadDatos)
	VALUES (@cveCandidato,
			@Password,
			@Nombre,
			@Apellido,
			@Confirmado,
			@Correo,
			@CodigoConfirmacion,
			@Telefono,
			@Nacionalidad,
			@RFC,
			@Direccion,
			@NombreImagen,
			@FechaPrivacidadDatos)
END

GO
/****** Object:  StoredProcedure [dbo].[AddCategoria]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddCategoria]
	@cveCategoria			varchar(50)		= '',
	@Nombre					varchar(100)	= '',
	@cveConvocatoria		varchar(50)		= '',
	@cveAplicacionGanadora	varchar(50)	= '',
	@FechaCreacion			date			= '',
	@UsuarioCreacion		varchar(50)		= '',
	@FechaEdicion			date			= '',
	@UsuarioEdicion			varchar(50)		= ''
AS
BEGIN
	INSERT INTO PI_BA_Categoria(cveCategoria, Nombre, cveConvocatoria, cveAplicacionGanadora, FechaCreacion, UsuarioCreacion, FechaEdicion, UsuarioEdicion)
	VALUES (@cveCategoria, @Nombre, @cveConvocatoria, @cveAplicacionGanadora, @FechaCreacion, @UsuarioCreacion, @FechaEdicion, @UsuarioEdicion)
END


GO
/****** Object:  StoredProcedure [dbo].[AddConvocatoria]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddConvocatoria]
	@cveConvocatoria		varchar(50)		= '',
	@FechaInicio			date			= '',
	@FechaFin				date			= '',
	@cvePremio				varchar(50)		= '',
	@TituloConvocatoria		varchar(100)	= '',
	@FechaVeredicto			date			= '',
	@FechaCreacion			date			= '',
	@UsuarioCreacion		varchar(50)		= '',
	@FechaEdicion			date			= '',
	@UsuarioEdicion			varchar(50)		= ''
AS
BEGIN
	INSERT INTO PI_BA_Convocatoria(cveConvocatoria, FechaInicio, FechaFin, cvePremio, TituloConvocatoria, FechaVeredicto, FechaCreacion,
	UsuarioCreacion, FechaEdicion, UsuarioEdicion)
	VALUES (@cveConvocatoria, @FechaInicio, @FechaFin, @cvePremio, @TituloConvocatoria, @FechaVeredicto, @FechaCreacion,
	@UsuarioCreacion, @FechaEdicion, @UsuarioEdicion)
END


GO
/****** Object:  StoredProcedure [dbo].[AddEvaluacion]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddEvaluacion]
	@cveEvaluacion		varchar(50)			= '',
	@Calificacion		smallint			= 0,
	@cveAplicacion		varchar(100)		= '',
	@cveJuez			varchar(100)		= ''
AS
BEGIN
	INSERT INTO PI_BA_Evaluacion(cveEvaluacion, Calificacion, cveAplicacion, cveJuez)
	VALUES (@cveEvaluacion, @Calificacion, @cveAplicacion, @cveJuez)
END


GO
/****** Object:  StoredProcedure [dbo].[AddForma]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddForma]
	@cveForma			varchar(50)		= '',
	@cveCategoria		varchar(50)		= '',
	@FechaCreacion		date			= '',
	@UsuarioCreacion	varchar(50)		= '',
	@FechaEdicion		date			= '',
	@UsuarioEdicion		varchar(50)		= ''
AS
BEGIN
	INSERT INTO PI_BA_Forma(cveForma, cveCategoria, FechaCreacion, UsuarioCreacion, FechaEdicion, UsuarioEdicion)
	VALUES (@cveForma, @cveCategoria, @FechaCreacion, @UsuarioCreacion, @FechaEdicion, @UsuarioEdicion)
END


GO
/****** Object:  StoredProcedure [dbo].[AddJuez]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[AddJuez]
	@cveJuez		varchar(50)			= '',
	@Password		varchar(100)		= '',
	@Nombre			varchar(100)		= '',
	@Apellido		varchar(100)		= '',
	@Correo			varchar(100)		= '',
	@NombreImagen	varchar(50)			= ''

AS
BEGIN
	INSERT INTO PI_BA_Juez (cveJuez, Password, Nombre, Apellido, Correo, NombreImagen)
	VALUES (@cveJuez, @Password, @Nombre, @Apellido, @Correo, @NombreImagen)
END


GO
/****** Object:  StoredProcedure [dbo].[AddPremio]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddPremio]
	@cvePremio			varchar(50)		= '',
	@Nombre				varchar(100)	= '',
	@NombreImagen		varchar(50)		= '',
	@Descripcion		varchar(250)	= '',
	@FechaCreacion		date			= '',
	@UsuarioCreacion	varchar(50)		= '',
	@FechaEdicion		date			= '',
	@UsuarioEdicion		varchar(50)		= ''
AS
BEGIN
	INSERT INTO PI_BA_Premio(cvePremio, Nombre, NombreImagen, Descripcion, FechaCreacion, UsuarioCreacion, FechaEdicion, UsuarioEdicion)
	VALUES (@cvePremio, @Nombre, @NombreImagen, @Descripcion, @FechaCreacion, @UsuarioCreacion, @FechaEdicion, @UsuarioEdicion)
END


GO
/****** Object:  StoredProcedure [dbo].[ConfirmarCandidato]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ConfirmarCandidato] 
	-- Add the parameters for the stored procedure here
	@CodigoConfirmacion		varchar(50)		= ''
AS
BEGIN
	UPDATE PI_BA_Candidato
	SET Confirmado = 1
	WHERE CodigoConfirmacion = @CodigoConfirmacion
END

GO
/****** Object:  StoredProcedure [dbo].[GetAdministrador]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[GetAdministrador]
	@Correo varchar(100)					= '',
	@cveAdministrador varchar(50)			= ''
AS
BEGIN
	-- GetAdministradorByMail
	if @Correo != '' begin
			select cveAdministrador[cveAdministrador], Password[Password], Correo[Correo]
			from PI_SE_Administrador
			where Correo = @Correo
	end

	-- GetAdministradoByID
	if @cveAdministrador != '' begin
			select cveAdministrador[cveAdministrador], Password[Password], Correo[Correo]
			from PI_SE_Administrador
			where cveAdministrador = @cveAdministrador
	end

	-- GetAdministradores
	begin
			select cveAdministrador[cveAdministrador], Password[Password], Correo[Correo]
			from PI_SE_Administrador
	end
END


GO
/****** Object:  StoredProcedure [dbo].[GetCandidato]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[GetCandidato]
	@Correo varchar(100)				= '',
	@cveCandidato varchar(50)			= ''
AS
BEGIN
	-- GetCandidatoByMail
	if @Correo != '' begin
			select cveCandidato[cveCandidato], Password[Password], Nombre[Nombre], Apellido[Apellido], Confirmado[Confirmado], Correo[Correo],
			CodigoConfirmacion[CodigoConfirmacion], Telefono[Telefono], Nacionalidad[Nacionalidad], RFC[RFC], Direccion[Direccion],
			NombreImagen[NombreImagen], FechaPrivacidadDatos[FechaPrivacidadDatos]
			from PI_BA_Candidato
			where Correo = @Correo
	end

	-- GetCandidatoByID
	if @cveCandidato != '' begin
			select cveCandidato[cveCandidato], Password[Password], Nombre[Nombre], Apellido[Apellido], Confirmado[Confirmado], Correo[Correo],
			CodigoConfirmacion[CodigoConfirmacion], Telefono[Telefono], Nacionalidad[Nacionalidad], RFC[RFC], Direccion[Direccion],
			NombreImagen[NombreImagen], FechaPrivacidadDatos[FechaPrivacidadDatos]
			from PI_BA_Candidato
			where cveCandidato = @cveCandidato
	end

	-- GetCandidatos
	begin
			select cveCandidato[cveCandidato], Password[Password], Nombre[Nombre], Apellido[Apellido], Confirmado[Confirmado], Correo[Correo],
			CodigoConfirmacion[CodigoConfirmacion], Telefono[Telefono], Nacionalidad[Nacionalidad], RFC[RFC], Direccion[Direccion],
			NombreImagen[NombreImagen], FechaPrivacidadDatos[FechaPrivacidadDatos]
			from PI_BA_Candidato
	end
END


GO
/****** Object:  StoredProcedure [dbo].[GetCategoria]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCategoria]
	@cveCategoria varchar(50)		= '',
	@cveConvocatoria varchar(50)		= ''

AS
BEGIN
	-- GetCategoriaById
	if @cveCategoria != '' begin
			select	cveCategoria[cveCategoria],
					Nombre[Nombre], 
					cveConvocatoria[cveConvocatoria], 
					cveAplicacionGanadora[cveAplicacionGanadora], 
					FechaCreacion[FechaCreacion], 
					UsuarioCreacion[UsuarioCreacion], 
					FechaEdicion[FechaEdicion], 
					UsuarioEdicion[UsuarioEdicion]
			from PI_BA_Categoria
			where cveCategoria = @cveCategoria
	end

	-- GetCategoriaByIdConvocatoria
	if @cveConvocatoria != '' begin
			select	cveCategoria[cveCategoria],
					Nombre[Nombre], 
					cveConvocatoria[cveConvocatoria], 
					cveAplicacionGanadora[cveAplicacionGanadora], 
					FechaCreacion[FechaCreacion], 
					UsuarioCreacion[UsuarioCreacion], 
					FechaEdicion[FechaEdicion], 
					UsuarioEdicion[UsuarioEdicion]
			from PI_BA_Categoria
			where cveConvocatoria = @cveConvocatoria
			order by Nombre desc
	end

	-- GetAllCategorias
	begin
			select	cveCategoria[cveCategoria],
					Nombre[Nombre], 
					cveConvocatoria[cveConvocatoria], 
					cveAplicacionGanadora[cveAplicacionGanadora], 
					FechaCreacion[FechaCreacion], 
					UsuarioCreacion[UsuarioCreacion], 
					FechaEdicion[FechaEdicion], 
					UsuarioEdicion[UsuarioEdicion]
			from PI_BA_Categoria
	end
END

GO
/****** Object:  StoredProcedure [dbo].[GetCategoriaByIdJuez]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GetCategoriaByIdJuez]
	@cveJuez varchar(50)			= ''
AS
BEGIN
	select  PI_BA_Categoria.cveCategoria[cveCategoria],
			Nombre[Nombre],
			cveConvocatoria[cveConvocatoria],
			cveAplicacionGanadora[cveAplicacionGanadora],
			FechaCreacion[FechaCreacion],
			UsuarioCreacion[UsuarioCreacion],
			FechaEdicion[FechaEdicion],
			UsuarioEdicion[UsuarioEdicion]
	from PI_BA_Categoria
	INNER JOIN PI_BA_JuezPorCategoria
	ON PI_BA_Categoria.cveCategoria = PI_BA_JuezPorCategoria.cveCategoria
	where PI_BA_JuezPorCategoria.cveJuez = @cveJuez
END


GO
/****** Object:  StoredProcedure [dbo].[GetCategoriasPendientes]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCategoriasPendientes]

AS
BEGIN
	select	cveCategoria[cveCategoria],
			Nombre[Nombre], 
			cveConvocatoria[cveConvocatoria], 
			cveAplicacionGanadora[cveAplicacionGanadora], 
			FechaCreacion[FechaCreacion], 
			UsuarioCreacion[UsuarioCreacion], 
			FechaEdicion[FechaEdicion], 
			UsuarioEdicion[UsuarioEdicion]
	from PI_BA_Categoria
	where cveAplicacionGanadora IS NULL;
END

GO
/****** Object:  StoredProcedure [dbo].[GetConfiguracion]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetConfiguracion]
	@cveConfiguracion varchar(50)			= ''
AS
BEGIN
	-- GetConfiguracionById
	if @cveConfiguracion != '' begin
		select cveConfiguracion[cveConfiguracion], Correo[Correo], Password[Password], Host[Host], Puerto[Puerto]
		from PI_SE_Configuracion
		where cveConfiguracion = @cveConfiguracion
	end
	-- GetAllConfiguraciones
	begin
		select cveConfiguracion[cveConfiguracion], Correo[Correo], Password[Password], Host[Host], Puerto[Puerto]
		from PI_SE_Configuracion
	end
END

GO
/****** Object:  StoredProcedure [dbo].[GetConvocatoria]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetConvocatoria]
	@cveConvocatoria	varchar(50)	= ''
AS
BEGIN
	-- GetConvocatoriaById
	if @cveConvocatoria != '' begin
		select	cveConvocatoria[cveConvocatoria],
				FechaInicio[FechaInicio],
				FechaFin[FechaFin],
				cvePremio[cvePremio],
				TituloConvocatoria[TituloConvocatoria],
				FechaVeredicto[FechaVeredicto],
				FechaCreacion[FechaCreacion],
				UsuarioCreacion[UsuarioCreacion],
				FechaEdicion[FechaEdicion],
				UsuarioEdicion[UsuarioEdicion]
		from PI_BA_Convocatoria
		where cveConvocatoria = @cveConvocatoria
	end

	-- GetAllCategorias
	begin
		select	cveConvocatoria[cveConvocatoria],
				FechaInicio[FechaInicio],
				FechaFin[FechaFin],
				cvePremio[cvePremio],
				TituloConvocatoria[TituloConvocatoria],
				FechaVeredicto[FechaVeredicto],
				FechaCreacion[FechaCreacion],
				UsuarioCreacion[UsuarioCreacion],
				FechaEdicion[FechaEdicion],
				UsuarioEdicion[UsuarioEdicion]
		from PI_BA_Convocatoria
	end
END


GO
/****** Object:  StoredProcedure [dbo].[GetEvaluacion]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEvaluacion]
	@cveEvaluacion varchar(50)		= '',
	@cveAplicacion varchar(50)		= '',
	@cveJuez varchar(50)			= ''

AS
BEGIN
	-- GetEvaluacionByEvaluacionId
	if @cveEvaluacion != '' begin
			select cveEvaluacion[cveEvaluacion], Calificacion[Calificacion], cveAplicacion[cveAplicacion], cveJuez[cveJuez]
			from PI_BA_Evaluacion
			where cveEvaluacion = @cveEvaluacion
	end

	-- GetEvaluacionByAplicacionAndJuez
	if @cveAplicacion != '' AND @cveJuez != '' begin
			select cveEvaluacion[cveEvaluacion], Calificacion[Calificacion], cveAplicacion[cveAplicacion], cveJuez[cveJuez]
			from PI_BA_Evaluacion
			where cveJuez = @cveJuez AND cveAplicacion = @cveAplicacion
	end

	-- GetEvaluacionesByAplicacion
	if @cveAplicacion != '' begin
			select cveEvaluacion[cveEvaluacion], Calificacion[Calificacion], cveAplicacion[cveAplicacion], cveJuez[cveJuez]
			from PI_BA_Evaluacion
			where cveAplicacion = @cveAplicacion
	end

	-- GetAllEvaluaciones
	begin
			select cveEvaluacion[cveEvaluacion], Calificacion[Calificacion], cveAplicacion[cveAplicacion], cveJuez[cveJuez]
			from PI_BA_Evaluacion
	end
END


GO
/****** Object:  StoredProcedure [dbo].[GetForma]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetForma]
	@cveForma varchar(50)		= ''

AS
BEGIN
	-- GetFormaById
	if @cveForma != '' begin
		select	cveForma[cveForma],
				cveCategoria[cveCategoria], 
				FechaCreacion[FechaCreacion], 
				UsuarioCreacion[UsuarioCreacion], 
				FechaEdicion[FechaEdicion], 
				UsuarioEdicion[UsuarioEdicion]
		from PI_BA_Forma
		where cveForma = @cveForma
	end

	-- GetAllFormas
	begin
		select	cveForma[cveForma],
				cveCategoria[cveCategoria], 
				FechaCreacion[FechaCreacion], 
				UsuarioCreacion[UsuarioCreacion], 
				FechaEdicion[FechaEdicion], 
				UsuarioEdicion[UsuarioEdicion]
		from PI_BA_Forma
	end
END

GO
/****** Object:  StoredProcedure [dbo].[GetJuez]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[GetJuez]
	@Correo varchar(100)			= '',
	@cveJuez varchar(50)			= ''
AS
BEGIN
	-- GetJuezByMail
	if @Correo != '' begin
			select cveJuez[cveJuez], Password[Password], Nombre[Nombre], Apellido[Apellido], Correo[Correo], NombreImagen[NombreImagen]
			from PI_BA_Juez
			where Correo = @Correo
	end

	-- GetJuezByID
	if @cveJuez != '' begin
			select cveJuez[cveJuez], Password[Password], Nombre[Nombre], Apellido[Apellido], Correo[Correo], NombreImagen[NombreImagen]
			from PI_BA_Juez
			where cveJuez = @cveJuez
	end

	-- GetJueces
	begin
			select cveJuez[cveJuez], Password[Password], Nombre[Nombre], Apellido[Apellido], Correo[Correo], NombreImagen[NombreImagen]
			from PI_BA_Juez
	end
END


GO
/****** Object:  StoredProcedure [dbo].[GetMostRecentConvocatoria]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMostRecentConvocatoria]
	@cvePremio	varchar(50)	= ''
AS
BEGIN
	-- GetConvocatoriaById
	select	cveConvocatoria[cveConvocatoria],
			FechaInicio[FechaInicio],
			FechaFin[FechaFin],
			cvePremio[cvePremio],
			TituloConvocatoria[TituloConvocatoria],
			FechaVeredicto[FechaVeredicto],
			FechaCreacion[FechaCreacion],
			UsuarioCreacion[UsuarioCreacion],
			FechaEdicion[FechaEdicion],
			UsuarioEdicion[UsuarioEdicion]
	from PI_BA_Convocatoria
	where cvePremio = @cvePremio
    order by FechaFin desc
END


GO
/****** Object:  StoredProcedure [dbo].[GetPremio]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPremio]
	@cvePremio varchar(50)		= ''

AS
BEGIN
	-- GetPremioById
	if @cvePremio != '' begin
			select	cvePremio[cvePremio],
					Nombre[Nombre],
					NombreImagen[NombreImagen],
					Descripcion[Descripcion],
					FechaCreacion[FechaCreacion],
					UsuarioCreacion[UsuarioCreacion],
					FechaEdicion[FechaEdicion],
					UsuarioEdicion[UsuarioEdicion]
			from PI_BA_Premio
			where cvePremio = @cvePremio
	end

	-- GetAllPremios
	begin
			select	cvePremio[cvePremio],
					Nombre[Nombre],
					NombreImagen[NombreImagen],
					Descripcion[Descripcion],
					FechaCreacion[FechaCreacion],
					UsuarioCreacion[UsuarioCreacion],
					FechaEdicion[FechaEdicion],
					UsuarioEdicion[UsuarioEdicion]
			from PI_BA_Premio
	end
END



GO
/****** Object:  StoredProcedure [dbo].[GetPremioByIdCategoria]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPremioByIdCategoria]
	@cveCategoria varchar(50)			= ''
AS
BEGIN
	select  PI_BA_Premio.cvePremio[cvePremio],
			PI_BA_Premio.Nombre[Nombre],
			PI_BA_Premio.NombreImagen[NombreImagen],
			PI_BA_Premio.Descripcion[Descripcion],
			PI_BA_Premio.FechaCreacion[FechaCreacion],
			PI_BA_Premio.UsuarioCreacion[UsuarioCreacion],
			PI_BA_Premio.FechaEdicion[FechaEdicion],
			PI_BA_Premio.UsuarioEdicion[UsuarioEdicion]
	from PI_BA_Premio
	INNER JOIN PI_BA_Convocatoria
	ON PI_BA_Convocatoria.cvePremio = PI_BA_Premio.cvePremio
	INNER JOIN PI_BA_Categoria
	ON PI_BA_Convocatoria.cveConvocatoria = PI_BA_Categoria.cveConvocatoria
	where PI_BA_Categoria.cveCategoria = @cveCategoria
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateAdministrador]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateAdministrador]
	@cveAdministrador	varchar(50)			= '',
	@Password			varchar(100)		= '',
	@Correo				varchar(100)		= ''

AS
BEGIN
	UPDATE PI_SE_Administrador
	SET cveAdministrador = @cveAdministrador,
		Password = @Password,
		Correo = @Correo
	WHERE Correo = @Correo
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateCandidato]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[UpdateCandidato]
	@cveCandidato			varchar(50)		= '',
	@Password				varchar(100)	= '',
	@Nombre					varchar(100)	= '',
	@Apellido				varchar(100)	= '',
	@Confirmado				bit				= null,
	@Correo					varchar(100)	= '',
	@CodigoConfirmacion		varchar(50)		= '',
	@Telefono				varchar(50)		= '',
	@Nacionalidad			varchar(50)		= '',
	@RFC					varchar(50)		= '',
	@Direccion				varchar(50)		= '',
	@NombreImagen			varchar(50)		= '',
	@FechaPrivacidadDatos	date			= null

AS
BEGIN
	UPDATE PI_BA_Candidato
	SET cveCandidato = @cveCandidato,
		Password = @Password,
		Nombre = @Nombre,
		Apellido = @Apellido,
		Confirmado = @Confirmado,
		Correo = @Correo,
		CodigoConfirmacion = @CodigoConfirmacion,
		Telefono = @Telefono,
		Nacionalidad = @Nacionalidad,
		RFC = @RFC,
		Direccion = @Direccion,
		NombreImagen = @NombreImagen,
		FechaPrivacidadDatos = @FechaPrivacidadDatos
	WHERE Correo = @Correo
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateConvocatoria]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateConvocatoria]
	@cveConvocatoria		varchar(50)		= '',
	@FechaInicio			date			= '',
	@FechaFin				date			= '',
	@cvePremio				varchar(50)		= '',
	@TituloConvocatoria		varchar(100)	= '',
	@FechaVeredicto			date			= '',
	@FechaCreacion			date			= '',
	@UsuarioCreacion		varchar(50)		= '',
	@FechaEdicion			date			= '',
	@UsuarioEdicion			varchar(50)		= ''
AS
BEGIN
	UPDATE PI_BA_Convocatoria
	SET cveConvocatoria = @cveConvocatoria,
		FechaInicio = @FechaInicio,
		FechaFin = @FechaFin,
		cvePremio = @cvePremio,
		TituloConvocatoria = @TituloConvocatoria,
		FechaVeredicto = @FechaVeredicto,
		FechaCreacion = @FechaCreacion,
		UsuarioCreacion = @UsuarioCreacion,
		FechaEdicion = @FechaEdicion,
		UsuarioEdicion = @UsuarioEdicion
	WHERE cveConvocatoria = @cveConvocatoria
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateEvaluacion]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateEvaluacion]
	@cveEvaluacion		varchar(50)			= '',
	@Calificacion		smallint			= 0,
	@cveAplicacion		varchar(100)		= '',
	@cveJuez			varchar(100)		= ''
AS
BEGIN
	UPDATE PI_BA_Evaluacion
	SET cveEvaluacion = @cveEvaluacion,
		Calificacion = @Calificacion,
		cveAplicacion = @cveAplicacion,
		cveJuez = @cveJuez
	WHERE cveEvaluacion = @cveEvaluacion
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateJuez]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[UpdateJuez]
	@cveJuez		varchar(50)			= '',
	@Password		varchar(100)		= '',
	@Nombre			varchar(100)		= '',
	@Apellido		varchar(100)		= '',
	@Correo			varchar(100)		= '',
	@NombreImagen	varchar(50)			= ''

AS
BEGIN
	UPDATE PI_BA_Juez
	SET cveJuez = @cveJuez,
		Password = @Password,
		Nombre = @Nombre,
		Apellido = @Apellido,
		Correo = @Correo,
		NombreImagen = @NombreImagen
	WHERE Correo = @Correo
END

GO
/****** Object:  StoredProcedure [dbo].[UpdatePremio]    Script Date: 08/08/2017 12:48:48 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePremio]
	@cvePremio			varchar(50)		= '',
	@Nombre				varchar(100)	= '',
	@NombreImagen		varchar(50)		= '',
	@Descripcion		varchar(250)	= '',
	@FechaCreacion		date			= '',
	@UsuarioCreacion	varchar(50)		= '',
	@FechaEdicion		date			= '',
	@UsuarioEdicion		varchar(50)		= ''
AS
BEGIN
	UPDATE PI_BA_Premio
	SET cvePremio = @cvePremio,
		Nombre = @Nombre,
		NombreImagen = @NombreImagen,
		Descripcion = @Descripcion,
		FechaCreacion = @FechaCreacion,
		UsuarioCreacion = @UsuarioCreacion,
		FechaEdicion = @FechaEdicion,
		UsuarioEdicion = @UsuarioEdicion
	WHERE cvePremio = @cvePremio
END

GO
