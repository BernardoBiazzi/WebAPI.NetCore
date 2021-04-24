USE master
GO

IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'KanbanUSF'
)
CREATE DATABASE KanbanUSF
GO

USE KanbanUSF
GO

IF OBJECT_ID('[dbo].[User]', 'U') IS NOT NULL
DROP TABLE [dbo].[User]
GO

CREATE TABLE [dbo].[User]
(
    [Id] INT IDENTITY NOT NULL PRIMARY KEY,
    [Nome] NVARCHAR(50) NOT NULL,
    [Senha] NVARCHAR(50) NOT NULL
);
GO
