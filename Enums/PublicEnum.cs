using CommonSolution.Resources;
using System.ComponentModel.DataAnnotations;

namespace CommonSolution.Enums
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

    public enum HotmartPurchaseEventType
    {
        PURCHASE_APPROVED = 1,
        PURCHASE_CANCELED = 2,
        PURCHASE_COMPLETE = 3,
        PURCHASE_BILLET_PRINTED = 4,
        PURCHASE_PROTEST = 5,
        PURCHASE_REFUNDED = 6,
        PURCHASE_CHARGEBACK = 7,
        PURCHASE_EXPIRED = 8,
        PURCHASE_DELAYED = 9
    }
}

