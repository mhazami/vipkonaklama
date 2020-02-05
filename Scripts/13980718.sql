
ALTER TABLE [WebDesign].Configuration ADD SMSAccountId int NULL 
GO
ALTER TABLE [WebDesign].Configuration ADD SMSAccountUserName varchar(20) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD SMSAccountPassword varchar(150) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD MailHost varchar(50) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD MailPassword varchar(100) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD MailUserName varchar(50) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD MailFrom varchar(50) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD MailPort smallint NULL
GO
ALTER TABLE [WebDesign].Configuration ADD EnableSSL bit  NULL 
GO
update [WebDesign].Configuration set EnableSSL=0
go
ALTER TABLE [WebDesign].Configuration alter column  EnableSSL bit not NULL 
GO

ALTER TABLE [WebDesign].Configuration ADD GroupEmailInterval smallint NULL
GO
ALTER TABLE [WebDesign].Configuration ADD DisscountCount smallint NULL
GO
ALTER TABLE [WebDesign].Configuration ADD MerchantId varchar(100) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD TerminalId int NULL
GO
ALTER TABLE [WebDesign].Configuration ADD BankId tinyint NULL
GO
ALTER TABLE [WebDesign].Configuration ADD TerminalUserName varchar(20) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD TerminalPassword varchar(4000) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD CertificatePath varchar(200) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD CertificatePassword varchar(200) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD MerchantPublicKey varchar(MAX) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD MerchantPrivateKey varchar(MAX) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD PaymentType varchar(20) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD BackgroundImage uniqueidentifier NULL
GO
ALTER TABLE [WebDesign].Configuration ADD BackgroundColor varchar(6) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD WebStatistics nvarchar(max) NULL
GO
ALTER TABLE [WebDesign].Configuration ADD HasFinancialOperation bit NULL
GO
ALTER TABLE [WebDesign].Configuration ADD RegisterEmailConfirm bit NULL
GO
ALTER TABLE [WebDesign].Configuration ADD UserRegisterInformType tinyint NULL
GO
ALTER TABLE [WebDesign].Configuration ADD EnableArticleComment bit NULL 
GO
ALTER TABLE [WebDesign].Configuration ADD EnableNewsComment bit NULL 
GO
ALTER TABLE [WebDesign].[Configuration]  WITH CHECK ADD  CONSTRAINT [FK_Configuration_MenuHtml] FOREIGN KEY([DefaultMenuHtmlId])
REFERENCES [ContentManage].[MenuHtml] ([Id])
GO
sp_rename '[Advertisement].AdvertisementAttribute','Attribute'
Go
sp_rename '[Advertisement].AdvertisementHistory','History'
GO
sp_rename '[Advertisement].AdvertisementSection','Section'
GO
sp_rename '[Advertisement].AdvertisementSectionPosition','SectionPosition'
GO
Insert Into [Security].[MenuGroup] Values(N'مدیریت محتوا', '349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61' , 1,NULL)
GO
Insert Into [Security].[MenuGroup] Values(N'مدیریت کاربران', '349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61' , 1,NULL)
GO
Insert Into [Security].[MenuGroup] Values(N'مدیریت سایت', '349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61' , 1,NULL)
Go
Insert Into [Security].[MenuGroup] Values(N'سایر امکانات', '349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61' , 1,NULL)
GO
INSERT [Security].[Operation] ([Id], [Title], [Enabled], [Order], [Expand], [LogoId]) VALUES (N'4207863d-b2ee-4ed0-bcf5-ef83a9d55d03', N'دفترچه تلفن', 1, 7, 0, NULL)
GO
INSERT [Security].[Operation] ([Id], [Title], [Enabled], [Order], [Expand], [LogoId]) VALUES (N'aee8af1e-319c-4610-91e6-0f2fd3691498', N'تبلیغات', 1, 1, 0, NULL)
GO
Insert Into [Security].Operation (Id,Title,LogoId,[Order],[Expand],[Enabled]) Values ('aee8af1e-319c-4610-91e6-0f2fd3691498',N'تبلیغات',NULL,1,0,1)
Go
insert into [Security].OperationMenu (OperationId,MenuId) values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','7B85C10A-2B1F-4431-87CE-44E9C2FE4AAB')
GO
  insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId) 
  values('91F517D0-312C-412B-AF31-4C707D01A0F7',NULL,N'موقعیت','/advertisements/advertisementsection/index',NULL,3,1,1,NULL,NULL,NULL)
  GO
  insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('D00586DC-6490-4EF3-A4B5-46847BF56F82',NULL,N'ایجاد','/advertisements/advertisementsection/Create','91F517D0-312C-412B-AF31-4C707D01A0F7',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','D00586DC-6490-4EF3-A4B5-46847BF56F82')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('412D873B-069C-4FC6-8ECB-741F1DC2BBEA',NULL,N'ویرایش','/advertisements/advertisementsection/Edit','91F517D0-312C-412B-AF31-4C707D01A0F7',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','412D873B-069C-4FC6-8ECB-741F1DC2BBEA')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('79CC5240-3CD2-4394-A41B-0F5F47BECFEC',NULL,N'حذف','/advertisements/advertisementsection/Delete','91F517D0-312C-412B-AF31-4C707D01A0F7',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','79CC5240-3CD2-4394-A41B-0F5F47BECFEC')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('DC12380A-7CF2-4873-97FB-7E1B31D938F7',NULL,N'جزئیات','/advertisements/advertisementsection/Details','91F517D0-312C-412B-AF31-4C707D01A0F7',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','DC12380A-7CF2-4873-97FB-7E1B31D938F7')
