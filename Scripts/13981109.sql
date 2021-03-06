
CREATE SCHEMA [Reservation]
GO
/****** Object:  Table [Reservation].[Customer]    Script Date: 1/29/2020 1:59:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Reservation].[Customer](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[PhoneNumber] [char](11) NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Reservation].[Order]    Script Date: 1/29/2020 1:59:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Reservation].[Order](
	[Id] [uniqueidentifier] NOT NULL,
	[RoomId] [tinyint] NOT NULL,
	[RoomTypeId] [tinyint] NOT NULL,
	[ReserveTypeId] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[PersonCount] [tinyint] NOT NULL,
	[DayCount] [tinyint] NULL,
	[HourCount] [tinyint] NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[PaymentStatus] [tinyint] NOT NULL,
	[OrderDate] [char](10) NOT NULL,
	[EntryDate] [char](10) NOT NULL,
	[ExitDate] [char](10) NOT NULL,
	[EntryTime] [char](5) NOT NULL,
	[ExitTime] [char](5) NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Reservation].[ReservePrice]    Script Date: 1/29/2020 1:59:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reservation].[ReservePrice](
	[Id] [uniqueidentifier] NOT NULL,
	[ReserveTypeId] [uniqueidentifier] NOT NULL,
	[DayCount] [tinyint] NULL,
	[PerDayPrice] [decimal](18, 2) NOT NULL,
	[RoomTypeId] [tinyint] NOT NULL,
 CONSTRAINT [PK_ReservePrice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Reservation].[ReserveType]    Script Date: 1/29/2020 1:59:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Reservation].[ReserveType](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Enable] [bit] NULL,
	[ReserveDate] [char](10) NOT NULL,
	[ReserveTime] [char](5) NOT NULL,
 CONSTRAINT [PK_ReserveType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Reservation].[Room]    Script Date: 1/29/2020 1:59:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reservation].[Room](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Idle] [bit] NOT NULL,
	[RoomTypeId] [tinyint] NOT NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Reservation].[RoomType]    Script Date: 1/29/2020 1:59:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reservation].[RoomType](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Enable] [bit] NULL,
	[PersonCapacity] [tinyint] NOT NULL,
	[PicId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_RoomType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [Reservation].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustomerId])
REFERENCES [Reservation].[Customer] ([Id])
GO
ALTER TABLE [Reservation].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [Reservation].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_ReserveType] FOREIGN KEY([ReserveTypeId])
REFERENCES [Reservation].[ReserveType] ([Id])
GO
ALTER TABLE [Reservation].[Order] CHECK CONSTRAINT [FK_Order_ReserveType]
GO
ALTER TABLE [Reservation].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Room] FOREIGN KEY([RoomId])
REFERENCES [Reservation].[Room] ([Id])
GO
ALTER TABLE [Reservation].[Order] CHECK CONSTRAINT [FK_Order_Room]
GO
ALTER TABLE [Reservation].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_RoomType] FOREIGN KEY([RoomTypeId])
REFERENCES [Reservation].[RoomType] ([Id])
GO
ALTER TABLE [Reservation].[Order] CHECK CONSTRAINT [FK_Order_RoomType]
GO
ALTER TABLE [Reservation].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([UserId])
REFERENCES [Security].[User] ([Id])
GO
ALTER TABLE [Reservation].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [Reservation].[ReservePrice]  WITH CHECK ADD  CONSTRAINT [FK_ReservePrice_ReserveType] FOREIGN KEY([ReserveTypeId])
REFERENCES [Reservation].[ReserveType] ([Id])
GO
ALTER TABLE [Reservation].[ReservePrice] CHECK CONSTRAINT [FK_ReservePrice_ReserveType]
GO
ALTER TABLE [Reservation].[ReservePrice]  WITH CHECK ADD  CONSTRAINT [FK_ReservePrice_RoomType] FOREIGN KEY([RoomTypeId])
REFERENCES [Reservation].[RoomType] ([Id])
GO
ALTER TABLE [Reservation].[ReservePrice] CHECK CONSTRAINT [FK_ReservePrice_RoomType]
GO
ALTER TABLE [Reservation].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_RoomType] FOREIGN KEY([RoomTypeId])
REFERENCES [Reservation].[RoomType] ([Id])
GO
ALTER TABLE [Reservation].[Room] CHECK CONSTRAINT [FK_Room_RoomType]
GO
