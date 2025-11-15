-- ==========================================
-- Tabela Person
-- ==========================================
CREATE TABLE "CoreSchema"."Person" (
    "PersonId" SERIAL PRIMARY KEY,
    "Name" VARCHAR(200) NOT NULL,
    "TaxNumber" VARCHAR(20) NOT NULL UNIQUE,
    "Type" INT NOT NULL,
    "Status" INT NOT NULL,
    "CreationDate" TIMESTAMPTZ DEFAULT now(),
    "UpdateDate" TIMESTAMPTZ DEFAULT now(),
    "CreationUserId" INT NOT NULL,
    "UpdateUserId" INT NOT NULL,
    "DeletionDate" TIMESTAMPTZ DEFAULT NULL
);

CREATE INDEX "IX_Person_DeletionDate"
ON "CoreSchema"."Person" ("DeletionDate");

CREATE INDEX "IX_Person_TaxNumber_DeletionDate"
ON "CoreSchema"."Person" ("TaxNumber", "DeletionDate")
INCLUDE ("PersonId", "Name");

-- ==========================================
-- Tabela Company
-- ==========================================
CREATE TABLE "CoreSchema"."Company" (
    "CompanyId" SERIAL PRIMARY KEY,
    "Name" VARCHAR(200) NOT NULL,
    "PersonId" INT NOT NULL,
    "Type" INT NOT NULL,
    "Status" INT NOT NULL,
    "CreationDate" TIMESTAMPTZ DEFAULT now(),
    "UpdateDate" TIMESTAMPTZ DEFAULT now(),
    "CreationUserId" INT NOT NULL,
    "UpdateUserId" INT NOT NULL,
    "DeletionDate" TIMESTAMPTZ DEFAULT NULL,
    CONSTRAINT fk_company_person FOREIGN KEY ("PersonId")
        REFERENCES "CoreSchema"."Person"("PersonId")
);

CREATE INDEX "IX_Company_DeletionDate"
ON "CoreSchema"."Company" ("DeletionDate");

CREATE INDEX "IX_Company_Type_Status_DeletionDate"
ON "CoreSchema"."Company" ("Type", "Status", "DeletionDate")
INCLUDE ("CompanyId", "Name");

-- ==========================================
-- Tabela BusinessUnit
-- ==========================================
CREATE TABLE "CoreSchema"."BusinessUnit" (
    "BusinessUnitId" SERIAL PRIMARY KEY,
    "Name" VARCHAR(200) NOT NULL,
    "PersonId" INT NOT NULL,
    "IsMain" BOOLEAN NOT NULL DEFAULT FALSE,
    "Status" INT NOT NULL,
    "CompanyId" INT NOT NULL,
    "CreationDate" TIMESTAMPTZ DEFAULT now(),
    "UpdateDate" TIMESTAMPTZ DEFAULT now(),
    "CreationUserId" INT NOT NULL,
    "UpdateUserId" INT NOT NULL,
    "DeletionDate" TIMESTAMPTZ DEFAULT NULL,
    CONSTRAINT fk_businessunit_company FOREIGN KEY ("CompanyId")
        REFERENCES "CoreSchema"."Company"("CompanyId")
);

CREATE INDEX "IX_BusinessUnit_Company_Status_DeletionDate"
ON "CoreSchema"."BusinessUnit" ("CompanyId", "Status", "DeletionDate")
INCLUDE ("BusinessUnitId", "Name", "IsMain");

CREATE INDEX "IX_BusinessUnit_IsMain"
ON "CoreSchema"."BusinessUnit" ("IsMain")
WHERE "DeletionDate" IS NULL;

-- ==========================================
-- Tabela Customer
-- ==========================================
CREATE TABLE "CoreSchema"."Customer" (
    "CustomerId" SERIAL PRIMARY KEY,
    "Name" VARCHAR(200) NOT NULL,
    "Email" VARCHAR(200) NOT NULL,
    "Phone" VARCHAR(50) NULL,
    "TaxNumber" VARCHAR(20) NULL,
    "BusinessUnitId" INT NOT NULL,
    "CreationDate" TIMESTAMPTZ DEFAULT now(),
    "UpdateDate" TIMESTAMPTZ DEFAULT now(),
    "CreationUserId" INT NOT NULL,
    "UpdateUserId" INT NOT NULL,
    "DeletionDate" TIMESTAMPTZ DEFAULT NULL,
    CONSTRAINT uq_customer_email UNIQUE ("Email"),
    CONSTRAINT fk_customer_businessunit FOREIGN KEY ("BusinessUnitId")
        REFERENCES "CoreSchema"."BusinessUnit"("BusinessUnitId")
);

CREATE UNIQUE INDEX "UX_Customer_Email_BusinessUnit"
ON "CoreSchema"."Customer" ("Email", "BusinessUnitId")
WHERE "DeletionDate" IS NULL;

CREATE INDEX "IX_Customer_BusinessUnit_DeletionDate"
ON "CoreSchema"."Customer" ("BusinessUnitId", "DeletionDate")
INCLUDE ("CustomerId", "Name", "Email");

-- ==========================================
-- Tabela Product
-- ==========================================
CREATE TABLE "CoreSchema"."Product" (
    "ProductId" SERIAL PRIMARY KEY,
    "Name" VARCHAR(200) NOT NULL,
    "Type" INT NOT NULL,
    "Status" INT NOT NULL,
    "SourceType" INT NOT NULL,
    "BusinessUnitId" INT NOT NULL,
    "CreationDate" TIMESTAMPTZ DEFAULT now(),
    "UpdateDate" TIMESTAMPTZ DEFAULT now(),
    "CreationUserId" INT NOT NULL,
    "UpdateUserId" INT NOT NULL,
    "DeletionDate" TIMESTAMPTZ DEFAULT NULL,
    CONSTRAINT fk_product_businessunit FOREIGN KEY ("BusinessUnitId")
        REFERENCES "CoreSchema"."BusinessUnit"("BusinessUnitId")
);

CREATE INDEX "IX_Product_Type_Status_DeletionDate"
ON "CoreSchema"."Product" ("Type", "Status", "DeletionDate")
INCLUDE ("ProductId", "Name", "BusinessUnitId");

CREATE INDEX "IX_Product_BusinessUnit"
ON "CoreSchema"."Product" ("BusinessUnitId")
WHERE "DeletionDate" IS NULL;
