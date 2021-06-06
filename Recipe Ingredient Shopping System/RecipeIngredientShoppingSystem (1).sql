--database name
use Sprint1
Go

---drop queries---

drop table Shipping
Go
drop table OrderIngredient
Go
drop table CartRepository
Go
drop table Cart
Go
drop table Ingredient
Go
drop table Recipe
Go
drop table Customer
Go
drop table UserLogin

------------------------------
--Admin table
CREATE TABLE UserLogin
( 
	UserId int Primary Key identity(1, 1),
	UserName Varchar(50) Unique NOT NULL,
	Password Varchar(30) NOT NULL,
	Role Varchar(20) NOT NULL,
	MobileNumber Varchar(13) Unique Not null,	
	Address Varchar(100) Not null,
	Email Varchar(100) Unique Not null,
	SecurityQuestion Varchar(100) NOT NULL,
	SecurityAnswer Varchar(100) NOT NULL
);

insert into UserLogin values ('sakshi', 'sakshi123', 'Customer', '998800000', 'Nagpur', 'abc@gmail.com', 'aaa', 'aaaaaa'),
('sakshi1', 'sakshi1234', 'Customer', '998801100', 'Nagpur', 'abcd1@gmail.com', 'aaa', 'aaaaaa'),
('sakshi2', 'sakshi1235', 'Customer', '998801400', 'Nagpur', 'abcas2@gmail.com', 'aaa', 'aaaaaa'),
('sakshi3', 'sakshi1236', 'Customer', '998802300', 'Nagpur', 'abcaaa3@gmail.com', 'aaa', 'aaaaaa'),
('sakshi4', 'sakshi1237', 'Customer', '998804500', 'Nagpur', 'abcqwe4@gmail.com', 'aaa', 'aaaaaa');

SELECT* from UserLogin;

--Customer table
--create table Customer  
--(
--	CustomerId Int primary key identity(1, 1),
--	Name Varchar(50) Not null,
--	Email Varchar(100) Unique Not null,
--	MobileNumber Varchar(13) Unique Not null,
--	UserName Varchar(50) Unique Not null,
--	Password Varchar(50) Not null,
--	Address Varchar(100) Not null, 
--);

--insert into Customer values('Aditi', 'adi@gmail.com', '9877890009', 'aditi', '123456', 'Nagpur' );

--Recipe table
create table Recipe
(
	RecipeId int primary key identity(1, 1),
	Name Varchar(50) not null,
	Description Varchar(300)
);

insert into recipe values ('Pasta', 'good'), ('Maggi', '2 minute breakfast');
insert into recipe values( 'Noodles', 'Noodles are a type of food made from unleavened dough which is rolled flat and cut, stretched or extruded, into long strips or strings');

--Ingredient table
create table Ingredient
(
	IngredientId int primary key identity(1, 1),
	Name Varchar(50) not null,
	RecipeId int references Recipe(RecipeId),
	ManufacturerName varchar(70),
	ManufacturerDate Datetime,
	Price decimal(18,2),
	ExpiryDate Datetime,
	Description Text not null
);

insert into Ingredient values ('Salt', 1, 'aa', '2-2-2021', 20, '4-4-2021', 'salty to taste');

--Cart table
create table Cart
(
	CartId int primary key identity(1, 1),
	UserID int references UserLogin(UserId),
	IngredientID int references Ingredient(IngredientId) on delete cascade on update cascade,
	IngredientQuantity decimal(18,2),
	Amount decimal(18,2)
);

insert into Cart values(1, 1, 2, 30 );
--alter table query to add check constraint
alter table Cart
add Check(IngredientQuantity >= 1);

--CartRepository table
create table CartRepository
(
	CartId int primary key identity(1, 1),
	UserID int references UserLogin(UserId),
	IngredientID int references Ingredient(IngredientId) on delete cascade on update cascade  ,
	IngredientQuantity decimal(18,2),
	Amount decimal(18,2)
);

--OrderIngredient Table
create table OrderIngredient
(
	OrderId int PRIMARY KEY identity(1, 1),
	UserID int foreign key references UserLogin(UserId),
	DateOfOrder datetime not null,
	MobileNumber varchar(20) not null,
	DeliveryAddress varchar(100),
	TotalBillAmount decimal(18,2),
	NoOfIngredients int,
	GrandTotal decimal(18,2)
);

--Shipping table
create table Shipping
(
	ShippingNumber int PRIMARY KEY identity(1, 1),
	OrderId int FOREIGN KEY REFERENCES OrderIngredient(OrderId) not null,
	UserID int foreign key references UserLogin(UserId),
	ExpectedDeliveryDate datetime not null
);

insert into Shipping values(1, 1, '2012-12-2'),
(1,1, '2020-2-2');
--select queries for all tables

select * from Shipping
Go
select * from OrderIngredient
Go
select * from Cart
Go
select * from CartRepository
Go
select * from Ingredient
Go
select * from Recipe
Go
select * from UserLogin
Go


------ TRIGGERS-------------------------------------->
------------------------------------------------------------------------>

-- trigger to enter details into shipping table and delete previous carts from cart table

CREATE TRIGGER AfterInsertInOrderIngredient
on OrderIngredient
AFTER INSERT 
AS 
BEGIN

	SET NOCOUNT ON;

	declare @expected_date date;
	set @expected_date = dateadd(DD, 5, getdate()); 

	INSERT INTO shipping (
		 OrderId,
		 UserId,
		 ExpectedDeliveryDate
	 )
	select
		 i.OrderId,
		 i.UserId,
		 @expected_date
	FROM
		inserted As i

	DELETE c FROM cart c INNER JOIN inserted i on i.UserId = c.UserID;
END

---------------------------------**********************************-------------------------------------------
---------------------------------**********************************-------------------------------------------

select * from Shipping
Go
select * from OrderIngredient
Go
select * from Cart
Go
select * from CartRepository
Go
select * from Ingredient
Go
select * from Recipe
Go
select * from UserLogin
Go