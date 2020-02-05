


CREATE TABLE [Payment].[Account](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[BankId] [int] NOT NULL,
	[AccountNo] [nvarchar](100) NOT NULL,
	[IsExternal] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Payment].[DiscountType]    Script Date: 10/10/2019 12:01:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Payment].[DiscountType](
	[Id] [uniqueidentifier] NOT NULL,
	[Enabled] [bit] NOT NULL  ,
	[IsPercent] [bit] NOT NULL ,
	[Title] [nvarchar](150) NULL,
	[Remark] [nvarchar](500) NULL,
	[ForceAttach] [bit] NOT NULL ,
	[IsExternal] [bit] NOT NULL ,
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
/****** Object:  Table [Payment].[DiscountTypeAutoCode]    Script Date: 10/10/2019 12:01:58 PM ******/
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
/****** Object:  Table [Payment].[DiscountTypeSection]    Script Date: 10/10/2019 12:01:58 PM ******/
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
/****** Object:  Table [Payment].[Temp]    Script Date: 10/10/2019 12:01:58 PM ******/
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
	[Date] [datetime] NOT NULL DEFAULT (getdate()),
	[Description] [nvarchar](400) NULL,
	[CurrencyType] [tinyint] NOT NULL DEFAULT ((0)),
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
/****** Object:  Table [Payment].[TempDiscount]    Script Date: 10/10/2019 12:01:58 PM ******/
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
/****** Object:  Table [Payment].[Transaction]    Script Date: 10/10/2019 12:01:58 PM ******/
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
	[Done] [bit] NOT NULL CONSTRAINT [DF_Transaction_Done]  DEFAULT ((0)),
	[AccountId] [smallint] NULL,
	[DocScan] [uniqueidentifier] NULL,
	[CallBackUrl] [varchar](250) NULL,
	[AdditionalData] [nvarchar](max) NULL,
	[CurrencyType] [tinyint] NOT NULL DEFAULT ((0)),
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
/****** Object:  Table [Payment].[TransactionDiscount]    Script Date: 10/10/2019 12:01:58 PM ******/
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
alter table [ContentManage].[Content] alter  column [UserScript] nvarchar(MAX)
go




CREATE TABLE [WebDesign].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[WebId] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [varchar](200) NULL,
	[RegisterDate] [char](10) NOT NULL,
	[Comment] [nvarchar](1000) NULL,
	[Status] [tinyint] NOT NULL ,
	[ParentId] [uniqueidentifier] NULL,
	[Number] [bigint] IDENTITY(100000,1) NOT NULL,
 CONSTRAINT [PK_User_3] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [WebDesign].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_EnterpriseNode] FOREIGN KEY([Id])
REFERENCES [EnterpriseNode].[EnterpriseNode] ([Id])
GO

ALTER TABLE [WebDesign].[User] CHECK CONSTRAINT [FK_User_EnterpriseNode]
GO

ALTER TABLE [WebDesign].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO

ALTER TABLE [WebDesign].[User] CHECK CONSTRAINT [FK_User_WebSite]
GO