GO
  insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId) 
  values('7B85C10A-2B1F-4431-87CE-44E9C2FE4AAB',0,N'تبلیغ','/Advertisements/Advertisement',NULL,0,1,1,NULL,NULL,NULL)
  GO
  insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('A91DFC7B-5206-49ED-942B-52910BAF812A',NULL,N'ایجاد','/Advertisements/Advertisement/Create','7B85C10A-2B1F-4431-87CE-44E9C2FE4AAB',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','A91DFC7B-5206-49ED-942B-52910BAF812A')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('F863779D-FEC7-4EFA-AE73-7D2AEBB91105',NULL,N'ویرایش','/Advertisements/Advertisement/Edit','7B85C10A-2B1F-4431-87CE-44E9C2FE4AAB',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','F863779D-FEC7-4EFA-AE73-7D2AEBB91105')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('BB4839A1-D2DD-4330-A328-3B2D092E1B63',NULL,N'حذف','/Advertisements/Advertisement/Delete','7B85C10A-2B1F-4431-87CE-44E9C2FE4AAB',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','BB4839A1-D2DD-4330-A328-3B2D092E1B63')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('0A331734-818E-44E7-932B-E9AF82AEC195',NULL,N'جزئیات','/Advertisements/Advertisement/Details','7B85C10A-2B1F-4431-87CE-44E9C2FE4AAB',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','0A331734-818E-44E7-932B-E9AF82AEC195')
GO
    insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId) 
  values('7975A24C-C0DD-4692-860E-3ACD5D344E6E',NULL,N'نوع تبلیغ','/advertisements/advertisementtype/index',NULL,2,1,1,NULL,NULL,NULL)
  GO
  insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('EF861108-CC2A-4043-A7FD-6A5605832AC3',NULL,N'ایجاد','/advertisements/advertisementtype/Create','7975A24C-C0DD-4692-860E-3ACD5D344E6E',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','EF861108-CC2A-4043-A7FD-6A5605832AC3')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('00DA1CB4-0246-4E24-BF5B-933B04C3F013',NULL,N'ویرایش','/advertisements/advertisementtype/Edit','7975A24C-C0DD-4692-860E-3ACD5D344E6E',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','00DA1CB4-0246-4E24-BF5B-933B04C3F013')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('2297AC77-98F1-49A2-9B2F-4DEF7F097505',NULL,N'حذف','/advertisements/advertisementtype/Delete','7975A24C-C0DD-4692-860E-3ACD5D344E6E',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','2297AC77-98F1-49A2-9B2F-4DEF7F097505')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('5FC7DFD7-7C10-4CC7-B90C-96031A181350',NULL,N'جزئیات','/advertisements/advertisementtype/Details','7975A24C-C0DD-4692-860E-3ACD5D344E6E',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','5FC7DFD7-7C10-4CC7-B90C-96031A181350')
