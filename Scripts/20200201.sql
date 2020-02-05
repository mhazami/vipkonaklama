
GO
ALTER TABLE [Reservation].[Order] ADD [Description] nvarchar(MAX) NULL
GO
ALTER TABLE [Reservation].[Customer] ADD Email nvarchar(100) NULL
GO
ALTER TABLE [Reservation].[Order] ADD PaymentType tinyint NULL
GO
ALTER TABLE [Reservation].[Order] ADD Code nvarchar(7) NULL
GO
ALTER TABLE [Reservation].[Order] ADD ReserveType tinyint NULL DEFAULT(0)
GO
ALTER TABLE [Reservation].[ReservePrice] ADD ReserveType tinyint NULL DEFAULT(0)
GO
