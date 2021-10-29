CREATE TABLE [dbo].[Order]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [Status] INT NOT NULL, 
    [PaymentType] INT NOT NULL, 
    [TotalPay] DECIMAL(18, 5) NOT NULL, 
    [ActualPay] DECIMAL(18, 5) NOT NULL, 
    [CreateTime] DATETIME NOT NULL DEFAULT GetDate()
);
go
CREATE TABLE [dbo].[Goods] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (50) NOT NULL,
    [Code]  NVARCHAR (50) NOT NULL,
    [Color] INT           NOT NULL,
    [Price] DECIMAL (18)  NOT NULL,
    [Stock] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
go
CREATE TABLE [dbo].[Member] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    [Password] NVARCHAR (50) NOT NULL,
    [Nickname] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

go
CREATE TABLE [dbo].[SysAdmin] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    [Password] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
go
SET IDENTITY_INSERT [dbo].[Goods] ON
INSERT INTO [dbo].[Goods] ([Id], [Name], [Code], [Color], [Price], [Stock]) VALUES (1, N'手机', N'SJ', 1, CAST(998 AS Decimal(18, 0)), 20)
INSERT INTO [dbo].[Goods] ([Id], [Name], [Code], [Color], [Price], [Stock]) VALUES (2, N'IPhoneX Max', N'X Max', 2, CAST(8800 AS Decimal(18, 0)), 2)
SET IDENTITY_INSERT [dbo].[Goods] OFF
go
SET IDENTITY_INSERT [dbo].[Member] ON
INSERT INTO [dbo].[Member] ([Id], [Name], [Password], [Nickname]) VALUES (1, N'member01', N'123456', N'奥特曼')
INSERT INTO [dbo].[Member] ([Id], [Name], [Password], [Nickname]) VALUES (2, N'member02', N'123456', N'小怪兽')
SET IDENTITY_INSERT [dbo].[Member] OFF


