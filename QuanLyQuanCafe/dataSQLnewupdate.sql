CREATE DATABASE QuanLyQuanCafe
GO

USE QuanLyQuanCafe
GO

-- Food
--Table
--FoodCategory
--Account
--Bill
--BillInfo

CREATE TABLE TableFood
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa có tên',
	status NVARCHAR(100) NOT NULL DEFAULT N'Trống'         --Trống || có người
)
GO

CREATE TABLE Account
(
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Kter',
	UserName NVARCHAR(100) PRIMARY KEY,
	PassWord NVARCHAR(1000) NOT NULL DEFAULT 0,
	Type INT NOT NULL DEFAULT 0  --1:admin && 0:staff
)
GO
CREATE TABLE FoodCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

CREATE TABLE Food
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0

	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idTable INT NOT NULL,
	status INT NOT NULL DEFAULT 0   --1:đã thanh toán && 0: chưa thanh toán

	FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id)
)
GO

CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0

	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
)
GO
INSERT INTO dbo.Account
(
	DisplayName ,
	UserName ,
	Password ,
	Type 
)
VALUES
(
	N'RongK9' , --DisplayName - nvarchar(100)
	N'K9' , --UserName - nvarchar(100)
	N'1' , --Password - nvarchar(1000)
	1 -- Type - int

)
INSERT INTO dbo.Account
(
	DisplayName ,
	UserName ,
	Password ,
	Type 
)
VALUES
(
	N'staff' , --DisplayName - nvarchar(100)
	N'staff' , --UserName - nvarchar(100)
	N'1' , --Password - nvarchar(1000)
	0 -- Type - int

)
GO
CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS
BEGIN
SELECT * FROM dbo.Account WHERE UserName =@userName
END 
GO

EXEC dbo.USP_GetAccountByUserName @userName = N'K9' --nvarchar(100)

GO

CREATE PROC USP_Login
@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN
    SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord 
END
GO

--Them ban
DECLARE @i INT = 0

WHILE @i <= 10
BEGIN 
     INSERT dbo.TableFood (name)VALUES  ( N'Bàn ' + CAST(@i AS nvarchar(100)))
	 SET @i = @i + 1
END

GO 

CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.TableFood
GO

UPDATE dbo.TableFood SET STATUS = N'Có người' WHERE id = 9
EXEC dbo.USP_GetTableList
GO
-- Thêm Category
INSERT dbo.FoodCategory
		( name )
VALUES  ( N'Hải sản')  --name - nvarchar(100)
INSERT dbo.FoodCategory
		( name )
VALUES  ( N'Nông sản')
INSERT dbo.FoodCategory
		( name )
VALUES  ( N'Lâm sản')
INSERT dbo.FoodCategory
		( name )
VALUES  ( N'Sản sản')
INSERT dbo.FoodCategory
		( name )
VALUES  ( N'Nước')

--Thêm món ăn
INSERT dbo.Food
		( name, idCategory, price )
VALUES  ( N'Mực một nắng nước sa tế', 1, 120000)-- name  - nvarchar(100)
INSERT dbo.Food
		( name, idCategory, price )
VALUES  ( N'Nghêu hấp xả', 1, 50000)
INSERT dbo.Food
		( name, idCategory, price )
VALUES  ( N'Dú dê nướng sữa', 2, 60000)
INSERT dbo.Food
		( name, idCategory, price )
VALUES  ( N'Heo rừng nướng muối ớt', 3, 75000)
INSERT dbo.Food
		( name, idCategory, price )
VALUES  ( N'Cơm chiên mushi', 4, 99999)
INSERT dbo.Food
		( name, idCategory, price )
VALUES  ( N'7Up', 5, 15000)
INSERT dbo.Food
		( name, idCategory, price )
VALUES  ( N'Cafe', 5, 12000)

-- Thêm bill
INSERT dbo.Bill
		( DateCheckIn,
		  DateCheckOut,
		  idTable,
		  status
		)
VALUES ( GETDATE(), --DateCheckIn - date
		 NULL, --DateCheckOut - date
		 1, --idTable - int
		 0 --status - int
		)
INSERT dbo.Bill
		( DateCheckIn,
		  DateCheckOut,
		  idTable,
		  status
		)
VALUES ( GETDATE(), --DateCheckIn - date
		 NULL, --DateCheckOut - date
		 2, --idTable - int
		 0 --status - int
		)
