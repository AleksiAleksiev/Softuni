SELECT c.CustomerID, CONCAT(c.FirstName,' ',c.LastName) AS FullName, c.Gender
FROM Customers AS c
ORDER BY Fullname, c.CustomerID