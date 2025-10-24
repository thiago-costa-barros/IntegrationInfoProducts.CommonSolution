using CommonSolution.Resources;
using System.ComponentModel.DataAnnotations;

namespace CommonSolution.Entities.Common.Enums
{
    public enum PublicEnum;

    public enum UserType
    {
        [Display(ResourceType = typeof(UserMessages), Name = "ECM0000")]
        ApiMethod = 0,

        [Display(ResourceType = typeof(UserMessages), Name = "ECM0001")]
        TeamUser = 1,

        [Display(ResourceType = typeof(UserMessages), Name = "ECM0002")]
        WorkerProcess = 2,

        [Display(ResourceType = typeof(UserMessages), Name = "ECM0003")]
        Integration = 3,

        [Display(ResourceType = typeof(UserMessages), Name = "ECM0004")]
        ScheduledTask = 4
    }

    public enum TokenType
    {
        [Display(ResourceType = typeof(UserMessages), Name = "ECM0005")]
        AccessToken = 1,
        [Display(ResourceType = typeof(UserMessages), Name = "ECM0006")]
        RefreshToken = 2
    }

    public enum TokenStatus
    {
        [Display(ResourceType = typeof(UserMessages), Name = "ECM0007")]
        Active = 1,
        [Display(ResourceType = typeof(UserMessages), Name = "ECM0008")]
        Expired = 2,
        [Display(ResourceType = typeof(UserMessages), Name = "ECM0009")]
        Revoked = 3,
        [Display(ResourceType = typeof(UserMessages), Name = "ECM0010")]
        Blocked = 4
    }

    public enum PersonType
    {
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0001")]
        Individual = 1,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0002")]
        LegalEntity = 2,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0003")]
        ForeignPerson = 3
    }
    public enum PersonStatus
    {
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0004")]
        Created = 0,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0005")]
        Active = 1,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0006")]
        Suspended = 2,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0007")]
        Blocked = 3
    }
    public enum CompanyType
    {
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0001")]
        Individual = 1,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0002")]
        LegalEntity = 2,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0003")]
        ForeignPerson = 3
    }

    public enum CompanyStatus
    {
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0004")]
        Created = 0,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0005")]
        Active = 1,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0006")]
        Suspended = 2,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0007")]
        Blocked = 3
    }

    public enum BusinessUnitStatus
    {
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0004")]
        Created = 0,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0005")]
        Active = 1,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0006")]
        Suspended = 2,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0007")]
        Blocked = 3
    }
    public enum ProductType
    {
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0008")]
        InfoProduct = 1,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0009")]
        PhysicalProduct = 2
    }
    public enum ProductStatus
    {
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0004")]
        Created = 0,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0005")]
        Active = 1,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0006")]
        Suspended = 2,
        [Display(ResourceType = typeof(CoreSchemaMessage), Name = "CSM0007")]
        Blocked = 3
    }
    public enum ProductSourceType
    {
        MainApi = 0,
        Hotmart = 1,
        Udemy = 2,
    }
    public enum ProductOfferCouponStatus
    {
        Active = 0,
        Expired = 1,
    }
    public enum ExternalWebhookReceiverSourceType
    {
        Hotmart = 1,
        Udemy = 2,
    }

    public enum ExternalWebhookReceiverStatus
    {
        Created = 0,
        Pending = 1,
        Proccessed = 2,
        Error = 3,
    }

    public enum ExternalAuthenticationType
    {
        Hotmart = 1,
        Udemy = 2,
    }
}

