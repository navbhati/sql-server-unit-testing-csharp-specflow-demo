CREATE TABLE [dbo].[Customers]
(
	[CustomerId] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [CustomerName] NVARCHAR(50) NOT NULL, 
    [CompanyName] NVARCHAR(50) NULL, 
    [Phone] NVARCHAR(20) NULL, 
    [DateCreated] DATETIME NOT NULL
)
