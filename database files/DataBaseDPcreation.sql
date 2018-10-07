USE [master]
GO
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'������')
DROP DATABASE [������]
GO
USE [master]
CREATE DATABASE [������] ON  PRIMARY 
( NAME = N'������', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\������.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'������_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\������_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
ALTER DATABASE [������] SET COMPATIBILITY_LEVEL = 100
GO
USE [������]



 
 CREATE TABLE ���������� (
        ����������           varchar(50) NOT NULL,
        ����������ID         int NOT NULL,
        �����������          int NOT NULL,
        �����������          float NOT NULL
 )
go
 
 
 ALTER TABLE ����������
        ADD PRIMARY KEY NONCLUSTERED (����������ID)
go
 
 
 CREATE TABLE �������� (
        ��������             varchar(20) NOT NULL,
        ��������ID           int NOT NULL
 )
go
 
 
 ALTER TABLE ��������
        ADD PRIMARY KEY NONCLUSTERED (��������ID)
go
 
 
 CREATE TABLE ����������������� (
        ��������������       varchar(20) NOT NULL,
        �����ID              int NOT NULL,
        �������              varchar(11) NOT NULL,
        �����                varchar(40) NOT NULL,
        ����                 varchar(11) NULL
 )
go
 
 
 ALTER TABLE �����������������
        ADD PRIMARY KEY NONCLUSTERED (�����ID)
go
 
 
 CREATE TABLE ������������ (
        ��������������       varchar(50) NOT NULL,
        �����������ID        int NOT NULL,
        ����                 date NOT NULL,
        ����������           int NOT NULL,
        �����ID              int NOT NULL
 )
go
 
 
 ALTER TABLE ������������
        ADD PRIMARY KEY NONCLUSTERED (�����������ID)
go
 
 
 CREATE TABLE ������ (
        ����������           date NOT NULL,
        �����ID              int IDENTITY(1,1),
        �������              varchar(50) NOT NULL,
        �����������ID        int NOT NULL,
        ���������ID          int NOT NULL,
        ��������ID           int NOT NULL,
        ������ID             int NOT NULL,
        ����������ID         int NOT NULL,
        ��������ID           int NULL
 )
go
 
 
 ALTER TABLE ������
        ADD PRIMARY KEY NONCLUSTERED (�����ID)
go
 
 
 CREATE TABLE �������� (
        ������������         float NOT NULL,
        ��������ID           int NOT NULL,
        �������ID            int NOT NULL,
        �����ID              int NOT NULL,
        ����������������     varchar(50) NOT NULL
 )
go
 
 
 ALTER TABLE ��������
        ADD PRIMARY KEY NONCLUSTERED (��������ID)
go
 
 
 CREATE TABLE ����������� (
        ����                 int NOT NULL,
        �����������ID        int NOT NULL,
        �������              varchar(11) NOT NULL,
        ���                  varchar(30) NOT NULL,
        ������������ID       int NOT NULL
 )
go
 
 
 ALTER TABLE �����������
        ADD PRIMARY KEY NONCLUSTERED (�����������ID)
go
 
 
 CREATE TABLE ������������������� (
        ������������         varchar(30) NULL,
        ������������ID       int NOT NULL
 )
go
 
 
 ALTER TABLE �������������������
        ADD PRIMARY KEY NONCLUSTERED (������������ID)
go
 
 
 CREATE TABLE ������� (
        �������              varchar(11) NOT NULL,
        ������ID             int IDENTITY(1,1),
        �����                varchar(40) NOT NULL,
        ���                  varchar(30) NOT NULL
 )
go
 
 
 ALTER TABLE �������
        ADD PRIMARY KEY NONCLUSTERED (������ID)
go
 
 
 CREATE TABLE ���������������� (
        ���������������      varchar(20) NOT NULL,
        �������ID            int NOT NULL
 )
go
 
 
 ALTER TABLE ����������������
        ADD PRIMARY KEY NONCLUSTERED (�������ID)
go
 
 
 CREATE TABLE ���������������� (
        ���������            varchar(30) NOT NULL,
        ���������ID          int NOT NULL
 )
go
 
 
 ALTER TABLE ����������������
        ADD PRIMARY KEY NONCLUSTERED (���������ID)
go
 
 
 ALTER TABLE ������������
        ADD FOREIGN KEY (�����ID)
                              REFERENCES �����������������
go
 
 
 ALTER TABLE ������
        ADD FOREIGN KEY (����������ID)
                              REFERENCES ����������
go
 
 
 ALTER TABLE ������
        ADD FOREIGN KEY (��������ID)
                              REFERENCES ��������
go
 
 
 ALTER TABLE ������
        ADD FOREIGN KEY (������ID)
                              REFERENCES �������
go
 
 
 ALTER TABLE ������
        ADD FOREIGN KEY (��������ID)
                              REFERENCES ��������
go
 
 
 ALTER TABLE ������
        ADD FOREIGN KEY (���������ID)
                              REFERENCES ����������������
go
 
 
 ALTER TABLE ������
        ADD FOREIGN KEY (�����������ID)
                              REFERENCES �����������
go
 
 
 ALTER TABLE ��������
        ADD FOREIGN KEY (�����ID)
                              REFERENCES �����������������
go
 
 
 ALTER TABLE ��������
        ADD FOREIGN KEY (�������ID)
                              REFERENCES ����������������
go
 
 
 ALTER TABLE �����������
        ADD FOREIGN KEY (������������ID)
                              REFERENCES �������������������
go
 
 