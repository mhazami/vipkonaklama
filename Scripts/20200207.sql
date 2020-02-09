Create Table Reservation.ReserveType
(
Id uniqueidentifier not null,
StartTime char(5) null,
EndTime char(5) null,
IsDaily bit default(0),
[Enabled] bit not null  default(1),
[Order] tinyint not null default(0) 
)
GO

Alter Table  [Reservation].[ReservePrice] drop constraint  [DF__ReservePr__Reser__3E3D3572]
Go
Alter Table [Reservation].[ReservePrice] drop column [ReserveType]
GO
Alter Table [Reservation].[ReservePrice] add [ReserveTypeId] uniqueidentifier null
GO
go
alter table [Reservation].ReserveType ADD CONSTRAINT PK_ReserveType PRIMARY KEY CLUSTERED (Id);
GO
ALTER TABLE [Reservation].[ReservePrice]
   ADD CONSTRAINT [FK_ReservePrice_ReserveType] FOREIGN KEY ([ReserveTypeId])
      REFERENCES [Reservation].ReserveType (Id)      
      ON UPDATE CASCADE
GO
Alter Table  [Reservation].[Order] drop constraint  [DF__Order__ReserveTy__3D491139]
GO
Alter Table [Reservation].[Order] drop column [ReserveType]
GO
Alter Table [Reservation].[Order] add [ReserveTypeId] uniqueidentifier null
GO

ALTER TABLE [Reservation].[Order]
   ADD CONSTRAINT [FK_Order_ReserveType] FOREIGN KEY ([ReserveTypeId])
      REFERENCES [Reservation].ReserveType (Id)      
      ON UPDATE CASCADE
GO

Create Table [Common].[Parish]
(
Id uniqueidentifier not null,
Name nvarchar(150) not null,
CityId int not null
);

GO
alter table [Common].[Parish] ADD CONSTRAINT PK_Parish PRIMARY KEY CLUSTERED (Id);
GO
ALTER TABLE [Common].[Parish]
   ADD CONSTRAINT [FK_Parish_City] FOREIGN KEY (CityId)
      REFERENCES [Common].[City] (Id)      
      ON UPDATE CASCADE
GO

Create Table [Reservation].[Hotel]
(
Id uniqueidentifier not null,
Name nvarchar(150) not null,
CountryId int not null,
ProvinceId int null,
CityId int null,
ParishId uniqueidentifier null,
[Enabled] bit not null default(1),
PicId  uniqueidentifier null,
GalleryId uniqueidentifier null,
[Address] nvarchar(300) null,
[Location] nvarchar(2000) null,
);
GO

alter table [Reservation].[Hotel] ADD CONSTRAINT PK_Hotel PRIMARY KEY CLUSTERED (Id);
GO

ALTER TABLE [Reservation].[Hotel]
   ADD CONSTRAINT [FK_Hotel_Country] FOREIGN KEY (CountryId)
      REFERENCES [Common].[Country] (Id)      
      ON UPDATE CASCADE

go
ALTER TABLE [Reservation].[Hotel]
   ADD CONSTRAINT [FK_Hotel_Province] FOREIGN KEY (ProvinceId)
      REFERENCES [Common].Province (Id)      
      ON UPDATE CASCADE

go
ALTER TABLE [Reservation].[Hotel]
   ADD CONSTRAINT [FK_Hotel_City] FOREIGN KEY (CityId)
      REFERENCES [Common].[City] (Id)      
      ON UPDATE CASCADE

go
ALTER TABLE [Reservation].[Hotel]
   ADD CONSTRAINT [FK_Hotel_Parish] FOREIGN KEY (ParishId)
      REFERENCES [Common].[Parish] (Id)  
go
Create Table [Reservation].[HotelFloor]
(
Id uniqueidentifier not null,
Name nvarchar(150) not null,
HotelId uniqueidentifier not null,
[Enabled] bit not null default(1)
);
GO
alter table [Reservation].[HotelFloor] ADD CONSTRAINT PK_HotelFloor PRIMARY KEY CLUSTERED (Id);
go
ALTER TABLE [Reservation].[HotelFloor]
   ADD CONSTRAINT [FK_Hotel_Floor] FOREIGN KEY (HotelId)
      REFERENCES [Reservation].[Hotel] (Id)      
      ON UPDATE CASCADE
GO

Alter Table [Reservation].[Room] Add FloorId uniqueidentifier null
GO
ALTER TABLE [Reservation].[Room]
   ADD CONSTRAINT [FK_Room_Floor] FOREIGN KEY (FloorId)
      REFERENCES [Reservation].[HotelFloor] (Id)      
      ON UPDATE CASCADE
GO

Create Table [Reservation].[HotelOffice]
(
Id uniqueidentifier not null,
Name nvarchar(150) not null,
HotelId uniqueidentifier not null,
[Enabled] bit not null default(1)
);
Go
alter table [Reservation].[HotelOffice] ADD CONSTRAINT PK_HotelOffice PRIMARY KEY CLUSTERED (Id);
GO
ALTER TABLE [Reservation].[HotelOffice]
   ADD CONSTRAINT [FK_Hotel_Office] FOREIGN KEY (HotelId)
      REFERENCES [Reservation].[Hotel] (Id)      
      ON UPDATE CASCADE
