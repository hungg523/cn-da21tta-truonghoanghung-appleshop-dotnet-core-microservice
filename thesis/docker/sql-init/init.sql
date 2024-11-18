-- ProductCategoryService -------------------------------------------------------
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

-- UserService -------------------------------------------------------
CREATE TABLE [user] (
    [id] INT PRIMARY KEY IDENTITY(1,1),
    [username] VARCHAR(255) NOT NULL,
    [email] VARCHAR(255) NOT NULL,
    [password] VARCHAR(255) NOT NULL,
    [firstname] NVARCHAR(255) NOT NULL,
    [lastname] NVARCHAR(255) NOT NULL,
    [phonenumber] VARCHAR(50),
    [createddate] DATETIME NOT NULL DEFAULT GETDATE(),
    [isactived] INT NOT NULL DEFAULT 0
);

CREATE TABLE [useraddress] (
    [id] INT PRIMARY KEY IDENTITY(1,1),
    [addressline1] NVARCHAR(255) NOT NULL,
    [addressline2] NVARCHAR(255),
    [city] NVARCHAR(100) NOT NULL,
    [country] NVARCHAR(100) NOT NULL,
    [userid] INT NOT NULL
);

-- OrderService -------------------------------------------------------
CREATE TABLE [order] (
    [id] INT PRIMARY KEY IDENTITY(1,1),
    [orderdate] DATETIME NOT NULL DEFAULT GETDATE(),
    [totalamount] DECIMAL(18, 2) NOT NULL,
    [orderstatus] NVARCHAR(50) NOT NULL,
    [createddate] DATETIME NOT NULL DEFAULT GETDATE(),
    [modifieddate] DATETIME NULL,
    [userid] INT NOT NULL,
);

CREATE TABLE [orderitems] (
    [id] INT PRIMARY KEY IDENTITY(1,1),
	[orderid] INT NOT NULL,
    [productid] INT NOT NULL,
    [quantity] INT NOT NULL,
    [unitprice] DECIMAL(18, 2) NOT NULL,
    [totalprice] DECIMAL(18, 2) NOT NULL,
);

-- CartService -------------------------------------------------------
CREATE TABLE [cart] (
    [id] INT PRIMARY KEY IDENTITY(1,1),
    [userid] INT NOT NULL,
    [createddate] DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE [cartitems] (
    [id] INT PRIMARY KEY IDENTITY(1,1),
    [cartid] INT NOT NULL,
    [productid] INT NOT NULL,
    [quantity] INT NOT NULL,
    [unitprice] DECIMAL(18, 2) NOT NULL,
);

-- PromotionService -------------------------------------------------------
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

-- InventoryService -------------------------------------------------------
Create database AppleInventory
CREATE TABLE [inventory] (
    [inventory_id] INT PRIMARY KEY IDENTITY(1,1),
    [product_id] INT NOT NULL,
    [available_stock] INT NOT NULL,
    [last_updated] DATETIME NOT NULL DEFAULT GETDATE()
);

-- InventoryService -------------------------------------------------------
Create database AppleColor
CREATE TABLE [color] (
    [color_id] INT PRIMARY KEY IDENTITY(1,1),
    [name] NVARCHAR(64) NOT NULL
);

CREATE TABLE [product_color] (
    [color_id] INT NOT NULL,
    [product_id] INT NOT NULL,
);

-- AuthenticationService
CREATE TABLE [usertokens] (
    [id] INT PRIMARY KEY IDENTITY(1,1),
    [userid] INT NOT NULL,
    [accesstoken] NVARCHAR(MAX) NOT NULL,
    [refreshtoken] NVARCHAR(MAX) NOT NULL,
    [expirationdate] DATETIME NOT NULL,
);