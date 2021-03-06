USE [master]
GO
/****** Object:  Database [ShoppingCart]    Script Date: 4/27/2021 12:38:58 PM ******/
CREATE DATABASE [ShoppingCart]
GO

USE [ShoppingCart]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/27/2021 12:38:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 4/27/2021 12:38:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [varchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 4/27/2021 12:38:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[Image] [varchar](100) NOT NULL,
	[Price] [decimal](9, 2) NOT NULL,
	[StockLevel] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductOrder]    Script Date: 4/27/2021 12:38:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductOrder](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[OrderId] [varchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_ProductOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/27/2021 12:38:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[Password] [varchar](200) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210425130113_Initial', N'5.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210425193159_GenerateOrderNo_Sp', N'5.0.5')
GO
INSERT [dbo].[Order] ([Id], [DateCreated], [UserId]) VALUES (N'ORD00001', CAST(N'2021-04-27T12:24:16.110' AS DateTime), N'55d17744-9da8-4b76-a783-9f0c0bc78812')
INSERT [dbo].[Order] ([Id], [DateCreated], [UserId]) VALUES (N'ORD00002', CAST(N'2021-04-27T12:35:12.210' AS DateTime), N'55d17744-9da8-4b76-a783-9f0c0bc78812')
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Image], [Price], [StockLevel]) VALUES (N'88706736-1195-492a-bbdb-0f891ff7b1de', N'Hux Shoes', N'Old Khakhi Black Shoes', N'old khakhi.jpg', CAST(1000.00 AS Decimal(9, 2)), 24)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Image], [Price], [StockLevel]) VALUES (N'ee4f159e-8823-47f2-a203-4e5c40264b4b', N'All Star', N'Black Converse All Star', N'all star.jpg', CAST(800.00 AS Decimal(9, 2)), 18)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Image], [Price], [StockLevel]) VALUES (N'954fc33f-11aa-4910-90c4-86bad0dec99e', N'Bass Shoes', N'Black Slassic shoes', N'bass shoes.jpg', CAST(2300.00 AS Decimal(9, 2)), 26)
GO
INSERT [dbo].[ProductOrder] ([Id], [ProductId], [OrderId], [Quantity]) VALUES (N'4625c281-1af4-4803-a9ce-2ab92442327a', N'954fc33f-11aa-4910-90c4-86bad0dec99e', N'ORD00002', 2)
INSERT [dbo].[ProductOrder] ([Id], [ProductId], [OrderId], [Quantity]) VALUES (N'a4a868e5-80fe-40be-b654-49962af89d7d', N'ee4f159e-8823-47f2-a203-4e5c40264b4b', N'ORD00001', 1)
INSERT [dbo].[ProductOrder] ([Id], [ProductId], [OrderId], [Quantity]) VALUES (N'b5ec5e91-3b12-4e95-8877-4deecc005731', N'88706736-1195-492a-bbdb-0f891ff7b1de', N'ORD00002', 3)
INSERT [dbo].[ProductOrder] ([Id], [ProductId], [OrderId], [Quantity]) VALUES (N'94422874-cbbd-42fb-a2bf-a4caed1fedcc', N'954fc33f-11aa-4910-90c4-86bad0dec99e', N'ORD00001', 2)
INSERT [dbo].[ProductOrder] ([Id], [ProductId], [OrderId], [Quantity]) VALUES (N'69ac0e64-f156-4da2-b3cd-ab9acc7c296e', N'88706736-1195-492a-bbdb-0f891ff7b1de', N'ORD00001', 3)
INSERT [dbo].[ProductOrder] ([Id], [ProductId], [OrderId], [Quantity]) VALUES (N'db694db5-333c-408b-acc4-fb6c5ca52c4a', N'ee4f159e-8823-47f2-a203-4e5c40264b4b', N'ORD00002', 1)
GO
INSERT [dbo].[User] ([Id], [Email], [Password]) VALUES (N'55d17744-9da8-4b76-a783-9f0c0bc78812', N'menelismthembu@gmail.com', N'$2a$11$/m.1Er3Bj3EKZnzHVD2RzusQ7e5CCRlCb.lNNpl5vtWBVfIOWCp3m')
GO
/****** Object:  Index [IX_Order_UserId]    Script Date: 4/27/2021 12:38:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_Order_UserId] ON [dbo].[Order]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductOrder_OrderId]    Script Date: 4/27/2021 12:38:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductOrder_OrderId] ON [dbo].[ProductOrder]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductOrder_ProductId]    Script Date: 4/27/2021 12:38:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductOrder_ProductId] ON [dbo].[ProductOrder]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[ProductOrder] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_UserId]
GO
ALTER TABLE [dbo].[ProductOrder]  WITH CHECK ADD  CONSTRAINT [FK_ProductOrder_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[ProductOrder] CHECK CONSTRAINT [FK_ProductOrder_OrderId]
GO
ALTER TABLE [dbo].[ProductOrder]  WITH CHECK ADD  CONSTRAINT [FK_ProductOrder_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[ProductOrder] CHECK CONSTRAINT [FK_ProductOrder_ProductId]
GO
/****** Object:  StoredProcedure [dbo].[spGenerateOrderNo]    Script Date: 4/27/2021 12:38:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spGenerateOrderNo]
AS
BEGIN
	DECLARE @LastOrderNumber VARCHAR(50),@OrderNo VARCHAR(50)
	SET @LastOrderNumber = (SELECT TOP 1 CONVERT(VARCHAR(100), CONVERT(INT, LTRIM(RTRIM(REPLACE(UPPER(Id),'ORD','')))) + 1)
							FROM [Order]
							ORDER BY CONVERT(INT, LTRIM(RTRIM(REPLACE(Id,'ORD','')))) DESC)
	SET @OrderNo = 'ORD'
	IF @LastOrderNumber IS NULL
	BEGIN
		 SET @OrderNo = FORMATMESSAGE('%s00001', @OrderNo)
	END
	ELSE
		BEGIN
				SET @OrderNo = CASE LEN(@LastOrderNumber) WHEN 1 THEN FORMATMESSAGE('%s%s%s', @OrderNo, REPLICATE('0',4), @LastOrderNumber)
												WHEN 2 THEN FORMATMESSAGE('%s%s%s', @OrderNo, REPLICATE('0',3), @LastOrderNumber)
												WHEN 3 THEN FORMATMESSAGE('%s%s%s', @OrderNo, REPLICATE('0',2), @LastOrderNumber)
												WHEN 4 THEN FORMATMESSAGE('%s%s%s', @OrderNo, REPLICATE('0',1), @LastOrderNumber)
												ELSE FORMATMESSAGE('%s%s', @OrderNo,@LastOrderNumber) END
		END
	SELECT @OrderNo AS OrderNo
END
GO
USE [master]
GO
ALTER DATABASE [ShoppingCart] SET  READ_WRITE 
GO
