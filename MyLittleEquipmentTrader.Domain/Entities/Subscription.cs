using System;

namespace MyLittleEquipmentTrader.Domain.Entities
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }

        // Required foreign keys & navigation
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public int PlanId { get; set; }
        public Plan Plan { get; set; }

        // Subscription details
        public string TargetAudience { get; set; }

        public bool IsDiscounted { get; set; }
        public decimal DiscountAmount { get; set; }
        public string? PromoCode { get; set; } // Make it nullable

        public int ContractLength { get; set; }                  // e.g., months
        public int MinimumCommitmentPeriod { get; set; }         // e.g., months

        public DateTime RenewalDate { get; set; }

        public bool IsUpgradeAvailable { get; set; }
        public bool AutoRenewal { get; set; }

        public string CurrencySymbol { get; set; }

        public int MaxActiveSessions { get; set; }
        public int TrialPeriodDays { get; set; }

        public decimal ReferralBonus { get; set; }

        public int UserCapacity { get; set; }
        public bool MultiplePaymentMethods { get; set; }

        public decimal AnnualSubscriptionPrice { get; set; }
        public decimal QuarterlySubscriptionPrice { get; set; }
        public decimal MonthlySubscriptionPrice { get; set; }

        public decimal PaymentGatewayFees { get; set; }
        public string DataRetentionPolicy { get; set; }

        public string AccessLevel { get; set; }

        public DateTime? ContractRenewalReminder { get; set; }   // Optional reminder date
        public string PlanStatus { get; set; }

        public string PaymentPeriodOptions { get; set; }
        public decimal TaxRate { get; set; }

        public bool AutoUpgrade { get; set; }
        public bool IsMultiTenant { get; set; }

        public string ClientRestrictions { get; set; }
        public bool MultiDeviceAccess { get; set; }

        public int RegionalSubscriptionLimit { get; set; }

        public int PriceLockPeriod { get; set; }                  // e.g., months

        public string LimitationsOnAddOns { get; set; }
        public decimal AnnualRenewalDiscount { get; set; }
        public decimal PaymentProcessingFee { get; set; }
        public int MaximumUserSeats { get; set; }

        public bool TrialExtensionEligibility { get; set; }
        public decimal PrepaidPlanDiscount { get; set; }
        public bool ResellerProgramEligibility { get; set; }
        public bool GlobalSupportAvailability { get; set; }

        public decimal CustomerRetentionRate { get; set; }
        public int CustomerReferralCap { get; set; }

        public bool SubscriptionPauseAvailability { get; set; }

        // Tracking fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
