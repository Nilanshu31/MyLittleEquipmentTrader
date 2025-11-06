using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLittleEquipmentTrader.Domain.Entities
{
    public class Report
    {
        // Basic Info
        public int ReportID { get; set; }
        public int TenantID { get; set; }
        public string ReportType { get; set; }
        public string ReportName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublic { get; set; }
        public string DashboardWidgetID { get; set; }
        public string CustomColumns { get; set; }
        public string AccessRoles { get; set; }

        // Report Period Info
        public DateTime ReportPeriodStart { get; set; }
        public DateTime ReportPeriodEnd { get; set; }
        public string Filters { get; set; }
        public string GroupBy { get; set; }
        public string SortBy { get; set; }
        public bool IsScheduled { get; set; }

        // Export Info
        public string ExportFormat { get; set; }
        public string ExportUrl { get; set; }

        // Sales Metrics
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageSalePrice { get; set; }
        public decimal TotalFeesCollected { get; set; }
        public string Currency { get; set; }
        public bool IncludeDiscounts { get; set; }
        public string RegionWiseSales { get; set; }

        // Performance Data
        public int TotalListingsSold { get; set; }
        public string TopListings { get; set; }
        public string TopCategories { get; set; }
        public decimal InventoryTurnover { get; set; }
        public int RecordCount { get; set; }
        public string TopSellers { get; set; }
        public string TopBuyers { get; set; }
        public string UserSegment { get; set; }
        public string SellerPerformance { get; set; }
        public int PageViews { get; set; }

    }
}
