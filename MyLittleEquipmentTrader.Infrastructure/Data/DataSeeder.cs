using System;
using System.Collections.Generic;
using System.Linq;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure.Data;

namespace MyLittleEquipmentTrader.Infrastructure
{
    public static class DataSeeder
    {
        public static void SeedSubscriptions(ApplicationDbContext context)
        {
            // Ensure there are Tenants and Plans before seeding Subscriptions
            if (!context.Tenants.Any() || !context.Plans.Any())
            {
                Console.WriteLine("⚠️ Cannot seed subscriptions: Tenants or Plans not found.");
                return;
            }

            // Grab existing tenant and plan IDs for linking
            var tenant1 = context.Tenants.FirstOrDefault();
            var plan1 = context.Plans.FirstOrDefault();

            if (tenant1 == null || plan1 == null)
            {
                Console.WriteLine("⚠️ Missing Tenant or Plan references for subscriptions.");
                return;
            }

            var subscriptions = new List<Subscription>
        {
            new Subscription
            {
                TenantId = tenant1.TenantID,
                PlanId = plan1.PlanId,
                TargetAudience = "Small Business",
                IsDiscounted = true,
                DiscountAmount = 20m,
                PromoCode = "START20",
                ContractLength = 12,
                MinimumCommitmentPeriod = 6,
                RenewalDate = DateTime.UtcNow.AddMonths(12),
                IsUpgradeAvailable = true,
                AutoRenewal = true,
                CurrencySymbol = "$",
                MaxActiveSessions = 10,
                TrialPeriodDays = 14,
                ReferralBonus = 10m,
                UserCapacity = 25,
                MultiplePaymentMethods = true,
                AnnualSubscriptionPrice = 999.99m,
                QuarterlySubscriptionPrice = 299.99m,
                MonthlySubscriptionPrice = 99.99m,
                PaymentGatewayFees = 2.5m,
                DataRetentionPolicy = "Data retained for 1 year post-cancellation",
                AccessLevel = "Standard",
                ContractRenewalReminder = DateTime.UtcNow.AddDays(30),
                PlanStatus = "Active",
                PaymentPeriodOptions = "Monthly, Quarterly, Annual",
                TaxRate = 0.18m,
                AutoUpgrade = true,
                IsMultiTenant = false,
                ClientRestrictions = "None",
                MultiDeviceAccess = true,
                RegionalSubscriptionLimit = 100,
                PriceLockPeriod = 12,
                LimitationsOnAddOns = "Add-ons limited to 3 per account",
                AnnualRenewalDiscount = 5.0m,
                PaymentProcessingFee = 1.0m,
                MaximumUserSeats = 50,
                TrialExtensionEligibility = true,
                PrepaidPlanDiscount = 3.0m,
                ResellerProgramEligibility = true,
                GlobalSupportAvailability = true,
                CustomerRetentionRate = 90.0m,
                CustomerReferralCap = 10,
                SubscriptionPauseAvailability = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Subscription
            {
                TenantId = tenant1.TenantID,
                PlanId = plan1.PlanId,
                TargetAudience = "Enterprise",
                IsDiscounted = false,
                DiscountAmount = 0m,
                PromoCode = null,
                ContractLength = 24,
                MinimumCommitmentPeriod = 12,
                RenewalDate = DateTime.UtcNow.AddMonths(24),
                IsUpgradeAvailable = true,
                AutoRenewal = true,
                CurrencySymbol = "$",
                MaxActiveSessions = 100,
                TrialPeriodDays = 30,
                ReferralBonus = 20m,
                UserCapacity = 200,
                MultiplePaymentMethods = true,
                AnnualSubscriptionPrice = 4999.99m,
                QuarterlySubscriptionPrice = 1499.99m,
                MonthlySubscriptionPrice = 499.99m,
                PaymentGatewayFees = 1.5m,
                DataRetentionPolicy = "Data retained for 3 years post-cancellation",
                AccessLevel = "Premium",
                ContractRenewalReminder = DateTime.UtcNow.AddDays(60),
                PlanStatus = "Active",
                PaymentPeriodOptions = "Monthly, Quarterly, Annual",
                TaxRate = 0.20m,
                AutoUpgrade = true,
                IsMultiTenant = true,
                ClientRestrictions = "Limited to 10 regional deployments",
                MultiDeviceAccess = true,
                RegionalSubscriptionLimit = 1000,
                PriceLockPeriod = 24,
                LimitationsOnAddOns = "No add-on limits",
                AnnualRenewalDiscount = 10.0m,
                PaymentProcessingFee = 1.0m,
                MaximumUserSeats = 1000,
                TrialExtensionEligibility = false,
                PrepaidPlanDiscount = 5.0m,
                ResellerProgramEligibility = true,
                GlobalSupportAvailability = true,
                CustomerRetentionRate = 95.0m,
                CustomerReferralCap = 20,
                SubscriptionPauseAvailability = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

            foreach (var sub in subscriptions)
            {
                if (!context.Subscriptions.Any(s => s.TenantId == sub.TenantId && s.PlanId == sub.PlanId))
                {
                    context.Subscriptions.Add(sub);
                }
            }

            try
            {
                context.SaveChanges();
                Console.WriteLine("✅ Subscriptions successfully seeded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error seeding subscriptions: " + ex.Message);
                throw;
            }
        }
        public static void SeedPlans(ApplicationDbContext context)
        {
            var plans = new List<Plan>
    {
        new Plan
        {
            PlanSystemAlias = "basic_monthly",
            PlanDisplayName = "Basic Monthly Plan",
            PlanCategoryAssignment = "Basic",
            PlanUrlSlug = "basic-monthly",
            BillingModel = "Monthly",
            EarlyTerminationFeeRule = "None",
            CustomSupportLevel = "Email Support",
            SupportHours = "9am-5pm (Mon-Fri)",
            DedicatedAccountManager = "No",
            ResourceLimits = "10 GB Storage, 100 GB Bandwidth",
            RegionalAvailability = "Global",
            AuditLogSettings = "Basic logs retained for 30 days",
            AnalyticsReportingLevel = "Standard",
            TransactionLimits = "Up to 1000/month",
            AllowedPaymentCycles = "Monthly",
            BulkDiscountRule = "None",
            DiscountEligibility = "New Users Only",
            AutomatedRenewDiscount = "5%",
            AccountTypeTag = "Standard",
            BandwidthDataAllocation = "100 GB",
            DeviceSeatAccessRule = "Up to 2 devices",
            EligibilityToggles = "true",
            GlobalPlanVisibility = true,
            OfferExpiryDate = DateTime.UtcNow.AddMonths(6),
            PlanLaunchDate = DateTime.UtcNow.AddMonths(-3),
            OverageChargeRule = "Standard Overage Rates Apply",
            SubscriptionPausePolicy = "Allowed once every 6 months",
            PlanArchivingDate = null,
            DataExportOptions = "CSV, JSON",
            DynamicPricingEngine = "Fixed",
            ServiceUpgradePath = "Pro Monthly Plan",
            PlanExpiryDate = null,
            InternalReviewCycle = "12 months",
            CustomBillingCycle = "Standard"
        },
        new Plan
        {
            PlanSystemAlias = "pro_annual",
            PlanDisplayName = "Pro Annual Plan",
            PlanCategoryAssignment = "Pro",
            PlanUrlSlug = "pro-annual",
            BillingModel = "Annual",
            EarlyTerminationFeeRule = "50% of remaining term",
            CustomSupportLevel = "Priority Email & Chat",
            SupportHours = "24/7",
            DedicatedAccountManager = "Yes",
            ResourceLimits = "100 GB Storage, 1 TB Bandwidth",
            RegionalAvailability = "North America, Europe",
            AuditLogSettings = "Advanced logs retained for 1 year",
            AnalyticsReportingLevel = "Advanced",
            TransactionLimits = "Unlimited",
            AllowedPaymentCycles = "Annual",
            BulkDiscountRule = "10% for 10+ users",
            DiscountEligibility = "All users",
            AutomatedRenewDiscount = "10%",
            AccountTypeTag = "Premium",
            BandwidthDataAllocation = "1 TB",
            DeviceSeatAccessRule = "Up to 10 devices",
            EligibilityToggles = "true",
            GlobalPlanVisibility = true,
            OfferExpiryDate = DateTime.UtcNow.AddYears(1),
            PlanLaunchDate = DateTime.UtcNow.AddMonths(-6),
            OverageChargeRule = "Discounted Overage Rates Apply",
            SubscriptionPausePolicy = "Allowed once a year",
            PlanArchivingDate = null,
            DataExportOptions = "CSV, JSON, XML",
            DynamicPricingEngine = "Adaptive",
            ServiceUpgradePath = "Enterprise Custom Plan",
            PlanExpiryDate = null,
            InternalReviewCycle = "6 months",
            CustomBillingCycle = "Customizable"
        }
    };

            foreach (var plan in plans)
            {
                // Add only if not already present (based on system alias or display name)
                if (!context.Plans.Any(p => p.PlanSystemAlias == plan.PlanSystemAlias))
                {
                    context.Plans.Add(plan);
                }
            }

            try
            {
                context.SaveChanges();
                Console.WriteLine("Plans successfully seeded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error seeding plans: " + ex.Message);
                throw;
            }
        }
        //user
        public static void SeedUser(ApplicationDbContext context)
        {
            if (!context.UserInfo.Any())
            {
                var defaultUserTemplate = new UserInfo
                {
                    TenantID = 1,
                    PasswordHash = "hashed_password_here",
                    IsActive = true,
                    RegistrationDate = DateTime.UtcNow,
                    Wishlist = "",
                    ViewedProducts = "",
                    SignupSource = "Seed",
                    EmailVerificationToken = "",
                    PasswordResetToken = "",
                    PreferredCurrency = "USD",
                    PhoneNumber = "0000000000",
                    PreferredContactMethod = "Email",
                    AddressLine1 = "",
                    City = "",
                    State = "",
                    ZipCode = "",
                    Country = "",
                    Gender = "Unspecified",
                    FirstName = "",
                    LastName = "",
                    UserType = "User",
                    TwoFactorEnabled = false,
                    IsEmailVerified = true,
                    IsPhoneVerified = false,
                    MarketingOptIn = false,
                    EmailNotificationsEnabled = true,
                    SMSNotificationsEnabled = false,
                    AppPushEnabled = false,
                    CompanyName = "",
                    BusinessType = "",
                    DealerLicenseNumber = "",
                    DealerProfileUrl = "",
                    ReferralCode = "",
                    ReferredBy = "",
                };

                context.UserInfo.AddRange(
                    new UserInfo
                    {
                        Username = "globaladmin",
                        Email = "admin@global.com",
                        Role = "GlobalAdmin",
                        // Copy defaults
                        TenantID = defaultUserTemplate.TenantID,
                        PasswordHash = defaultUserTemplate.PasswordHash,
                        IsActive = defaultUserTemplate.IsActive,
                        RegistrationDate = defaultUserTemplate.RegistrationDate,
                        Wishlist = defaultUserTemplate.Wishlist,
                        ViewedProducts = defaultUserTemplate.ViewedProducts,
                        SignupSource = defaultUserTemplate.SignupSource,
                        EmailVerificationToken = defaultUserTemplate.EmailVerificationToken,
                        PasswordResetToken = defaultUserTemplate.PasswordResetToken,
                        PreferredCurrency = defaultUserTemplate.PreferredCurrency,
                        PhoneNumber = defaultUserTemplate.PhoneNumber,
                        PreferredContactMethod = defaultUserTemplate.PreferredContactMethod,
                        AddressLine1 = defaultUserTemplate.AddressLine1,
                        City = defaultUserTemplate.City,
                        State = defaultUserTemplate.State,
                        ZipCode = defaultUserTemplate.ZipCode,
                        Country = defaultUserTemplate.Country,
                        Gender = defaultUserTemplate.Gender,
                        FirstName = defaultUserTemplate.FirstName,
                        LastName = defaultUserTemplate.LastName,
                        UserType = defaultUserTemplate.UserType
                    },
                    new UserInfo
                    {
                        Username = "tenantadmin",
                        Email = "tenant@company.com",
                        Role = "TenantAdmin",
                        TenantID = defaultUserTemplate.TenantID,
                        PasswordHash = defaultUserTemplate.PasswordHash,
                        IsActive = defaultUserTemplate.IsActive,
                        RegistrationDate = defaultUserTemplate.RegistrationDate,
                        Wishlist = defaultUserTemplate.Wishlist,
                        ViewedProducts = defaultUserTemplate.ViewedProducts,
                        SignupSource = defaultUserTemplate.SignupSource,
                        EmailVerificationToken = defaultUserTemplate.EmailVerificationToken,
                        PasswordResetToken = defaultUserTemplate.PasswordResetToken,
                        PreferredCurrency = defaultUserTemplate.PreferredCurrency,
                        PhoneNumber = defaultUserTemplate.PhoneNumber,
                        PreferredContactMethod = defaultUserTemplate.PreferredContactMethod,
                        AddressLine1 = defaultUserTemplate.AddressLine1,
                        City = defaultUserTemplate.City,
                        State = defaultUserTemplate.State,
                        ZipCode = defaultUserTemplate.ZipCode,
                        Country = defaultUserTemplate.Country,
                        Gender = defaultUserTemplate.Gender,
                        FirstName = defaultUserTemplate.FirstName,
                        LastName = defaultUserTemplate.LastName,
                        UserType = defaultUserTemplate.UserType
                    },
                    new UserInfo
                    {
                        Username = "normaluser",
                        Email = "user@example.com",
                        Role = "User",
                        TenantID = defaultUserTemplate.TenantID,
                        PasswordHash = defaultUserTemplate.PasswordHash,
                        IsActive = defaultUserTemplate.IsActive,
                        RegistrationDate = defaultUserTemplate.RegistrationDate,
                        Wishlist = defaultUserTemplate.Wishlist,
                        ViewedProducts = defaultUserTemplate.ViewedProducts,
                        SignupSource = defaultUserTemplate.SignupSource,
                        EmailVerificationToken = defaultUserTemplate.EmailVerificationToken,
                        PasswordResetToken = defaultUserTemplate.PasswordResetToken,
                        PreferredCurrency = defaultUserTemplate.PreferredCurrency,
                        PhoneNumber = defaultUserTemplate.PhoneNumber,
                        PreferredContactMethod = defaultUserTemplate.PreferredContactMethod,
                        AddressLine1 = defaultUserTemplate.AddressLine1,
                        City = defaultUserTemplate.City,
                        State = defaultUserTemplate.State,
                        ZipCode = defaultUserTemplate.ZipCode,
                        Country = defaultUserTemplate.Country,
                        Gender = defaultUserTemplate.Gender,
                        FirstName = defaultUserTemplate.FirstName,
                        LastName = defaultUserTemplate.LastName,
                        UserType = defaultUserTemplate.UserType
                    }
                );

                context.SaveChanges();
            }
        }


        //user

        public static void SeedTenants(ApplicationDbContext context)
        {
            var tenants = new List<Tenant>
    {
        new Tenant
        {
            TenantName = "Tenant One",
            PrimaryContactEmail = "contact@tenantone.com",
            BrandName = "TenantOne Brand",
            DomainName = "tenantone.com",
            Subdomain = "tenantone",
            LogoURL = "https://example.com/tenantone-logo.png",
            FaviconURL = "https://example.com/tenantone-favicon.ico",
            ThemeSettings = "{\"theme\":\"light\"}",
            CustomDomain = null, // Will be set below if null
            SiteTitle = "Tenant One Site",
            Tagline = "The first tenant",
            FooterText = "© Tenant One 2025",
            SupportEmail = "support@tenantone.com",
            APIKey = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            OnboardingStatus = "Completed"
        },
        new Tenant
        {
            TenantName = "Tenant Two",
            PrimaryContactEmail = "info@tenanttwo.com",
            BrandName = "TenantTwo Brand",
            DomainName = "tenanttwo.com",
            Subdomain = "tenanttwo",
            LogoURL = "https://example.com/tenanttwo-logo.png",
            FaviconURL = "https://example.com/tenanttwo-favicon.ico",
            ThemeSettings = "{\"theme\":\"dark\"}",
            CustomDomain = "custom.tenanttwo.com",
            SiteTitle = "Tenant Two Site",
            Tagline = "The second tenant",
            FooterText = "© Tenant Two 2025",
            SupportEmail = "help@tenanttwo.com",
            APIKey = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            OnboardingStatus = "Pending"
        }
    };

            foreach (var tenant in tenants)
            {
                // Fallback value if CustomDomain is null or empty
                if (string.IsNullOrWhiteSpace(tenant.CustomDomain))
                {
                    tenant.CustomDomain = $"{tenant.Subdomain}.yourapp.com"; // Use your actual base domain here
                }

                // Add only if not already present
                if (!context.Tenants.Any(t => t.TenantName == tenant.TenantName))
                {
                    context.Tenants.Add(tenant);
                }
            }

            try
            {
                context.SaveChanges();
                Console.WriteLine("Tenants successfully saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving tenants: " + ex.Message);
                throw;
            }
        }


        // Method to seed data into the database
        public static void Seed(ApplicationDbContext context)
        {
            // First, seed product tags if not already present
            // Seed AccessRoles if not already present
            var accessRoles = new List<AccessRole>
{
    new AccessRole
    {
        
        RoleName = "Admin",
        Description = "Administrator role with full access",
        Permissions = "ALL",
        IsActive = true,
        CreatedDate = DateTime.UtcNow,
        LastUpdated = DateTime.UtcNow,
        IsDefault = false,
        RoleType = "System",
        IsAssignable = true,
        RoleLevel = 1,
        PermissionsGroup = "AdminGroup",
        IsGlobal = true
    },
    new AccessRole
    {
        
        RoleName = "User",
        Description = "Standard user with read-only access",
        Permissions = "READ_ONLY",
        IsActive = true,
        CreatedDate = DateTime.UtcNow,
        LastUpdated = DateTime.UtcNow,
        IsDefault = true,
        RoleType = "Standard",
        IsAssignable = true,
        RoleLevel = 5,
        PermissionsGroup = "UserGroup",
        IsGlobal = false
    }
};

            // Add roles only if they don't already exist
            foreach (var role in accessRoles)
            {
                if (!context.AccessRoles.Any(r => r.RoleName == role.RoleName))
                {
                    context.AccessRoles.Add(role);
                }
            }

            try
            {
                context.SaveChanges();
                Console.WriteLine("Access roles successfully saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving access roles: " + ex.Message);
                throw;
            }

            var productTags = new List<ProductTag>
            {
                new ProductTag
                {
                    TagName = "smartphone",
                    Description = "Mobile devices with smart functionalities",
                    CategoryID = 1,
                    Subcategory = "Electronics",
                    TagIcon = "smartphone-icon.png",
                    TagColor = "#FF5733",
                    CreatedDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    IsActive = true,
                    Priority = "High",
                    UserCount = 500,
                    TagType = "Basic",
                    Visibility = "Public"
                },
                new ProductTag
                {
                    TagName = "truck",
                    Description = "Heavy-duty vehicles for transportation",
                    CategoryID = 2,
                    Subcategory = "Vehicles",
                    TagIcon = "truck-icon.png",
                    TagColor = "#4287f5",
                    CreatedDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    IsActive = true,
                    Priority = "Medium",
                    UserCount = 150,
                    TagType = "Featured",
                    Visibility = "Public"
                },
                new ProductTag
                {
                    TagName = "aircraft",
                    Description = "Airplanes and related products",
                    CategoryID = 3,
                    Subcategory = "Aviation",
                    TagIcon = "aircraft-icon.png",
                    TagColor = "#f39c12",
                    CreatedDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    IsActive = true,
                    Priority = "High",
                    UserCount = 50,
                    TagType = "Premium",
                    Visibility = "Public"
                }
            };

            // Add tags only if they don't already exist
            foreach (var tag in productTags)
            {
                if (!context.ProductTags.Any(t => t.TagName == tag.TagName))
                {
                    context.ProductTags.Add(tag);
                }
            }

            try
            {
                context.SaveChanges();
                Console.WriteLine("Product tags successfully saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving product tags: " + ex.Message);
                throw;
            }

            // Fetch saved ProductTags to associate with products
            var savedTags = context.ProductTags.ToList();

            // Seed Manufacturers if not already present
          var manufacturers = new List<Manufacturer>
{
    new Manufacturer
    {
        Name = "Samsung",
        Country = "South Korea",
        City = "Seoul",
        Address = "129 Samsung-ro, Yeongtong-gu",
        WebsiteUrl = "https://www.samsung.com",
        ContactPerson = "John Doe",
        ContactEmail = "contact@samsung.com",
        ContactPhone = "+82-2-0000-0000",
        FaxNumber = "+82-2-0000-0001",
        CustomerSupportEmail = "support@samsung.com",
        Industry = "Consumer Electronics",
        CompanyOverview = "Samsung Electronics is a global leader in consumer electronics, semiconductors, and telecommunications.",
        YearEstablished = 1938,
        ProductCategories = "Smartphones, Tablets, TVs, Home Appliances",
        MOQ = 1000,
        LeadTime = "4-6 weeks",
        PaymentTerms = "Net 30",
        CurrencyUsed = "USD",
        AverageRating = 4.5,
        ReviewCount = 1500,
        ManufacturerStatus = "Active",
        DateAdded = DateTime.UtcNow,
        LastUpdated = DateTime.UtcNow,
        SupplierTier = "Premium",
        LogoImage = "https://www.samsung.com/logo.png",
        BannerImage = "https://www.samsung.com/banner.jpg"
    },
    new Manufacturer
    {
        Name = "Volvo",
        Country = "Sweden",
        City = "Gothenburg",
        Address = "Volvo Car Corporation, 405 31 Gothenburg, Sweden",
        WebsiteUrl = "https://www.volvo.com",
        ContactPerson = "Jane Smith",
        ContactEmail = "contact@volvo.com",
        ContactPhone = "+46-31-0000-0000",
        FaxNumber = "+46-31-0000-0001",
        CustomerSupportEmail = "support@volvo.com",
        Industry = "Automotive",
        CompanyOverview = "Volvo is a leading manufacturer of premium cars, trucks, and construction equipment.",
        YearEstablished = 1927,
        ProductCategories = "Cars, Trucks, Construction Equipment",
        MOQ = 500,
        LeadTime = "6-8 weeks",
        PaymentTerms = "Net 30",
        CurrencyUsed = "SEK",
        AverageRating = 4.7,
        ReviewCount = 1200,
        ManufacturerStatus = "Active",
        DateAdded = DateTime.UtcNow,
        LastUpdated = DateTime.UtcNow,
        SupplierTier = "Standard",
        LogoImage = "https://www.volvo.com/logo.png",
        BannerImage = "https://www.volvo.com/banner.jpg"
    },
    new Manufacturer
    {
        Name = "Boeing",
        Country = "USA",
        City = "Chicago",
        Address = "100 North Riverside Plaza, Chicago, IL 60606, USA",
        WebsiteUrl = "https://www.boeing.com",
        ContactPerson = "Michael Johnson",
        ContactEmail = "contact@boeing.com",
        ContactPhone = "+1-800-000-0000",
        FaxNumber = "+1-800-000-0001",
        CustomerSupportEmail = "support@boeing.com",
        Industry = "Aerospace & Defense",
        CompanyOverview = "Boeing is a leading global aerospace company, producing commercial and military aircraft.",
        YearEstablished = 1916,
        ProductCategories = "Aircraft, Space Systems, Defense Systems",
        MOQ = 10,
        LeadTime = "12-18 months",
        PaymentTerms = "Net 60",
        CurrencyUsed = "USD",
        AverageRating = 4.9,
        ReviewCount = 500,
        ManufacturerStatus = "Active",
        DateAdded = DateTime.UtcNow,
        LastUpdated = DateTime.UtcNow,
        SupplierTier = "Premium",
        LogoImage = "https://www.boeing.com/logo.png",
        BannerImage = "https://www.boeing.com/banner.jpg"
    }
};

// Add manufacturers only if they don't already exist
foreach (var manufacturer in manufacturers)
{
    if (!context.Manufacturers.Any(m => m.Name == manufacturer.Name))
    {
        context.Manufacturers.Add(manufacturer);
    }
}

try
{
    context.SaveChanges();
    Console.WriteLine("Manufacturers successfully saved.");
}
catch (Exception ex)
{
    Console.WriteLine("Error saving manufacturers: " + ex.Message);
    throw;
}


            // Fetch saved Manufacturers to associate with products
            var savedManufacturers = context.Manufacturers.ToList();

            // Create products and associate them with tags and manufacturers
            var productsToAdd = new List<Product>
            {
                new Product
                {
                    ProductName = "Samsung Galaxy S22",
                    ProductType = "Smartphone",
                    Price = 999.99m,
                    OriginalPrice = 1099.99m,
                    SalePrice = 899.99m,
                    RatingAverage = 4.7,
                    ReviewCount = 1450,
                    SKU = "SGS22-001",
                    StockQuantity = 120,
                    QuantitySold = 350,
                    IsActive = true,
                    IsDiscounted = true,
                    IsFeatured = true,
                    IsInStock = true,
                    IsNew = true,
                    //Manufacturer = savedManufacturers.First(m => m.Name == "Samsung"),
                    BrandID = 1,
                    UPC = "123456789012",
                    CountryOfOrigin = "South Korea",
                    Weight = 0.35m,
                    Dimensions = "15.2 x 7.2 x 0.8 cm",
                    Color = "Phantom Black",
                    MetaTitle = "Samsung Galaxy S22 - Flagship Smartphone",
                    MetaDescription = "Samsung Galaxy S22 with powerful performance and sleek design.",
                    Slug = "samsung-galaxy-s22",
                    ThumbnailURL = "https://example.com/images/sgs22-thumbnail.jpg",
                    ImageURLs = new List<string> { "https://example.com/images/sgs22-1.jpg", "https://example.com/images/sgs22-2.jpg" },
                    VideoURL = "https://example.com/videos/sgs22.mp4",
                    CreatedBy = "admin",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = "admin",
                    ModifiedDate = DateTime.UtcNow,
                    Tags = new List<string> { "smartphone", "android", "samsung" },
                    CustomAttributes = new Dictionary<string, string>
                    {
                        { "Screen Size", "6.1 inches" },
                        { "RAM", "8 GB" }
                    },
                    CrossSellProducts = new List<int> { 2, 3 },
                    AvailabilitySchedule = "Mon-Fri: 9am-5pm",
                    ProductTags = new List<ProductTag> { savedTags.First(t => t.TagName == "smartphone") }
                },
                new Product
                {
                    ProductName = "Volvo FH Truck",
                    ProductType = "Truck",
                    Price = 120000m,
                    OriginalPrice = 130000m,
                    SalePrice = 115000m,
                    RatingAverage = 4.5,
                    ReviewCount = 250,
                    SKU = "VOLVO-FH-001",
                    StockQuantity = 15,
                    QuantitySold = 5,
                    IsActive = true,
                    IsDiscounted = true,
                    IsFeatured = false,
                    IsInStock = true,
                    IsNew = false,
                    //Manufacturer = savedManufacturers.First(m => m.Name == "Volvo"),
                    BrandID = 2,
                    UPC = "987654321098",
                    CountryOfOrigin = "Sweden",
                    Weight = 8000m,
                    Dimensions = "600 x 250 x 350 cm",
                    Color = "White",
                    MetaTitle = "Volvo FH Truck - Heavy Duty",
                    MetaDescription = "The Volvo FH Truck is built for long hauls and tough conditions.",
                    Slug = "volvo-fh-truck",
                    ThumbnailURL = "https://example.com/images/volvo-fh-thumbnail.jpg",
                    ImageURLs = new List<string> { "https://example.com/images/volvo-fh-1.jpg", "https://example.com/images/volvo-fh-2.jpg" },
                    VideoURL = "https://example.com/videos/volvo-fh.mp4",
                    CreatedBy = "admin",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = "admin",
                    ModifiedDate = DateTime.UtcNow,
                    Tags = new List<string> { "truck", "heavy-duty", "volvo" },
                    CustomAttributes = new Dictionary<string, string>
                    {
                        { "Engine Power", "540 HP" },
                        { "Fuel Type", "Diesel" }
                    },
                    CrossSellProducts = new List<int> { },
                    AvailabilitySchedule = "Mon-Sat: 8am-6pm",
                    ProductTags = new List<ProductTag> { savedTags.First(t => t.TagName == "truck") }
                },
                new Product
                {
                    ProductName = "Boeing 737",
                    ProductType = "Plane",
                    Price = 85000000m,
                    OriginalPrice = 90000000m,
                    SalePrice = 80000000m,
                    RatingAverage = 4.8,
                    ReviewCount = 100,
                    SKU = "BOEING-737-001",
                    StockQuantity = 3,
                    QuantitySold = 1,
                    IsActive = true,
                    IsDiscounted = true,
                    IsFeatured = true,
                    IsInStock = true,
                    IsNew = true,
                    //Manufacturer = savedManufacturers.First(m => m.Name == "Boeing"),
                    BrandID = 3,
                    UPC = "123789456123",
                    CountryOfOrigin = "USA",
                    Weight = 41400m,
                    Dimensions = "39.5 x 35.8 x 12.5 m",
                    Color = "White",
                    MetaTitle = "Boeing 737 - Commercial Aircraft",
                    MetaDescription = "The Boeing 737 is a popular commercial airplane known for efficiency and reliability.",
                    Slug = "boeing-737",
                    ThumbnailURL = "https://example.com/images/boeing-737-thumbnail.jpg",
                    ImageURLs = new List<string> { "https://example.com/images/boeing-737-1.jpg", "https://example.com/images/boeing-737-2.jpg" },
                    VideoURL = "https://example.com/videos/boeing-737.mp4",
                    CreatedBy = "admin",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = "admin",
                    ModifiedDate = DateTime.UtcNow,
                    Tags = new List<string> { "plane", "aircraft", "boeing" },
                    CustomAttributes = new Dictionary<string, string>
                    {
                        { "Seating Capacity", "189" },
                        { "Range", "3515 km" }
                    },
                    CrossSellProducts = new List<int> { },
                    AvailabilitySchedule = "Mon-Fri: 7am-7pm",
                    ProductTags = new List<ProductTag> { savedTags.First(t => t.TagName == "aircraft") }
                }
            };

            // Add products only if SKU does not exist to avoid duplicates
            foreach (var product in productsToAdd)
            {
                if (!context.Products.Any(p => p.SKU == product.SKU))
                {
                    context.Products.Add(product);
                }
            }

            try
            {
                context.SaveChanges();
                Console.WriteLine("Products successfully saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving products: " + ex.Message);
                throw;
            }

            // Seed product attributes only if empty
            if (!context.ProductAttributes.Any())
            {
                var attributes = new List<ProductAttribute>();

                foreach (var product in context.Products)
                {
                    Console.WriteLine($"Seeding attributes for {product.ProductName}");
                    attributes.AddRange(GenerateProductAttributes(product));
                }

                context.ProductAttributes.AddRange(attributes);
                try
                {
                    context.SaveChanges();
                    Console.WriteLine("Product attributes successfully saved.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving product attributes: " + ex.Message);
                    throw;
                }
            }
        }


        private static IEnumerable<ProductAttribute> GenerateProductAttributes(Product product)
        {
            var attributes = new List<ProductAttribute>();
            switch (product.ProductType)
            {
                case "Smartphone":
                    attributes.Add(new ProductAttribute
                    {
                        ProductID = product.ProductID,
                        AttributeName = "Screen Size",
                        AttributeValue = "6.1 inches",
                        IsRequired = true,
                        AttributeType = "string",
                        AttributeCode = "screen_size",
                        Description = "The size of the screen",
                        DisplayOrder = 1,
                        ValidationRule = "None",
                        IsActive = true,
                        DefaultValue = "6.1 inches",
                        UnitOfMeasurement = "inches",
                        UnitType = "Display",
                        CreatedDate = DateTime.UtcNow,
                        LastUpdated = DateTime.UtcNow,
                        IsSearchable = true,
                        AttributeGroup = "Display",
                        CategoryID = 1,
                        IsVisibleInFilter = true,
                        TooltipText = "The size of the screen in inches",
                        InputMask = "",
                        AttributeScope = "Global"
                    });
                    break;

                case "Truck":
                    attributes.Add(new ProductAttribute
                    {
                        ProductID = product.ProductID,
                        AttributeName = "Engine Power",
                        AttributeValue = "540 HP",
                        IsRequired = true,
                        AttributeType = "string",
                        AttributeCode = "engine_power",
                        Description = "Engine power in horsepower",
                        DisplayOrder = 1,
                        ValidationRule = "None",
                        IsActive = true,
                        DefaultValue = "540 HP",
                        UnitOfMeasurement = "HP",
                        UnitType = "Engine",
                        CreatedDate = DateTime.UtcNow,
                        LastUpdated = DateTime.UtcNow,
                        IsSearchable = true,
                        AttributeGroup = "Engine",
                        CategoryID = 2,
                        IsVisibleInFilter = true,
                        TooltipText = "Engine power of the truck",
                        InputMask = "",
                        AttributeScope = "Global"
                    });
                    break;

                case "Plane":
                    attributes.Add(new ProductAttribute
                    {
                        ProductID = product.ProductID,
                        AttributeName = "Seating Capacity",
                        AttributeValue = "189",
                        IsRequired = true,
                        AttributeType = "string",
                        AttributeCode = "seating_capacity",
                        Description = "Number of seats",
                        DisplayOrder = 1,
                        ValidationRule = "None",
                        IsActive = true,
                        DefaultValue = "189",
                        UnitOfMeasurement = "seats",
                        UnitType = "Capacity",
                        CreatedDate = DateTime.UtcNow,
                        LastUpdated = DateTime.UtcNow,
                        IsSearchable = true,
                        AttributeGroup = "Capacity",
                        CategoryID = 3,
                        IsVisibleInFilter = true,
                        TooltipText = "Seating capacity of the plane",
                        InputMask = "",
                        AttributeScope = "Global"
                    });
                    break;
            }
            return attributes;
        }

    }
}
