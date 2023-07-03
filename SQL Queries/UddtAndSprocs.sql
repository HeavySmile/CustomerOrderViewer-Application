USE CustomerOrderViewer;

CREATE TYPE [dbo].[CustomerOrderType] AS TABLE
(
	CustomerOrderId INT NOT NULL,
	CustomerId INT NOT NULL,
	ItemId INT NOT NULL
);
GO

ALTER VIEW [dbo].[CustomerOrderDetail] AS
	SELECT 
		t1.CustomerOrderID, 
		t2.CustomerID, 
		t3.ItemId,
		t2.FirstName, 
		t2.LastName,
		t3.[Description],
		t3.Price,
		t1.ActiveInd
	FROM 
		dbo.CustomerOrder t1 
	INNER JOIN 
		dbo.Customer t2 ON t2.CustomerId = t1.CustomerId
	INNER JOIN
		dbo.Item t3 ON t3.ItemId = t1.ItemId
GO

CREATE PROCEDURE [dbo].[CustomerOrderDetail_GetList]
AS
	SELECT
		CustomerOrderID, 
		CustomerID, 
		ItemId,
		FirstName, 
		LastName,
		[Description],
		Price
	FROM dbo.CustomerOrderDetail
	WHERE ActiveInd = CONVERT(BIT, 1)
GO

CREATE PROCEDURE [dbo].[CustomerOrderDetail_Delete]
	@CustomerOrderId INT,
	@UserId VARCHAR(50)
AS
	UPDATE CustomerOrder
	SET
		ActiveInd = CONVERT(BIT, 0),
		UpdateID = @UserId,
		UpdateDate = GETDATE()
	WHERE
		CustomerOrderId = @CustomerOrderId AND ActiveInd = CONVERT(BIT, 1);
GO

CREATE PROCEDURE [dbo].[CustomerOrderDetail_Upsert]
	@CustomerOrderType CustomerOrderType READONLY,
	@UserId VARCHAR(50)
AS
	MERGE INTO CustomerOrder TARGET
	USING
	(
		SELECT
			CustomerOrderId,
			CustomerId,
			ItemId,
			@UserId [UpdateId],
			GETDATE() [UpdateDate],
			@UserId [CreateId],
			GETDATE() [CreateDate]
		FROM @CustomerOrderType
	) AS SOURCE
	ON
	(
		TARGET.CustomerOrderId = SOURCE.CustomerOrderId
	)
	WHEN MATCHED THEN
		UPDATE SET
			TARGET.CustomerId = SOURCE.CustomerId,
			TARGET.ItemId = SOURCE.ItemId,
			TARGET.UpdateId = SOURCE.UpdateId,
			TARGET.UpdateDate = SOURCE.UpdateDate
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (CustomerId, ItemId, CreateId, UpdateId, UpdateDate, ActiveInd)
		VALUES (SOURCE.CustomerID, SOURCE.ItemId, SOURCE.CreateId, SOURCE.UpdateID, SOURCE.UpdateDate, CONVERT(BIT, 1));
GO

CREATE PROCEDURE [dbo].[Customer_GetList]
AS
	SELECT
		[CustomerId],
		[FirstName],
		[MiddleName],
		[LastName],
		[Age]
	FROM [dbo].[Customer]
GO

CREATE PROCEDURE [dbo].[Item_GetList]
AS
	SELECT
		[ItemId],
		[Description],
		[Price]
	FROM [dbo].[Item]
GO