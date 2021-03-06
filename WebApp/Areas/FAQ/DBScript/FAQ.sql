/****** Object:  Schema [FAQ]    Script Date: 10/24/2019 11:54:53 AM ******/
CREATE SCHEMA [FAQ]
GO
/****** Object:  Table [FAQ].[FAQ]    Script Date: 10/24/2019 11:54:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [FAQ].[FAQ](
	[Id] [uniqueidentifier] NOT NULL,
	[Question] [nvarchar](500) NOT NULL,
	[Answer] [ntext] NOT NULL,
	[CreatorId] [uniqueidentifier] NULL,
	[CreateDate] [char](10) NOT NULL,
	[ViewCount] [int] NULL,
	[ThumbnailId] [uniqueidentifier] NULL,
	[IsExternal] [bit] NOT NULL,
 CONSTRAINT [PK_FAQ] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [FAQ].[FAQContent]    Script Date: 10/24/2019 11:54:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [FAQ].[FAQContent](
	[Id] [uniqueidentifier] NOT NULL,
	[Question] [nvarchar](500) NULL,
	[Answer] [ntext] NULL,
	[LanguageId] [char](5) NOT NULL,
 CONSTRAINT [PK_FAQContent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [FAQ].[SiteFAQ]    Script Date: 10/24/2019 11:54:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FAQ].[SiteFAQ](
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SiteFAQ] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [FAQ].[WebDesignFAQ]    Script Date: 10/24/2019 11:54:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FAQ].[WebDesignFAQ](
	[WebId] [uniqueidentifier] NOT NULL,
	[FAQId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_FAQ_1] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[FAQId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [FAQ].[FAQ] ADD  CONSTRAINT [DF_FAQ_ViewCount]  DEFAULT ((0)) FOR [ViewCount]
GO
ALTER TABLE [FAQ].[FAQ] ADD  DEFAULT ((0)) FOR [IsExternal]
GO
ALTER TABLE [FAQ].[FAQContent]  WITH CHECK ADD  CONSTRAINT [FK_FAQContent_FAQ] FOREIGN KEY([Id])
REFERENCES [FAQ].[FAQ] ([Id])
GO
ALTER TABLE [FAQ].[FAQContent] CHECK CONSTRAINT [FK_FAQContent_FAQ]
GO
ALTER TABLE [FAQ].[FAQContent]  WITH CHECK ADD  CONSTRAINT [FK_FAQContent_Language] FOREIGN KEY([LanguageId])
REFERENCES [Common].[Language] ([Id])
GO
ALTER TABLE [FAQ].[FAQContent] CHECK CONSTRAINT [FK_FAQContent_Language]
GO
ALTER TABLE [FAQ].[SiteFAQ]  WITH CHECK ADD  CONSTRAINT [FK_SiteFAQ_FAQ] FOREIGN KEY([Id])
REFERENCES [FAQ].[FAQ] ([Id])
GO
ALTER TABLE [FAQ].[SiteFAQ] CHECK CONSTRAINT [FK_SiteFAQ_FAQ]
GO
ALTER TABLE [FAQ].[WebDesignFAQ]  WITH CHECK ADD  CONSTRAINT [FK_FAQ_FAQ1] FOREIGN KEY([FAQId])
REFERENCES [FAQ].[FAQ] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [FAQ].[WebDesignFAQ] CHECK CONSTRAINT [FK_FAQ_FAQ1]
GO
ALTER TABLE [FAQ].[WebDesignFAQ]  WITH CHECK ADD  CONSTRAINT [FK_FAQ_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [FAQ].[WebDesignFAQ] CHECK CONSTRAINT [FK_FAQ_WebSite]
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'9e1476cb-5a81-456d-ab02-06ed6eddfd0f', NULL, N'حذف', N'/FAQ/webdesignfaq/delete', N'd0bb0246-bd26-42d2-9c70-a6fc224240fc', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'7d7146e9-b87f-450b-815d-1796b0c4ae48', 0, N'جزئیات', N'/FAQ/FAQ/Details', N'df72824e-7a40-4a67-9ee5-73cf33b6d87f', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'1749bedf-6883-4e71-bef9-1d4e3eb8a514', NULL, N'ویرایش', N'/FAQ/webdesignfaq/edit', N'd0bb0246-bd26-42d2-9c70-a6fc224240fc', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'72c425b4-844c-4e76-a4b8-385a563789ce', NULL, N'لیست', N'/FAQ/webdesignfaq/index', N'd0bb0246-bd26-42d2-9c70-a6fc224240fc', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'791cafc3-adc8-4459-92ab-5fdf5a902969', NULL, N'جزئیات', N'/FAQ/webdesignfaq/details', N'd0bb0246-bd26-42d2-9c70-a6fc224240fc', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'fd594268-f841-484f-9dd6-62ea5caf73d5', 0, N'لیست پرسشها و پاسخ ها', N'/FAQ/FAQ/Index', NULL, 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'df72824e-7a40-4a67-9ee5-73cf33b6d87f', 0, N'سوالات متداول', N'/FAQ/FAQ', NULL, 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'd0bb0246-bd26-42d2-9c70-a6fc224240fc', NULL, N'FAQ', N'/FAQ/webdesignfaq', NULL, 0, 1, 1, NULL, 13)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'738926fe-5353-43d7-a627-ab4eab89832a', NULL, N'ایجاد', N'/FAQ/webdesignfaq/create', N'd0bb0246-bd26-42d2-9c70-a6fc224240fc', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'209cb7d6-775e-49a2-b843-bf1266e984e2', 0, N'مشاهده لیست', N'/FAQ/FAQ/', N'df72824e-7a40-4a67-9ee5-73cf33b6d87f', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'7b7c224d-826b-42fd-8d53-c0295ff18921', 0, N'ویرایش پرسش', N'/FAQ/FAQ/Edit', N'e43730c1-28ce-4959-b6da-f7f2355a914d', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'4063a8c6-92fe-4287-8fae-fac1c6bdad18', 0, N'ایجاد', N'/FAQ/FAQ/Create', N'df72824e-7a40-4a67-9ee5-73cf33b6d87f', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'9402bf1b-f2f4-4813-95dd-fd3daf78b9db', 0, N'حذف', N'/FAQ/FAQ/Delete', N'df72824e-7a40-4a67-9ee5-73cf33b6d87f', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'9e1476cb-5a81-456d-ab02-06ed6eddfd0f')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'1749bedf-6883-4e71-bef9-1d4e3eb8a514')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'72c425b4-844c-4e76-a4b8-385a563789ce')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'791cafc3-adc8-4459-92ab-5fdf5a902969')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'd0bb0246-bd26-42d2-9c70-a6fc224240fc')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'738926fe-5353-43d7-a627-ab4eab89832a')
GO
