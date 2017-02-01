CREATE PROCEDURE usp_PurchaseTicket (@CustomerID INT, @FlightID INT, @TicketPrice DECIMAL(8,2), @Class VARCHAR(6), @Seat VARCHAR(5))
AS
BEGIN
	DECLARE @customerBalance DECIMAL(10,2)  = (SELECT a.Balance FROM CustomerBankAccounts AS a WHERE a.CustomerID = @CustomerID)
	IF (@customerBalance< @TicketPrice)
	BEGIN
		RAISERROR('Insufficient bank account balance for ticket purchase.', 16, 1)
		RETURN
	END
	DECLARE @ticketID INT = (SELECT TOP 1 t.TicketID FROM Tickets AS t ORDER BY t.TicketID DESC) + 1
	INSERT INTO Tickets VALUES (@ticketID, @TicketPrice, @Class, @Seat, @CustomerID, @FlightID)
	UPDATE CustomerBankAccounts
	SET Balance -= @TicketPrice
	WHERE CustomerID = @CustomerID
END

--EXEC usp_PurchaseTicket 1, 1, 1, 'First', 'asd2'