USE tc_portfolio;
GO

-- ============================
-- Company
-- ============================

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Company_Type_Status_DeletionDate')
    DROP INDEX IX_Company_Type_Status_DeletionDate ON CoreSchema.Company;
GO

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Company_DeletionDate')
    DROP INDEX IX_Company_DeletionDate ON CoreSchema.Company;
GO

-- ============================
-- CompanyBranch
-- ============================

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_CompanyBranch_TaxNumber')
    DROP INDEX UX_CompanyBranch_TaxNumber ON CoreSchema.CompanyBranch;
GO

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_CompanyBranch_Company_Status_DeletionDate')
    DROP INDEX IX_CompanyBranch_Company_Status_DeletionDate ON CoreSchema.CompanyBranch;
GO

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_CompanyBranch_IsHeadquarters')
    DROP INDEX IX_CompanyBranch_IsHeadquarters ON CoreSchema.CompanyBranch;
GO

-- ============================
-- Customer
-- ============================

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Customer_Email_CompanyBranch')
    DROP INDEX UX_Customer_Email_CompanyBranch ON CoreSchema.Customer;
GO

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Customer_CompanyBranch_DeletionDate')
    DROP INDEX IX_Customer_CompanyBranch_DeletionDate ON CoreSchema.Customer;
GO

-- ============================
-- Product
-- ============================

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Product_Type_Status_DeletionDate')
    DROP INDEX IX_Product_Type_Status_DeletionDate ON CoreSchema.Product;
GO

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Product_CompanyBranch')
    DROP INDEX IX_Product_CompanyBranch ON CoreSchema.Product;
GO
