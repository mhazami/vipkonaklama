delete from [Reservation].[ReserveType]
go
EXEC sp_RENAME '[Reservation].[ReservePrice].[DayCount]' , 'DayType', 'COLUMN'
go
alter table [Reservation].[ReservePrice] add [FromHour] tinyint not null
GO
alter table [Reservation].[ReservePrice] add [ToHour] tinyint not null
Go
alter table [Reservation].[ReservePrice] drop constraint [FK_ReservePrice_ReserveType]
go
alter table [Reservation].[ReservePrice] drop column [ReserveTypeId]
go
alter table [Reservation].[Order] drop constraint [FK_Order_ReserveType]
go
alter table [Reservation].[Order] drop column [ReserveTypeId]
go
drop table  [Reservation].[ReserveType]