USE tc_portfolio;
GO

-- ============================
-- Company
-- ============================

-- �ndice composto para queries por tipo/status com soft delete
CREATE NONCLUSTERED INDEX IX_Company_Type_Status_DeletionDate
ON CoreSchema.Company (Type, Status, DeletionDate)
INCLUDE (CompanyId, Name);
GO

-- �ndice auxiliar para soft delete isolado
CREATE NONCLUSTERED INDEX IX_Company_DeletionDate
ON CoreSchema.Company (DeletionDate);
GO

-- ============================
-- CompanyBranch
-- ============================

-- �ndice �nico para CNPJ por filial (somente registros ativos)
CREATE UNIQUE INDEX UX_CompanyBranch_TaxNumber
ON CoreSchema.CompanyBranch (TaxNumber)
WHERE DeletionDate IS NULL;
GO

-- �ndice para filiais ativas por empresa e status
CREATE NONCLUSTERED INDEX IX_CompanyBranch_Company_Status_DeletionDate
ON CoreSchema.CompanyBranch (CompanyId, Status, DeletionDate)
INCLUDE (CompanyBranchId, Name, IsHeadquarters);
GO

-- �ndice para matriz por empresa
CREATE NONCLUSTERED INDEX IX_CompanyBranch_IsHeadquarters
ON CoreSchema.CompanyBranch (IsHeadquarters)
WHERE DeletionDate IS NULL;
GO

-- ============================
-- Customer
-- ============================

-- �ndice �nico para email + filial (clientes ativos)
CREATE UNIQUE INDEX UX_Customer_Email_CompanyBranch
ON CoreSchema.Customer (Email, CompanyBranchId)
WHERE DeletionDate IS NULL;
GO

-- �ndice para busca por clientes ativos por filial
CREATE NONCLUSTERED INDEX IX_Customer_CompanyBranch_DeletionDate
ON CoreSchema.Customer (CompanyBranchId, DeletionDate)
INCLUDE (CustomerId, Name, Email);
GO

-- ============================
-- Product
-- ============================

-- �ndice composto por tipo, status e soft delete
CREATE NONCLUSTERED INDEX IX_Product_Type_Status_DeletionDate
ON CoreSchema.Product (Type, Status, DeletionDate)
INCLUDE (ProductId, Name, CompanyBranchId);
GO

-- �ndice para produtos ativos por filial
CREATE NONCLUSTERED INDEX IX_Product_CompanyBranch
ON CoreSchema.Product (CompanyBranchId)
WHERE DeletionDate IS NULL;
GO