CREATE TABLE [WebDesign].[UserForms](
	[WebId] [uniqueidentifier] NOT NULL,
	[FormId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserForms] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[FormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



ALTER TABLE [WebDesign].[User]  WITH CHECK ADD  CONSTRAINT [FK_UserForm_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO

ALTER TABLE [WebDesign].[User] CHECK CONSTRAINT [FK_UserForm_WebSite]
GO
CREATE TABLE [WebDesign].[DiscountType](
	[WebId] [uniqueidentifier] NOT NULL,
	[DiscountTypeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WebDesignDiscountType] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[DiscountTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [WebDesign].[DiscountType]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignDiscountType_DiscountType] FOREIGN KEY([DiscountTypeId])
REFERENCES [Payment].[DiscountType] ([Id])
GO

ALTER TABLE [WebDesign].[DiscountType] CHECK CONSTRAINT [FK_WebDesignDiscountType_DiscountType]
GO

ALTER TABLE [WebDesign].[DiscountType]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignDiscountType_WebDesign] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO
ALTER TABLE [WebDesign].[DiscountType] CHECK CONSTRAINT [FK_WebDesignDiscountType_WebDesign]
GO

Insert Into [Security].[Menu] Values('ACD4920B-C097-4EBE-BD79-5DC2D14C70DB',0,N'مدیریت کاربران','/WebDesign/User/Index',NULL,0,1,1,NULL,NULL,11)
Go
Insert Into [Security].[OperationMenu] Values('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','ACD4920B-C097-4EBE-BD79-5DC2D14C70DB')
Go
Insert Into [Security].[Menu] Values('C9C230AD-BB6C-4BCC-B6B8-50C2A6516D77',0,N'ایجاد','/WebDesign/User/Create','ACD4920B-C097-4EBE-BD79-5DC2D14C70DB',0,0,1,NULL,NULL,11)
GO
Insert Into [Security].[OperationMenu] Values('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','C9C230AD-BB6C-4BCC-B6B8-50C2A6516D77')
GO
Insert Into [Security].[Menu] Values('536A80C8-55B8-4128-9C3B-A875386D49A8',0,N'ویرایش','/WebDesign/User/Edit','ACD4920B-C097-4EBE-BD79-5DC2D14C70DB',0,0,1,NULL,NULL,11)
GO
Insert Into [Security].[OperationMenu] Values('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','536A80C8-55B8-4128-9C3B-A875386D49A8')
Go
Insert Into [Security].[Menu] Values('9E4275DC-463C-460C-B6D2-F548C6F84E0A',0,N'حذف','/WebDesign/User/DeleteUser','ACD4920B-C097-4EBE-BD79-5DC2D14C70DB',0,0,1,NULL,NULL,11)
GO
Insert Into [Security].[OperationMenu] Values('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','9E4275DC-463C-460C-B6D2-F548C6F84E0A')
Go
Insert Into [Security].[Menu] Values('9B49D13B-E8E2-4066-AA3E-49563EEE7AA4',0,N'جزئیات','/WebDesign/User/Details','ACD4920B-C097-4EBE-BD79-5DC2D14C70DB',0,0,1,NULL,NULL,11)
GO
Insert Into [Security].[OperationMenu] Values('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','9B49D13B-E8E2-4066-AA3E-49563EEE7AA4')
Go
UPDATE [Security].[Menu] SET [Security].[Menu].[MenuGroupId] = 10
WHERE [Security].[Menu].[Id]  IN ('AE4B3A32-7AAB-40BF-AC37-03E0EB007E42','44293EE0-CA6B-4B7F-9A32-293DCC67D2D2' , 'ECECA015-B895-4FC7-A579-2BBB0F65F7AB' , 'E8A83ACF-18D7-48B9-9D30-436257773BC8',
'A8A8CE08-150F-4182-BF5E-4A66B2659ED4','930518EA-14E8-4FD1-A288-5FC9B5B7B42E' , 'BAE9CB12-8EF1-4F0B-9140-6FD30BE86721' , '6FC9BC15-9FFF-48E5-B380-8C736719A335' , '3F065BA6-50DE-4D8F-960A-9181C97D24F1',
'9DA4FF99-B595-4501-A931-B53A2FB4507D','60AF751D-30C8-4055-9180-E2A4D86D1611','0B50C83E-EBCE-4FE3-90FC-502BADF8CE88')
Go
UPDATE [Security].[Menu] SET [Security].[Menu].[MenuGroupId] = 11
WHERE [Security].[Menu].[Id]  IN ('ACD4920B-C097-4EBE-BD79-5DC2D14C70DB')
Go
UPDATE [Security].[Menu] SET [Security].[Menu].[MenuGroupId] = 12
WHERE [Security].[Menu].[Id]  IN ('7B4C8BEE-32DF-4509-946C-5A07D6F49EAC','860E6617-A9CA-464A-89F5-9EA3D3712A57')
Go
UPDATE [Security].[Menu] SET [Security].[Menu].[MenuGroupId] = 13
WHERE [Security].[Menu].[Id]  IN ('D0BB0246-BD26-42D2-9C70-A6FC224240FC','40178441-3C28-423A-B116-FB173E5D68A8')
go


INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [HelpId], [ImageId], [MenuGroupId]) VALUES (N'38c4cf3a-4a49-4a26-9d09-e446dbf118c3', 0, N'تخفیفات', N'/WebDesign/discounttype/', NULL, 18, 1, 1, NULL, NULL, 13)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [HelpId], [ImageId], [MenuGroupId]) VALUES (N'5528039f-791c-46e7-8d58-bbe933cf7d3b', 0, N'ایجاد', N'/WebDesign/discounttype/create', N'38c4cf3a-4a49-4a26-9d09-e446dbf118c3', 0, 1, 1, NULL, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [HelpId], [ImageId], [MenuGroupId]) VALUES (N'3161456f-fe4a-4b71-8bd5-5f97202ca779', 0, N'ویرایش', N'/WebDesign/discounttype/edit', N'38c4cf3a-4a49-4a26-9d09-e446dbf118c3', 0, 0, 1, NULL, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [HelpId], [ImageId], [MenuGroupId]) VALUES (N'87dcfc95-b5f4-41bf-8c1e-1b2bcb2c343c', 0, N'لیست', N'/WebDesign/discounttype/index', N'38c4cf3a-4a49-4a26-9d09-e446dbf118c3', 0, 1, 1, NULL, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [HelpId], [ImageId], [MenuGroupId]) VALUES (N'e1db2594-d080-45b5-8a46-30d63320623c', 0, N'حذف', N'/WebDesign/discounttype/delete', N'38c4cf3a-4a49-4a26-9d09-e446dbf118c3', 0, 0, 1, NULL, NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled], [HelpId], [ImageId], [MenuGroupId]) VALUES (N'f6266e9f-159e-4040-ba39-b3863c9a0b1e', 0, N'جزئیات', N'/WebDesign/discounttype/details', N'38c4cf3a-4a49-4a26-9d09-e446dbf118c3', 0, 0, 1, NULL, NULL, NULL)
GO


