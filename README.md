# Web API base setup
 Web API using .Net Core

To make it work, in the first place

-> Install the SQL Server and SQL Server Management Studio
-> Create a new BD called EmployeeDB

After that, just run this query inside of the EmployeeDB

     create table dbo.Employee(
     EmployeeID int identity (1,1),
     EmployeeName varchar(500),
     Department varchar(500),
     DateOfJoining date,
     PhotoFileName varchar(500)
     )
     go

     insert into dbo.Employee values
     ('RandomName', 'RandomDepartment', '2021-03-13', 'RandomPath.png')
     go

     create table dbo.Department(
     DepartmentID int identity (1,1),
     DepartmentName varchar(500)
     )
     go

     insert into dbo.Department values
     ('RandomDepartmentName')
     go

After that you can run the solution on VisualStudio 2019 :D

If you wanna test, you can call the httpget using the Postman
-> try using httpget with this: http://localhost:3201/api/employee or http://localhost:3201/api/department

After understand the code, i bet you can build your own DB with your own tables and just change the reference inside of the project :)
