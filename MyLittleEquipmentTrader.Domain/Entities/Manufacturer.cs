// MyLittleEquipmentTrader.Domain/Entities/Manufacturer.cs

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLittleEquipmentTrader.Domain.Entities
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }

        
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string WebsiteUrl { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string FaxNumber { get; set; }
        public string CustomerSupportEmail { get; set; }
        public string Industry { get; set; }
        public string CompanyOverview { get; set; }
        public int YearEstablished { get; set; }
        public string ProductCategories { get; set; }
        public int MOQ { get; set; } // Minimum Order Quantity
        public string LeadTime { get; set; }
        public string PaymentTerms { get; set; }
        public string CurrencyUsed { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public string ManufacturerStatus { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastUpdated { get; set; }
        public string SupplierTier { get; set; }
        public string LogoImage { get; set; }
        public string BannerImage { get; set; }
       
    }
}
