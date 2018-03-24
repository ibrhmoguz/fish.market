# FishMarket


Users
-------------
ibrahim@test.com
123

test@test.com
123

Restful API Address
-------------------
http://localhost:50018/api/FishApi



Create Error Log DB
-------------------
CREATE DATABASE [NLogDb]
USE [NLogDb]
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventDateTime] [datetime] NULL,
	[EventLevel] [nvarchar](250) NULL,
	[UserName] [nvarchar](250) NULL,
	[MachineName] [nvarchar](250) NULL,
	[EventMessage] [nvarchar](max) NULL,
	[StackTrace] [nvarchar](4000) NULL,
	[Type] [nvarchar](4000) NULL,
	[Method] [nvarchar](4000) NULL,
	[ErrorMessage] [nvarchar](4000) NULL,
	[ClientName] [nvarchar](4000) NULL,
	[MessageType] [nvarchar](250) NULL,
	[ReqResCorrelationId] [nvarchar](250) NULL
) ON [PRIMARY]
