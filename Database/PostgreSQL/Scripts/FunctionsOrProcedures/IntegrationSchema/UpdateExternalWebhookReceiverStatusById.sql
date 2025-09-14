CREATE OR REPLACE FUNCTION "IntegrationSchema"."UpdateExternalWebhookReceiverStatusById"(
    _paramExternalWebhookReceiverId INT,
    _paramStatus INT,
    _paramUserId INT
)
RETURNS void AS $$
BEGIN
    UPDATE "IntegrationSchema"."ExternalWebhookReceiver"
       SET "Status" = _paramStatus,
           "UpdateDate" = now(),
           "UpdateUserId" = _paramUserId
     WHERE "ExternalWebhookReceiverId" = _paramExternalWebhookReceiverId;
END;
$$ LANGUAGE plpgsql;
