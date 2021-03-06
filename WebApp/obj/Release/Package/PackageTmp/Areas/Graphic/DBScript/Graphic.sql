/****** Object:  Schema [Graphic]    Script Date: 10/24/2019 12:14:59 PM ******/
CREATE SCHEMA [Graphic]
GO
/****** Object:  Table [Graphic].[Resource]    Script Date: 10/24/2019 12:14:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Graphic].[Resource](
	[Id] [uniqueidentifier] NOT NULL,
	[ThemeId] [uniqueidentifier] NOT NULL,
	[LanuageId] [char](5) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[FileId] [uniqueidentifier] NULL,
	[Order] [tinyint] NOT NULL,
	[Text] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Graphic].[Theme]    Script Date: 10/24/2019 12:14:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Graphic].[Theme](
	[Id] [uniqueidentifier] NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_Theme_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [Graphic].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Theme] FOREIGN KEY([ThemeId])
REFERENCES [Graphic].[Theme] ([Id])
GO
ALTER TABLE [Graphic].[Resource] CHECK CONSTRAINT [FK_Resource_Theme]
GO
