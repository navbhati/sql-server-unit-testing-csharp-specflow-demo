@debug
Feature: Process Order

Scenario: Process incoming orders and update orders table with latest order
	Given the following tables on 'SqlSampleDatabase' are empty:
		| table name         |
		| dbo.Orders         |
		| staging.IncomingOrders |
		And the table 'staging.IncomingOrders' on 'SqlSampleDatabase' contains the data:
		| OrderId [int] | CustomerId [nvarchar] | OrderDate [datetime] | OrderQuantity [int] |
		| 5745          | testuser              | 2020/07/06 17:59:59  | 4                   |
	When the 'proc.ProcessOrder' stored procedure on 'SqlSampleDatabase' with params is executed:
		| ParameterName | Value        | Type     |
		| ProcedureName | ProcessOrder | nvarchar |
	Then the table 'dbo.Orders' on 'SqlSampleDatabase' should only contain the data without strict ordering:
		| OrderId [int] | CustomerId [nvarchar] | OrderDate [datetime] | OrderQuantity [int] |
		| 5745          | Testuser              | 2020/07/06 17:59:59  | 4                   |


Scenario: Only process incoming orders and update orders table with latest order if its not already processed
	Given the following tables on 'SqlSampleDatabase' are empty:
		| table name         |
		| dbo.Orders         |
		| staging.IncomingOrders |
		And the table 'dbo.Orders' on 'SqlSampleDatabase' contains the data:
		| OrderId [int] | CustomerId [nvarchar] | OrderDate [datetime] | OrderQuantity [int] |
		| 5745          | testuser              | 2020/07/06 17:59:59  | 4                   |
		And the table 'staging.IncomingOrders' on 'SqlSampleDatabase' contains the data:
		| OrderId [int] | CustomerId [nvarchar] | OrderDate [datetime] | OrderQuantity [int] |
		| 5745          | testuser              | 2020/07/06 17:59:59  | 4                   |
		| 5755          | testuser              | 2020/07/07 18:59:59  | 1                   |
	When the 'proc.ProcessOrder' stored procedure on 'SqlSampleDatabase' with params is executed:
		| ParameterName | Value        | Type     |
		| ProcedureName | ProcessOrder | nvarchar |
	Then the table 'dbo.Orders' on 'SqlSampleDatabase' should only contain the data without strict ordering:
		| OrderId [int] | CustomerId [nvarchar] | OrderDate [datetime] | OrderQuantity [int] |
		| 5745          | testuser              | 2020/07/06 17:59:59  | 4                   |
		| 5755          | Testuser              | 2020/07/07 18:59:59  | 1                   |