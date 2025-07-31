--002_CoreScripts/001_CreateTablesCoreSchema

USE tc_portfolio;
GO

CREATE TABLE CoreSchema.Company (
    CompanyId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    TaxNumber VARCHAR(20) NOT NULL UNIQUE,
    Type INT NOT NULL,
    Status INT NOT NULL,
    CreationDate DATETIME2 DEFAULT GETUTCDATE(),
    UpdateDate DATETIME2 DEFAULT GETUTCDATE(),
    CreationUserId INT NOT NULL,
    UpdateUserId INT NOT NULL,
    DeletionDate DATETIME2 DEFAULT NULL
);

CREATE TABLE CoreSchema.CompanyBranch (
    CompanyBranchId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    TaxNumber VARCHAR(20) NOT NULL UNIQUE,
    IsHeadquarters BIT NOT NULL DEFAULT 0,
    Status INT NOT NULL,
    CompanyId INT NOT NULL,
    CreationDate DATETIME2 DEFAULT GETUTCDATE(),
    UpdateDate DATETIME2 DEFAULT GETUTCDATE(),
    CreationUserId INT NOT NULL,
    UpdateUserId INT NOT NULL,
    DeletionDate DATETIME2 DEFAULT NULL,
    FOREIGN KEY (CompanyId) REFERENCES CoreSchema.Company(CompanyId)
);

CREATE TABLE CoreSchema.Customer (
    CustomerId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(50) NULL,
    TaxNumber VARCHAR(20) NOT NULL,
    CompanyBranchId INT NOT NULL,
    CreationDate DATETIME2 DEFAULT GETUTCDATE(),
    UpdateDate DATETIME2 DEFAULT GETUTCDATE(),
    CreationUserId INT NOT NULL,
    UpdateUserId INT NOT NULL,
    DeletionDate DATETIME2 DEFAULT NULL,
    FOREIGN KEY (CompanyBranchId) REFERENCES CoreSchema.CompanyBranch(CompanyBranchId)
);

CREATE TABLE CoreSchema.Product (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    Type INT NOT NULL,
    Status INT NOT NULL,
    SourceType INT NOT NULL,
    CompanyBranchId INT NOT NULL,
    CreationDate DATETIME2 DEFAULT GETUTCDATE(),
    UpdateDate DATETIME2 DEFAULT GETUTCDATE(),
    CreationUserId INT NOT NULL,
    UpdateUserId INT NOT NULL,
    DeletionDate DATETIME2 DEFAULT NULL,
    FOREIGN KEY (CompanyBranchId) REFERENCES CoreSchema.CompanyBranch(CompanyBranchId)
);