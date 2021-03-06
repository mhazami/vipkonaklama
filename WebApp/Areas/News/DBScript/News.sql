/****** Object:  Schema [News]    Script Date: 10/24/2019 12:18:36 PM ******/
CREATE SCHEMA [News]
GO
/****** Object:  Table [News].[News]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [News].[News](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SaveDate] [datetime] NOT NULL,
	[PublishDate] [nchar](10) NOT NULL,
	[PublishTime] [nchar](10) NOT NULL,
	[InterviewerId] [uniqueidentifier] NULL,
	[CreatorId] [uniqueidentifier] NULL,
	[EditorId] [uniqueidentifier] NULL,
	[NewsCategoryId] [uniqueidentifier] NULL,
	[Enabled] [bit] NOT NULL CONSTRAINT [DF_News_Enabled]  DEFAULT ((1)),
	[ThumbnailId] [uniqueidentifier] NULL,
	[IsExternal] [bit] NOT NULL DEFAULT ((0)),
	[Visible] [bit] NULL,
	[Pined] [bit] NOT NULL,
	[GetComment] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [News].[NewsAttachedFile]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [News].[NewsAttachedFile](
	[NewsId] [int] NOT NULL,
	[FileId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_NewsAttachedFile] PRIMARY KEY CLUSTERED 
(
	[NewsId] ASC,
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [News].[NewsAttribute]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [News].[NewsAttribute](
	[Id] [int] NOT NULL,
	[GalleryId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_NewsAttribute] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [News].[NewsCategory]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [News].[NewsCategory](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[ParentCategoryId] [uniqueidentifier] NULL,
	[Enabled] [bit] NOT NULL CONSTRAINT [DF_NewsCategory_Enabled]  DEFAULT ((1)),
 CONSTRAINT [PK_NewsCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [News].[NewsComment]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [News].[NewsComment](
	[CommentId] [uniqueidentifier] NOT NULL,
	[NewsId] [int] NOT NULL,
 CONSTRAINT [PK_CommentNews] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC,
	[NewsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [News].[NewsContent]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [News].[NewsContent](
	[Id] [int] NOT NULL,
	[LanguageId] [char](5) NOT NULL,
	[OverTitle] [nvarchar](250) NULL,
	[Title1] [nvarchar](4000) NOT NULL,
	[Title2] [nvarchar](4000) NULL,
	[Lead] [nvarchar](max) NULL,
	[Body] [ntext] NULL,
	[Sutitr] [nvarchar](max) NULL,
	[VisitCount] [int] NOT NULL CONSTRAINT [DF_NewsContent_VisitCount]  DEFAULT ((0)),
 CONSTRAINT [PK_NewsContent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [News].[NewsContentType]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [News].[NewsContentType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_NewsContentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [News].[NewsDailyVisit]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [News].[NewsDailyVisit](
	[Id] [int] NOT NULL,
	[LanguageId] [char](5) NOT NULL,
	[Day] [date] NOT NULL,
	[VisitCount] [int] NOT NULL,
 CONSTRAINT [PK_NewsDailyVisit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[LanguageId] ASC,
	[Day] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [News].[NewsProperty]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [News].[NewsProperty](
	[Id] [int] NOT NULL,
	[IsSelection] [bit] NOT NULL CONSTRAINT [DF_NewsProperty_IsSelection]  DEFAULT ((0)),
	[IsGeneralView] [bit] NOT NULL CONSTRAINT [DF_NewsProperty_IsGeneralView]  DEFAULT ((1)),
	[IsNewsGroupSelection] [bit] NOT NULL CONSTRAINT [DF_NewsProperty_NewsGroupSelection]  DEFAULT ((0)),
	[Order] [smallint] NOT NULL CONSTRAINT [DF_NewsProperty_Order]  DEFAULT ((0)),
	[NewsGroupOrder] [smallint] NOT NULL CONSTRAINT [DF_NewsProperty_NewsGroupOrder]  DEFAULT ((0)),
	[HasContentPic] [bit] NOT NULL CONSTRAINT [DF_NewsProperty_HasContentPic]  DEFAULT ((0)),
	[HasAttachment] [bit] NOT NULL CONSTRAINT [DF_NewsProperty_HasAttachment]  DEFAULT ((0)),
	[IsImageReport] [bit] NOT NULL CONSTRAINT [DF_NewsProperty_IsImageReport]  DEFAULT ((0)),
	[NewsContentTypeId] [int] NULL,
	[IsSoundNews] [bit] NOT NULL CONSTRAINT [DF_NewsProperty_IsSoundNews]  DEFAULT ((0)),
	[IsMovieNews] [bit] NOT NULL CONSTRAINT [DF_NewsProperty_IsMovieNews]  DEFAULT ((0)),
 CONSTRAINT [PK_NewsProperty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [News].[NewsPublishType]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [News].[NewsPublishType](
	[NewsId] [int] NOT NULL,
	[PublishTypeId] [int] NOT NULL,
 CONSTRAINT [PK_NewsPublishType] PRIMARY KEY CLUSTERED 
(
	[NewsId] ASC,
	[PublishTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [News].[PublishType]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [News].[PublishType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_PublishType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [News].[RelatedNews]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [News].[RelatedNews](
	[RelatedNewsGroupId] [uniqueidentifier] NOT NULL,
	[NewsId] [int] NOT NULL,
 CONSTRAINT [PK_RelatedNews] PRIMARY KEY CLUSTERED 
(
	[RelatedNewsGroupId] ASC,
	[NewsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [News].[RelatedNewsGroup]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [News].[RelatedNewsGroup](
	[Id] [uniqueidentifier] NOT NULL,
	[MainNewsId] [int] NULL,
 CONSTRAINT [PK_RelatedNewsGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [News].[WebDesignNews]    Script Date: 10/24/2019 12:18:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [News].[WebDesignNews](
	[WebId] [uniqueidentifier] NOT NULL,
	[NewsId] [int] NOT NULL,
 CONSTRAINT [PK_News_1] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[NewsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [News].[NewsDailyVisit] ADD  CONSTRAINT [DF_NewsDailyVisit_VisitCount]  DEFAULT ((0)) FOR [VisitCount]
GO
ALTER TABLE [News].[PublishType] ADD  CONSTRAINT [DF_PublishType_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [News].[News]  WITH CHECK ADD  CONSTRAINT [FK_News_Account1] FOREIGN KEY([CreatorId])
REFERENCES [Security].[User] ([Id])
GO
ALTER TABLE [News].[News] CHECK CONSTRAINT [FK_News_Account1]
GO
ALTER TABLE [News].[News]  WITH CHECK ADD  CONSTRAINT [FK_News_Account2] FOREIGN KEY([EditorId])
REFERENCES [EnterpriseNode].[EnterpriseNode] ([Id])
GO
ALTER TABLE [News].[News] CHECK CONSTRAINT [FK_News_Account2]
GO
ALTER TABLE [News].[News]  WITH CHECK ADD  CONSTRAINT [FK_News_NewsCategory] FOREIGN KEY([NewsCategoryId])
REFERENCES [News].[NewsCategory] ([Id])
GO
ALTER TABLE [News].[News] CHECK CONSTRAINT [FK_News_NewsCategory]
GO
ALTER TABLE [News].[NewsAttachedFile]  WITH CHECK ADD  CONSTRAINT [FK_NewsAttachedFile_File] FOREIGN KEY([FileId])
REFERENCES [FileManager].[File] ([Id])
GO
ALTER TABLE [News].[NewsAttachedFile] CHECK CONSTRAINT [FK_NewsAttachedFile_File]
GO
ALTER TABLE [News].[NewsAttachedFile]  WITH CHECK ADD  CONSTRAINT [FK_NewsAttachedFile_News] FOREIGN KEY([NewsId])
REFERENCES [News].[News] ([Id])
GO
ALTER TABLE [News].[NewsAttachedFile] CHECK CONSTRAINT [FK_NewsAttachedFile_News]
GO
ALTER TABLE [News].[NewsAttribute]  WITH CHECK ADD  CONSTRAINT [FK_NewsAttribute_Gallery] FOREIGN KEY([GalleryId])
REFERENCES [Gallery].[Gallery] ([Id])
GO
ALTER TABLE [News].[NewsAttribute] CHECK CONSTRAINT [FK_NewsAttribute_Gallery]
GO
ALTER TABLE [News].[NewsAttribute]  WITH CHECK ADD  CONSTRAINT [FK_NewsAttribute_News] FOREIGN KEY([Id])
REFERENCES [News].[News] ([Id])
GO
ALTER TABLE [News].[NewsAttribute] CHECK CONSTRAINT [FK_NewsAttribute_News]
GO
ALTER TABLE [News].[NewsCategory]  WITH CHECK ADD  CONSTRAINT [FK_NewsCategory_NewsCategory] FOREIGN KEY([ParentCategoryId])
REFERENCES [News].[NewsCategory] ([Id])
GO
ALTER TABLE [News].[NewsCategory] CHECK CONSTRAINT [FK_NewsCategory_NewsCategory]
GO
ALTER TABLE [News].[NewsComment]  WITH CHECK ADD  CONSTRAINT [FK_CommentNews_Comment] FOREIGN KEY([CommentId])
REFERENCES [Comment].[Comment] ([Id])
GO
ALTER TABLE [News].[NewsComment] CHECK CONSTRAINT [FK_CommentNews_Comment]
GO
ALTER TABLE [News].[NewsComment]  WITH CHECK ADD  CONSTRAINT [FK_CommentNews_News] FOREIGN KEY([NewsId])
REFERENCES [News].[News] ([Id])
GO
ALTER TABLE [News].[NewsComment] CHECK CONSTRAINT [FK_CommentNews_News]
GO
ALTER TABLE [News].[NewsContent]  WITH CHECK ADD  CONSTRAINT [FK_NewsContent_Language] FOREIGN KEY([LanguageId])
REFERENCES [Common].[Language] ([Id])
GO
ALTER TABLE [News].[NewsContent] CHECK CONSTRAINT [FK_NewsContent_Language]
GO
ALTER TABLE [News].[NewsContent]  WITH CHECK ADD  CONSTRAINT [FK_NewsContent_News] FOREIGN KEY([Id])
REFERENCES [News].[News] ([Id])
GO
ALTER TABLE [News].[NewsContent] CHECK CONSTRAINT [FK_NewsContent_News]
GO
ALTER TABLE [News].[NewsDailyVisit]  WITH CHECK ADD  CONSTRAINT [FK_NewsDailyVisit_Language] FOREIGN KEY([LanguageId])
REFERENCES [Common].[Language] ([Id])
GO
ALTER TABLE [News].[NewsDailyVisit] CHECK CONSTRAINT [FK_NewsDailyVisit_Language]
GO
ALTER TABLE [News].[NewsDailyVisit]  WITH CHECK ADD  CONSTRAINT [FK_NewsDailyVisit_News] FOREIGN KEY([Id])
REFERENCES [News].[News] ([Id])
GO
ALTER TABLE [News].[NewsDailyVisit] CHECK CONSTRAINT [FK_NewsDailyVisit_News]
GO
ALTER TABLE [News].[NewsProperty]  WITH CHECK ADD  CONSTRAINT [FK_NewsProperty_News] FOREIGN KEY([Id])
REFERENCES [News].[News] ([Id])
GO
ALTER TABLE [News].[NewsProperty] CHECK CONSTRAINT [FK_NewsProperty_News]
GO
ALTER TABLE [News].[NewsProperty]  WITH CHECK ADD  CONSTRAINT [FK_NewsProperty_NewsContentType] FOREIGN KEY([NewsContentTypeId])
REFERENCES [News].[NewsContentType] ([Id])
GO
ALTER TABLE [News].[NewsProperty] CHECK CONSTRAINT [FK_NewsProperty_NewsContentType]
GO
ALTER TABLE [News].[NewsPublishType]  WITH CHECK ADD  CONSTRAINT [FK_NewsPublishType_News] FOREIGN KEY([NewsId])
REFERENCES [News].[News] ([Id])
GO
ALTER TABLE [News].[NewsPublishType] CHECK CONSTRAINT [FK_NewsPublishType_News]
GO
ALTER TABLE [News].[NewsPublishType]  WITH CHECK ADD  CONSTRAINT [FK_NewsPublishType_PublishType] FOREIGN KEY([PublishTypeId])
REFERENCES [News].[PublishType] ([Id])
GO
ALTER TABLE [News].[NewsPublishType] CHECK CONSTRAINT [FK_NewsPublishType_PublishType]
GO
ALTER TABLE [News].[RelatedNews]  WITH CHECK ADD  CONSTRAINT [FK_RelatedNews_News] FOREIGN KEY([NewsId])
REFERENCES [News].[News] ([Id])
GO
ALTER TABLE [News].[RelatedNews] CHECK CONSTRAINT [FK_RelatedNews_News]
GO
ALTER TABLE [News].[RelatedNews]  WITH CHECK ADD  CONSTRAINT [FK_RelatedNews_RelatedNewsGroup] FOREIGN KEY([RelatedNewsGroupId])
REFERENCES [News].[RelatedNewsGroup] ([Id])
GO
ALTER TABLE [News].[RelatedNews] CHECK CONSTRAINT [FK_RelatedNews_RelatedNewsGroup]
GO
ALTER TABLE [News].[RelatedNewsGroup]  WITH CHECK ADD  CONSTRAINT [FK_RelatedNewsGroup_News] FOREIGN KEY([MainNewsId])
REFERENCES [News].[News] ([Id])
GO
ALTER TABLE [News].[RelatedNewsGroup] CHECK CONSTRAINT [FK_RelatedNewsGroup_News]
GO
ALTER TABLE [News].[WebDesignNews]  WITH CHECK ADD  CONSTRAINT [FK_News_News1] FOREIGN KEY([NewsId])
REFERENCES [News].[News] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [News].[WebDesignNews] CHECK CONSTRAINT [FK_News_News1]
GO
ALTER TABLE [News].[WebDesignNews]  WITH CHECK ADD  CONSTRAINT [FK_News_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [News].[WebDesignNews] CHECK CONSTRAINT [FK_News_WebSite]
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'd12c2c08-8a15-4a4b-b005-010b625d13be', 0, N'نوع محتوا', N'/News/NewsContentType', NULL, 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'59a397d6-15ae-4dfe-8ae1-06abfc97622c', 0, N'حذف بخش', N'/News/Newscategory/Delete/', N'3b5f388c-796c-4b39-bf1c-f5666c9c2c97', 1, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'fa732161-1b95-480c-a5fb-0fe73ea6b166', 0, N'حذف', N'/News/News/Delete', N'fc2c598b-465d-4fc3-8837-22c6ce9d8a4e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'b95052e3-c923-4603-860a-14c50ab8f8dc', 0, N'جزئیات دسته بندی', N'/News/NewsCategory/Details', NULL, 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'26aa07c4-44f5-4e9d-8dc4-14d9318c337a', 0, N'نحوه انتشار', N'/News/PublishType', NULL, 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'00a40e12-da9a-4172-aab0-14f3c2ef0f01', 0, N'جزئیات نحوه انتشار/اخبار', N'/News/PublishType/Details', NULL, 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'45fbed48-4bff-4e74-9445-19ad397321d7', 0, N'جزئیات نوع محتوا/اخبار', N'/News/NewsContentType/Details', NULL, 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'4cecf1a8-dad7-4943-afeb-1b0183945305', NULL, N'جزئیات', N'/News/webdesignnews/details', N'3f065ba6-50de-4d8f-960a-9181c97d24f1', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'fc2c598b-465d-4fc3-8837-22c6ce9d8a4e', 0, N'مدیریت اخبار', N'/News/News/', NULL, 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'3cacbfd5-8052-4fb8-91ea-43fbf0aa0a4c', 0, N'جزییات بخش', N'/News/Newscategory/Detail/', N'3b5f388c-796c-4b39-bf1c-f5666c9c2c97', 3, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'4063f44b-5a1d-42db-84d5-4a5fde02fd6c', NULL, N'حذف', N'/News/webdesignnews/delete', N'3f065ba6-50de-4d8f-960a-9181c97d24f1', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'9f984585-83b0-4ee4-b261-50232f3e8fca', 0, N'لیست', N'/News/NewsCategory/Index', N'3b5f388c-796c-4b39-bf1c-f5666c9c2c97', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'd14babc4-7d1b-4e99-9c63-5a9d6ce67d8c', 0, N'ویرایش', N'/News/News/Edit', N'fc2c598b-465d-4fc3-8837-22c6ce9d8a4e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'46747747-370b-4340-a656-71c55a5c4903', 0, N'ایجاد', N'/News/NewscontentType/Create', N'd12c2c08-8a15-4a4b-b005-010b625d13be', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'b3eabfeb-d4a9-4078-ad3c-8816806c792d', 0, N'ویرایش دسته بندی/اخبار', N'/News/NewsCategory/Edit', NULL, 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'58b70cb8-03b7-47e4-941e-896266fe4f5a', 0, N'ویرایش بخش', N'/News/Newscategory/Update/', N'3b5f388c-796c-4b39-bf1c-f5666c9c2c97', 2, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'7ae9e45f-d9d1-4d6d-87a1-8b3324fc18e5', 0, N'ویرایش نحوه انتشار', N'/News/PublishType/Edit', NULL, 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'5ceaefdf-df1f-4e5b-bb59-8d06ba48e60b', 0, N'ایجاد', N'/News/News/Create', N'fc2c598b-465d-4fc3-8837-22c6ce9d8a4e', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'3f065ba6-50de-4d8f-960a-9181c97d24f1', 0, N'اخبار', N'/News/webdesignnews', NULL, 0, 0, 1, NULL, 10)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'5a760695-6db9-493e-bdc1-931dbe1ea2a7', NULL, N'اخبار', N'/news/newscomment/index', NULL, 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'43b9a556-7e72-4a7f-95a2-950288f8c13b', 0, N'حذف نحوه انتشار', N'/News/PublishType/Delete', NULL, 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'f1e95dd5-9ec1-45a7-bed1-a269613df5a7', 0, N'لیست', N'/News/News/Index', N'fc2c598b-465d-4fc3-8837-22c6ce9d8a4e', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'7bb4931e-9338-461c-abdb-a2872fdd057b', 0, N'حذف دسته بندی', N'/News/NewsCategory/Delete', NULL, 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'c355e58c-d678-495f-94b0-a3c44ff7f820', 0, N'ایجاد بخش', N'/News/NewsCategory/Create', N'3b5f388c-796c-4b39-bf1c-f5666c9c2c97', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'1cf57a69-af31-4560-bd46-aad5f4a9eacd', NULL, N'ویرایش', N'/News/webdesignnews/edit', N'3f065ba6-50de-4d8f-960a-9181c97d24f1', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'b3d1a6ab-875a-4c3b-b64d-ac4c92e055cd', 0, N'جزییات', N'/News/News/Details', N'fc2c598b-465d-4fc3-8837-22c6ce9d8a4e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'0ed66054-1688-4fe5-b97b-b01e8258a3ee', NULL, N'ایجاد', N'/News/webdesignnews/create', N'3f065ba6-50de-4d8f-960a-9181c97d24f1', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'b3c61a8b-b59c-447d-b52a-b5a247a193dc', 0, N'ایجاد', N'/News/PublishType/Create', N'26aa07c4-44f5-4e9d-8dc4-14d9318c337a', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'0520a032-c4ba-4986-bf59-bb42f1842d5e', 0, N'حذف نوع محتوا', N'/News/NewsContentType/Delete', NULL, 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'0e48b0b8-4cef-4bf3-897c-c29316f1c82e', NULL, N'لیست', N'/News/webdesignnews/index', N'3f065ba6-50de-4d8f-960a-9181c97d24f1', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'b071b5a9-a1be-4873-bbfd-ce32cebc6414', 0, N'لیست', N'/News/PublishType/Index', N'26aa07c4-44f5-4e9d-8dc4-14d9318c337a', 1, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'a23d9c44-1aa3-4d8a-8b70-efe763f421d5', 0, N'ویرایش نوع محتوا', N'/News/NewsContentType/Edit', NULL, 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'4e8e6bfd-8b57-47c4-b78c-f0a9a69c42e1', 0, N'لیست', N'/News/NewsContentType/Index', N'd12c2c08-8a15-4a4b-b005-010b625d13be', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'3b5f388c-796c-4b39-bf1c-f5666c9c2c97', 0, N'طبقه بندی اخبار', N'/News/Newscategory/', NULL, 1, 1, 1, NULL, NULL)
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'3989d2a2-454c-4aca-bed7-1b1a27aaa7d4', N'5a760695-6db9-493e-bdc1-931dbe1ea2a7')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'4cecf1a8-dad7-4943-afeb-1b0183945305')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'4063f44b-5a1d-42db-84d5-4a5fde02fd6c')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'3f065ba6-50de-4d8f-960a-9181c97d24f1')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'1cf57a69-af31-4560-bd46-aad5f4a9eacd')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'0ed66054-1688-4fe5-b97b-b01e8258a3ee')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'0e48b0b8-4cef-4bf3-897c-c29316f1c82e')
GO
