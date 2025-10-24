﻿using CommonSolution.Entities.Common;
using CommonSolution.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonSolution.Entities.CoreSchema
{
    [Table ("ProductOfferCoupon", Schema = "CoreSchema")]
    public class ProductOfferCoupon: AuditableEntity
    {
        public int ProductOfferCouponId { get; set; }
        public string? Code { get; set; }
        public decimal? DiscountAmount { get; set; } = null;
        public decimal? DiscountPercentage { get; set; } = null;
        public DateTime? StartDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public ProductOfferCouponStatus Status { get; set; } = 0;
    }
}
