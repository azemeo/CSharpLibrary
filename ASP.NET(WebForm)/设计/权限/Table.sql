if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TModule]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[TModule]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TRoleInfo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[TRoleInfo]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TUserInfo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[TUserInfo]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Tfunction]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Tfunction]
GO

CREATE TABLE [dbo].[TModule] (
	[innerId] [int] IDENTITY (1, 1) NOT NULL ,
	[moduleNo] [int] NOT NULL ,
	[moduleName] [nvarchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[moduelEName] [nvarchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[moduleNum] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TRoleInfo] (
	[InnerId] [int] IDENTITY (1, 1) NOT NULL ,
	[RoleName] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[RoleValue] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[RoleDesc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TUserInfo] (
	[innerId] [int] IDENTITY (1, 1) NOT NULL ,
	[YourName] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[Acco] [char] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[Pass] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[RoleID] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Tfunction] (
	[innerId] [int] IDENTITY (1, 1) NOT NULL ,
	[funcNo] [int] NOT NULL ,
	[funcName] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[funcEName] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[moduleNo] [int] NOT NULL 
) ON [PRIMARY]
GO