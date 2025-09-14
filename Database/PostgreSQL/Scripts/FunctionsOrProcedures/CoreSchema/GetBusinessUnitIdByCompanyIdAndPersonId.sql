CREATE OR REPLACE FUNCTION "CoreSchema"."GetBusinessUnitIdByCompanyIdAndPersonId"(
    _paramCompanyId INT,
    _paramPersonId VARCHAR
)
RETURNS TABLE (
    "BusinessUnitId" INT,
    "Name" VARCHAR,
    "PersonId" INT,
    "IsMain" BOOLEAN,
    "Status" INT,
    "CompanyId" INT,
    "CreationDate" TIMESTAMPTZ,
    "UpdateDate" TIMESTAMPTZ,
    "CreationUserId" INT,
    "UpdateUserId" INT,
    "DeletionDate" TIMESTAMPTZ
) AS $$
BEGIN
    RETURN QUERY
    SELECT "BusinessUnitId",
           "Name",
           "PersonId",
           "IsMain",
           "Status",
           "CompanyId",
           "CreationDate",
           "UpdateDate",
           "CreationUserId",
           "UpdateUserId",
           "DeletionDate"
    FROM "CoreSchema"."BusinessUnit"
    WHERE "DeletionDate" IS NULL
      AND "CompanyId" = _paramCompanyId
      AND "PersonId" = _paramPersonId;
END;
$$ LANGUAGE plpgsql;
