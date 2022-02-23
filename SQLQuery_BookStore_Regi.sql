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


Alter proc SpAddUserDetails
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

Create Table Books
(
	BookId int IDENTITY(1,1) NOT NULL,
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
