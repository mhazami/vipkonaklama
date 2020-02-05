alter table [WebDesign].[Configuration] add CRMServiceUserName Nvarchar(50)
go
alter table [WebDesign].[Configuration] add CRMServicePassword Nvarchar(200)
go
alter table [WebDesign].[Configuration] add CRMServiceUrl Nvarchar(200)
GO



CREATE TABLE [ContentManage].[Pages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](500) NULL,
	[HtmlId] [uniqueidentifier] NOT NULL,
	[Enabled] [bit] NOT NULL DEFAULT (0),
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ContentManage].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_HtmlDesgin] FOREIGN KEY([HtmlId])
REFERENCES [ContentManage].[HtmlDesgin] ([Id])
GO

ALTER TABLE [ContentManage].[Pages] CHECK CONSTRAINT [FK_Pages_HtmlDesgin]
GO




CREATE TABLE [ContentManage].[WebDesignPages](
	[WebId] [uniqueidentifier] NOT NULL,
	[PagesId] [int] NOT NULL,
 CONSTRAINT [PK_WebDesignPages] PRIMARY KEY CLUSTERED 
(
[WebId] ASC,
	[PagesId] ASC
	
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ContentManage].[WebDesignPages]  WITH CHECK ADD  CONSTRAINT [FK_WebDesignPages_WebSite] FOREIGN KEY([WebId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO

ALTER TABLE [ContentManage].[WebDesignPages] CHECK CONSTRAINT [FK_WebDesignPages_WebSite]
GO






insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],ImageId,MenuGroupId)
values('A0A7C6DD-C473-4E2E-ADCA-3BCE5F8FD7DA',NULL,N'صفحات','/ContentManager/WebDesignPages/index',NULL,1,1,1,NULL,10)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883c3-5fc3-4e4a-bf41-8da6be7f8e61','A0A7C6DD-C473-4E2E-ADCA-3BCE5F8FD7DA')
Go
insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],ImageId,MenuGroupId)
values('1E092269-E4BB-4E25-B020-01767153F4DE',NULL,N'ایجاد','/ContentManager/WebDesignPages/Create','A0A7C6DD-C473-4E2E-ADCA-3BCE5F8FD7DA',0,0,1,NULL,10)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883c3-5fc3-4e4a-bf41-8da6be7f8e61','1E092269-E4BB-4E25-B020-01767153F4DE')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],ImageId,MenuGroupId)
values('189D7FFF-A8B0-4E54-82F4-22DBBE2A9394',NULL,N'ویرایش','/ContentManager/WebDesignPages/Edit','A0A7C6DD-C473-4E2E-ADCA-3BCE5F8FD7DA',0,0,1,NULL,10)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883c3-5fc3-4e4a-bf41-8da6be7f8e61','189D7FFF-A8B0-4E54-82F4-22DBBE2A9394')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],ImageId,MenuGroupId)
values('44A99DF7-6795-4A7E-9C8E-4628D97A63BB',NULL,N'حذف','/ContentManager/WebDesignPages/Delete','A0A7C6DD-C473-4E2E-ADCA-3BCE5F8FD7DA',0,0,1,NULL,10)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883c3-5fc3-4e4a-bf41-8da6be7f8e61','44A99DF7-6795-4A7E-9C8E-4628D97A63BB')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],ImageId,MenuGroupId)
values('5FBFE0D4-DAB0-4744-A799-AF9360F8076B',NULL,N'جزئیات','/ContentManager/WebDesignPages/Details','A0A7C6DD-C473-4E2E-ADCA-3BCE5F8FD7DA',0,0,1,NULL,10)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883c3-5fc3-4e4a-bf41-8da6be7f8e61','5FBFE0D4-DAB0-4744-A799-AF9360F8076B')
GO

CREATE TABLE [ContentManage].[DefaultHtmlDesgin](
	[Id] [uniqueidentifier] NOT NULL,
    [Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_[DefaultHtmlDesgin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO


insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],ImageId,MenuGroupId)
values('B943F762-22AF-4FDD-BBB6-41DC7C542ED4',NULL,N'html پیش فرض','/ContentManager/DefaultHtmlDesgin/index',NULL,1,1,1,NULL,10)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883c3-5fc3-4e4a-bf41-8da6be7f8e61','B943F762-22AF-4FDD-BBB6-41DC7C542ED4')
Go
insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],ImageId,MenuGroupId)
values('044A9737-18ED-4A57-8086-F1D7151D9638',NULL,N'ایجاد','/ContentManager/DefaultHtmlDesgin/Create','B943F762-22AF-4FDD-BBB6-41DC7C542ED4',0,0,1,NULL,10)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883c3-5fc3-4e4a-bf41-8da6be7f8e61','044A9737-18ED-4A57-8086-F1D7151D9638')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],ImageId,MenuGroupId)
values('F1098DE4-E02A-4796-A3ED-89D9C989E75B',NULL,N'ویرایش','/ContentManager/DefaultHtmlDesgin/Edit','B943F762-22AF-4FDD-BBB6-41DC7C542ED4',0,0,1,NULL,10)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883c3-5fc3-4e4a-bf41-8da6be7f8e61','F1098DE4-E02A-4796-A3ED-89D9C989E75B')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],ImageId,MenuGroupId)
values('55FCEBAD-B296-43A8-BB01-8177BC381B12',NULL,N'حذف','/ContentManager/DefaultHtmlDesgin/Delete','B943F762-22AF-4FDD-BBB6-41DC7C542ED4',0,0,1,NULL,10)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883c3-5fc3-4e4a-bf41-8da6be7f8e61','55FCEBAD-B296-43A8-BB01-8177BC381B12')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],ImageId,MenuGroupId)
values('B8DE95EB-A8E4-43D0-8F90-45AD39A1A83B',NULL,N'جزئیات','/ContentManager/DefaultHtmlDesgin/Details','B943F762-22AF-4FDD-BBB6-41DC7C542ED4',0,0,1,NULL,10)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883c3-5fc3-4e4a-bf41-8da6be7f8e61','B8DE95EB-A8E4-43D0-8F90-45AD39A1A83B')
GO