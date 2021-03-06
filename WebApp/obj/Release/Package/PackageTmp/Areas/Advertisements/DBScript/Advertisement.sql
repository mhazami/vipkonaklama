/****** Object:  Schema [Advertisement]    Script Date: 10/24/2019 11:46:54 AM ******/
CREATE SCHEMA [Advertisement]
GO
/****** Object:  Table [Advertisement].[Advertisement]    Script Date: 10/24/2019 11:46:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Advertisement].[Advertisement](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[PositionId] [int] NOT NULL,
	[FromDate] [char](10) NULL,
	[ToDate] [char](10) NULL,
	[ClickCount] [int] NOT NULL,
	[VisitCount] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
	[FileId] [uniqueidentifier] NULL,
	[NavigateUrl] [nvarchar](400) NOT NULL,
	[Enable] [bit] NOT NULL,
	[Height] [smallint] NOT NULL,
	[width] [smallint] NOT NULL,
	[Text] [ntext] NULL,
	[Timeout] [int] NULL,
 CONSTRAINT [PK_Advertisement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Advertisement].[AdvertisementType]    Script Date: 10/24/2019 11:46:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Advertisement].[AdvertisementType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AdvertisementType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Advertisement].[Attribute]    Script Date: 10/24/2019 11:46:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Advertisement].[Attribute](
	[Id] [uniqueidentifier] NOT NULL,
	[TariffId] [uniqueidentifier] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Orientation] [bit] NOT NULL,
	[AdvertisemntId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AdvertisemetAttribute] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Advertisement].[CustomerAdvertisement]    Script Date: 10/24/2019 11:46:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Advertisement].[CustomerAdvertisement](
	[CustomerId] [uniqueidentifier] NOT NULL,
	[AdvertisementId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CustomerAdvertisement] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC,
	[AdvertisementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Advertisement].[History]    Script Date: 10/24/2019 11:46:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Advertisement].[History](
	[Id] [uniqueidentifier] NOT NULL,
	[AdvertisementId] [uniqueidentifier] NOT NULL,
	[FromDate] [char](10) NOT NULL,
	[ToDate] [char](10) NOT NULL,
	[ClickCount] [int] NOT NULL,
	[VisiteCount] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[TarrifClassHistoryId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AdvertisementHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Advertisement].[Position]    Script Date: 10/24/2019 11:46:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Advertisement].[Position](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Advertisement].[Section]    Script Date: 10/24/2019 11:46:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Advertisement].[Section](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[KeyWord] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AdvertisementSection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Advertisement].[SectionPosition]    Script Date: 10/24/2019 11:46:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Advertisement].[SectionPosition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[KeyWord] [varchar](50) NOT NULL,
	[SectionId] [int] NOT NULL,
	[AdsShowCount] [int] NOT NULL,
	[Orientation] [bit] NOT NULL,
 CONSTRAINT [PK_AdvertisementSectionPosition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AdvertisementSectionPosition] UNIQUE NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Advertisement].[Tariff]    Script Date: 10/24/2019 11:46:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Advertisement].[Tariff](
	[Id] [uniqueidentifier] NOT NULL,
	[TariffClassId] [uniqueidentifier] NOT NULL,
	[DayCount] [int] NULL,
	[ClickCount] [int] NULL,
	[VisitCount] [int] NULL,
	[Description] [nvarchar](100) NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Tariff] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Advertisement].[TariffClass]    Script Date: 10/24/2019 11:46:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Advertisement].[TariffClass](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[PerVisit] [int] NULL,
	[PerClick] [int] NULL,
	[PerDay] [int] NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[SectionPositionId] [int] NOT NULL,
 CONSTRAINT [PK_TariffClass] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Advertisement].[TariffClassHistory]    Script Date: 10/24/2019 11:46:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Advertisement].[TariffClassHistory](
	[Id] [uniqueidentifier] NOT NULL,
	[PositionId] [int] NOT NULL,
	[PerDay] [int] NOT NULL,
	[PerClick] [int] NOT NULL,
	[PerVisite] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[HistoryDate] [char](10) NOT NULL,
 CONSTRAINT [PK_TariffClassHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Advertisement].[TariffDefinitionClass]    Script Date: 10/24/2019 11:46:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Advertisement].[TariffDefinitionClass](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[PerVisit] [int] NOT NULL,
	[PerClick] [int] NOT NULL,
	[PerDay] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[SectionPositionId] [int] NOT NULL,
 CONSTRAINT [PK_TariffDefinition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [Advertisement].[Advertisement] ADD  CONSTRAINT [DF_Advertisement_ClickCount]  DEFAULT ((0)) FOR [ClickCount]
GO
ALTER TABLE [Advertisement].[Advertisement] ADD  CONSTRAINT [DF_Advertisement_VisitCount]  DEFAULT ((0)) FOR [VisitCount]
GO
ALTER TABLE [Advertisement].[Advertisement] ADD  CONSTRAINT [DF_Advertisement_NavigateUrl]  DEFAULT (N'http://') FOR [NavigateUrl]
GO
ALTER TABLE [Advertisement].[Advertisement] ADD  CONSTRAINT [DF_Advertisement_Timeout]  DEFAULT ((0)) FOR [Timeout]
GO
ALTER TABLE [Advertisement].[SectionPosition] ADD  CONSTRAINT [DF_AdvertisementSectionPosition_AdsShowCount]  DEFAULT ((0)) FOR [AdsShowCount]
GO
ALTER TABLE [Advertisement].[SectionPosition] ADD  CONSTRAINT [DF_AdvertisementSectionPosition_Orientation]  DEFAULT ((1)) FOR [Orientation]
GO
ALTER TABLE [Advertisement].[Advertisement]  WITH CHECK ADD  CONSTRAINT [FK_Advertisement_AdvertisementSectionPosition] FOREIGN KEY([PositionId])
REFERENCES [Advertisement].[SectionPosition] ([Id])
GO
ALTER TABLE [Advertisement].[Advertisement] CHECK CONSTRAINT [FK_Advertisement_AdvertisementSectionPosition]
GO
ALTER TABLE [Advertisement].[Advertisement]  WITH CHECK ADD  CONSTRAINT [FK_Advertisement_AdvertisementType] FOREIGN KEY([TypeId])
REFERENCES [Advertisement].[AdvertisementType] ([Id])
GO
ALTER TABLE [Advertisement].[Advertisement] CHECK CONSTRAINT [FK_Advertisement_AdvertisementType]
GO
ALTER TABLE [Advertisement].[Attribute]  WITH CHECK ADD  CONSTRAINT [FK_AdvertisemetAttribute_Advertisement] FOREIGN KEY([Id])
REFERENCES [Advertisement].[Advertisement] ([Id])
GO
ALTER TABLE [Advertisement].[Attribute] CHECK CONSTRAINT [FK_AdvertisemetAttribute_Advertisement]
GO
ALTER TABLE [Advertisement].[Attribute]  WITH CHECK ADD  CONSTRAINT [FK_AdvertisemetAttribute_Advertisement1] FOREIGN KEY([AdvertisemntId])
REFERENCES [Advertisement].[Advertisement] ([Id])
GO
ALTER TABLE [Advertisement].[Attribute] CHECK CONSTRAINT [FK_AdvertisemetAttribute_Advertisement1]
GO
ALTER TABLE [Advertisement].[Attribute]  WITH CHECK ADD  CONSTRAINT [FK_AdvertisemetAttribute_Tariff] FOREIGN KEY([TariffId])
REFERENCES [Advertisement].[Tariff] ([Id])
GO
ALTER TABLE [Advertisement].[Attribute] CHECK CONSTRAINT [FK_AdvertisemetAttribute_Tariff]
GO
ALTER TABLE [Advertisement].[CustomerAdvertisement]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAdvertisement_Advertisement] FOREIGN KEY([AdvertisementId])
REFERENCES [Advertisement].[Advertisement] ([Id])
GO
ALTER TABLE [Advertisement].[CustomerAdvertisement] CHECK CONSTRAINT [FK_CustomerAdvertisement_Advertisement]
GO
ALTER TABLE [Advertisement].[CustomerAdvertisement]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAdvertisement_EnterpriseNode] FOREIGN KEY([CustomerId])
REFERENCES [EnterpriseNode].[EnterpriseNode] ([Id])
GO
ALTER TABLE [Advertisement].[CustomerAdvertisement] CHECK CONSTRAINT [FK_CustomerAdvertisement_EnterpriseNode]
GO
ALTER TABLE [Advertisement].[History]  WITH CHECK ADD  CONSTRAINT [FK_AdvertisementHistory_Advertisement] FOREIGN KEY([AdvertisementId])
REFERENCES [Advertisement].[Advertisement] ([Id])
GO
ALTER TABLE [Advertisement].[History] CHECK CONSTRAINT [FK_AdvertisementHistory_Advertisement]
GO
ALTER TABLE [Advertisement].[History]  WITH CHECK ADD  CONSTRAINT [FK_AdvertisementHistory_AdvertisementHistory] FOREIGN KEY([TarrifClassHistoryId])
REFERENCES [Advertisement].[TariffClassHistory] ([Id])
GO
ALTER TABLE [Advertisement].[History] CHECK CONSTRAINT [FK_AdvertisementHistory_AdvertisementHistory]
GO
ALTER TABLE [Advertisement].[SectionPosition]  WITH CHECK ADD  CONSTRAINT [FK_AdvertisementSectionPosition_AdvertisementSection] FOREIGN KEY([SectionId])
REFERENCES [Advertisement].[Section] ([Id])
GO
ALTER TABLE [Advertisement].[SectionPosition] CHECK CONSTRAINT [FK_AdvertisementSectionPosition_AdvertisementSection]
GO
ALTER TABLE [Advertisement].[Tariff]  WITH CHECK ADD  CONSTRAINT [FK_Tariff_TariffClass] FOREIGN KEY([TariffClassId])
REFERENCES [Advertisement].[TariffClass] ([Id])
GO
ALTER TABLE [Advertisement].[Tariff] CHECK CONSTRAINT [FK_Tariff_TariffClass]
GO
ALTER TABLE [Advertisement].[TariffClass]  WITH CHECK ADD  CONSTRAINT [FK_TariffClass_AdvertisementSectionPosition] FOREIGN KEY([SectionPositionId])
REFERENCES [Advertisement].[SectionPosition] ([Id])
GO
ALTER TABLE [Advertisement].[TariffClass] CHECK CONSTRAINT [FK_TariffClass_AdvertisementSectionPosition]
GO
INSERT [Security].[Operation] ([Id], [Title], [Enabled], [Order], [Expand], [LogoId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'تبلیغات', 1, 1, 0, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'79cc5240-3cd2-4394-a41b-0f5f47becfec', NULL, N'حذف', N'/advertisements/advertisementsection/Delete', N'91f517d0-312c-412b-af31-4c707d01a0f7', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'6225ad42-b95a-4d33-b034-22749e3d765e', NULL, N'جزئیات', N'/advertisements/tariff/Details', N'c1daf8e2-c97f-49b4-85ae-7c10d59cbdd8', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'7975a24c-c0dd-4692-860e-3acd5d344e6e', NULL, N'نوع تبلیغ', N'/advertisements/advertisementtype/index', NULL, 2, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'2268ec07-5d11-41b4-bdf5-44c548cb4b81', 0, N'ایجاد', N'/Advertisements/Advertisement/Create', N'7b85c10a-2b1f-4431-87ce-44e9c2fe4aab', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'7b85c10a-2b1f-4431-87ce-44e9c2fe4aab', 0, N'تبلیغ', N'/Advertisements/Advertisement', NULL, 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'd00586dc-6490-4ef3-a4b5-46847bf56f82', NULL, N'ایجاد', N'/advertisements/advertisementsection/Create', N'91f517d0-312c-412b-af31-4c707d01a0f7', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'91f517d0-312c-412b-af31-4c707d01a0f7', NULL, N'موقعیت', N'/advertisements/advertisementsection/index', NULL, 3, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'2297ac77-98f1-49a2-9b2f-4def7f097505', NULL, N'حذف', N'/advertisements/advertisementtype/Delete', N'7975a24c-c0dd-4692-860e-3acd5d344e6e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'b7c42c2a-64a0-4716-bac8-5a1d9bbb2289', 0, N'مشاهده لیست', N'/Advertisements/Advertisement/Index', N'7b85c10a-2b1f-4431-87ce-44e9c2fe4aab', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'fea85e1a-b276-48d0-9073-5f6a284eee61', NULL, N'ایجاد', N'/advertisements/tariff/Create', N'c1daf8e2-c97f-49b4-85ae-7c10d59cbdd8', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'185ed2fc-779f-4908-afc8-66eab38ee74b', NULL, N'ویرایش', N'/advertisements/tariff/Edit', N'c1daf8e2-c97f-49b4-85ae-7c10d59cbdd8', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'ef861108-cc2a-4043-a7fd-6a5605832ac3', NULL, N'ایجاد', N'/advertisements/advertisementtype/Create', N'7975a24c-c0dd-4692-860e-3acd5d344e6e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'412d873b-069c-4fc6-8ecb-741f1dc2bbea', NULL, N'ویرایش', N'/advertisements/advertisementsection/Edit', N'91f517d0-312c-412b-af31-4c707d01a0f7', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'c1daf8e2-c97f-49b4-85ae-7c10d59cbdd8', NULL, N'تعرفه', N'/advertisements/tariff/index', NULL, 4, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'dc12380a-7cf2-4873-97fb-7e1b31d938f7', NULL, N'جزئیات', N'/advertisements/advertisementsection/Details', N'91f517d0-312c-412b-af31-4c707d01a0f7', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'2145b2d5-3230-4edf-adf2-83975028ec53', 0, N'ویرایش', N'/Advertisements/Advertisement/Edit', N'7b85c10a-2b1f-4431-87ce-44e9c2fe4aab', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'00da1cb4-0246-4e24-bf5b-933b04c3f013', NULL, N'ویرایش', N'/advertisements/advertisementtype/Edit', N'7975a24c-c0dd-4692-860e-3acd5d344e6e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'9531e9bd-a979-45a9-b7d2-95e380d5d6c5', NULL, N'حذف', N'/advertisements/tariff/Delete', N'c1daf8e2-c97f-49b4-85ae-7c10d59cbdd8', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'5fc7dfd7-7c10-4cc7-b90c-96031a181350', NULL, N'جزئیات', N'/advertisements/advertisementtype/Details', N'7975a24c-c0dd-4692-860e-3acd5d344e6e', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'9b319bd5-a95d-4b14-adfa-c11a71344ddf', 0, N'جزئیات', N'/Advertisements/Advertisement/Details', N'7b85c10a-2b1f-4431-87ce-44e9c2fe4aab', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'47eaeaba-c19a-411b-8741-d2a3041d6bd3', 0, N'حذف', N'/Advertisements/Advertisement/Delete', N'7b85c10a-2b1f-4431-87ce-44e9c2fe4aab', 0, 0, 1, NULL, NULL)
GO

INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'79cc5240-3cd2-4394-a41b-0f5f47becfec')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'6225ad42-b95a-4d33-b034-22749e3d765e')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'7975a24c-c0dd-4692-860e-3acd5d344e6e')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'7b85c10a-2b1f-4431-87ce-44e9c2fe4aab')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'd00586dc-6490-4ef3-a4b5-46847bf56f82')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'91f517d0-312c-412b-af31-4c707d01a0f7')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'2297ac77-98f1-49a2-9b2f-4def7f097505')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'fea85e1a-b276-48d0-9073-5f6a284eee61')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'185ed2fc-779f-4908-afc8-66eab38ee74b')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'ef861108-cc2a-4043-a7fd-6a5605832ac3')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'412d873b-069c-4fc6-8ecb-741f1dc2bbea')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'c1daf8e2-c97f-49b4-85ae-7c10d59cbdd8')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'dc12380a-7cf2-4873-97fb-7e1b31d938f7')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'00da1cb4-0246-4e24-bf5b-933b04c3f013')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'5fc7dfd7-7c10-4cc7-b90c-96031a181350')
GO