GO
  insert into [Security].OperationMenu (OperationId,MenuId) values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','91F517D0-312C-412B-AF31-4C707D01A0F7')
  GO
   insert into [Security].OperationMenu (OperationId,MenuId) values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','7B85C10A-2B1F-4431-87CE-44E9C2FE4AAB')
  GO
     insert into [Security].OperationMenu (OperationId,MenuId) values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','7975A24C-C0DD-4692-860E-3ACD5D344E6E')
  GO
  ALTER TABLE [WebDesign].Configuration ADD EnableAds BIT  NULL 
  GO
  update [WebDesign].Configuration set EnableAds=0
  go
    ALTER TABLE [WebDesign].Configuration alter column  EnableAds BIT NOT NULL 
  GO
   Go
  insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId) 
  values('C1DAF8E2-C97F-49B4-85AE-7C10D59CBDD8',NULL,N'تعرفه','/advertisements/tariff/index',NULL,4,1,1,NULL,NULL,NULL)
  Go
   insert into [Security].OperationMenu (OperationId,MenuId) values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','C1DAF8E2-C97F-49B4-85AE-7C10D59CBDD8')
  Go

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('fea85e1a-b276-48d0-9073-5f6a284eee61',NULL,N'ایجاد','/advertisements/tariff/Create','C1DAF8E2-C97F-49B4-85AE-7C10D59CBDD8',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','fea85e1a-b276-48d0-9073-5f6a284eee61')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('185ed2fc-779f-4908-afc8-66eab38ee74b',NULL,N'ویرایش','/advertisements/tariff/Edit','C1DAF8E2-C97F-49B4-85AE-7C10D59CBDD8',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','185ed2fc-779f-4908-afc8-66eab38ee74b')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('9531e9bd-a979-45a9-b7d2-95e380d5d6c5',NULL,N'حذف','/advertisements/tariff/Delete','C1DAF8E2-C97F-49B4-85AE-7C10D59CBDD8',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','9531e9bd-a979-45a9-b7d2-95e380d5d6c5')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('6225ad42-b95a-4d33-b034-22749e3d765e',NULL,N'جزئیات','/advertisements/tariff/Details','C1DAF8E2-C97F-49B4-85AE-7C10D59CBDD8',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('AEE8AF1E-319C-4610-91E6-0F2FD3691498','6225ad42-b95a-4d33-b034-22749e3d765e')
GO



insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
 values('54053383-00ED-4B2C-AA77-C00853F6A34B',NULL,N'حساب بانکی','/webdesign/webdesignaccount/index',NULL,4,1,1,NULL,NULL,13)
 GO
 insert into [Security].OperationMenu (OperationId,MenuId)values('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','54053383-00ED-4B2C-AA77-C00853F6A34B')
 GO
insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('8e0cfb16-68ac-4611-8d33-9d3a447e7ac9',NULL,N'ایجاد','/webdesign/webdesignaccount/Create','54053383-00ED-4B2C-AA77-C00853F6A34B',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','8e0cfb16-68ac-4611-8d33-9d3a447e7ac9')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('594e14e3-b23d-4782-ba85-a3fae7045c0e',NULL,N'ویرایش','/webdesign/webdesignaccount/Edit','54053383-00ED-4B2C-AA77-C00853F6A34B',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','594e14e3-b23d-4782-ba85-a3fae7045c0e')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('cdf25672-253f-418b-b521-fcac87e97171',NULL,N'حذف','/webdesign/webdesignaccount/Delete','54053383-00ED-4B2C-AA77-C00853F6A34B',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','cdf25672-253f-418b-b521-fcac87e97171')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('04441dcb-74ed-4382-82ea-418d2dd55108',NULL,N'جزئیات','/webdesign/webdesignaccount/Details','54053383-00ED-4B2C-AA77-C00853F6A34B',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('349883C3-5FC3-4E4A-BF41-8DA6BE7F8E61','04441dcb-74ed-4382-82ea-418d2dd55108')
GO

 update [Security].Menu set MenuGroupId=13 where Id='5510063E-24F3-452E-8516-767097F943A7'
 GO
  insert into [Security].OperationMenu (OperationId,MenuId)values('6BC5434D-E6A1-4545-9211-8E578BD8A03F','5510063E-24F3-452E-8516-767097F943A7')
  Go
 
CREATE SCHEMA PhoneBook;
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
CREATE TABLE [PhoneBook].[PersonAddress](
	[Id] [uniqueidentifier] NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL,
	[AddressTypeId] [smallint] NOT NULL,
 CONSTRAINT [PK_PersonAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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

CREATE TABLE [PhoneBook].[PhoneType](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[ShowSearchResult] [bit] not null,
 CONSTRAINT [PK_PhoneType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [PhoneBook].[Person](
	[Id] [uniqueidentifier] NOT NULL,
	[PersoneliCode] [nvarchar](50) NULL,
	[Remark] [nvarchar](max) NULL,
	[Enabled] [bit] NOT NULL,
	[Office] [nvarchar](max) NOT NULL,
	[Department] [nvarchar](max) NULL,
	[JopTitle] [nvarchar](500) NULL,
	[CityId] [int] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [PhoneBook].[Person] ADD  CONSTRAINT [DF_Person_Enabled]  DEFAULT ((1)) FOR [Enabled]
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
ALTER TABLE [PhoneBook].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_EnterpriseNode] FOREIGN KEY([Id])
REFERENCES [EnterpriseNode].[EnterpriseNode] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [PhoneBook].[Person] CHECK CONSTRAINT [FK_Person_EnterpriseNode]
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('FE334B7A-9125-41E9-AB0C-97A15D66C900',NULL,N'اشخاص','/phonebook/person/index',NULL,2,1,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','FE334B7A-9125-41E9-AB0C-97A15D66C900')
GO



insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('8778863a-7e95-4223-aaf4-71ec062e3cc8',NULL,N'ایجاد','/phonebook/Person/Create','FE334B7A-9125-41E9-AB0C-97A15D66C900',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','8778863a-7e95-4223-aaf4-71ec062e3cc8')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('468e7a60-2185-4d8e-a3ce-a0fad43034bf',NULL,N'ویرایش','/phonebook/Person/Edit','FE334B7A-9125-41E9-AB0C-97A15D66C900',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','468e7a60-2185-4d8e-a3ce-a0fad43034bf')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('349dcace-64b8-4a2a-9601-b2d559a45fe7',NULL,N'حذف','/phonebook/Person/Delete','FE334B7A-9125-41E9-AB0C-97A15D66C900',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','349dcace-64b8-4a2a-9601-b2d559a45fe7')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('a9520e47-2e31-4d21-88d6-d70b7c8e2d19',NULL,N'جزئیات','/phonebook/Person/Details','FE334B7A-9125-41E9-AB0C-97A15D66C900',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','a9520e47-2e31-4d21-88d6-d70b7c8e2d19')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('BD918A8F-DD2A-4D9F-889A-A747946BDD21',NULL,N'نوع آدرس','/phonebook/addresstype/index',NULL,0,1,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','BD918A8F-DD2A-4D9F-889A-A747946BDD21')
Go

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('060b2854-ad64-4a48-86f5-c7fe10e9538d',NULL,N'ایجاد','/phonebook/addresstype/Create','BD918A8F-DD2A-4D9F-889A-A747946BDD21',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','060b2854-ad64-4a48-86f5-c7fe10e9538d')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('9eff8d23-7486-4f20-a0a0-361f926575ba',NULL,N'ویرایش','/phonebook/addresstype/Edit','BD918A8F-DD2A-4D9F-889A-A747946BDD21',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','9eff8d23-7486-4f20-a0a0-361f926575ba')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('a6cc12a7-1a38-41a3-8a32-1c2993e6ec8a',NULL,N'حذف','/phonebook/addresstype/Delete','BD918A8F-DD2A-4D9F-889A-A747946BDD21',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','a6cc12a7-1a38-41a3-8a32-1c2993e6ec8a')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('3d63f959-c39c-4215-9348-63b8cf80d63a',NULL,N'جزئیات','/phonebook/addresstype/Details','BD918A8F-DD2A-4D9F-889A-A747946BDD21',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','3d63f959-c39c-4215-9348-63b8cf80d63a')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('2D2D1AB1-F7E7-4512-9C37-F999BF92F3F6',NULL,N'نوع تلفن','/phonebook/phonetype/index',NULL,1,1,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','2D2D1AB1-F7E7-4512-9C37-F999BF92F3F6')
Go




insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('ba4837c7-2b81-4dd4-a39c-2559b1a482bd',NULL,N'ایجاد','/phonebook/phonetype/Create','2D2D1AB1-F7E7-4512-9C37-F999BF92F3F6',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','ba4837c7-2b81-4dd4-a39c-2559b1a482bd')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('d55eb3c9-fd18-454b-b8b5-1ee8259f8af6',NULL,N'ویرایش','/phonebook/phonetype/Edit','2D2D1AB1-F7E7-4512-9C37-F999BF92F3F6',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','d55eb3c9-fd18-454b-b8b5-1ee8259f8af6')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('38cf02ac-f2fd-4d39-a178-1d2af6679fe7',NULL,N'حذف','/phonebook/phonetype/Delete','2D2D1AB1-F7E7-4512-9C37-F999BF92F3F6',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','38cf02ac-f2fd-4d39-a178-1d2af6679fe7')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('1211df37-613d-46f0-aa6f-40afc18d1816',NULL,N'جزئیات','/phonebook/phonetype/Details','2D2D1AB1-F7E7-4512-9C37-F999BF92F3F6',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','1211df37-613d-46f0-aa6f-40afc18d1816')
GO





CREATE TABLE [PhoneBook].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

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




INSERT INTO [PhoneBook].[Office]
           ([Description])
     (select distinct LTRIM(RTRIM([Office]))  from [PhoneBook].[Person] where [Office] is not null and [Office]<>''  group by [Office]
 )
GO


INSERT INTO [PhoneBook].[Department]
           ([Description])
     (select distinct LTRIM(RTRIM([Department]))  from [PhoneBook].[Person] where [Department] is not null and Department<>''  group by [Department] )
GO



alter table [PhoneBook].[Person] add OfficeId int
go
ALTER TABLE [PhoneBook].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Office] FOREIGN KEY([OfficeId])
REFERENCES [PhoneBook].[Office] ([Id])
GO
ALTER TABLE [PhoneBook].[Person] CHECK CONSTRAINT [FK_Person_Office]
GO
alter table [PhoneBook].[Person] add DepartmentId int
go
ALTER TABLE [PhoneBook].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Department] FOREIGN KEY([DepartmentId])
REFERENCES [PhoneBook].[Department] ([Id])
GO
ALTER TABLE [PhoneBook].[Person] CHECK CONSTRAINT [FK_Person_Department]
GO


update p1    set p1.[OfficeId]=of1.Id
from  [PhoneBook].[Person] as p1 inner join [PhoneBook].[Office]
as of1 on of1.Description=p1.Office where p1.Office is not null and p1.Office<>''


go

update p1    set p1.DepartmentId=of1.Id
from  [PhoneBook].[Person] as p1 inner join [PhoneBook].[Department]
as of1 on of1.Description=p1.Department where p1.Department is not null and p1.Department<>''
go
alter table [PhoneBook].[Person] drop column [Office]
go
alter table [PhoneBook].[Person] drop column [Department]
go

alter table [PhoneBook].[PersonAddress] add IsDefault bit
go
update [PhoneBook].[PersonAddress] set IsDefault=0
go

alter table [PhoneBook].[PersonAddress] alter column  IsDefault bit not null
go

INSERT INTO [Common].[LanguageContent]
           ([Key]
           ,[Value]
           ,[LanguageId]
           ,[IsDefault])
    (select 'PhoneBook.Department.'+cast(Id as nvarchar(100))+'.Title',[Description],'fa-IR',0 from [PhoneBook].[Department])
GO


INSERT INTO [Common].[LanguageContent]
           ([Key]
           ,[Value]
           ,[LanguageId]
           ,[IsDefault])
    (select 'PhoneBook.Office.'+cast(Id as nvarchar(100))+'.Title',[Description],'fa-IR',0 from [PhoneBook].[Office])
GO

update [PhoneBook].[Department] set [Description]=null
go
update [PhoneBook].[Office] set [Description]=null
go

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('4C3B1037-4554-4141-8CB9-A7D9D877ED89',NULL,N'دپارتمان','/phonebook/Department/index',NULL,0,1,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','4C3B1037-4554-4141-8CB9-A7D9D877ED89')
GO


insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('4eb9eca7-e156-430c-9675-4a77a460a168',NULL,N'ایجاد','/phonebook/Department/Create','4C3B1037-4554-4141-8CB9-A7D9D877ED89',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','4eb9eca7-e156-430c-9675-4a77a460a168')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('33e4eb57-23c3-43b3-938f-26eb21ea8c26',NULL,N'ویرایش','/phonebook/Department/Edit','4C3B1037-4554-4141-8CB9-A7D9D877ED89',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','33e4eb57-23c3-43b3-938f-26eb21ea8c26')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('ecedee2a-bd05-4b80-828b-5090e052b115',NULL,N'حذف','/phonebook/Department/Delete','4C3B1037-4554-4141-8CB9-A7D9D877ED89',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','ecedee2a-bd05-4b80-828b-5090e052b115')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('ae1f2ea6-f796-4921-a756-98df3efdb0db',NULL,N'جزئیات','/phonebook/Department/Details','4C3B1037-4554-4141-8CB9-A7D9D877ED89',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','ae1f2ea6-f796-4921-a756-98df3efdb0db')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('8CD4162D-B556-4F4E-A8A2-997146CA8195',NULL,N'اداره کل','/phonebook/Office/index',NULL,1,1,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','8CD4162D-B556-4F4E-A8A2-997146CA8195')
Go
insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('a6c5e000-4b2c-4bc6-803e-343c245eec27',NULL,N'ایجاد','/phonebook/Office/Create','8CD4162D-B556-4F4E-A8A2-997146CA8195',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','a6c5e000-4b2c-4bc6-803e-343c245eec27')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('4b6572c4-bc29-4544-82dd-b90595f37525',NULL,N'ویرایش','/phonebook/Office/Edit','8CD4162D-B556-4F4E-A8A2-997146CA8195',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','4b6572c4-bc29-4544-82dd-b90595f37525')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('4c56d322-25fa-4f87-904f-bb43e795ce55',NULL,N'حذف','/phonebook/Office/Delete','8CD4162D-B556-4F4E-A8A2-997146CA8195',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','4c56d322-25fa-4f87-904f-bb43e795ce55')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('ccb19063-b98f-4fe9-afad-5b546637e7da',NULL,N'جزئیات','/phonebook/Office/Details','8CD4162D-B556-4F4E-A8A2-997146CA8195',0,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','ccb19063-b98f-4fe9-afad-5b546637e7da')
GO







ALTER TABLE [EnterpriseNode].[EnterpriseNode] add CityId int
go
ALTER TABLE [EnterpriseNode].[EnterpriseNode] add ProvinceId int
go

ALTER TABLE [EnterpriseNode].[EnterpriseNode] add Fax varchar(50)
go

ALTER TABLE [EnterpriseNode].[EnterpriseNode]  WITH CHECK ADD  CONSTRAINT [FK_EnterpriseNodeCity] FOREIGN KEY([CityId])
REFERENCES [Common].[City] ([Id])
GO

ALTER TABLE [EnterpriseNode].[EnterpriseNode] CHECK CONSTRAINT [FK_EnterpriseNodeCity]
GO

ALTER TABLE [EnterpriseNode].[EnterpriseNode]  WITH CHECK ADD  CONSTRAINT [FK_EnterpriseNodeProvince] FOREIGN KEY([ProvinceId])
REFERENCES [Common].[Province] ([Id])
GO

ALTER TABLE [EnterpriseNode].[EnterpriseNode] CHECK CONSTRAINT [FK_EnterpriseNodeProvince]
GO



update p1    set p1.CityId=p1.CityId
from  [EnterpriseNode].[EnterpriseNode] as p1 inner join 
[PhoneBook].[Person] as P2 on P2.Id=p1.Id 
where P2.CityId is not null

go
update p1    set p1.ProvinceId=c1.ProvinceId
from  [EnterpriseNode].[EnterpriseNode] as p1 inner join 
[PhoneBook].[Person] as P2 on P2.Id=p1.Id inner join 
[Common].[City] as c1 on p1.CityId=c1.Id 
where P2.CityId is not null
go

alter table [PhoneBook].[Person] drop column [CityId]
go

alter table [PhoneBook].[Department] add OfficeId int 

go
ALTER TABLE [PhoneBook].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Office] FOREIGN KEY([OfficeId])
REFERENCES [PhoneBook].[Office] ([Id])
GO
ALTER TABLE [PhoneBook].[Department] CHECK CONSTRAINT [FK_Department_Office]
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

ALTER TABLE [PhoneBook].[Configuration]  WITH CHECK ADD  CONSTRAINT [FK_Configuration_WebSite] FOREIGN KEY([WebSiteId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO

ALTER TABLE [PhoneBook].[Configuration] CHECK CONSTRAINT [FK_Configuration_WebSite]
GO


insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('0A3FB002-D898-405E-8BC0-A4ABB3454305',NULL,N'تنظیمات','/phonebook/Configuration/GetConfiguration',NULL,1,1,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','0A3FB002-D898-405E-8BC0-A4ABB3454305')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('86CC1AE0-1550-417A-97A1-1DF1EEDE5A83',NULL,N'ویرایش','/phonebook/Configuration/Edit','0A3FB002-D898-405E-8BC0-A4ABB3454305',1,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','86CC1AE0-1550-417A-97A1-1DF1EEDE5A83')
GO

insert into [Security].Menu (Id,ApplicationID,Title,Url,ParentId,[Order],Display,[Enabled],HelpId,ImageId,MenuGroupId)
values('889E60A5-2C10-4D1F-9A0F-25D3B8F08A60',NULL,N'ویرایش','/phonebook/Configuration/Create','0A3FB002-D898-405E-8BC0-A4ABB3454305',1,0,1,NULL,NULL,NULL)
GO
insert into [Security].OperationMenu (OperationId,MenuId)values('4207863D-B2EE-4ED0-BCF5-EF83A9D55D03','889E60A5-2C10-4D1F-9A0F-25D3B8F08A60')
GO

CREATE TABLE [WebDesign].[WebSiteAlias](
	[Id] [uniqueidentifier] NOT NULL,
	[WebSiteId] [uniqueidentifier] NOT NULL,
	[Url] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_WebSiteAlias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [WebDesign].[WebSiteAlias]  WITH CHECK ADD  CONSTRAINT [FK_WebSiteAlias_WebSite] FOREIGN KEY([WebSiteId])
REFERENCES [WebDesign].[WebSite] ([Id])
GO

ALTER TABLE [WebDesign].[WebSiteAlias] CHECK CONSTRAINT [FK_WebSiteAlias_WebSite]
GO


alter table [WebDesign].[Configuration] add IntroPageUrl nvarchar(250)