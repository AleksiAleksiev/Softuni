CREATE PROCEDURE usp_SubmitReview (@CustomerID INT, @ReviewContent VARCHAR(255), @ReviewGrade TINYINT, @AirlineName VARCHAR(30))
AS
BEGIN
	IF (@AirlineName NOT IN (SELECT a.AirlineName FROM Airlines AS a))
	BEGIN
		RAISERROR('Airline does not exist', 16,1)
		RETURN
	END
	DECLARE @airlineID INT = (SELECT a.AirlineID FROM Airlines AS a WHERE a.AirlineName = @AirlineName)
	DECLARE @id INT = (SELECT TOP 1 c.ReviewID FROM CustomerReviews AS c ORDER BY c.ReviewID DESC) + 1
	INSERT INTO CustomerReviews VALUES (@id, @ReviewContent, @ReviewGrade, @airlineID, @CustomerID)
END

--EXEC usp_SubmitReview 3, 'asdljasdjas', 5, 'USA Airlines'