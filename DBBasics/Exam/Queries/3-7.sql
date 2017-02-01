SELECT DISTINCT c.CustomerID, CONCAT(c.FirstName, ' ', c.LastName) AS FullName, DATEDIFF(YEAR, c.DateOfBirth, '2016') AS Age
FROM Customers AS c
INNER JOIN Tickets AS t
ON c.CustomerID = t.CustomerID
INNER JOIN Flights AS f
ON t.FlightID = f.FlightID
AND f.Status = 'Departing'
ORDER BY DATEDIFF(YEAR, c.DateOfBirth, '2016'), c.CustomerID