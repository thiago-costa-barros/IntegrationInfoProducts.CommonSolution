using CommonSolution.Domain.Resources;
using System.ComponentModel.DataAnnotations;

namespace CommonSolution.Domain.Entities.Common.Enums
{
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
    public enum OperationStatus
    {
        Created = 0,
        Pending = 1,
        Registred = 2,
        Approved = 3,
        Completed = 4,
        Cancelled = 5,
        Refunded = 6,
        AwaitingConfirmation = 7,
        Error = 8
    }
    public enum OperationType
    {
        Pix = 0,
        Billet = 1,
        HybridBillet = 2,
        BankTransfer = 3,
        PurchaseCard = 4,
        DigitalWallet = 5
    }
}

