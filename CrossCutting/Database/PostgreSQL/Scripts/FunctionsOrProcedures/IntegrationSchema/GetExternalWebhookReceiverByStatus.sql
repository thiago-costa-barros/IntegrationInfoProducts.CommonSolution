CREATE OR REPLACE FUNCTION "IntegrationSchema"."GetExternalWebhookReceiverByStatus"(_paramStatus INT)
RETURNS TABLE (
    "ExternalWebhookReceiverId" INT,
    "SourceType" INT,
    "Status" INT,
    "CompanyId" INT,
    "ExternalIdentifier" VARCHAR,
    "Payload" JSONB,
    "CreationDate" TIMESTAMPTZ,
    "UpdateDate" TIMESTAMPTZ,
    "CreationUserId" INT,
    "UpdateUserId" INT,
    "DeletionDate" TIMESTAMPTZ
) AS $$
BEGIN
    RETURN QUERY
    SELECT "ExternalWebhookReceiverId",
           "SourceType",
           "Status",
           "CompanyId",
           "ExternalIdentifier",
           "Payload",
           "CreationDate",
           "UpdateDate",
           "CreationUserId",
           "UpdateUserId",
           "DeletionDate"
    FROM "IntegrationSchema"."ExternalWebhookReceiver"
    WHERE "DeletionDate" IS NULL
      AND "Status" in (_paramStatus)
    ORDER BY "ExternalWebhookReceiverId" ASC;
END;
$$ LANGUAGE plpgsql;
