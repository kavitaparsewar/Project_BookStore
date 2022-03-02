create DataBase BookStore

Use BookStore

--From here User Table Stored Procedure

create table User_Registration(
UserId int primary key identity,
FullName varchar(50),
MobileNumber varchar(15),
EmailId varchar(20),
Password varchar(20)
);

select * from User_Registration;


create proc SpAddUserDetails
@FullName varchar(50),
@MobileNumber varchar(50),
@EmailId varchar(50),
@Password varchar(20)
as 
set nocount on
declare @UserId as int
Insert into User_Registration (FullName,MobileNumber,EmailId,Password)values (@FullName,@MobileNumber,@EmailId,@Password)
set @UserId= SCOPE_IDENTITY()
select * from User_Registration where UserId=@UserId;
go

execute SpAddUserDetails 'abc','256555555','abc@gmail.com','12345';

create procedure spUpdate
(
@FullName nvarchar(max),
@UserId bigint,
 @EmailId nvarchar(max),
 @MobileNumber bigint
)
as
Begin
Update User_Registration 
set
FullName=@FullName,EmailId=@EmailId,MobileNumber=@MobileNumber where UserId=@UserId
End


Exec spUpdate 'Kavita',k@gmail.com,78523645,1



create procedure spDelete
(
@UserId bigint
)
As
Begin
Delete from User_Registration where UserId =@UserId 
End

Execute spDelete 1



create procedure spGetAll
as
Begin
select * from User_Registration
End



CREATE PROCEDURE spLogin
(
   @EmailId VARCHAR(50),
   @Password VARCHAR(20)
)
AS
BEGIN
	SELECT * FROM User_Registration WHERE EmailId = @EmailId and Password=@Password
END


CREATE PROCEDURE spResetPassword
(
   @EmailId VARCHAR(50),
   @Password VARCHAR(20)
)
AS
BEGIN
    UPDATE  User_Registration SET Password = @Password
    WHERE EmailId =@EmailId
END


--From here Books Stored Procedure.

Use BookStore
create Table Books(
    BookId int IDENTITY(1,1) Primary Key,	
	BookName varchar(50) NULL,
	AuthorName varchar(50) NULL,
	TotalRating float NULL,
	RatedCount int NULL,
	DiscountPrice float NULL,
	OriginalPrice float NULL,	
	Description varchar(250) NULL,
	BookImage varchar(100) NULL,
	Quantity int NULL		
)
SELECT * FROM Books;

Alter table dbo.Books DROP column UserId;
Go




CREATE PROCEDURE spAddBook    
(   
	@BookName VARCHAR(50),
	@AuthorName VARCHAR(50),
	@TotalRating float,
	@RatedCount int,
	@DiscountPrice float,
	@OriginalPrice float,
	@Description VARCHAR(250),
	@BookImage VARCHAR(100),
	@Quantity int,	
	@BookId int out
)   
AS
BEGIN	
	SET NOCOUNT ON;    
	IF NOT EXISTS 
    (
      SELECT *
      FROM Books
      WHERE  BookName= @BookName or AuthorName = @AuthorName
    )
	INSERT INTO Books(BookName,AuthorName,Description,BookImage,Quantity,OriginalPrice,DiscountPrice,TotalRating ,RatedCount)   
    Values (@BookName,@AuthorName,@Description,@BookImage,@Quantity,@OriginalPrice,@DiscountPrice,@TotalRating ,@RatedCount)   
	SET @BookId = SCOPE_IDENTITY()
	RETURN @BookId
END



CREATE PROCEDURE spDeleteBook    
(   
	@BookId int
)   
AS
BEGIN
    DELETE FROM Books WHERE BookId=@BookId;
END



