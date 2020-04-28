CREATE TABLE Department (
	DepartmentId INT PRIMARY KEY,
	Name NVARCHAR(255),
	Location NVARCHAR(255),
);

CREATE TABLE Employee (
	EmployeeId INT PRIMARY KEY,
	FirstName NVARCHAR(255),
	LastName NVARCHAR(255),
	SSN NVARCHAR(255),
	DeptId int FOREIGN KEY REFERENCES Department,
);

CREATE TABLE EmpDetails (
	EmpDetailsId INT PRIMARY KEY,
	EmployeeId INT FOREIGN KEY REFERENCES Employee,
	Salary INT,
	Address1 NVARCHAR(255),
	Address2 NVARCHAR(255),
	City NVARCHAR(255),
	State NVARCHAR(255),
	Country NVARCHAR(255),
);

-- add at least 3 records into each table

INSERT INTO Department VALUES (1, 'Marketing', 'Floor 3');
INSERT INTO Department VALUES (2, 'Sales', 'Floor 2');
INSERT INTO Department VALUES (3, 'Manufacturing', 'Floor 1');

INSERT INTO Employee VALUES (1, 'John', 'Doe', '123-45-6789', 1);
INSERT INTO Employee VALUES (2, 'Jane', 'Doe', '987-65-4321', 2);
INSERT INTO Employee VALUES (3, 'Alice', 'Smith', '111-22-333', 1);

INSERT INTO EmpDetails VALUES (1, 1, 60000, '123 The Street', '','Townsville, 55555', 'NY', 'USA');
INSERT INTO EmpDetails VALUES (2, 2, 70000, '555 Somewhere', 'Apt 1', 'Bigcity, 11111', 'CA', 'USA');
INSERT INTO EmpDetails VALUES (3, 3, 55000, '321 Main Street', 'Unit 3', 'Metropolis, 99999', 'TX', 'USA');

-- add employee Tina Smith.

INSERT INTO Employee (EmployeeId, FirstName, LastName, SSN) VALUES (4, 'Tina', 'Smith', '333-44-9999');
INSERT INTO EmpDetails VALUES (4, 4, 49000, '900 Tina Turnpike', 'Bldg3', 'Suburbs, 12333', 'AZ', 'USA');

-- add department Marketing (see above)

-- list all employees in Marketing
SELECT EmployeeId, FirstName, LastName FROM Employee
WHERE DeptId IN (SELECT DepartmentId FROM Department WHERE Name = 'Marketing');

-- report total salary of Marketing
SELECT SUM(Salary) FROM EmpDetails
WHERE EmployeeId
  IN (
    SELECT EmployeeId FROM Employee WHERE DeptId 
	IN (
      SELECT DepartmentId FROM Department WHERE Name = 'Marketing'
	)
  )

-- report total employees by department
SELECT dept.Name, COUNT(EmployeeId) AS NumEmployees FROM Employee
JOIN Department dept ON dept.DepartmentId = Employee.DeptId
GROUP BY dept.Name

-- increase salary of Tina Smith to $90,000
UPDATE EmpDetails SET Salary = 90000
WHERE EmployeeId
  IN (SELECT EmployeeId FROM Employee WHERE FirstName = 'Tina' AND LastName = 'Smith')