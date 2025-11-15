using CommonSolution.Domain.Resources;

namespace CommonSolution.Utils.Helpers
{
    public static class ExceptionMessageHelper
    {
        public static string UserNotFound(string id) =>
            string.Format(ExceptionMessages.UserNotFound, id);

        public static string InvalidCredentials() =>
            ExceptionMessages.InvalidCredentials;

        public static string UnauthorizedAccess() =>
            ExceptionMessages.UnauthorizedAccess;

        public static string RequiredFieldMissing(string field) =>
            string.Format(ExceptionMessages.RequiredFieldMissing, field);

        public static string InvalidFieldFormat(string field) =>
            string.Format(ExceptionMessages.InvalidFieldFormat, field);

        public static string EntityNotFound(string entity, string id) =>
            string.Format(ExceptionMessages.EntityNotFound, entity, id);

        public static string SaveEntityError(string entity, string details) =>
            string.Format(ExceptionMessages.SaveEntityError, entity, details);

        public static string UpdateEntityError(string entity, string id) =>
            string.Format(ExceptionMessages.UpdateEntityError, entity, id);

        public static string DeleteEntityError(string entity, string id) =>
            string.Format(ExceptionMessages.DeleteEntityError, entity, id);

        public static string ExternalServiceUnavailable(string service) =>
            string.Format(ExceptionMessages.ExternalServiceUnavailable, service);

        public static string ExternalServiceError(string service, string msg) =>
            string.Format(ExceptionMessages.ExternalServiceError, service, msg);

        public static string UnexpectedError() =>
            ExceptionMessages.UnexpectedError;

        public static string InvalidEnumValue(string value, string field) =>
            string.Format(ExceptionMessages.InvalidEnumValue, value, field);

        public static string DuplicateEntry(string field, string value) =>
            string.Format(ExceptionMessages.DuplicateEntry, field, value);

        public static string InvalidWebhookSignature() =>
            ExceptionMessages.InvalidWebhookSignature;

        public static string InvalidJsonPayload() =>
            ExceptionMessages.InvalidJsonPayload;
    }
}
