-- IF  NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'TechTalksDB')
--     BEGIN
--         CREATE DATABASE [TechTalksDB]
--     END;
-- GO

IF  EXISTS (SELECT * FROM sys.databases WHERE name = N'TechTalksDB')
    BEGIN
        PRINT 'Dropping existing database...'
        DROP DATABASE [TechTalksDB]
    END;
GO

PRINT 'Creating database TechTalksDB...'
CREATE DATABASE [TechTalksDB]

SELECT Name from sys.Databases
GO

USE TechTalksDB
GO

if not exists (select * from sysobjects where name='Categories' and xtype='U')
    BEGIN
        PRINT 'Creating Categories table'
        CREATE TABLE Categories (id INT, categoryName NVARCHAR(50), description NVARCHAR(100))

        PRINT 'Inserting default values in categories table...'
        INSERT INTO Categories VALUES(1, 'Meetup', 'Community event organized via meetup');
        INSERT INTO Categories VALUES(2, 'Free Conference', 'Free Tech Conference');
        INSERT INTO Categories VALUES(3, 'Paid Conference', 'Paid Tech Conference');
        INSERT INTO Categories VALUES(4, 'Hackathon', 'Hackathon');
        INSERT INTO Categories VALUES(5, 'Eventribe', 'Community event organized via Eventribe');
    END
GO

if not exists (select * from sysobjects where name='TechTalk' and xtype='U')
    BEGIN
        PRINT 'Creating TechTalk table'
        CREATE TABLE TechTalk (id INT, name NVARCHAR(50), category INT)

        PRINT 'Inserting default values into TechTalk table'
        INSERT INTO TechTalk VALUES (1, 'Scaling Docker Containers', 1); 
        INSERT INTO TechTalk VALUES (2, 'Azure Container Services', 2);
    END
GO

if not exists (select * from sysobjects where name='KeyValue' and xtype='U')
    BEGIN
        CREATE TABLE KeyValue ([Key] NVARCHAR(10), [Value] NVARCHAR(100))

        INSERT INTO KeyValue VALUES('GoT', 'American fantasy drama television series');
        INSERT INTO KeyValue VALUES('MS', 'Microsoft');
        INSERT INTO KeyValue VALUES('OS', 'Open source');
        INSERT INTO KeyValue VALUES('Doc', 'Doc-chain aka Blockchain');
        INSERT INTO KeyValue VALUES('AZ', 'Azure');
        INSERT INTO KeyValue VALUES('ACS', 'Azure Container Service');

    END
GO