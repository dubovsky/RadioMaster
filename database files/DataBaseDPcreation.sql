USE [master]
GO
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'Сервис')
DROP DATABASE [Сервис]
GO
USE [master]
CREATE DATABASE [Сервис] ON  PRIMARY 
( NAME = N'Сервис', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\Сервис.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Сервис_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\Сервис_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
ALTER DATABASE [Сервис] SET COMPATIBILITY_LEVEL = 100
GO
USE [Сервис]



 
 CREATE TABLE ВидРемонта (
        ТипРемонта           varchar(50) NOT NULL,
        ВидРемонтаID         int NOT NULL,
        СрокРемонта          int NOT NULL,
        ЦенаРемонта          float NOT NULL
 )
go
 
 
 ALTER TABLE ВидРемонта
        ADD PRIMARY KEY NONCLUSTERED (ВидРемонтаID)
go
 
 
 CREATE TABLE Гарантии (
        Гарантия             varchar(20) NOT NULL,
        ГарантияID           int NOT NULL
 )
go
 
 
 ALTER TABLE Гарантии
        ADD PRIMARY KEY NONCLUSTERED (ГарантияID)
go
 
 
 CREATE TABLE ЗаводИзготовитель (
        НазваниеЗавода       varchar(20) NOT NULL,
        ЗаводID              int NOT NULL,
        Телефон              varchar(11) NOT NULL,
        Адрес                varchar(40) NOT NULL,
        Факс                 varchar(11) NULL
 )
go
 
 
 ALTER TABLE ЗаводИзготовитель
        ADD PRIMARY KEY NONCLUSTERED (ЗаводID)
go
 
 
 CREATE TABLE ЗаказДеталей (
        НазваниеДетали       varchar(50) NOT NULL,
        НоваяДетальID        int NOT NULL,
        Дата                 date NOT NULL,
        Количество           int NOT NULL,
        ЗаводID              int NOT NULL
 )
go
 
 
 ALTER TABLE ЗаказДеталей
        ADD PRIMARY KEY NONCLUSTERED (НоваяДетальID)
go
 
 
 CREATE TABLE Заказы (
        ДатаЗаказа           date NOT NULL,
        ЗаказID              int IDENTITY(1,1),
        Поломка              varchar(50) NOT NULL,
        ИсполнительID        int NOT NULL,
        СостояниеID          int NOT NULL,
        ГарантияID           int NOT NULL,
        КлиентID             int NOT NULL,
        ВидРемонтаID         int NOT NULL,
        ЗапчастьID           int NULL
 )
go
 
 
 ALTER TABLE Заказы
        ADD PRIMARY KEY NONCLUSTERED (ЗаказID)
go
 
 
 CREATE TABLE Запчасти (
        ЦенаЗапчасти         float NOT NULL,
        ЗапчастьID           int NOT NULL,
        НаличиеID            int NOT NULL,
        ЗаводID              int NOT NULL,
        НазваниеЗапчасти     varchar(50) NOT NULL
 )
go
 
 
 ALTER TABLE Запчасти
        ADD PRIMARY KEY NONCLUSTERED (ЗапчастьID)
go
 
 
 CREATE TABLE Исполнители (
        Стаж                 int NOT NULL,
        ИсполнительID        int NOT NULL,
        Телефон              varchar(11) NOT NULL,
        ФИО                  varchar(30) NOT NULL,
        КвалификацияID       int NOT NULL
 )
go
 
 
 ALTER TABLE Исполнители
        ADD PRIMARY KEY NONCLUSTERED (ИсполнительID)
go
 
 
 CREATE TABLE КвалификацияМастера (
        Квалификация         varchar(30) NULL,
        КвалификацияID       int NOT NULL
 )
go
 
 
 ALTER TABLE КвалификацияМастера
        ADD PRIMARY KEY NONCLUSTERED (КвалификацияID)
go
 
 
 CREATE TABLE Клиенты (
        Телефон              varchar(11) NOT NULL,
        КлиентID             int IDENTITY(1,1),
        Адрес                varchar(40) NOT NULL,
        ФИО                  varchar(30) NOT NULL
 )
go
 
 
 ALTER TABLE Клиенты
        ADD PRIMARY KEY NONCLUSTERED (КлиентID)
go
 
 
 CREATE TABLE НаличиеЗапчастей (
        НаличиеЗапчасти      varchar(20) NOT NULL,
        НаличиеID            int NOT NULL
 )
go
 
 
 ALTER TABLE НаличиеЗапчастей
        ADD PRIMARY KEY NONCLUSTERED (НаличиеID)
go
 
 
 CREATE TABLE СостояниеРемонта (
        Состояние            varchar(30) NOT NULL,
        СостояниеID          int NOT NULL
 )
go
 
 
 ALTER TABLE СостояниеРемонта
        ADD PRIMARY KEY NONCLUSTERED (СостояниеID)
go
 
 
 ALTER TABLE ЗаказДеталей
        ADD FOREIGN KEY (ЗаводID)
                              REFERENCES ЗаводИзготовитель
go
 
 
 ALTER TABLE Заказы
        ADD FOREIGN KEY (ВидРемонтаID)
                              REFERENCES ВидРемонта
go
 
 
 ALTER TABLE Заказы
        ADD FOREIGN KEY (ЗапчастьID)
                              REFERENCES Запчасти
go
 
 
 ALTER TABLE Заказы
        ADD FOREIGN KEY (КлиентID)
                              REFERENCES Клиенты
go
 
 
 ALTER TABLE Заказы
        ADD FOREIGN KEY (ГарантияID)
                              REFERENCES Гарантии
go
 
 
 ALTER TABLE Заказы
        ADD FOREIGN KEY (СостояниеID)
                              REFERENCES СостояниеРемонта
go
 
 
 ALTER TABLE Заказы
        ADD FOREIGN KEY (ИсполнительID)
                              REFERENCES Исполнители
go
 
 
 ALTER TABLE Запчасти
        ADD FOREIGN KEY (ЗаводID)
                              REFERENCES ЗаводИзготовитель
go
 
 
 ALTER TABLE Запчасти
        ADD FOREIGN KEY (НаличиеID)
                              REFERENCES НаличиеЗапчастей
go
 
 
 ALTER TABLE Исполнители
        ADD FOREIGN KEY (КвалификацияID)
                              REFERENCES КвалификацияМастера
go
 
 