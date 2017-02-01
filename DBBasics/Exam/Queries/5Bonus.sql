--IF (OBJECT_ID('ArrivedFlights') IS NULL)
--BEGIN
--	CREATE TABLE ArrivedFlights
--	(
--	FlightID INT PRIMARY KEY,
--	ArrivalTime DATETIME NOT NULL,
--	Origin VARCHAR(50) NOT NULL,
--	Destination VARCHAR(50) NOT NULL,
--	Passengers INT NOT NULL
--	)
--END

CREATE TRIGGER TR_LogArrived
ON Flights
AFTER UPDATE
AS
BEGIN
	DECLARE @flightID INT,
			@departureTime DATETIME,
			@arrivalTime DATETIME,
			@oldStatus VARCHAR(9),
			@newStatus VARCHAR(9),
			@originID int,
			@destinationID int,
			@airlineID int

	DECLARE kur CURSOR
	FOR
	SELECT i.FlightID, i.DepartureTime,i.ArrivalTime,i.[Status], i.OriginAirportID,i.DestinationAirportID,i.AirlineID,d.[Status]
	FROM inserted AS i
	INNER JOIN deleted AS d
	ON i.FlightID = d.FlightID
	OPEN kur
	FETCH NEXT FROM kur
	INTO @flightID, @departureTime, @arrivalTime, @newStatus, @originID, @destinationID, @airlineID, @oldStatus
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF (@oldStatus != @newStatus AND @newStatus = 'Arrived')
		BEGIN
			DECLARE @origin VARCHAR(50) = (SELECT a.AirportName 
											FROM Airports AS a 
											WHERE a.AirportID = @originID)
			DECLARE @destination VARCHAR(50) = (SELECT a.AirportName 
												FROM Airports AS a 
												WHERE a.AirportID = @destinationID)
			DECLARE @passengers INT = (SELECT COUNT(t.TicketID) 
										FROM Tickets AS t 
										WHERE t.FlightID = @flightID)
			INSERT INTO ArrivedFlights 
			VALUES (@flightID, @arrivalTime, @origin, @destination, @passengers)
		END
		FETCH NEXT FROM kur
		INTO @flightID, @departureTime, @arrivalTime, @newStatus, @originID, @destinationID, @airlineID, @oldStatus
	END
	CLOSE kur
    DEALLOCATE kur
END