CREATE TABLE [dbo].[Orders]
(
    [Id] int identity NOT NULL PRIMARY KEY, 
    [order_number] NCHAR(16) NOT NULL, 
    [order_datetime] DATETIME2 NOT NULL
)
