/****** Object:  Schema [PhoneBook]    Script Date: 10/24/2019 12:26:59 PM ******/
CREATE SCHEMA [PhoneBook]
GO
/****** Object:  Table [PhoneBook].[AddressType]    Script Date: 10/24/2019 12:26:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PhoneBook].[AddressType](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AddressType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [PhoneBook].[Configuration]    Script Date: 10/24/2019 12:26:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PhoneBook].[Configuration](
	[WebSiteId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED 
(
	[WebSiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [PhoneBook].[Department]    Script Date: 10/24/2019 12:26:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PhoneBook].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[OfficeId] [int] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [PhoneBook].[Office]    Script Date: 10/24/2019 12:26:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PhoneBook].[Office](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Office] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [PhoneBook].[Person]    Script Date: 10/24/2019 12:26:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PhoneBook].[Person](
	[Id] [uniqueidentifier] NOT NULL,
	[PersoneliCode] [nvarchar](50) NULL,
	[Remark] [nvarchar](max) NULL,
	[Enabled] [bit] NOT NULL,
	[JopTitle] [nvarchar](500) NULL,
	[OfficeId] [int] NULL,
	[DepartmentId] [int] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [PhoneBook].[PersonAddress]    Script Date: 10/24/2019 12:26:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PhoneBook].[PersonAddress](
	[Id] [uniqueidentifier] NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL,
	[AddressTypeId] [smallint] NOT NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_PersonAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [PhoneBook].[PersonPhone]    Script Date: 10/24/2019 12:26:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [PhoneBook].[PersonPhone](
	[Id] [uniqueidentifier] NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL,
	[TelNumber] [varchar](35) NOT NULL,
	[PhoneTypeId] [smallint] NOT NULL,
 CONSTRAINT [PK_PersonPhone] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [PhoneBook].[PhoneType]    Script Date: 10/24/2019 12:26:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PhoneBook].[PhoneType](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[ShowSearchResult] [bit] NOT NULL,
 CONSTRAINT [PK_PhoneType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [PhoneBook].[Person] ADD  CONSTRAINT [DF_Person_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [PhoneBook].[Configuration]  WITH CHECK ADD  CONSTRAINT [FK_Configuration_WebSite] FOREIGN KEY([WebSiteId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO
ALTER TABLE [PhoneBook].[Configuration] CHECK CONSTRAINT [FK_Configuration_WebSite]
GO
ALTER TABLE [PhoneBook].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Office] FOREIGN KEY([OfficeId])
REFERENCES [PhoneBook].[Office] ([Id])
GO
ALTER TABLE [PhoneBook].[Department] CHECK CONSTRAINT [FK_Department_Office]
GO
ALTER TABLE [PhoneBook].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Department] FOREIGN KEY([DepartmentId])
REFERENCES [PhoneBook].[Department] ([Id])
GO
ALTER TABLE [PhoneBook].[Person] CHECK CONSTRAINT [FK_Person_Department]
GO
ALTER TABLE [PhoneBook].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_EnterpriseNode] FOREIGN KEY([Id])
REFERENCES [EnterpriseNode].[EnterpriseNode] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [PhoneBook].[Person] CHECK CONSTRAINT [FK_Person_EnterpriseNode]
GO
ALTER TABLE [PhoneBook].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Office] FOREIGN KEY([OfficeId])
REFERENCES [PhoneBook].[Office] ([Id])
GO
ALTER TABLE [PhoneBook].[Person] CHECK CONSTRAINT [FK_Person_Office]
GO
ALTER TABLE [PhoneBook].[PersonAddress]  WITH CHECK ADD  CONSTRAINT [FK_PersonAddress_AddressType] FOREIGN KEY([AddressTypeId])
REFERENCES [PhoneBook].[AddressType] ([Id])
GO
ALTER TABLE [PhoneBook].[PersonAddress] CHECK CONSTRAINT [FK_PersonAddress_AddressType]
GO
ALTER TABLE [PhoneBook].[PersonAddress]  WITH CHECK ADD  CONSTRAINT [FK_PersonAddress_Person] FOREIGN KEY([PersonId])
REFERENCES [PhoneBook].[Person] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [PhoneBook].[PersonAddress] CHECK CONSTRAINT [FK_PersonAddress_Person]
GO
ALTER TABLE [PhoneBook].[PersonPhone]  WITH CHECK ADD  CONSTRAINT [FK_PersonPhone_Person] FOREIGN KEY([PersonId])
REFERENCES [PhoneBook].[Person] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [PhoneBook].[PersonPhone] CHECK CONSTRAINT [FK_PersonPhone_Person]
GO
ALTER TABLE [PhoneBook].[PersonPhone]  WITH CHECK ADD  CONSTRAINT [FK_PersonPhone_PhoneType] FOREIGN KEY([PhoneTypeId])
REFERENCES [PhoneBook].[PhoneType] ([Id])
GO
ALTER TABLE [PhoneBook].[PersonPhone] CHECK CONSTRAINT [FK_PersonPhone_PhoneType]
GO
INSERT [Security].[Operation] ([Id], [Title], [Enabled], [Order], [Expand], [LogoId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'دفترچه تلفن', 1, 7, 0, NULL)
GO

INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'a6cc12a7-1a38-41a3-8a32-1c2993e6ec8a', NULL, N'حذف', N'/phonebook/addresstype/Delete', N'bd918a8f-dd2a-4d9f-889a-a747946bdd21', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'38cf02ac-f2fd-4d39-a178-1d2af6679fe7', NULL, N'حذف', N'/phonebook/phonetype/Delete', N'2d2d1ab1-f7e7-4512-9c37-f999bf92f3f6', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'86cc1ae0-1550-417a-97a1-1df1eede5a83', NULL, N'ویرایش', N'/phonebook/Configuration/Edit', N'0a3fb002-d898-405e-8bc0-a4abb3454305', 1, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'd55eb3c9-fd18-454b-b8b5-1ee8259f8af6', NULL, N'ویرایش', N'/phonebook/phonetype/Edit', N'2d2d1ab1-f7e7-4512-9c37-f999bf92f3f6', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'ba4837c7-2b81-4dd4-a39c-2559b1a482bd', NULL, N'ایجاد', N'/phonebook/phonetype/Create', N'2d2d1ab1-f7e7-4512-9c37-f999bf92f3f6', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'889e60a5-2c10-4d1f-9a0f-25d3b8f08a60', NULL, N'ویرایش', N'/phonebook/Configuration/Create', N'0a3fb002-d898-405e-8bc0-a4abb3454305', 1, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'33e4eb57-23c3-43b3-938f-26eb21ea8c26', NULL, N'ویرایش', N'/phonebook/Department/Edit', N'4c3b1037-4554-4141-8cb9-a7d9d877ed89', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'a6c5e000-4b2c-4bc6-803e-343c245eec27', NULL, N'ایجاد', N'/phonebook/Office/Create', N'8cd4162d-b556-4f4e-a8a2-997146ca8195', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'9eff8d23-7486-4f20-a0a0-361f926575ba', NULL, N'ویرایش', N'/phonebook/addresstype/Edit', N'bd918a8f-dd2a-4d9f-889a-a747946bdd21', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'1211df37-613d-46f0-aa6f-40afc18d1816', NULL, N'جزئیات', N'/phonebook/phonetype/Details', N'2d2d1ab1-f7e7-4512-9c37-f999bf92f3f6', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'4eb9eca7-e156-430c-9675-4a77a460a168', NULL, N'ایجاد', N'/phonebook/Department/Create', N'4c3b1037-4554-4141-8cb9-a7d9d877ed89', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'ecedee2a-bd05-4b80-828b-5090e052b115', NULL, N'حذف', N'/phonebook/Department/Delete', N'4c3b1037-4554-4141-8cb9-a7d9d877ed89', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'ccb19063-b98f-4fe9-afad-5b546637e7da', NULL, N'جزئیات', N'/phonebook/Office/Details', N'8cd4162d-b556-4f4e-a8a2-997146ca8195', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'3d63f959-c39c-4215-9348-63b8cf80d63a', NULL, N'جزئیات', N'/phonebook/addresstype/Details', N'bd918a8f-dd2a-4d9f-889a-a747946bdd21', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'8778863a-7e95-4223-aaf4-71ec062e3cc8', NULL, N'ایجاد', N'/phonebook/Person/Create', N'fe334b7a-9125-41e9-ab0c-97a15d66c900', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'fe334b7a-9125-41e9-ab0c-97a15d66c900', NULL, N'اشخاص', N'/phonebook/person/index', NULL, 2, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'ae1f2ea6-f796-4921-a756-98df3efdb0db', NULL, N'جزئیات', N'/phonebook/Department/Details', N'4c3b1037-4554-4141-8cb9-a7d9d877ed89', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'8cd4162d-b556-4f4e-a8a2-997146ca8195', NULL, N'اداره کل', N'/phonebook/Office/index', NULL, 1, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'468e7a60-2185-4d8e-a3ce-a0fad43034bf', NULL, N'ویرایش', N'/phonebook/Person/Edit', N'fe334b7a-9125-41e9-ab0c-97a15d66c900', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'0a3fb002-d898-405e-8bc0-a4abb3454305', NULL, N'تنظیمات', N'/phonebook/Configuration/GetConfiguration', NULL, 1, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'bd918a8f-dd2a-4d9f-889a-a747946bdd21', NULL, N'نوع آدرس', N'/phonebook/addresstype/index', NULL, 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'4c3b1037-4554-4141-8cb9-a7d9d877ed89', NULL, N'دپارتمان', N'/phonebook/Department/index', NULL, 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'349dcace-64b8-4a2a-9601-b2d559a45fe7', NULL, N'حذف', N'/phonebook/Person/Delete', N'fe334b7a-9125-41e9-ab0c-97a15d66c900', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'4b6572c4-bc29-4544-82dd-b90595f37525', NULL, N'ویرایش', N'/phonebook/Office/Edit', N'8cd4162d-b556-4f4e-a8a2-997146ca8195', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'4c56d322-25fa-4f87-904f-bb43e795ce55', NULL, N'حذف', N'/phonebook/Office/Delete', N'8cd4162d-b556-4f4e-a8a2-997146ca8195', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'060b2854-ad64-4a48-86f5-c7fe10e9538d', NULL, N'ایجاد', N'/phonebook/addresstype/Create', N'bd918a8f-dd2a-4d9f-889a-a747946bdd21', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'a9520e47-2e31-4d21-88d6-d70b7c8e2d19', NULL, N'جزئیات', N'/phonebook/Person/Details', N'fe334b7a-9125-41e9-ab0c-97a15d66c900', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'2d2d1ab1-f7e7-4512-9c37-f999bf92f3f6', NULL, N'نوع تلفن', N'/phonebook/phonetype/index', NULL, 1, 1, 1, NULL, NULL)
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'a6cc12a7-1a38-41a3-8a32-1c2993e6ec8a')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'38cf02ac-f2fd-4d39-a178-1d2af6679fe7')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'86cc1ae0-1550-417a-97a1-1df1eede5a83')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'd55eb3c9-fd18-454b-b8b5-1ee8259f8af6')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'ba4837c7-2b81-4dd4-a39c-2559b1a482bd')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'889e60a5-2c10-4d1f-9a0f-25d3b8f08a60')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'33e4eb57-23c3-43b3-938f-26eb21ea8c26')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'a6c5e000-4b2c-4bc6-803e-343c245eec27')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'9eff8d23-7486-4f20-a0a0-361f926575ba')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'1211df37-613d-46f0-aa6f-40afc18d1816')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'4eb9eca7-e156-430c-9675-4a77a460a168')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'ecedee2a-bd05-4b80-828b-5090e052b115')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'ccb19063-b98f-4fe9-afad-5b546637e7da')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'3d63f959-c39c-4215-9348-63b8cf80d63a')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'8778863a-7e95-4223-aaf4-71ec062e3cc8')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'fe334b7a-9125-41e9-ab0c-97a15d66c900')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'ae1f2ea6-f796-4921-a756-98df3efdb0db')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'8cd4162d-b556-4f4e-a8a2-997146ca8195')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'468e7a60-2185-4d8e-a3ce-a0fad43034bf')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'0a3fb002-d898-405e-8bc0-a4abb3454305')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'bd918a8f-dd2a-4d9f-889a-a747946bdd21')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'4c3b1037-4554-4141-8cb9-a7d9d877ed89')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'349dcace-64b8-4a2a-9601-b2d559a45fe7')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'4b6572c4-bc29-4544-82dd-b90595f37525')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'4c56d322-25fa-4f87-904f-bb43e795ce55')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'060b2854-ad64-4a48-86f5-c7fe10e9538d')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'a9520e47-2e31-4d21-88d6-d70b7c8e2d19')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'2d2d1ab1-f7e7-4512-9c37-f999bf92f3f6')
GO