GO


Alter table [Reservation].[Order] drop constraint [FK_Order_Room]
Go

Alter table [Reservation].[Room] drop constraint [PK_Room]
Go
Alter table  [Reservation].[Room] drop column Id 
GO
Alter table  [Reservation].[Room] add Id uniqueidentifier  DEFAULT NEWID() NOT NULL;
GO

alter table [Reservation].[Room] ADD CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED (Id);
GO
Alter table  [Reservation].[Order] drop column [RoomId] 
GO
Alter Table  [Reservation].[Order] Add [RoomId] uniqueidentifier null
Go
ALTER TABLE [Reservation].[Order]
   ADD CONSTRAINT [FK_Order_Room] FOREIGN KEY (RoomId)
      REFERENCES [Reservation].[Room] (Id)      
      ON UPDATE CASCADE
GO
Alter Table [Reservation].[ReserveType] Add HotelId uniqueidentifier not null
GO
ALTER TABLE [Reservation].[ReserveType]
   ADD CONSTRAINT [FK_ReserveType_Hotel] FOREIGN KEY (HotelId)
      REFERENCES [Reservation].[Hotel] (Id)      
     
GO

Alter Table [Reservation].[RoomType] Add HotelId uniqueidentifier 
GO
ALTER TABLE [Reservation].[RoomType]
   ADD CONSTRAINT [FK_RoomType_Hotel] FOREIGN KEY (HotelId)
      REFERENCES [Reservation].[Hotel] (Id)      
     
GO


Create table [Reservation].UserHotelAccess
(
Id UNIQUEIDENTIFIER NOT NULL,
UserId uniqueidentifier not null,
HotelId uniqueidentifier not null,
OfficeId uniqueidentifier null,
RoomId	uniqueidentifier null
)
GO
ALTER TABLE Reservation.UserHotelAccess ADD CONSTRAINT [PK_UserHotelAccess] PRIMARY KEY CLUSTERED (Id);
GO
ALTER TABLE [Reservation].UserHotelAccess
   ADD CONSTRAINT [FK_UserHotelAccess_User] FOREIGN KEY (UserId)
      REFERENCES [Security].[User] (Id)      
      ON UPDATE CASCADE
GO
ALTER TABLE [Reservation].UserHotelAccess
   ADD CONSTRAINT [FK_UserHotelAccess_Hotel] FOREIGN KEY (HotelId)
      REFERENCES [Reservation].[Hotel] (Id)      
      ON UPDATE CASCADE
GO
ALTER TABLE [Reservation].UserHotelAccess
   ADD CONSTRAINT [FK_UserHotelAccess_Office] FOREIGN KEY (OfficeId)
      REFERENCES [Reservation].[HotelOffice] (Id)      
 

GO

ALTER TABLE [Reservation].UserHotelAccess
   ADD CONSTRAINT [FK_UserHotelAccess_Room] FOREIGN KEY (RoomId)
      REFERENCES [Reservation].[Room] (Id)      
      
