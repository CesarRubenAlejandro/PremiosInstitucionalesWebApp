USE [wPremiosInstitucionalesdb]
GO
/****** Object:  Table [dbo].[PI_BA_Aplicacion]    Script Date: 12/1/2016 10:08:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PI_BA_Aplicacion](
	[cveAplicacion] [varchar](50) NOT NULL,
	[Ganador] [bit] NULL,
	[Status] [varchar](20) NULL,
	[cveCandidato] [varchar](50) NULL,
	[cveCategoria] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Aplicacion] PRIMARY KEY CLUSTERED 
(
	[cveAplicacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PI_BA_Candidato]    Script Date: 12/1/2016 10:08:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
 CONSTRAINT [PK_PI_BA_Candidato] PRIMARY KEY CLUSTERED 
(
	[cveCandidato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PI_BA_Categoria]    Script Date: 12/1/2016 10:08:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PI_BA_Categoria](
	[cveCategoria] [varchar](50) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[cveConvocatoria] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Categoria] PRIMARY KEY CLUSTERED 
(
	[cveCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PI_BA_Convocatoria]    Script Date: 12/1/2016 10:08:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PI_BA_Convocatoria](
	[cveConvocatoria] [varchar](50) NOT NULL,
	[Descripcion] [varchar](max) NULL,
	[FechaInicio] [date] NULL,
	[FechaFin] [date] NULL,
	[cvePremio] [varchar](50) NULL,
	[TituloConvocatoria] [varchar](100) NULL,
	[FechaVeredicto] [date] NULL,
 CONSTRAINT [PK_PI_BA_Convocatoria] PRIMARY KEY CLUSTERED 
(
	[cveConvocatoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PI_BA_Evaluacion]    Script Date: 12/1/2016 10:08:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PI_BA_Evaluacion](
	[cveEvaluacion] [varchar](50) NOT NULL,
	[Calificacion] [smallint] NULL,
	[cveRespuesta] [varchar](50) NULL,
	[cveJuez] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Evaluacion] PRIMARY KEY CLUSTERED 
(
	[cveEvaluacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PI_BA_Forma]    Script Date: 12/1/2016 10:08:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PI_BA_Forma](
	[cveForma] [varchar](50) NOT NULL,
	[cveCategoria] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Forma] PRIMARY KEY CLUSTERED 
(
	[cveForma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PI_BA_Juez]    Script Date: 12/1/2016 10:08:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PI_BA_Juez](
	[cveJuez] [varchar](50) NOT NULL,
	[UserName] [varchar](100) NULL,
	[Password] [varchar](100) NULL,
	[Nombre] [varchar](100) NULL,
	[Apellido] [varchar](100) NULL,
	[Correo] [varchar](100) NULL,
 CONSTRAINT [PK_PI_BA_Juez] PRIMARY KEY CLUSTERED 
(
	[cveJuez] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PI_BA_JuezPorCategoria]    Script Date: 12/1/2016 10:08:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PI_BA_Pregunta]    Script Date: 12/1/2016 10:08:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PI_BA_Pregunta](
	[cvePregunta] [varchar](50) NOT NULL,
	[Texto] [varchar](100) NULL,
	[IdentificadorObjeto] [varchar](100) NULL,
	[TipoCampo] [char](5) NULL,
	[Orden] [int] NULL,
 CONSTRAINT [PK_PI_BA_Pregunta] PRIMARY KEY CLUSTERED 
(
	[cvePregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PI_BA_PreguntasPorForma]    Script Date: 12/1/2016 10:08:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PI_BA_Premio]    Script Date: 12/1/2016 10:08:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PI_BA_Premio](
	[cvePremio] [varchar](50) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[NombreImagen] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Premio] PRIMARY KEY CLUSTERED 
(
	[cvePremio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PI_BA_Respuesta]    Script Date: 12/1/2016 10:08:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PI_BA_Respuesta](
	[cveRespuesta] [varchar](50) NOT NULL,
	[Valor] [varchar](200) NULL,
	[cvePregunta] [varchar](50) NULL,
	[cveAplicacion] [varchar](50) NULL,
 CONSTRAINT [PK_PI_BA_Respuesta] PRIMARY KEY CLUSTERED 
(
	[cveRespuesta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PI_SE_Administrador]    Script Date: 12/1/2016 10:08:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PI_SE_Administrador](
	[CveAdministrador] [varchar](50) NOT NULL,
	[UserName] [varchar](100) NULL,
	[Password] [varchar](100) NULL,
	[Correo] [varchar](100) NULL,
 CONSTRAINT [PK_PI_SE_Administrador] PRIMARY KEY CLUSTERED 
(
	[CveAdministrador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[PI_BA_Aplicacion]  WITH CHECK ADD  CONSTRAINT [FK_PI_BA_Aplicacion_PI_BA_Candidato] FOREIGN KEY([cveCandidato])
REFERENCES [dbo].[PI_BA_Candidato] ([cveCandidato])
GO
ALTER TABLE [dbo].[PI_BA_Aplicacion] CHECK CONSTRAINT [FK_PI_BA_Aplicacion_PI_BA_Candidato]
GO
ALTER TABLE [dbo].[PI_BA_Aplicacion]  WITH CHECK ADD  CONSTRAINT [FK_PI_BA_Aplicacion_PI_BA_Categoria] FOREIGN KEY([cveCategoria])
REFERENCES [dbo].[PI_BA_Categoria] ([cveCategoria])
GO
ALTER TABLE [dbo].[PI_BA_Aplicacion] CHECK CONSTRAINT [FK_PI_BA_Aplicacion_PI_BA_Categoria]
GO
ALTER TABLE [dbo].[PI_BA_Categoria]  WITH CHECK ADD  CONSTRAINT [FK_PI_BA_Categoria_PI_BA_Convocatoria] FOREIGN KEY([cveConvocatoria])
REFERENCES [dbo].[PI_BA_Convocatoria] ([cveConvocatoria])
GO
ALTER TABLE [dbo].[PI_BA_Categoria] CHECK CONSTRAINT [FK_PI_BA_Categoria_PI_BA_Convocatoria]
GO
ALTER TABLE [dbo].[PI_BA_Convocatoria]  WITH CHECK ADD  CONSTRAINT [FK_PI_BA_Convocatoria_PI_BA_Premio] FOREIGN KEY([cvePremio])
REFERENCES [dbo].[PI_BA_Premio] ([cvePremio])
GO
ALTER TABLE [dbo].[PI_BA_Convocatoria] CHECK CONSTRAINT [FK_PI_BA_Convocatoria_PI_BA_Premio]
GO
ALTER TABLE [dbo].[PI_BA_Evaluacion]  WITH CHECK ADD  CONSTRAINT [FK_PI_BA_Evaluacion_PI_BA_Juez] FOREIGN KEY([cveEvaluacion])
REFERENCES [dbo].[PI_BA_Juez] ([cveJuez])
GO
ALTER TABLE [dbo].[PI_BA_Evaluacion] CHECK CONSTRAINT [FK_PI_BA_Evaluacion_PI_BA_Juez]
GO
ALTER TABLE [dbo].[PI_BA_Evaluacion]  WITH CHECK ADD  CONSTRAINT [FK_PI_BA_Evaluacion_PI_BA_Respuesta] FOREIGN KEY([cveEvaluacion])
REFERENCES [dbo].[PI_BA_Respuesta] ([cveRespuesta])
GO
ALTER TABLE [dbo].[PI_BA_Evaluacion] CHECK CONSTRAINT [FK_PI_BA_Evaluacion_PI_BA_Respuesta]
GO
ALTER TABLE [dbo].[PI_BA_Forma]  WITH CHECK ADD  CONSTRAINT [FK_PI_BA_Forma_PI_BA_Categoria] FOREIGN KEY([cveCategoria])
REFERENCES [dbo].[PI_BA_Categoria] ([cveCategoria])
GO
ALTER TABLE [dbo].[PI_BA_Forma] CHECK CONSTRAINT [FK_PI_BA_Forma_PI_BA_Categoria]
GO
ALTER TABLE [dbo].[PI_BA_JuezPorCategoria]  WITH CHECK ADD  CONSTRAINT [FK_PI_BA_JuezPorCategoria_PI_BA_Categoria] FOREIGN KEY([cveCategoria])
REFERENCES [dbo].[PI_BA_Categoria] ([cveCategoria])
GO
ALTER TABLE [dbo].[PI_BA_JuezPorCategoria] CHECK CONSTRAINT [FK_PI_BA_JuezPorCategoria_PI_BA_Categoria]
GO
ALTER TABLE [dbo].[PI_BA_JuezPorCategoria]  WITH CHECK ADD  CONSTRAINT [FK_PI_BA_JuezPorCategoria_PI_BA_Juez] FOREIGN KEY([cveJuez])
REFERENCES [dbo].[PI_BA_Juez] ([cveJuez])
GO
ALTER TABLE [dbo].[PI_BA_JuezPorCategoria] CHECK CONSTRAINT [FK_PI_BA_JuezPorCategoria_PI_BA_Juez]
GO
ALTER TABLE [dbo].[PI_BA_PreguntasPorForma]  WITH CHECK ADD  CONSTRAINT [FK_PI_BA_PreguntasPorForma_PI_BA_Forma] FOREIGN KEY([cveForma])
REFERENCES [dbo].[PI_BA_Forma] ([cveForma])
GO
ALTER TABLE [dbo].[PI_BA_PreguntasPorForma] CHECK CONSTRAINT [FK_PI_BA_PreguntasPorForma_PI_BA_Forma]
GO
ALTER TABLE [dbo].[PI_BA_PreguntasPorForma]  WITH CHECK ADD  CONSTRAINT [FK_PI_BA_PreguntasPorForma_PI_BA_Pregunta] FOREIGN KEY([cvePregunta])
REFERENCES [dbo].[PI_BA_Pregunta] ([cvePregunta])
GO
ALTER TABLE [dbo].[PI_BA_PreguntasPorForma] CHECK CONSTRAINT [FK_PI_BA_PreguntasPorForma_PI_BA_Pregunta]
GO
ALTER TABLE [dbo].[PI_BA_Respuesta]  WITH CHECK ADD  CONSTRAINT [FK_PI_BA_Respuesta_PI_BA_Aplicacion] FOREIGN KEY([cveAplicacion])
REFERENCES [dbo].[PI_BA_Aplicacion] ([cveAplicacion])
GO
ALTER TABLE [dbo].[PI_BA_Respuesta] CHECK CONSTRAINT [FK_PI_BA_Respuesta_PI_BA_Aplicacion]
GO
ALTER TABLE [dbo].[PI_BA_Respuesta]  WITH CHECK ADD  CONSTRAINT [FK_PI_BA_Respuesta_PI_BA_Pregunta] FOREIGN KEY([cvePregunta])
REFERENCES [dbo].[PI_BA_Pregunta] ([cvePregunta])
GO
ALTER TABLE [dbo].[PI_BA_Respuesta] CHECK CONSTRAINT [FK_PI_BA_Respuesta_PI_BA_Pregunta]
GO