CREATE PROCEDURE spUpdateBook    
(   
    @BookId int ,
    @BookName VARCHAR(50),
	@AuthorName VARCHAR(50),
	@TotalRating float,
	@RatedCount int,
	@DiscountPrice float,
	@OriginalPrice float,
	@Description VARCHAR(250),
	@BookImage VARCHAR(100),
	@Quantity int		
)   
AS
BEGIN
	UPDATE Books SET BookName=@BookName,AuthorName=@AuthorName,Description=@Description,BookImage=@BookImage,Quantity=@Quantity,
	OriginalPrice=@OriginalPrice,DiscountPrice=@DiscountPrice,TotalRating=@TotalRating,RatedCount=@RatedCount
	WHERE BookId = @BookId
END


create procedure spGetAllBook
as
Begin
select * from Books
End



--From here Cart Stored Procedure.

Use BookStore

create table Cart
(
    CartId int IDENTITY(1,1) Primary Key,
    BookId int foreign key references Books(BookId),
    UserId int foreign key references User_Registration(UserId),
	CartQuantity int 
)

select * from Cart

create procedure spAddCart
(

  @CartId int out,
  @CartQuantity int
)
AS
BEGIN	
	SET NOCOUNT ON;    
	IF NOT EXISTS 
    (
      SELECT *
      FROM Cart
      WHERE  CartId= @CartId
    )
	INSERT INTO Cart(CartQuantity)   
    Values (@CartQuantity)   
	SET @CartId = SCOPE_IDENTITY()
	RETURN @CartId
END


CREATE PROCEDURE spDeleteFromCart   
(   
	@CartId int
)   
AS
BEGIN
    DELETE FROM Cart WHERE CartId=@CartId;
END

CREATE PROCEDURE spUpdateCart   
(   
    @CartId int ,
	@CartQuantity int
   
)   
AS
BEGIN
	UPDATE Cart SET CartQuantity=@CartQuantity
	WHERE CartId = @CartId
END



create procedure spGetAllFromCart
as
Begin
select * from Cart
End





--From here Wishlist Stored Procedure.

Use BookStore

create table WishList
(
    WishListId int IDENTITY(1,1) Primary Key,
    BookId int foreign key references Books(BookId),
    UserId int foreign key references User_Registration(UserId),
)

select * from WishList

create procedure spAddWishList
  @UserId int,
  @BookId int 
AS
BEGIN    
	IF EXISTS 
    (
      SELECT *
      FROM WishList
      WHERE  BookId= @BookId AND UserId=@UserId)
	  SELECT 1;
	  ELSE
	  BEGIN
	  If EXISTS (select * from Books where BookId = @BookId)
	  BEGIN
	INSERT INTO WishList(UserId,BookId)   
    Values (@UserId,@BookId)   
	END
	ELSE
	  SELECT 2
	  END
END


CREATE PROCEDURE spDeleteFromWishList  
(   
	@WishListId int
)  
AS
BEGIN 
DELETE FROM WishList WHERE WishListId=@WishListId;
END



create procedure spGetAllFromWishList
@UserId int
AS
BEGIN
Select Books.BookId,Books.BookName,Books.AuthorName,Books.DiscountPrice,Books.OriginalPrice,Books.BookImage,WishList.WishListId,WishList.UserId,WishList.BookId from Books
inner join WishList
on
WishList.BookId=Books.BookId where WishList.UserId=@UserId
End






-------create Table AddressType---

create table AddressType 
(
AddTId int NOT NULL IDENTITY(1,1) Primary Key,
addressType varchar (500)
)
Insert INTO [AddressType]
Values ('HOME')

Insert INTO [AddressType]
Values ('OFFICE')

Insert INTO [AddressType]
Values ('OTHER')

select * from AddressType;


----create Addresses Table---

create table Addresses
(
 AddId int NOT NULL IDENTITY (1,1) PRIMARY KEY,
 UserId int NOT NULL Foreign Key(UserId) references User_Registration(UserId),
 FullAddress varchar (500) NOT NULL,
 City varchar (50),
  state varchar (50),
  AddTId int Foreign Key (AddTId)references AddressType(AddTId)
);

select * from Addresses;

