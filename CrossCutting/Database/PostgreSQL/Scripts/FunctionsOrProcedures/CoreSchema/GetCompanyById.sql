CREATE OR REPLACE FUNCTION "CoreSchema"."GetCompanyById"(_paramId INT)
RETURNS TABLE (
    "CompanyId" INT,
    "Name" VARCHAR,
    "PersonId" INT,
    "Type" INT,
    "Status" INT,
    "CreationDate" TIMESTAMPTZ,
    "UpdateDate" TIMESTAMPTZ,
    "CreationUserId" INT,
    "UpdateUserId" INT,
    "DeletionDate" TIMESTAMPTZ
) AS $$
BEGIN
    RETURN QUERY
    SELECT "CompanyId",
           "Name",
           "PersonId",
           "Type",
           "Status",
           "CreationDate",
           "UpdateDate",
           "CreationUserId",
           "UpdateUserId",
           "DeletionDate"
    FROM "CoreSchema"."Company"
    WHERE "DeletionDate" IS NULL
      AND "CompanyId" = _paramId;
END;
$$ LANGUAGE plpgsql;
