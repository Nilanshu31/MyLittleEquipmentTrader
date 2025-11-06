// MyLittleEquipmentTrader.Application/Models/DTOS/PlanDto.cs

using System;

namespace MyLittleEquipmentTrader.Application.Models.DTOS
{
    public class PlanDto
    {
        public int PlanId { get; set; }

        public string PlanSystemAlias { get; set; }
        public string PlanDisplayName { get; set; }
        public string PlanCategoryAssignment { get; set; }
        public string PlanUrlSlug { get; set; }
        public string BillingModel { get; set; }
        public string EarlyTerminationFeeRule { get; set; }
        public string CustomSupportLevel { get; set; }
        public string SupportHours { get; set; }
        public string DedicatedAccountManager { get; set; }
        public string ResourceLimits { get; set; }
        public string RegionalAvailability { get; set; }
        public string AuditLogSettings { get; set; }
        public string AnalyticsReportingLevel { get; set; }
        public string TransactionLimits { get; set; }
        public string AllowedPaymentCycles { get; set; }
        public string BulkDiscountRule { get; set; }
        public string DiscountEligibility { get; set; }
        public string AutomatedRenewDiscount { get; set; }
        public string AccountTypeTag { get; set; }
        public string BandwidthDataAllocation { get; set; }
        public string DeviceSeatAccessRule { get; set; }
        public string EligibilityToggles { get; set; }
        public bool GlobalPlanVisibility { get; set; }
        public DateTime? OfferExpiryDate { get; set; }
        public DateTime? PlanLaunchDate { get; set; }
        public string OverageChargeRule { get; set; }
        public string SubscriptionPausePolicy { get; set; }
        public DateTime? PlanArchivingDate { get; set; }
        public string DataExportOptions { get; set; }
        public string DynamicPricingEngine { get; set; }
        public string ServiceUpgradePath { get; set; }
        public DateTime? PlanExpiryDate { get; set; }
        public string InternalReviewCycle { get; set; }
        public string CustomBillingCycle { get; set; }
    }
}
