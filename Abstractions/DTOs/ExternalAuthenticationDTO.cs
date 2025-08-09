using CommonSolution.Entities.Common.Enums;

namespace ExternalWebhookReceiverAPI.Application.DTOs.Common
{
    public class ExternalAuthenticationDTO
    {
        public ExternalAuthenticationType Type { get; set; }
        public string? AuthKey { get; set; }
    }
}
