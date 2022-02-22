create DataBase BookStore

Use BookStore

--User Table Name is User_Registration

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
