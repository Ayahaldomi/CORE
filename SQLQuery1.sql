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


-- Create the Users table
CREATE TABLE Users (
    UserID int PRIMARY KEY IDENTITY(1,1),
    Username varchar(100) NOT NULL,
    Password varchar(100) NOT NULL,
    Email varchar(100) NOT NULL
);

-- Create the Orders table
CREATE TABLE Orders (
    OrderID int PRIMARY KEY IDENTITY(1,1),
    UserID int NOT NULL,
	productId int NOT NULL,
    OrderDate datetime NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (productId) REFERENCES Products(ProductID),
);

-- Insert data into the Users table
INSERT INTO Users (Username, Password, Email)
VALUES 
('john_doe', 'password123', 'john.doe@example.com'),
('jane_smith', 'securePass456', 'jane.smith@example.com'),
('mike_jones', 'mikePass789', 'mike.jones@example.com'),
('sarah_lee', 'leePass321', 'sarah.lee@example.com'),
('david_clark', 'clarkPass654', 'david.clark@example.com'),
('emily_davis', 'emilyPass987', 'emily.davis@example.com'),
('james_taylor', 'jamesPass111', 'james.taylor@example.com'),
('olivia_martin', 'oliviaPass222', 'olivia.martin@example.com'),
('chris_wilson', 'chrisPass333', 'chris.wilson@example.com'),
('sophia_brown', 'sophiaPass444', 'sophia.brown@example.com');

-- Insert data into the Orders table
INSERT INTO Orders (UserID, productId, OrderDate)
VALUES 
(1, 1, '2024-08-01 10:00:00'),
(2, 3, '2024-08-02 11:30:00'),
(3, 5, '2024-08-03 09:15:00'),
(4, 7, '2024-08-04 14:00:00'),
(5, 2, '2024-08-05 16:45:00'),
(6, 4, '2024-08-06 08:00:00'),
(7, 6, '2024-08-07 12:30:00'),
(8, 8, '2024-08-08 13:15:00'),
(9, 1, '2024-08-09 15:45:00'),
(10, 3, '2024-08-10 10:30:00');
