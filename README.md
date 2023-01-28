<h1>Realization of the sum allocation method</h1>

<b>3 types of allocation:</b>
 - Proportional
 - "First-in-the-list" priority
 - "Last-in-the-list" priority


<b>Input parameters:</b>
 - Allocation type (string)
 - Initial sum (double)
 - Required sums (string, delimiter separated list of required sums)

<b>Output:</b>
- Allocated sums (string, delimiter separated list of allocated sums)


<h1>SQL task</h1>

<b>Task:</b> There are products and categories in the MS SQL Server database. One product can have many categories, and one category can be assigned many products. Write an SQL query to select all "Product name - Category name" pairs. The name of product should be displayed, even if there aren't any categories assigned to it.

<b>Solution:</b>

1. Creating main entities - <b>Category</b> and <b>Product</b> - and a <b>ProductCategory</b> for realization <i>many-to-many</i> relationship between them; 
```
CREATE TABLE [dbo].[Category] (
    [Id]   INT           NOT NULL IDENTITY,
    [Name] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
```
```
CREATE TABLE [dbo].[Product] (
    [Id]   INT           NOT NULL IDENTITY,
    [Name] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
```
```
CREATE TABLE [dbo].[ProductCategory] (
    [ProductId]  INT NOT NULL,
    [CategoryId] INT NULL,
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]),
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id])
);
```
2. Filling DB with data:
```
INSERT INTO Product VALUES ('Shampoo'), ('Bread'), ('Book'), ('Alien space ship');
INSERT INTO Category VALUES ('Food'), ('Clothes'), ('Cosmetics'), ('Eatable'), ('Uneatable');
INSERT INTO ProductCategory VALUES (1, 3), (1, 5), (2, 1), (2, 4), (3, 5);
```
3. There are two variants of query:
3.1 - If we want to select Product-Category pairs by referencing ONLY ProductCategory table - therefore, even product without category must have a record like ('Sausages', NULL) in this table to be selected
```
Select p.Name as ProductName, 
	   c.Name as CategoryName
       from Product p
join ProductCategory pc on pc.ProductId = p.Id
left join Category c on pc.CategoryId = c.Id

order by c.Name, p.Name;
```
3.2 Preferable variant - If ProductCategory table contains only records of existable category assignments to a products (and no NULLs allowed)
```
Select p.Name as ProductName, 
	   c.Name as CategoryName
       from Product p
left join ProductCategory pc on pc.ProductId = p.Id
left join Category c on pc.CategoryId = c.Id

order by c.Name, p.Name;
```

4. Query result: </br></br>
![image](https://user-images.githubusercontent.com/69399170/215284876-f3898534-5f40-45f5-95e7-640701b2df89.png)
