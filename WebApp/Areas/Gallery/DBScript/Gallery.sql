/****** Object:  Schema [Gallery]    Script Date: 10/24/2019 12:10:08 PM ******/
CREATE SCHEMA [Gallery]
GO
/****** Object:  Table [Gallery].[Gallery]    Script Date: 10/24/2019 12:10:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Gallery].[Gallery](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](150) NULL,
	[ParentGallery] [uniqueidentifier] NULL,
	[CreateDate] [char](10) NOT NULL,
	[Creator] [uniqueidentifier] NULL,
	[Enabled] [bit] NOT NULL CONSTRAINT [DF_Gallery_Enabled]  DEFAULT ((1)),
	[Thumbnail] [uniqueidentifier] NULL,
	[IsExternal] [bit] NOT NULL CONSTRAINT [DF_Gallery_IsExternal]  DEFAULT ((1)),
	[Order] [int] NOT NULL,
 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Gallery].[Photo]    Script Date: 10/24/2019 12:10:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Gallery].[Photo](
	[Id] [uniqueidentifier] NOT NULL,
	[GalleryId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](550) NULL,
	[PicFile] [uniqueidentifier] NOT NULL,
	[UploadDate] [char](10) NOT NULL,
	[Uploader] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Gallery].[WebDesignGallery]    Script Date: 10/24/2019 12:10:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Gallery].[WebDesignGallery](
	[WebId] [uniqueidentifier] NOT NULL,
	[GalleryId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Gallery_1] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[GalleryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [Gallery].[Gallery]  WITH CHECK ADD  CONSTRAINT [FK_Gallery_EnterpriseNode] FOREIGN KEY([Creator])
REFERENCES [EnterpriseNode].[EnterpriseNode] ([Id])
GO
ALTER TABLE [Gallery].[Gallery] CHECK CONSTRAINT [FK_Gallery_EnterpriseNode]
GO
ALTER TABLE [Gallery].[Gallery]  WITH CHECK ADD  CONSTRAINT [FK_Gallery_Gallery] FOREIGN KEY([ParentGallery])
REFERENCES [Gallery].[Gallery] ([Id])
GO
ALTER TABLE [Gallery].[Gallery] CHECK CONSTRAINT [FK_Gallery_Gallery]
GO
ALTER TABLE [Gallery].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_EnterpriseNode] FOREIGN KEY([Uploader])
REFERENCES [EnterpriseNode].[EnterpriseNode] ([Id])
GO
ALTER TABLE [Gallery].[Photo] CHECK CONSTRAINT [FK_Photo_EnterpriseNode]
GO
ALTER TABLE [Gallery].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_File1] FOREIGN KEY([PicFile])
REFERENCES [FileManager].[File] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Gallery].[Photo] CHECK CONSTRAINT [FK_Photo_File1]
GO
ALTER TABLE [Gallery].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_Gallery] FOREIGN KEY([GalleryId])
REFERENCES [Gallery].[Gallery] ([Id])
GO
ALTER TABLE [Gallery].[Photo] CHECK CONSTRAINT [FK_Photo_Gallery]
GO
ALTER TABLE [Gallery].[WebDesignGallery]  WITH CHECK ADD  CONSTRAINT [FK_Gallery_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Gallery].[WebDesignGallery] CHECK CONSTRAINT [FK_Gallery_WebSite]
GO


INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'f62ac0e3-c293-4453-b13c-2d694c509ef3', 0, N'حذف عکس', N'/Gallery/Photo/Delete', N'47c11542-f96b-4f80-8b79-39f88b4cff9e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'9105253c-557c-491a-a471-2edb7ddb0e7e', 0, N'جزئیات گالری', N'/Gallery/Gallery/Details', N'47c11542-f96b-4f80-8b79-39f88b4cff9e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'51487a16-ef3e-4ea8-93ce-320636dec3e4', NULL, N'حذف', N'/Gallery/webdesigngallery/delete', N'6fc9bc15-9fff-48e5-b380-8c736719a335', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'47c11542-f96b-4f80-8b79-39f88b4cff9e', 0, N'گالری', N'/Gallery/Gallery/', NULL, 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'd84d5f0c-3f95-415c-ba4d-434e93c9c73a', NULL, N'ویرایش', N'/Gallery/webdesigngallery/edit', N'6fc9bc15-9fff-48e5-b380-8c736719a335', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'1ab4c1e5-b99f-4cb0-9675-49040f57af59', 0, N'عکس ها', N'/Gallery/Photo', N'47c11542-f96b-4f80-8b79-39f88b4cff9e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'ac567c20-16e9-40d2-80b5-56b831f6e9c2', 0, N'حذف گالری', N'/Gallery/Gallery/Delete', N'47c11542-f96b-4f80-8b79-39f88b4cff9e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'17747327-9ff3-4ff8-9988-5ccf781ca05b', 0, N'ویرایش عکس', N'/Gallery/Photo/Edit', N'47c11542-f96b-4f80-8b79-39f88b4cff9e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'6fc9bc15-9fff-48e5-b380-8c736719a335', NULL, N'گالری تصاویر', N'/Gallery/webdesigngallery', NULL, 0, 1, 1, NULL, 10)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'eae5868f-8433-4c3a-9b9e-9cf7293c03ab', 0, N'ایجاد گالری', N'/Gallery/Gallery/Create', N'47c11542-f96b-4f80-8b79-39f88b4cff9e', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'3e352bf2-9e93-4aba-9283-9f9159bac6ef', 0, N'ویرایش گالری', N'/Gallery/Gallery/Edit', N'47c11542-f96b-4f80-8b79-39f88b4cff9e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'60021781-8f33-4b99-ae83-b6b7dbc22af5', NULL, N'لیست', N'/Gallery/webdesigngallery/index', N'6fc9bc15-9fff-48e5-b380-8c736719a335', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'1ace406b-d24e-4610-87b4-cb6a1e18b8ce', NULL, N'ایجاد', N'/Gallery/webdesigngallery/create', N'6fc9bc15-9fff-48e5-b380-8c736719a335', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'191c3b29-5e8d-42c2-84e1-dd89bab31055', 0, N'عکس جدید', N'/Gallery/Photo/Create', N'47c11542-f96b-4f80-8b79-39f88b4cff9e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'd89ad8d3-c4af-41e6-a6f9-f5548260210a', NULL, N'جزئیات', N'/Gallery/webdesigngallery/details', N'6fc9bc15-9fff-48e5-b380-8c736719a335', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'9aa98c2c-e04f-45d9-ab75-fa2cfd5c1be6', 0, N'مشاهده لیست', N'/Gallery/Gallery/Index', N'47c11542-f96b-4f80-8b79-39f88b4cff9e', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'51487a16-ef3e-4ea8-93ce-320636dec3e4')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'd84d5f0c-3f95-415c-ba4d-434e93c9c73a')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'6fc9bc15-9fff-48e5-b380-8c736719a335')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'60021781-8f33-4b99-ae83-b6b7dbc22af5')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'1ace406b-d24e-4610-87b4-cb6a1e18b8ce')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'd89ad8d3-c4af-41e6-a6f9-f5548260210a')
GO
