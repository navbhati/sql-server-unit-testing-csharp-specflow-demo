CREATE FUNCTION [dbo].[CapitalizeFirstLetter]
(
    --string need to format
    @string nvarchar(50) --increase the variable size depending on your needs.
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
    DECLARE @ResultString nvarchar(50)
    SET @ResultString = @string
	-- Return the result of the function  
	RETURN upper(left(@ResultString,1)) + lower(substring(@ResultString,2,len(@ResultString))) 
END