create procedure spAddAddress
(
@UserId int,
@FullAddress varchar (500),
@City varchar (50),
@State varchar (50),
@AddTId int )
AS
BEGIN
IF (EXISTS(SELECT * from User_Registration where @UserId=@UserId))
BEGIN
Insert into Addresses (UserId,FullAddress,City,State,AddTId)
           values (@UserId,@FullAddress,@City,@State,@AddTId);
	END
	ELSE
	BEGIN
	SELECT 1
	END
END

Exec spAddAddress 1,'near temple','udgir','maha',1

--UpdateAddress 

create procedure spUpdateAddress
(
@AddId int,
@FullAddress varchar (100),
@City varchar (100),
@state varchar (100),
@AddTId int
)
as
begin
if (EXISTS (SELECT * from Addresses where AddId=@AddId))
begin
UPDATE Addresses
set
FullAddress=@FullAddress,
City=@City,
state=@state,
AddTId=@AddTId
   where AddId =@AddId;
END
ELSE
BEGIN
SELECT 1;
END
END


--GetAllAddress

create PROCEDURE spGetAllAddress
as
Begin
  Begin
  select * from Addresses;
  End
END


---ORDERs TABLE

CREATE TABLE Orders(
 OrderId int NOT NULL IDENTITY (1,1) PRIMARY KEY,
 UserId int NOT NULL Foreign Key(UserId) references User_Registration(UserId),
 AddId int Foreign Key(AddId) references Addresses(AddId),
 BookId int NOT NULL Foreign Key(BookId) references Books(BookId),
 TotalPrice int,
 QuantityToBuy int
)


CREATE PROCEDURE spAddOrders
@UserId int,
@AddId int,
@BookId int,
@QuantityToBuy int
as
Declare @TotalPrice int
BEGIN
select @TotalPrice=DiscountPrice from Books where BookId=@BookId;
If (EXISTS(Select * from Books where BookId =@BookId))
Begin
 If (EXISTS(select * from User_Registration where UserId=@UserId))
 begin
 BEGIN try
        BEGIN TRANSACTION
		     Insert into Orders (UserId,AddId,BookId,TotalPrice,QuantityToBuy)
             values(@UserId,@AddId,@BookId,@TotalPrice,@QuantityToBuy)
             UPDATE Books set Quantity = Quantity-@QuantityToBuy
             Delete from Cart where BookId=@BookId and UserId = @UserId
        COMMIT TRANSACTION
END try
Begin catch
		   RollBack TRANSACTION
End catch 
		END 
		ELSE
		BEGIN
		   SELECT 1
		END
END
ELSE
BEGIN
    SELECT 2
END
END

		

CREATE PROCEDURE spGetAllOrders
@UserId int
as
BEGIN
select 
Books.BookId,Books.BookName,Books.AuthorName,Books.DiscountPrice,Books.OriginalPrice,Books.BookImage,Orders.OrderId
From Books
inner join Orders 
on
Orders.BookId=Books.BookId where Orders.UserId=@UserId
End




----FeedBack TABLE----


CREATE TABLE FeedBack(
 FeedBackId int NOT NULL IDENTITY (1,1) PRIMARY KEY,
 UserId int NOT NULL Foreign Key(UserId) references User_Registration(UserId),
 BookId int NOT NULL Foreign Key(BookId) references Books(BookId),
 Ratings int,
 Comment varchar (500)
)


CREATE PROCEDURE spAddFeedback
@UserId int ,
@BookId int ,
@Comment varchar(250),
@Ratings int
as 
set nocount on

Insert into FeedBack (UserId,BookId,Comment,Ratings)
values (@UserId,@BookId,@Comment,@Ratings)
set 
@UserId = SCOPE_IDENTITY()
select * from FeedBack where UserId=@UserId;
go


EXECUTE spAddFeedback 1,1,'good',4



CREATE PROCEDURE spGetAllFeedback
as
@BookId int
Begin
  Begin
  SELECT * from FeedBack where BookId=@BookId;
  End
END






-- all table
select * from User_Registration;

select * from Books;

select * from Cart;

select * from WishList;

select * from AddressType;

select * from Addresses;

select * from Orders;

select * from FeedBack;