INSERT dbo.Bill
		( DateCheckIn,
		  DateCheckOut,
		  idTable,
		  status
		)
VALUES ( GETDATE(), --DateCheckIn - date
		 GETDATE(), --DateCheckOut - date
		 2, --idTable - int
		 1 --status - int
		)

-- Thêm bill info
INSERT dbo.BillInfo
		( idBill, idFood, count )
VALUES	( 1, -- idBill - int
		  1, --idFood - int
		  2  -- count - int
		 )
INSERT dbo.BillInfo
		( idBill, idFood, count )
VALUES	( 1, -- idBill - int
		  3, --idFood - int
		  4  -- count - int
		 )
INSERT dbo.BillInfo
		( idBill, idFood, count )
VALUES	( 1, -- idBill - int
		  5, --idFood - int
		  1  -- count - int
		 )
INSERT dbo.BillInfo
		( idBill, idFood, count )
VALUES	( 2, -- idBill - int
		  1, --idFood - int
		  2  -- count - int
		 )
		 INSERT dbo.BillInfo
		( idBill, idFood, count )
VALUES	( 2, -- idBill - int
		  6, --idFood - int
		  2  -- count - int
		 )
INSERT dbo.BillInfo
		( idBill, idFood, count )
VALUES	( 3, -- idBill - int
		  5, --idFood - int
		  2  -- count - int
		 )
GO

SELECT f.name, bi.count, f.price, f.price*bi.count AS totalPrice FROM dbo.BillInfo AS bi, dbo.Bill AS b, dbo.Food AS f
WHERE bi.idBill = b.id AND bi.idFood = f.id AND b.status = 0 AND b.idTable = 2

SELECT * FROM dbo.Bill
SELECT * FROM dbo.BillInfo
SELECT * FROM dbo.Food
SELECT * FROM dbo.FoodCategory
GO

ALTER TABLE dbo.Bill
ADD discount INT 
GO

UPDATE dbo.Bill SET discount = 0
GO

CREATE PROC USP_INSERTBILL
@idTable INT
AS
BEGIN
	INSERT dbo.Bill
		(
		DateCheckIn ,
		DateCheckOut ,
		idTable ,
		status,
		discount
		)
	VALUES
		(
			GETDATE() ,	--DateCheckIn - date
			NULL , --DateCheckOut -date
			@idTable , -- idTable - int
			0, --status - int
			0
		)

END
GO

--sau khi chạy 1 lần đầu thì sửa create thành alter nghe
CREATE PROC InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN

	DECLARE @isExitsBillInfo INT
	DECLARE @foodCount INT =1
	SELECT @isExitsBillInfo =id, @foodCount = b.count 
	FROM dbo.BillInfo AS b 
	WHERE idBill= @idBill AND idFood=@idFood
	IF(@isExitsBillInfo>0)
	BEGIN
		DECLARE @newCount INT = @foodCount +@count
		IF(@newCount>0)
			UPDATE dbo.BillInfo SET count =@foodCount+@count WHERE idFood=@idFood
		ELSE
			DELETE dbo.BillInfo WHERE idBill=@idBill AND idFood=@idFood
	END
	ELSE
	BEGIN
			INSERT dbo.BillInfo
			( idBill, idFood, count )
			VALUES
			( @idBill, -- idBill - int
			  @idFood, --idFood - int
			  @count  -- count - int
			)
	END


END
GO


DELETE dbo.BillInfo
DELETE dbo.Bill
GO

CREATE TRIGGER UTG_UpdateBillInfo
ON dbo.BillInfo FOR INSERT, UPDATE 
AS
BEGIN
	DECLARE @idBill INT

	SELECT @idBill= idBill FROM inserted

	DECLARE @idTable INT

	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill AND status =0

	DECLARE @count INT
	SELECT @count = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idBill

	IF (@count > 0)
	BEGIN

		PRINT @idTable 
		PRINT @idBill
		PRINT @count

		UPDATE dbo.TableFood SET status = N'Có người' WHERE id= @idTable
		
	END
	ELSE 
	BEGIN
	PRINT @idTable 
		PRINT @idBill
		PRINT @count
	UPDATE dbo.TableFood SET status = N'Trống' WHERE id= @idTable
	END

END
GO


CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = id FROM inserted
	DECLARE @idTable INT
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill
	DECLARE @count INT = 0
	SELECT @count = COUNT(*) FROM dbo.Bill WHERE idTable=@idTable AND status = 0
	IF(@count= 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id=@idTable
END
GO




CREATE PROC USP_SwitchTabel
@idTable1 INT, @idTable2 int 
AS BEGIN 
	
	DECLARE @idFirstBill int
	DECLARE @idSeconrdBill INT

	DECLARE @isFirstTablEmty INT = 1
	DECLARE @isSeconrdTablEmty INT = 1

	SELECT @idSeconrdBill = id FROM dbo.Bill WHERE idTable = @idTable2 AND status =  0 
	SELECT @idFirstBill = id FROM dbo.Bill WHERE idTable = @idTable1 AND status =  0

	PRINT @idFirstBill 
	PRINT @idSeconrdBill
	PRINT '----------'

	IF (@idFirstBill IS NULL)
	BEGIN
		PRINT '0000001'
		INSERT dbo.Bill
			(
			DateCheckIn ,
			DateCheckOut ,
			idTable ,
			status
			
			)
		VALUES
			(
				GETDATE() ,	--DateCheckIn - date
				NULL , --DateCheckOut -date
				@idTable1 , -- idTable - int
				0 --status - int
				
			)
			

		SELECT @idFirstBill = MAX(id) FROM dbo.Bill WHERE idTable = @idTable1 AND status =  0	
		
		
	END

	SELECT @isFirstTablEmty = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idFirstBill

	PRINT @idFirstBill 
	PRINT @idSeconrdBill
	PRINT '----------'

	IF (@idSeconrdBill IS NULL)
	BEGIN
		PRINT '0000002'
		INSERT dbo.Bill
			(
			DateCheckIn ,
			DateCheckOut ,
			idTable ,
			status
			
			)
		VALUES
			(
				GETDATE() ,	--DateCheckIn - date
				NULL , --DateCheckOut -date
				@idTable2 , -- idTable - int
				0 --status - int
				
			)
			

		SELECT @idSeconrdBill = MAX(id) FROM dbo.Bill WHERE idTable = @idTable2 AND status =  0	
		

	END

	SELECT @isSeconrdTablEmty = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idSeconrdBill

	PRINT @idFirstBill 
	PRINT @idSeconrdBill
	PRINT '----------'

	SELECT id INTO IDBillInfoTable FROM dbo.BillInfo WHERE idBill = @idSeconrdBill

	UPDATE dbo.BillInfo SET idBill = @idSeconrdBill WHERE idBill = @idFirstBill

	UPDATE dbo.BillInfo SET idBill = @idFirstBill WHERE id IN (SELECT * FROM IDBillInfoTable)

	DROP TABLE IDBillInfoTable

	IF (@isFirstTablEmty = 0 )
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable2

	IF (@isSeconrdTablEmty = 0 )
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable1

END
GO

EXEC dbo.USP_SwitchTabel @idTable1 = 8,  --int 
	@idTable2 = 11 --int
GO
--vid 15
ALTER TABLE dbo.Bill ADD totalPrice FLOAT

DELETE dbo.BillInfo
DELETE dbo.Bill
GO


--sau khi chạy 1 lần đầu thì sửa create thành alter nghe

CREATE PROC USP_GetListBillByDate
@checkIn date, @checkOut date
AS
BEGIN
    SELECT t.name AS [Tên bàn], b.totalPrice AS [Tổng tiền], DateCheckIn AS [Ngày vào], DateCheckOut AS [Ngày ra], discount AS [Giảm giá]
    FROM dbo.Bill AS b, dbo.TableFood AS t
    WHERE DateCheckIn >= @checkIn 
          AND DateCheckOut <= @checkOut 
          AND b.status = 1 
          AND t.id = b.idTable
END
GO
--het vid 15

--vid 16
CREATE PROC USP_UpdateAccount
@userName NVARCHAR(100), @displayName NVARCHAR(100), @password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
    DECLARE @isRightPass INT = 0

    SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE USERName = @userName AND PassWord = @password

    IF (@isRightPass = 1)
    BEGIN
        IF (@newPassword = NULL OR @newPassword = '')
        BEGIN
            UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName
        END
		ELSE
			UPDATE dbo.Account SET DisplayName = @displayName, PassWord = @newPassword WHERE UserName = @userName
    END
END
GO

