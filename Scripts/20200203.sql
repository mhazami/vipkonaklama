alter table [Reservation].[Order] drop constraint [FK_Order_Room]
GO
alter table [Reservation].[Room] drop constraint PK_Room
go
alter table [Reservation].[Room] alter column [Id] smallint not null
go
alter table [Reservation].[Room] ADD CONSTRAINT PK_Room PRIMARY KEY CLUSTERED (Id);
go
alter table [Reservation].[Order] drop column [RoomId]
GO
alter table [Reservation].[Order] add  [RoomId] smallint
GO
ALTER TABLE [Reservation].[Order]
   ADD CONSTRAINT [FK_Order_Room] FOREIGN KEY (RoomID)
      REFERENCES [Reservation].[Room] (Id)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO
Alter Table [Reservation].[Order] alter column [CustomerId] uniqueidentifier NULL
GO
Alter Table [Reservation].[Order] alter column [UserId] uniqueidentifier NULL
GO
Alter Table [Reservation].[Order] alter column [EntryTime] char(5) NULL
GO
Alter Table [Reservation].[Order] alter column [ExitTime] char(5) NULL
GO