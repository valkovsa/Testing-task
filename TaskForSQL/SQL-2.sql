--Необходимо одним запросом удалить все полные дубликаты из таблицы оставив только по одной записи.

IF Object_id('tempdb..#test_table') IS NOT NULL 
  DROP TABLE #test_table 

CREATE TABLE #test_table 
  ( 
     NUMBER INT, 
     NAME   VARCHAR(255) 
  ) 

GO 

INSERT INTO #test_table VALUES (1, 'Red') --<-- Полный дулбикат
INSERT INTO #test_table VALUES (2, 'Yellow') 
INSERT INTO #test_table VALUES (3, 'Green') 
INSERT INTO #test_table VALUES (1, 'Blue') 
INSERT INTO #test_table VALUES (1, 'Red') --<-- Полный дулбикат
INSERT INTO #test_table VALUES (4, 'Black') 
INSERT INTO #test_table VALUES (2, 'Red') 
GO

SELECT * 
FROM   #test_table  

GO

-- -- SOLUTION -- --

;WITH CTE AS
  (
    SELECT
      NUMBER,
      NAME,
      ROW_NUMBER() OVER(PARTITION BY NUMBER, NAME ORDER BY NUMBER, NAME) row_num
    FROM #test_table
  )
DELETE FROM CTE WHERE row_num > 1

-- CHECK --
SELECT *
FROM #test_table

