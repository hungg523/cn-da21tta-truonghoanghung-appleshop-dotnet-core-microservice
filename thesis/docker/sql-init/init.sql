-- Product Category Service -------------------------------------------------------
Create Database AppleCategory
CREATE TABLE [category] (
    [category_id] INT PRIMARY KEY IDENTITY(1,1),
    [name] NVARCHAR(255) NOT NULL,
    [description] NVARCHAR(512),
	[icon] varchar(32),
    [createddate] DATETIME NOT NULL DEFAULT GETDATE(),
    [isactived] INT NOT NULL DEFAULT 0
);

CREATE TABLE [product] (
    [product_id] INT PRIMARY KEY IDENTITY(1,1),
    [name] NVARCHAR(255) NOT NULL,
    [description] NVARCHAR(4000),
    [price] DECIMAL(18, 2) NOT NULL,
    [discount_price] DECIMAL(18, 2),
    [createddate] DATETIME NOT NULL,
    [category_id] INT,
    [isactived] INT NOT NULL DEFAULT 0,
);

CREATE TABLE [product_image] (
    [product_image_id] INT PRIMARY KEY IDENTITY(1,1),
	[title] nvarchar(128),
	[position] int not null,
    [image_url] VARCHAR(64) NOT NULL,
    [product_id] INT NOT NULL
);

CREATE TABLE [color] (
    [color_id] INT PRIMARY KEY IDENTITY(1,1),
    [name] NVARCHAR(64) NOT NULL
);

CREATE TABLE [product_color] (
    [color_id] INT NOT NULL,
    [product_id] INT NOT NULL,
);

-- Order Service -------------------------------------------------------
Create database AppleOrder
CREATE TABLE [order] (
    [order_id] INT PRIMARY KEY IDENTITY(1,1),
    [totalamount] DECIMAL(18, 2) NOT NULL,
    [orderstatus] NVARCHAR(50) NOT NULL,
    [payment] NVARCHAR(64) NOT NULL,
    [createddate] DATETIME NOT NULL DEFAULT GETDATE(),
    [promotion_id] Int NULL,
    [user_id] INT NOT NULL,
);

CREATE TABLE [order_item] (
    [order_item_id] INT PRIMARY KEY IDENTITY(1,1),
	[order_id] INT NOT NULL,
    [product_id] INT NOT NULL,
    [quantity] INT NOT NULL,
    [unit_price] DECIMAL(18, 2) NOT NULL,
    [totalprice] DECIMAL(18, 2) NOT NULL,
);

-- Cart Service -------------------------------------------------------
CREATE database AppleCart
CREATE TABLE [cart] (
    [cart_id] INT PRIMARY KEY IDENTITY(1,1),
    [user_id] INT NOT NULL,
    [createddate] DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE [cart_item] (
    [cart_item-id] INT PRIMARY KEY IDENTITY(1,1),
    [cart_id] INT NOT NULL,
    [product_id] INT NOT NULL,
    [quantity] INT NOT NULL,
    [unit_price] DECIMAL(18, 2) NOT NULL,
);

-- Promotion Service -------------------------------------------------------
CREATE TABLE [promotion] (
    [id] INT PRIMARY KEY IDENTITY(1,1),
    [code] NVARCHAR(50) NOT NULL,
    [description] NVARCHAR(MAX),
    [discountpercentage] DECIMAL(5, 2) NOT NULL,
    [startdate] DATETIME NOT NULL,
    [enddate] DATETIME NOT NULL,
    [isactived] INT NOT NULL DEFAULT 0
);

CREATE TABLE [appliedpromotions] (
    [id] INT PRIMARY KEY IDENTITY(1,1),
    [orderid] INT NOT NULL,
    [promotionid] INT NOT NULL,
    [discountamount] DECIMAL(18, 2) NOT NULL
);

-- Inventory Service -------------------------------------------------------
Create database AppleInventory
CREATE TABLE [inventory] (
    [inventory_id] INT PRIMARY KEY IDENTITY(1,1),
    [product_id] INT NOT NULL,
    [available_stock] INT NOT NULL,
    [last_updated] DATETIME NOT NULL DEFAULT GETDATE()
);

-- User Service -------------------------------------------------------
Create database AppleUser
CREATE TABLE [user] (
    [user_id] INT PRIMARY KEY IDENTITY(1,1),
    [username] VARCHAR(255) NOT NULL,
    [email] VARCHAR(255) NOT NULL,
    [password] VARCHAR(255) NOT NULL,
    [image_url] varchar(64),
    [role] int not null,
    [otp] varchar(6) not null,
    [otp_attempt] INT DEFAULT 0,
    [otp_expiration] DATETIME not null,
    [createddate] DATETIME NOT NULL DEFAULT GETDATE(),
    [last_login] DATETIME,
    [isactived] INT NOT NULL DEFAULT 0
);

CREATE TABLE [useraddress] (
    [useraddress_id] INT PRIMARY KEY IDENTITY(1,1),
    [firstname] NVARCHAR(255) NOT NULL,
    [lastname] NVARCHAR(255) NOT NULL,
    [addressline] NVARCHAR(255) NOT NULL,
    [phonenumber] VARCHAR(50),
    [province] NVARCHAR(100) NOT NULL,
    [district] NVARCHAR(100) NOT NULL,
    [user_id] INT NOT NULL
);

-- Authentication Service -------------------------------------------------------
Create database AppleAuth
CREATE TABLE [usertoken] (
    [usertoken_id] INT PRIMARY KEY IDENTITY(1,1),
    [user_id] INT NOT NULL,
    [refreshtoken] VARCHAR(MAX) NOT NULL,
    [issued_at] DATETIME NOT NULL DEFAULT GETDATE(),
    [expiration] DATETIME NOT NULL,
    [revoked_at] DATETIME,
    [isactived] INT NOT NULL DEFAULT 0
);