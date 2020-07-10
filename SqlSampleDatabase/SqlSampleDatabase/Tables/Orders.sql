CREATE TABLE [dbo].[Orders]
(
	[OrderId] INT NOT NULL PRIMARY KEY, 
    [CustomerId] NVARCHAR(50) NOT NULL, 
    [OrderDate] DATETIME NULL, 
    [OrderQuantity] INT NULL
)
