using System;

namespace MyLittleEquipmentTrader.Application.Models.Dtos
{
    public class TenantDto
    {
        public int TenantID { get; set; }
        public string TenantName { get; set; }
        public string PrimaryContactEmail { get; set; }
        public string BrandName { get; set; }
        public string DomainName { get; set; }
        public string Subdomain { get; set; }
        public string LogoURL { get; set; }
        public string FaviconURL { get; set; }
        public string ThemeSettings { get; set; }
        public string CustomDomain { get; set; }
        public string SiteTitle { get; set; }
        public string Tagline { get; set; }
        public string FooterText { get; set; }
        public string SupportEmail { get; set; }
        public string APIKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OnboardingStatus { get; set; }
    }
}