INSERT INTO [Security].[OperationMenu]
           ([OperationId]
           ,[MenuId])
     VALUES ('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','38c4cf3a-4a49-4a26-9d09-e446dbf118c3')
GO

INSERT INTO [Security].[OperationMenu]
           ([OperationId]
           ,[MenuId])
     VALUES ('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','5528039f-791c-46e7-8d58-bbe933cf7d3b')
GO
INSERT INTO [Security].[OperationMenu]
           ([OperationId]
           ,[MenuId])
     VALUES ('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','3161456f-fe4a-4b71-8bd5-5f97202ca779')
GO
INSERT INTO [Security].[OperationMenu]
           ([OperationId]
           ,[MenuId])
     VALUES ('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','87dcfc95-b5f4-41bf-8c1e-1b2bcb2c343c')
GO
INSERT INTO [Security].[OperationMenu]
           ([OperationId]
           ,[MenuId])
     VALUES ('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','e1db2594-d080-45b5-8a46-30d63320623c')
GO
INSERT INTO [Security].[OperationMenu]
           ([OperationId]
           ,[MenuId])
     VALUES ('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','f6266e9f-159e-4040-ba39-b3863c9a0b1e')
GO


CREATE TABLE [WebDesign].[Account](
	[WebId] [uniqueidentifier] NOT NULL,
	[AccountId] [smallint] NOT NULL,
 CONSTRAINT [PK_WebDesignAccount] PRIMARY KEY CLUSTERED 
(
	[WebId] ASC,
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [WebDesign].[Account]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignAccount_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO

ALTER TABLE [WebDesign].[Account] CHECK CONSTRAINT [FK_WebDesignAccount_WebSite]
GO


