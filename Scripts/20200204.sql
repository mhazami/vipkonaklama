update Reservation.[Order] set OrderDate ='2020/2/2'
go
Alter table [Reservation].[Order] alter column [OrderDate] datetime not null
go
update Reservation.[Order] set EntryDate ='2020/2/2'
go
Alter table [Reservation].[Order] alter column [EntryDate] datetime 
go
update Reservation.[Order] set ExitDate ='2020/2/2'
go
Alter table [Reservation].[Order] alter column [ExitDate] datetime 
GO
Alter Table [Reservation].[Order] Add OrderId int
Go

