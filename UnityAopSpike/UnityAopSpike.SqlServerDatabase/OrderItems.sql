CREATE TABLE [dbo].[OrderItems]
(
    [Id] INT NOT NULL PRIMARY KEY, 
    [order_id] INT NOT NULL, 
    [quantity] SMALLINT NOT NULL, 
    CONSTRAINT [FK_OrderItems_ToOrders] FOREIGN KEY ([order_id]) REFERENCES [Orders]([Id]) 
)
