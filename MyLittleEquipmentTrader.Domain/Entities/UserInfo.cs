using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLittleEquipmentTrader.Domain.Entities
{
    public class UserInfo
    {
        // Basic Info
        public int UserInfoID { get; set; }

        public int TenantID { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? UserType { get; set; } = "Individual";
        public string Role { get; set; } = "User";
        public bool IsActive { get; set; } = true;
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime? DateOfBirth { get; set; } = null; // nullable
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string PreferredCurrency { get; set; } = "USD";

        // Address Info
        public int AddressID { get; set; } = 0;
        public string? AddressLine1 { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }

        // Verification & Security Info
        public bool TwoFactorEnabled { get; set; } = false;
        public bool IsEmailVerified { get; set; } = false;
        public bool IsPhoneVerified { get; set; } = false;
        public int AccessFailedCount { get; set; } = 0;
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        public string? EmailVerificationToken { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? TokenExpiryDate { get; set; }

        // User Activity & Stats
        public int TotalOrders { get; set; } = 0;
        public decimal TotalSpent { get; set; } = 0m;
        public DateTime? LastOrderDate { get; set; }
        public string? Wishlist { get; set; }
        public string? ViewedProducts { get; set; }

        // Seller Info
        public int TotalListings { get; set; } = 0;
        public int ActiveListingsCount { get; set; } = 0;
        public decimal TotalSalesValue { get; set; } = 0m;
        public decimal AverageRatingAsSeller { get; set; } = 0m;
        public int ReviewCount { get; set; } = 0;

        // Business Info
        public string? CompanyName { get; set; }
        public string? BusinessType { get; set; }
        public string? DealerLicenseNumber { get; set; }
        public bool IsVerifiedDealer { get; set; } = false;
        public string? DealerProfileUrl { get; set; }

        // Referral & Signup Info
        public string? SignupSource { get; set; }
        public string? ReferralCode { get; set; }
        public string? ReferredBy { get; set; }

        // Marketing Preferences
        public bool MarketingOptIn { get; set; } = false;
        public bool EmailNotificationsEnabled { get; set; } = true;
        public bool SMSNotificationsEnabled { get; set; } = false;
        public bool AppPushEnabled { get; set; } = false;
        public string? PreferredContactMethod { get; set; } = "Email";

        // Plain Password (for DTO mapping only, not stored)
        [NotMapped]
        public string? Password { get; set; }

        // ✅ Alias for ID (not mapped)
        [NotMapped]
        public int Id => UserInfoID;
    }
}
