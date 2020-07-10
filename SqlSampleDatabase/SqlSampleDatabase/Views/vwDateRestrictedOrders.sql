CREATE VIEW [presentation].[vwDateRestrictedOrders]
	AS SELECT * FROM [Orders]
	WHERE OrderDate >= DATEADD(month, -1, GETDATE())