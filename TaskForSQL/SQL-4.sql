--� ������� ��������� 8 ����� �� ��������� �� 1 �� 10, � ������������ �������. ������ ����� ����������� ���� ���. 
--���������� �������� ������, ������� ������ ����� �� ��������� [min, max] ������������� � �������. 
--������������ ����������� ������. ������� ������ ���� �������� �������, �������� � �������������. 
--��������� ������� �� �������, � ������� ������������ ����� � ��������� 1 000 000. 


IF Object_id('tempdb..#test_table') IS NOT NULL 
  DROP TABLE #test_table 

CREATE TABLE #test_table 
  ( 
     id INT 
  ) 
GO 

INSERT INTO #test_table 
VALUES (1), (2), (8), (4), (9), (7), (3), (10) --<-- ����������� ����� 5 � 6
GO


-- -- SOLUTION -- --

IF OBJECT_ID('tempdb..#temp_range') IS NOT NULL
DROP TABLE #temp_range 

CREATE TABLE #temp_range (id INT);

DECLARE @firstNum INT
DECLARE @lastNum INT
SET @firstNum = 1
SET @lastNum = 1000000
-- 
WHILE (@firstNum <= @lastNum)
BEGIN
    INSERT INTO #temp_range(id) VALUES(@firstNum)
    SET @firstNum = @firstNum + 1;
END
--
SELECT tr.id
FROM #temp_range as tr
LEFT JOIN #test_table as tt ON tt.id = tr.id
WHERE tt.id IS NULL
ORDER BY tr.id

GO



