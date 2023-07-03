USE CustomerOrderViewer;

CREATE VIEW [dbo].[CustomerOrderDetail] AS
	SELECT 
		t1.CustomerOrderID, 
		t2.CustomerID, 
		t3.ItemId,
		t2.FirstName, 
		t2.LastName,
		t3.[Description],
		t3.Price
	FROM 
		dbo.CustomerOrder t1 
	INNER JOIN 
		dbo.Customer t2 ON t2.CustomerId = t1.CustomerId
	INNER JOIN
		dbo.Item t3 ON t3.ItemId = t1.ItemId