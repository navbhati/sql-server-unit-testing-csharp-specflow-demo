@debug
Feature: vwDateRestrictedOrders

Scenario: Check output of view with reduced dataset based on order date
	Given the following tables on 'SqlSampleDatabase' are empty:
		| table name         |
		| dbo.Orders         |
		And the table 'dbo.Orders' on 'SqlSampleDatabase' contains the data:
		| OrderId [int] | CustomerId [nvarchar] | OrderDate [datetime] | OrderQuantity [int] |
		| 5745          | testuser1             | 2020/05/06 17:59:59  | 4                   |
		| 5747          | testuser2             | 2020/06/20 16:59:59  | 7                   |
		| 5747          | testuser3             | 2020/07/09 15:59:59  | 1                   |
	Then the view 'presentation.vwDateRestrictedOrders' on 'SqlSampleDatabase' should only contain the data without strict ordering:
		| OrderId [int] | CustomerId [nvarchar] | OrderDate [datetime] | OrderQuantity [int] |
		| 5747          | testuser2             | 2020/06/20 16:59:59  | 7                   |
		| 5747          | testuser3             | 2020/07/09 15:59:59  | 1                   |