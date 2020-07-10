
CREATE PROCEDURE [proc].[ProcessOrder]
(
	@ProcedureName nvarchar(100)
)
AS
BEGIN

 DECLARE @MaxKey int = 0
 SELECT @MaxKey =MAX(OrderId) from staging.IncomingOrders

 DECLARE @MaxOrderID int = 0
 SELECT @MaxOrderID =MAX(OrderId) from dbo.Orders
  
 if(@MaxOrderID IS NULL)
	SET @MaxOrderID = 0
 
 if (@MaxOrderID < @MaxKey)
	 BEGIN
		INSERT INTO [dbo].[Orders] (OrderId, CustomerId, OrderDate, OrderQuantity) 
		SELECT [OrderId], [dbo].[CapitalizeFirstLetter]([CustomerId]),[OrderDate],[OrderQuantity]
		FROM staging.IncomingOrders where OrderId = @MaxKey
	 END
END