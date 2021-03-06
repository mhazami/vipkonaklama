/****** Object:  Schema [Statistics]    Script Date: 10/24/2019 12:32:48 PM ******/
CREATE SCHEMA [Statistics]
GO
/****** Object:  Table [Statistics].[Accounts]    Script Date: 10/24/2019 12:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Statistics].[Accounts](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Statistics].[Browser]    Script Date: 10/24/2019 12:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Statistics].[Browser](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[LatinName] [varchar](50) NOT NULL,
	[LogoId] [uniqueidentifier] NULL,
	[Version] [nchar](10) NULL,
 CONSTRAINT [PK_Browser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Statistics].[Country]    Script Date: 10/24/2019 12:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Statistics].[Country](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](300) NULL,
	[Logo] [varbinary](max) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Statistics].[IPReng]    Script Date: 10/24/2019 12:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Statistics].[IPReng](
	[Id] [int] NOT NULL,
	[FromIP] [nchar](15) NOT NULL,
	[CountryId] [int] NOT NULL,
	[ToIP] [nchar](15) NOT NULL,
 CONSTRAINT [PK_IPReng_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Statistics].[Log]    Script Date: 10/24/2019 12:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Statistics].[Log](
	[Id] [uniqueidentifier] NOT NULL,
	[IP] [varchar](15) NOT NULL,
	[SesstionId] [nvarchar](50) NOT NULL,
	[WebSiteId] [uniqueidentifier] NOT NULL,
	[HttpReferer] [nchar](100) NULL,
	[Date] [datetime] NOT NULL,
	[Url] [nvarchar](400) NULL,
	[BrowserId] [int] NULL,
	[Path] [nchar](200) NULL,
	[PhysicalPath] [nchar](200) NULL,
	[IsLocal] [bit] NULL,
	[RawUrl] [nchar](100) NULL,
	[OsId] [int] NULL,
	[SearchEngineId] [int] NULL,
	[FromMobile] [bit] NULL,
	[AccountId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Statistics].[LogItems]    Script Date: 10/24/2019 12:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Statistics].[LogItems](
	[Id] [uniqueidentifier] NOT NULL,
	[RequestId] [uniqueidentifier] NULL,
	[UserAgent] [nchar](10) NULL,
	[UserHostName] [nchar](10) NULL,
	[UserHostAddress] [nchar](10) NULL,
	[UserLanguages] [nchar](10) NULL,
	[HttpHost] [nchar](10) NULL,
	[ServerName] [nchar](10) NULL,
	[ServerPort] [nchar](10) NULL,
	[ServerSoftware] [nchar](10) NULL,
	[RemotHost] [nchar](10) NULL,
	[RemotPort] [nchar](10) NULL,
	[RemotAddress] [nchar](10) NULL,
	[LocalAddress] [nchar](10) NULL,
	[HttpCookie] [nchar](10) NULL,
	[QueryString] [nchar](10) NULL,
	[CertSubject] [nchar](10) NULL,
	[CertServerSubject] [nchar](10) NULL,
	[ScreenPixelHeight] [int] NULL,
	[ScreenPixelWith] [int] NULL,
	[ServerAddress] [nchar](10) NULL,
 CONSTRAINT [PK_RequestItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Statistics].[OS]    Script Date: 10/24/2019 12:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Statistics].[OS](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[LatinName] [nchar](50) NOT NULL,
	[LogoId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Statistics].[SearchEngine]    Script Date: 10/24/2019 12:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Statistics].[SearchEngine](
	[Id] [int] NOT NULL,
	[Title] [nchar](10) NULL,
	[LogoId] [uniqueidentifier] NULL,
	[IpAddress] [nchar](10) NULL,
 CONSTRAINT [PK_SearchEngine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Statistics].[WebSite]    Script Date: 10/24/2019 12:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Statistics].[WebSite](
	[Id] [uniqueidentifier] NOT NULL,
	[Url] [nvarchar](200) NOT NULL,
	[OwnerId] [uniqueidentifier] NOT NULL,
	[RegisterDate] [nchar](10) NOT NULL,
	[Title] [nvarchar](50) NULL,
 CONSTRAINT [PK_WebSite] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [Statistics].[IPReng]  WITH CHECK ADD  CONSTRAINT [FK_IPReng_Country] FOREIGN KEY([CountryId])
REFERENCES [Statistics].[Country] ([Id])
GO
ALTER TABLE [Statistics].[IPReng] CHECK CONSTRAINT [FK_IPReng_Country]
GO
ALTER TABLE [Statistics].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Request_Accounts] FOREIGN KEY([AccountId])
REFERENCES [Statistics].[Accounts] ([Id])
GO
ALTER TABLE [Statistics].[Log] CHECK CONSTRAINT [FK_Request_Accounts]
GO
ALTER TABLE [Statistics].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Request_Browser] FOREIGN KEY([BrowserId])
REFERENCES [Statistics].[Browser] ([Id])
GO
ALTER TABLE [Statistics].[Log] CHECK CONSTRAINT [FK_Request_Browser]
GO
ALTER TABLE [Statistics].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Request_OS] FOREIGN KEY([OsId])
REFERENCES [Statistics].[OS] ([Id])
GO
ALTER TABLE [Statistics].[Log] CHECK CONSTRAINT [FK_Request_OS]
GO
ALTER TABLE [Statistics].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Request_SearchEngine] FOREIGN KEY([SearchEngineId])
REFERENCES [Statistics].[SearchEngine] ([Id])
GO
ALTER TABLE [Statistics].[Log] CHECK CONSTRAINT [FK_Request_SearchEngine]
GO
ALTER TABLE [Statistics].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Request_WebSite] FOREIGN KEY([WebSiteId])
REFERENCES [Statistics].[WebSite] ([Id])
GO
ALTER TABLE [Statistics].[Log] CHECK CONSTRAINT [FK_Request_WebSite]
GO
ALTER TABLE [Statistics].[LogItems]  WITH CHECK ADD  CONSTRAINT [FK_RequestItems_Request] FOREIGN KEY([RequestId])
REFERENCES [Statistics].[Log] ([Id])
GO
ALTER TABLE [Statistics].[LogItems] CHECK CONSTRAINT [FK_RequestItems_Request]
GO
ALTER TABLE [Statistics].[WebSite]  WITH CHECK ADD  CONSTRAINT [FK_WebSite_EnterpriseNode] FOREIGN KEY([OwnerId])
REFERENCES [EnterpriseNode].[EnterpriseNode] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Statistics].[WebSite] CHECK CONSTRAINT [FK_WebSite_EnterpriseNode]
GO

