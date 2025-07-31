USE tc_portfolio;
GO

-- ============================
-- Company
-- ============================

-- Índice composto para queries por tipo/status com soft delete
CREATE NONCLUSTERED INDEX IX_Company_Type_Status_DeletionDate
ON CoreSchema.Company (Type, Status, DeletionDate)
INCLUDE (CompanyId, Name);
GO

-- Índice auxiliar para soft delete isolado
CREATE NONCLUSTERED INDEX IX_Company_DeletionDate
ON CoreSchema.Company (DeletionDate);
GO

-- ============================
-- CompanyBranch
-- ============================

-- Índice único para CNPJ por filial (somente registros ativos)
CREATE UNIQUE INDEX UX_CompanyBranch_TaxNumber
ON CoreSchema.CompanyBranch (TaxNumber)
WHERE DeletionDate IS NULL;
GO

-- Índice para filiais ativas por empresa e status
CREATE NONCLUSTERED INDEX IX_CompanyBranch_Company_Status_DeletionDate
ON CoreSchema.CompanyBranch (CompanyId, Status, DeletionDate)
INCLUDE (CompanyBranchId, Name, IsHeadquarters);
GO

-- Índice para matriz por empresa
CREATE NONCLUSTERED INDEX IX_CompanyBranch_IsHeadquarters
ON CoreSchema.CompanyBranch (IsHeadquarters)
WHERE DeletionDate IS NULL;
GO

-- ============================
-- Customer
-- ============================

-- Índice único para email + filial (clientes ativos)
CREATE UNIQUE INDEX UX_Customer_Email_CompanyBranch
ON CoreSchema.Customer (Email, CompanyBranchId)
WHERE DeletionDate IS NULL;
GO

-- Índice para busca por clientes ativos por filial
CREATE NONCLUSTERED INDEX IX_Customer_CompanyBranch_DeletionDate
ON CoreSchema.Customer (CompanyBranchId, DeletionDate)
INCLUDE (CustomerId, Name, Email);
GO

-- ============================
-- Product
-- ============================

-- Índice composto por tipo, status e soft delete
CREATE NONCLUSTERED INDEX IX_Product_Type_Status_DeletionDate
ON CoreSchema.Product (Type, Status, DeletionDate)
INCLUDE (ProductId, Name, CompanyBranchId);
GO

-- Índice para produtos ativos por filial
CREATE NONCLUSTERED INDEX IX_Product_CompanyBranch
ON CoreSchema.Product (CompanyBranchId)
WHERE DeletionDate IS NULL;
GO
