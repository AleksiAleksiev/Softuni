SELECT TOP 3 c.CustomerID, CONCAT(c.FirstName,' ',c.LastName) AS FullName, t.Price, a.AirportName AS Destination
FROM Customers AS c
INNER JOIN Tickets AS t
ON c.CustomerID = t.CustomerID
INNER JOIN Flights AS f
ON t.FlightID = f.FlightID
AND f.Status = 'Delayed'
INNER JOIN Airports AS a
ON f.DestinationAirportID = a.AirportID
ORDER BY t.Price DESC, c.CustomerID