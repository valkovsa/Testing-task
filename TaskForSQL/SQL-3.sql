--Объяснить почему запись вставляется и реализовать скрипт таким образом что бы запись таки не вставилась из-за ошибки деления на 0.
IF Object_id('tempdb..#test_tran') IS NOT NULL 
  DROP TABLE #test_tran 

CREATE TABLE #test_tran 
  ( 
     id   INT, 
     name NVARCHAR(255) 
  ) 
GO
---------------------------------------- 
BEGIN TRAN 
  DECLARE @a FLOAT = 1 / 0.0 
  INSERT INTO #test_tran VALUES (1, N'Красный') 
COMMIT TRAN 
---------------------------------------- 
GO 

SELECT * FROM   #test_tran


-- -- SOLUTION -- --
-- Запись в коде выше вставляется из-за отсутствия обработки ошибок, корректный код ниже
BEGIN TRY
  BEGIN TRAN 
    DECLARE @a FLOAT = 1 / 0.0 
    INSERT INTO #test_tran VALUES (1, N'Красный') 
	COMMIT TRAN
END TRY
BEGIN CATCH
    PRINT ERROR_MESSAGE()
	ROLLBACK
END CATCH
---------------------------------------- 
GO 

SELECT * FROM   #test_tran