CREATE OR REPLACE FUNCTION "CoreSchema"."GetPersonByTaxNumber"(_paramTaxNumber VARCHAR)
RETURNS TABLE (
    "PersonId" INT,
    "Email" VARCHAR,
    "Name" VARCHAR,
    "TaxNumber" VARCHAR,
    "Type" INT,
    "CreationDate" TIMESTAMPTZ,
    "UpdateDate" TIMESTAMPTZ,
    "CreationUserId" INT,
    "UpdateUserId" INT,
    "DeletionDate" TIMESTAMPTZ
) AS $$
BEGIN
    RETURN QUERY
    SELECT "PersonId",
           "Email",
           "Name",
           "TaxNumber",
           "Type",
           "CreationDate",
           "UpdateDate",
           "CreationUserId",
           "UpdateUserId",
           "DeletionDate"
    FROM "CoreSchema"."Person"
    WHERE "DeletionDate" IS NULL
      AND "TaxNumber" = _paramTaxNumber
    ORDER BY "PersonId" ASC
    LIMIT 1;
END;
$$ LANGUAGE plpgsql;
