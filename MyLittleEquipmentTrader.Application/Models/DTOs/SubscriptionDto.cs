using MyLittleEquipmentTrader.Application.Models.DTOS;
using System;

namespace MyLittleEquipmentTrader.Application.Models.Dtos
{
    public class SubscriptionDto
    {
        public int SubscriptionId { get; set; }

        // Relationships
        public int TenantId { get; set; }
        public int PlanId { get; set; }
        public TenantDto Tenant { get; set; }
        public PlanDto Plan { get; set; }

        // Core subscription details
        public DateTime RenewalDate { get; set; }
        public bool AutoRenewal { get; set; }
        public string PlanStatus { get; set; }

        public decimal MonthlySubscriptionPrice { get; set; }
        public decimal AnnualSubscriptionPrice { get; set; }

        // Optional tracking
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
