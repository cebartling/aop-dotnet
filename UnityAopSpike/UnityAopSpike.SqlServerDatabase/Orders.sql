CREATE TABLE [dbo].[Orders]
(
    [Id] INT NOT NULL PRIMARY KEY, 
    [order_number] NCHAR(16) NOT NULL, 
    [order_timestamp] TIMESTAMP NOT NULL
)
