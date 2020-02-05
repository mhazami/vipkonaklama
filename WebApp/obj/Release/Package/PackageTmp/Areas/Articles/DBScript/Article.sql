/****** Object:  Schema [Article]    Script Date: 10/24/2019 11:34:57 AM ******/
CREATE SCHEMA [Article]
GO
/****** Object:  Table [Article].[Article]    Script Date: 10/24/2019 11:34:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Article].[Article](
	[Id] [uniqueidentifier] NOT NULL,
	[PublishDate] [char](10) NOT NULL,
	[ThumbnailId] [uniqueidentifier] NULL,
	[VisitCount] [int] NOT NULL CONSTRAINT [DF_Article_VisitCount]  DEFAULT ((0)),
	[ArticleCategoryId] [int] NOT NULL,
	[Order] [int] NOT NULL,
	[Enable] [bit] NOT NULL CONSTRAINT [DF_Article_Enabel]  DEFAULT ((1)),
	[GetComment] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Article].[ArticleCategory]    Script Date: 10/24/2019 11:34:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Article].[ArticleCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[ParentId] [int] NULL,
	[Order] [int] NOT NULL,
	[Enable] [bit] NOT NULL,
	[FileId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ArticleCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Article].[ArticleComment]    Script Date: 10/24/2019 11:34:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Article].[ArticleComment](
	[CommentId] [uniqueidentifier] NOT NULL,
	[ArticleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CommentArticle] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC,
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Article].[ArticleFileAttachment]    Script Date: 10/24/2019 11:34:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Article].[ArticleFileAttachment](
	[ArticleId] [uniqueidentifier] NOT NULL,
	[FileId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ArticleFileAttachment] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC,
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Article].[ArticleResource]    Script Date: 10/24/2019 11:34:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Article].[ArticleResource](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[WebAddress] [char](50) NULL,
	[PublishDateResource] [char](10) NULL,
	[Publisher] [nvarchar](200) NULL,
	[Country] [nvarchar](70) NULL,
	[Language] [nvarchar](70) NULL,
	[Auther] [nvarchar](200) NULL,
 CONSTRAINT [PK_ArticleResource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Article].[ArticleResources]    Script Date: 10/24/2019 11:34:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Article].[ArticleResources](
	[ArticleId] [uniqueidentifier] NOT NULL,
	[ArticleResourceId] [smallint] NOT NULL,
 CONSTRAINT [PK_ArticleResources] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC,
	[ArticleResourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [Article].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_ArticleCategory] FOREIGN KEY([ArticleCategoryId])
REFERENCES [Article].[ArticleCategory] ([Id])
GO
ALTER TABLE [Article].[Article] CHECK CONSTRAINT [FK_Article_ArticleCategory]
GO
ALTER TABLE [Article].[ArticleCategory]  WITH CHECK ADD  CONSTRAINT [FK_ArticleCategory_ArticleCategory] FOREIGN KEY([ParentId])
REFERENCES [Article].[ArticleCategory] ([Id])
GO
ALTER TABLE [Article].[ArticleCategory] CHECK CONSTRAINT [FK_ArticleCategory_ArticleCategory]
GO
ALTER TABLE [Article].[ArticleCategory]  WITH CHECK ADD  CONSTRAINT [FK_ArticleCategory_File] FOREIGN KEY([FileId])
REFERENCES [FileManager].[File] ([Id])
GO
ALTER TABLE [Article].[ArticleCategory] CHECK CONSTRAINT [FK_ArticleCategory_File]
GO
ALTER TABLE [Article].[ArticleComment]  WITH CHECK ADD  CONSTRAINT [FK_CommentArticle_Article] FOREIGN KEY([ArticleId])
REFERENCES [Article].[Article] ([Id])
GO
ALTER TABLE [Article].[ArticleComment] CHECK CONSTRAINT [FK_CommentArticle_Article]
GO
ALTER TABLE [Article].[ArticleComment]  WITH CHECK ADD  CONSTRAINT [FK_CommentArticle_Comment] FOREIGN KEY([CommentId])
REFERENCES [Comment].[Comment] ([Id])
GO
ALTER TABLE [Article].[ArticleComment] CHECK CONSTRAINT [FK_CommentArticle_Comment]
GO
ALTER TABLE [Article].[ArticleFileAttachment]  WITH CHECK ADD  CONSTRAINT [FK_ArticleFileAttachment_Article] FOREIGN KEY([ArticleId])
REFERENCES [Article].[Article] ([Id])
GO
ALTER TABLE [Article].[ArticleFileAttachment] CHECK CONSTRAINT [FK_ArticleFileAttachment_Article]
GO
ALTER TABLE [Article].[ArticleResources]  WITH CHECK ADD  CONSTRAINT [FK_ArticleResources_Article] FOREIGN KEY([ArticleId])
REFERENCES [Article].[Article] ([Id])
GO
ALTER TABLE [Article].[ArticleResources] CHECK CONSTRAINT [FK_ArticleResources_Article]
GO
ALTER TABLE [Article].[ArticleResources]  WITH CHECK ADD  CONSTRAINT [FK_ArticleResources_ArticleResource] FOREIGN KEY([ArticleResourceId])
REFERENCES [Article].[ArticleResource] ([Id])
GO
ALTER TABLE [Article].[ArticleResources] CHECK CONSTRAINT [FK_ArticleResources_ArticleResource]
GO

INSERT [Security].[Operation] ([Id], [Title], [Enabled], [Order], [Expand], [LogoId]) VALUES (N'5c241e29-0198-49aa-96df-f36cb9be48e5', N'مقالات', 1, NULL, 0, NULL)
GO

INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'fb95d2b6-7de6-46fa-bf96-0123ebde8913', 0, N'مقالات', N'/Articles/Articles/Index', NULL, 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'11fab394-7eea-4078-88aa-223c3640f5a6', 0, N'لیست مقالات', N'/Articles/Articles/', N'fb95d2b6-7de6-46fa-bf96-0123ebde8913', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'7dc57ff0-2daf-4e94-974c-2af93ba7b5c2', 0, N'جزئیات مقالات', N'/Articles/ArticleCategory/Details', N'3ca21e10-95c6-43ff-8fca-adf8805dc29e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'15ae4154-cb26-42ea-b410-30f88239f74a', 0, N'حذف مقاله', N'/Articles/Articles/Delete', N'fb95d2b6-7de6-46fa-bf96-0123ebde8913', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'44f41903-7421-4c00-a7ed-759086e78562', 0, N'حذف دسته بندی', N'/Articles/ArticleCategory/Delete', N'3ca21e10-95c6-43ff-8fca-adf8805dc29e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'bdce3cf1-a4e7-401a-acf1-9fe575c3703f', 0, N'ویرایش مقاله ', N'/Articles/Articles/Edit', N'fb95d2b6-7de6-46fa-bf96-0123ebde8913', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'3ca21e10-95c6-43ff-8fca-adf8805dc29e', 0, N'دسته بندی مقالات', N'/Articles/ArticleCategory/', NULL, 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'fdd3e63b-0f79-4c50-a713-b19ab394efb8', 0, N'ویرایش دسته بدندی مقالات', N'/Articles/ArticleCategory/Edit', N'3ca21e10-95c6-43ff-8fca-adf8805dc29e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'ffacb4b1-b045-4588-8f13-cd770d560e8b', NULL, N'مقالات', N'/articles/articlecomment/index', NULL, 1, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'0ea39465-aafd-4261-b2b9-d2433fda64da', 0, N'لیست', N'/Articles/ArticleCategory/Index', N'3ca21e10-95c6-43ff-8fca-adf8805dc29e', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'b2717f2a-31a2-4119-a4a2-e85b0aeb482c', 0, N'ایجاد دسته بندی مقالات', N'/Articles/ArticleCategory/Create', N'3ca21e10-95c6-43ff-8fca-adf8805dc29e', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'f893b2af-8ae7-4580-831e-ec0b0e571666', 0, N'ایجاد', N'/Articles/Articles/Create', N'fb95d2b6-7de6-46fa-bf96-0123ebde8913', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'813ccbd4-7d24-4b45-b67c-ff8af39bade3', 0, N'جزئیات مقالعه', N'/Articles/Articles/Details', N'fb95d2b6-7de6-46fa-bf96-0123ebde8913', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'5c241e29-0198-49aa-96df-f36cb9be48e5', N'fb95d2b6-7de6-46fa-bf96-0123ebde8913')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'5c241e29-0198-49aa-96df-f36cb9be48e5', N'11fab394-7eea-4078-88aa-223c3640f5a6')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'5c241e29-0198-49aa-96df-f36cb9be48e5', N'7dc57ff0-2daf-4e94-974c-2af93ba7b5c2')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'5c241e29-0198-49aa-96df-f36cb9be48e5', N'15ae4154-cb26-42ea-b410-30f88239f74a')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'5c241e29-0198-49aa-96df-f36cb9be48e5', N'44f41903-7421-4c00-a7ed-759086e78562')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'5c241e29-0198-49aa-96df-f36cb9be48e5', N'bdce3cf1-a4e7-401a-acf1-9fe575c3703f')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'5c241e29-0198-49aa-96df-f36cb9be48e5', N'3ca21e10-95c6-43ff-8fca-adf8805dc29e')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'5c241e29-0198-49aa-96df-f36cb9be48e5', N'fdd3e63b-0f79-4c50-a713-b19ab394efb8')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'5c241e29-0198-49aa-96df-f36cb9be48e5', N'0ea39465-aafd-4261-b2b9-d2433fda64da')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'5c241e29-0198-49aa-96df-f36cb9be48e5', N'b2717f2a-31a2-4119-a4a2-e85b0aeb482c')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'5c241e29-0198-49aa-96df-f36cb9be48e5', N'f893b2af-8ae7-4580-831e-ec0b0e571666')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'5c241e29-0198-49aa-96df-f36cb9be48e5', N'813ccbd4-7d24-4b45-b67c-ff8af39bade3')
GO