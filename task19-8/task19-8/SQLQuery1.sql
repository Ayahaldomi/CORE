Create DataBase task1;

create TABLE Categories (
    CategoryID int primary key identity(1,1),
    CategoryName varchar(max),
    CategoryImage  varchar(max)
);

create TABLE Products (
    ProductID int primary key identity(1,1),
    ProductName varchar(max),
    Description varchar(max),
    Price decimal(18,2),
    CategoryID int,
    ProductImage varchar(max),
	foreign key (CategoryID) references Categories(CategoryID)
);

INSERT INTO Categories (CategoryName, CategoryImage)
VALUES 
('Fruits', 'https://example.com/images/fruits.jpg'),
('Vegetables', 'https://example.com/images/vegetables.jpg'),
('Dairy', 'https://example.com/images/dairy.jpg'),
('Bakery', 'https://example.com/images/bakery.jpg');

INSERT INTO Products (ProductName, Description, Price, CategoryID, ProductImage)
VALUES 
('Apple', 'Fresh and crispy apples', 1.99, 1, 'https://example.com/images/apple.jpg'),
('Banana', 'Sweet and ripe bananas', 0.99, 1, 'https://example.com/images/banana.jpg'),
('Carrot', 'Fresh organic carrots', 1.49, 2, 'https://example.com/images/carrot.jpg'),
('Broccoli', 'Green and healthy broccoli', 2.49, 2, 'https://example.com/images/broccoli.jpg'),
('Milk', 'Organic whole milk', 3.99, 3, 'https://example.com/images/milk.jpg'),
('Cheese', 'Creamy cheddar cheese', 4.99, 3, 'https://example.com/images/cheese.jpg'),
('Bread', 'Freshly baked bread', 2.99, 4, 'https://example.com/images/bread.jpg'),
('Croissant', 'Buttery French croissant', 3.49,4, 'https://example.com/images/croissant.jpg');

