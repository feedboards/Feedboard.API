IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'Feedboard')
BEGIN
    CREATE DATABASE [Feedboard];
END