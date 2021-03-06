/****** Object:  Schema [Payment]    Script Date: 10/24/2019 12:23:59 PM ******/
CREATE SCHEMA [Payment]
GO
/****** Object:  Table [Payment].[Account]    Script Date: 10/24/2019 12:23:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Payment].[Account](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[BankId] [int] NOT NULL,
	[AccountNo] [nvarchar](100) NOT NULL,
	[IsExternal] [bit] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Payment].[DiscountType]    Script Date: 10/24/2019 12:23:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Payment].[DiscountType](
	[Id] [uniqueidentifier] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[IsPercent] [bit] NOT NULL,
	[Title] [nvarchar](150) NULL,
	[Remark] [nvarchar](500) NULL,
	[ForceAttach] [bit] NOT NULL,
	[IsExternal] [bit] NOT NULL,
	[StartDate] [char](10) NULL,
	[EndDate] [char](10) NULL,
	[Code] [varchar](20) NULL,
	[Capacity] [smallint] NULL,
	[RemainCapacity] [smallint] NULL,
 CONSTRAINT [PK_DiscountType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Payment].[DiscountTypeAutoCode]    Script Date: 10/24/2019 12:23:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Payment].[DiscountTypeAutoCode](
	[Id] [uniqueidentifier] NOT NULL,
	[DiscountTypeId] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](20) NOT NULL,
	[Used] [bit] NOT NULL,
 CONSTRAINT [PK_DiscountTypeAutoCode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_DiscountTypeAutoCode] UNIQUE NONCLUSTERED 
(
	[DiscountTypeId] ASC,
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Payment].[DiscountTypeSection]    Script Date: 10/24/2019 12:23:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Payment].[DiscountTypeSection](
	[DiscountTypeId] [uniqueidentifier] NOT NULL,
	[MoudelName] [varchar](50) NOT NULL,
	[Section] [tinyint] NOT NULL,
 CONSTRAINT [PK_DiscountTypeSection] PRIMARY KEY CLUSTERED 
(
	[DiscountTypeId] ASC,
	[MoudelName] ASC,
	[Section] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Payment].[Temp]    Script Date: 10/24/2019 12:23:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Payment].[Temp](
	[Id] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PayerId] [uniqueidentifier] NULL,
	[PayerTitle] [nvarchar](150) NULL,
	[CallBackUrl] [varchar](250) NULL,
	[TransactionId] [uniqueidentifier] NULL,
	[Date] [datetime] NOT NULL,
	[Description] [nvarchar](400) NULL,
	[CurrencyType] [tinyint] NOT NULL,
	[AdditionalData] [nvarchar](max) NULL,
	[ParentId] [uniqueidentifier] NULL,
	[TrackYourOrderNum] [bigint] IDENTITY(10000100101,1) NOT NULL,
 CONSTRAINT [PK_Temp] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Payment].[TempDiscount]    Script Date: 10/24/2019 12:23:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Payment].[TempDiscount](
	[TempId] [uniqueidentifier] NOT NULL,
	[DiscountTypeId] [uniqueidentifier] NOT NULL,
	[AttachId] [uniqueidentifier] NULL,
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TempDiscount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Payment].[Transaction]    Script Date: 10/24/2019 12:23:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Payment].[Transaction](
	[Id] [uniqueidentifier] NOT NULL,
	[RefId] [varchar](50) NULL,
	[InvoiceId] [bigint] NULL,
	[SaleReferenceId] [bigint] NULL,
	[PayTypeId] [tinyint] NULL,
	[PayDate] [datetime] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Status] [int] NULL,
	[LastSentDate] [datetime] NULL,
	[LastReceiveDate] [datetime] NULL,
	[PayerId] [uniqueidentifier] NULL,
	[PayerTitle] [nvarchar](150) NULL,
	[DocNo] [nvarchar](50) NULL,
	[Done] [bit] NOT NULL,
	[AccountId] [smallint] NULL,
	[DocScan] [uniqueidentifier] NULL,
	[CallBackUrl] [varchar](250) NULL,
	[AdditionalData] [nvarchar](max) NULL,
	[CurrencyType] [tinyint] NOT NULL,
	[SaleTrackingId] [bigint] NULL,
	[OnlineBankId] [tinyint] NULL,
	[TrackYourOrderNum] [bigint] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Payment].[TransactionDiscount]    Script Date: 10/24/2019 12:23:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Payment].[TransactionDiscount](
	[TransactionId] [uniqueidentifier] NOT NULL,
	[DiscountTypeId] [uniqueidentifier] NOT NULL,
	[AttachId] [uniqueidentifier] NULL,
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TransactionDiscount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Payment].[WebDesignAccount]    Script Date: 10/24/2019 12:23:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Payment].[WebDesignAccount](
	[WebId] [uniqueidentifier] NOT NULL,
	[AccountId] [smallint] NOT NULL,
 CONSTRAINT [PK_WebDesignAccount] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Payment].[WebDesignDiscountType]    Script Date: 10/24/2019 12:23:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Payment].[WebDesignDiscountType](
	[WebId] [uniqueidentifier] NOT NULL,
	[DiscountTypeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WebDesignDiscountType] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[DiscountTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [Payment].[Account] ADD  DEFAULT ((0)) FOR [IsExternal]
GO
ALTER TABLE [Payment].[DiscountTypeAutoCode] ADD  CONSTRAINT [DF_DiscountTypeAutoCode_Used]  DEFAULT ((0)) FOR [Used]
GO
ALTER TABLE [Payment].[Temp] ADD  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [Payment].[Temp] ADD  DEFAULT ((0)) FOR [CurrencyType]
GO
ALTER TABLE [Payment].[Transaction] ADD  CONSTRAINT [DF_Transaction_Done]  DEFAULT ((0)) FOR [Done]
GO
ALTER TABLE [Payment].[Transaction] ADD  DEFAULT ((0)) FOR [CurrencyType]
GO
ALTER TABLE [Payment].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Bank] FOREIGN KEY([BankId])
REFERENCES [Common].[Bank] ([Id])
GO
ALTER TABLE [Payment].[Account] CHECK CONSTRAINT [FK_Account_Bank]
GO
ALTER TABLE [Payment].[DiscountTypeAutoCode]  WITH CHECK ADD  CONSTRAINT [FK_DiscountTypeAutoCode_DiscountType] FOREIGN KEY([DiscountTypeId])
REFERENCES [Payment].[DiscountType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Payment].[DiscountTypeAutoCode] CHECK CONSTRAINT [FK_DiscountTypeAutoCode_DiscountType]
GO
ALTER TABLE [Payment].[DiscountTypeSection]  WITH CHECK ADD  CONSTRAINT [FK_DiscountTypeSection_DiscountType] FOREIGN KEY([DiscountTypeId])
REFERENCES [Payment].[DiscountType] ([Id])
GO
ALTER TABLE [Payment].[DiscountTypeSection] CHECK CONSTRAINT [FK_DiscountTypeSection_DiscountType]
GO
ALTER TABLE [Payment].[TempDiscount]  WITH CHECK ADD  CONSTRAINT [FK_TempDiscount_DiscountType1] FOREIGN KEY([DiscountTypeId])
REFERENCES [Payment].[DiscountType] ([Id])
GO
ALTER TABLE [Payment].[TempDiscount] CHECK CONSTRAINT [FK_TempDiscount_DiscountType1]
GO
ALTER TABLE [Payment].[TempDiscount]  WITH CHECK ADD  CONSTRAINT [FK_TempDiscount_Temp] FOREIGN KEY([TempId])
REFERENCES [Payment].[Temp] ([Id])
GO
ALTER TABLE [Payment].[TempDiscount] CHECK CONSTRAINT [FK_TempDiscount_Temp]
GO
ALTER TABLE [Payment].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Account] FOREIGN KEY([AccountId])
REFERENCES [Payment].[Account] ([Id])
GO
ALTER TABLE [Payment].[Transaction] CHECK CONSTRAINT [FK_Transaction_Account]
GO
ALTER TABLE [Payment].[TransactionDiscount]  WITH CHECK ADD  CONSTRAINT [FK_TransactionDiscount_DiscountType] FOREIGN KEY([DiscountTypeId])
REFERENCES [Payment].[DiscountType] ([Id])
GO
ALTER TABLE [Payment].[TransactionDiscount] CHECK CONSTRAINT [FK_TransactionDiscount_DiscountType]
GO
ALTER TABLE [Payment].[TransactionDiscount]  WITH CHECK ADD  CONSTRAINT [FK_TransactionDiscount_Transaction] FOREIGN KEY([TransactionId])
REFERENCES [Payment].[Transaction] ([Id])
GO
ALTER TABLE [Payment].[TransactionDiscount] CHECK CONSTRAINT [FK_TransactionDiscount_Transaction]
GO
ALTER TABLE [Payment].[WebDesignAccount]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignAccount_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO
ALTER TABLE [Payment].[WebDesignAccount] CHECK CONSTRAINT [FK_WebDesignAccount_WebSite]
GO
ALTER TABLE [Payment].[WebDesignDiscountType]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignDiscountType_DiscountType] FOREIGN KEY([DiscountTypeId])
REFERENCES [Payment].[DiscountType] ([Id])
GO
ALTER TABLE [Payment].[WebDesignDiscountType] CHECK CONSTRAINT [FK_WebDesignDiscountType_DiscountType]
GO
ALTER TABLE [Payment].[WebDesignDiscountType]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignDiscountType_WebDesign] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO
ALTER TABLE [Payment].[WebDesignDiscountType] CHECK CONSTRAINT [FK_WebDesignDiscountType_WebDesign]
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'87dcfc95-b5f4-41bf-8c1e-1b2bcb2c343c', 0, N'لیست', N'/Payment/webdesigndiscounttype/index', N'38c4cf3a-4a49-4a26-9d09-e446dbf118c3', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'd03fdeb0-eceb-4cdd-b5a2-1bb48cfd5f86', 0, N'جزئیات', N'/Payment/DiscountType/Details', N'263bd8ac-8612-4772-800c-7c24c8f0ab08', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'910d99dd-d8d9-47f3-8f6e-1cde729e7baf', 0, N'لیست', N'/Payment/DiscountType/Index', N'263bd8ac-8612-4772-800c-7c24c8f0ab08', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'e1db2594-d080-45b5-8a46-30d63320623c', 0, N'حذف', N'/Payment/webdesigndiscounttype/delete', N'38c4cf3a-4a49-4a26-9d09-e446dbf118c3', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'8bbccac7-811a-4c38-9d5c-3c3041da7b5f', 0, N'ویرایش', N'/Payment/DiscountType/Edit', N'263bd8ac-8612-4772-800c-7c24c8f0ab08', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'04441dcb-74ed-4382-82ea-418d2dd55108', NULL, N'جزئیات', N'/Payment/webdesignaccount/Details', N'54053383-00ed-4b2c-aa77-c00853f6a34b', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'3161456f-fe4a-4b71-8bd5-5f97202ca779', 0, N'ویرایش', N'/Payment/webdesigndiscounttype/edit', N'38c4cf3a-4a49-4a26-9d09-e446dbf118c3', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'3225568d-f19b-415e-9d11-715b84b019d7', 0, N'ایجاد', N'/Payment/DiscountType/Create', N'263bd8ac-8612-4772-800c-7c24c8f0ab08', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'263bd8ac-8612-4772-800c-7c24c8f0ab08', 0, N'تخفیفات', N'/Payment/DiscountType', NULL, 0, 1, 1, N'eac3467a-d9f9-4986-a665-ba4a8b8e87a2', NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'8e0cfb16-68ac-4611-8d33-9d3a447e7ac9', NULL, N'ایجاد', N'/Payment/webdesignaccount/Create', N'54053383-00ed-4b2c-aa77-c00853f6a34b', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'594e14e3-b23d-4782-ba85-a3fae7045c0e', NULL, N'ویرایش', N'/Payment/webdesignaccount/Edit', N'54053383-00ed-4b2c-aa77-c00853f6a34b', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'f6266e9f-159e-4040-ba39-b3863c9a0b1e', 0, N'جزئیات', N'/Payment/webdesigndiscounttype/details', N'38c4cf3a-4a49-4a26-9d09-e446dbf118c3', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'5528039f-791c-46e7-8d58-bbe933cf7d3b', 0, N'ایجاد', N'/Payment/webdesigndiscounttype/create', N'38c4cf3a-4a49-4a26-9d09-e446dbf118c3', 0, 1, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'54053383-00ed-4b2c-aa77-c00853f6a34b', NULL, N'حساب بانکی', N'/Payment/webdesignaccount/index', NULL, 4, 1, 1, NULL, 13)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'4770b52f-c393-43ec-b725-c050f0c20fa8', 0, N'حذف', N'/Payment/DiscountType/Delete', N'263bd8ac-8612-4772-800c-7c24c8f0ab08', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'38c4cf3a-4a49-4a26-9d09-e446dbf118c3', 0, N'تخفیفات', N'/Payment/webdesigndiscounttype/', NULL, 18, 1, 1, NULL, 13)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [ImageId], [MenuGroupId]) VALUES (N'cdf25672-253f-418b-b521-fcac87e97171', NULL, N'حذف', N'/Payment/webdesignaccount/Delete', N'54053383-00ed-4b2c-aa77-c00853f6a34b', 0, 0, 1, NULL, NULL)
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'87dcfc95-b5f4-41bf-8c1e-1b2bcb2c343c')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'e1db2594-d080-45b5-8a46-30d63320623c')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'04441dcb-74ed-4382-82ea-418d2dd55108')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'3161456f-fe4a-4b71-8bd5-5f97202ca779')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'8e0cfb16-68ac-4611-8d33-9d3a447e7ac9')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'594e14e3-b23d-4782-ba85-a3fae7045c0e')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'f6266e9f-159e-4040-ba39-b3863c9a0b1e')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'5528039f-791c-46e7-8d58-bbe933cf7d3b')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'54053383-00ed-4b2c-aa77-c00853f6a34b')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'38c4cf3a-4a49-4a26-9d09-e446dbf118c3')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'349883c3-5fc3-4e4a-bf41-8da6be7f8e61', N'cdf25672-253f-418b-b521-fcac87e97171')
GO
