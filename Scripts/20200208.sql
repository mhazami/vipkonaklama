Create table [Reservation].[OfficeRoom]
(
Id uniqueidentifier not null,
OfficeId uniqueidentifier not null,
RoomId uniqueidentifier  not null
)
GO
alter table [Reservation].[OfficeRoom] ADD CONSTRAINT PK_OfficeRoom PRIMARY KEY CLUSTERED (Id);
Go
ALTER TABLE [Reservation].[OfficeRoom]
   ADD CONSTRAINT [FK_OfficeRoom_Office] FOREIGN KEY (OfficeId)
      REFERENCES [Reservation].[HotelOffice] (Id)      
      ON UPDATE CASCADE
GO
ALTER TABLE [Reservation].[OfficeRoom]
   ADD CONSTRAINT [FK_OfficeRoom_Room] FOREIGN KEY (RoomId)
      REFERENCES [Reservation].[Room] (Id)      
    
GO