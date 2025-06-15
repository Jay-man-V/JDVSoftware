USE [master];

DECLARE @kill varchar(8000) = '';  
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  
FROM sys.dm_exec_sessions
WHERE database_id  = db_id('UnitTesting')

EXEC(@kill);


/****** Object:  Database [UnitTesting]    Script Date: 12/05/2018 23:06:16 ******/
USE [master]

IF EXISTS(SELECT * FROM SYS.DATABASES WHERE NAME = 'UnitTesting')
BEGIN
    ALTER DATABASE [UnitTesting] SET AUTO_CLOSE OFF 
    DROP DATABASE [UnitTesting]
END
