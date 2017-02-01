SELECT c.CustomerID, CONCAT(c.FirstName,' ', c.LastName) AS FullName, t.TownName AS Hometown
FROM Customers AS c
INNER JOIN Tickets AS ti
ON c.CustomerID = ti.CustomerID
INNER JOIN Flights AS f
ON ti.FlightID = f.FlightID
INNER JOIN Airports AS a
ON f.OriginAirportID = a.AirportID
INNER JOIN Towns AS t
ON a.TownID = t.TownID
WHERE c.HomeTownID = a.TownID AND f.Status = 'Departing'
ORDER BY c.CustomerID