GO
ALTER TABLE [Reservation].UserHotelAccess add CONSTRAINT [UserHotelAccessUniq] UNIQUE NONCLUSTERED
(
UserId,HotelId ,OfficeId ,RoomId	
)
GO
--Parish Menu
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'83f86a44-75dd-41c5-947a-384d1b3cb61d', 0, N'محله ها', N'/Common/Parish/Index',null, 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'83f86a44-75dd-42c5-947a-384d1b3cb61d', 0, N'ایجاد', N'/Common/Parish/Create', N'83f86a44-75dd-41c5-947a-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'83f86a44-75dd-43c5-947a-384d1b3cb61d', 0, N'ویرایش', N'/Common/Parish/Edit', N'83f86a44-75dd-41c5-947a-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'83f86a44-75dd-44c5-947a-384d1b3cb61d', 0, N'جزییات', N'/Common/Parish/Details', N'83f86a44-75dd-41c5-947a-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'83f86a44-75dd-45c5-947a-384d1b3cb61d', 0, N'حذف', N'/Common/Parish/Delete', N'83f86a44-75dd-41c5-947a-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'6bc5434d-e6a1-4545-9211-8e578bd8a03f', N'83f86a44-75dd-41c5-947a-384d1b3cb61d')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'6bc5434d-e6a1-4545-9211-8e578bd8a03f', N'83f86a44-75dd-42c5-947a-384d1b3cb61d')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'6bc5434d-e6a1-4545-9211-8e578bd8a03f', N'83f86a44-75dd-43c5-947a-384d1b3cb61d')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'6bc5434d-e6a1-4545-9211-8e578bd8a03f', N'83f86a44-75dd-44c5-947a-384d1b3cb61d')
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'6bc5434d-e6a1-4545-9211-8e578bd8a03f', N'83f86a44-75dd-45c5-947a-384d1b3cb61d')
GO
--Hotel Menu
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, N'Otel', N'/Reservation/Hotel/Index',null, 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'83f86a44-75dd-42c5-9472-384d1b3cb61d', 0, N'ایجاد', N'/Reservation/Hotel/Create', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'83f86a44-75dd-43c5-9473-384d1b3cb61d', 0, N'ویرایش', N'/Reservation/Hotel/Edit', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'83f86a44-75dd-44c5-9474-384d1b3cb61d', 0, N'جزییات', N'/Reservation/Hotel/Details', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'83f86a44-75dd-45c5-9475-384d1b3cb61d', 0, N'حذف', N'/Reservation/Hotel/Delete', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'83f86a44-75dd-41c5-9471-384d1b3cb61d')
GO																	 
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'83f86a44-75dd-42c5-9472-384d1b3cb61d')
GO																	 
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'83f86a44-75dd-43c5-9473-384d1b3cb61d')
GO																	  
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'83f86a44-75dd-44c5-9474-384d1b3cb61d')
GO																	 
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'83f86a44-75dd-45c5-9475-384d1b3cb61d')
GO
--HotelOffice Menu
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, N'Ofis', N'/Reservation/HotelOffice/Index',N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'84f86a44-75dd-42c5-9472-384d1b3cb61d', 0, N'ایجاد', N'/Reservation/HotelOffice/Create', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'85f86a44-75dd-43c5-9473-384d1b3cb61d', 0, N'ویرایش', N'/Reservation/HotelOffice/Edit', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'86f86a44-75dd-44c5-9474-384d1b3cb61d', 0, N'جزییات', N'/Reservation/HotelOffice/Details', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'87f86a44-75dd-45c5-9475-384d1b3cb61d', 0, N'حذف', N'/Reservation/HotelOffice/Delete', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'83f86a44-75dd-41c5-9471-384d1b3cb61d')
GO																	 
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'84f86a44-75dd-42c5-9472-384d1b3cb61d')
GO																	 
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'85f86a44-75dd-43c5-9473-384d1b3cb61d')
GO																	  
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'86f86a44-75dd-44c5-9474-384d1b3cb61d')
GO																	 
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'87f86a44-75dd-45c5-9475-384d1b3cb61d')
GO
--HotelFloor Menu
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'83f86a44-751d-41c5-9471-384d1b3cb61d', 0, N'Otel Katlari', N'/Reservation/HotelFloor/Index',N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'84f86a44-752d-42c5-9472-384d1b3cb61d', 0, N'ایجاد', N'/Reservation/HotelFloor/Create', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'85f86a44-753d-43c5-9473-384d1b3cb61d', 0, N'ویرایش', N'/Reservation/HotelFloor/Edit', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'86f86a44-754d-44c5-9474-384d1b3cb61d', 0, N'جزییات', N'/Reservation/HotelFloor/Details', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'87f86a44-755d-45c5-9475-384d1b3cb61d', 0, N'حذف', N'/Reservation/HotelFloor/Delete', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'83f86a44-751d-41c5-9471-384d1b3cb61d')
GO																	 
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'84f86a44-752d-42c5-9472-384d1b3cb61d')
GO																	 
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'85f86a44-753d-43c5-9473-384d1b3cb61d')
GO																	  
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'86f86a44-754d-44c5-9474-384d1b3cb61d')
GO																	 
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'87f86a44-755d-45c5-9475-384d1b3cb61d')
GO
--UserHotelAccess Menu
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'83f86a44-751d-41c5-9471-384d1b3cb61d', 0, N'Otel Katlari', N'/Reservation/UserHotelAccess/Index',N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'84f86a44-752d-42c5-9472-384d1b3cb62d', 0, N'ایجاد', N'/Reservation/UserHotelAccess/Create', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'85f86a44-753d-43c5-9473-384d1b3cb63d', 0, N'ویرایش', N'/Reservation/UserHotelAccess/Edit', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'86f86a44-754d-44c5-9474-384d1b3cb64d', 0, N'جزییات', N'/Reservation/UserHotelAccess/Details', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[Menu] ([Id], [ApplicationID], [Title], [Url], [ParentId], [Order], [Display], [Enabled],  [ImageId], [MenuGroupId]) VALUES (N'87f86a44-755d-45c5-9475-384d1b3cb65d', 0, N'حذف', N'/Reservation/UserHotelAccess/Delete', N'83f86a44-75dd-41c5-9471-384d1b3cb61d', 0, 1, 1,  NULL, NULL)
GO
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'83f86a44-751d-41c5-9471-384d1b3cb61d')
GO																	 
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'84f86a44-752d-42c5-9472-384d1b3cb62d')
GO																	 
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'85f86a44-753d-43c5-9473-384d1b3cb63d')
GO																	  
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'86f86a44-754d-44c5-9474-384d1b3cb64d')
GO																	 
INSERT [Security].[OperationMenu] ([OperationId], [MenuId]) VALUES (N'8099C2C5-3D3E-4D17-91C5-E7AAB6BB13C1', N'87f86a44-755d-45c5-9475-384d1b3cb65d')
GO
