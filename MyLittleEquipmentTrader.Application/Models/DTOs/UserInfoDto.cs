// Application/Models/Dtos/UserInfoDto.cs
using System;
using System.Collections.Generic;

namespace MyLittleEquipmentTrader.Application.Models.Dtos
{
    public class UserInfoDto
    {
        // Basic Info
        public int UserInfoID { get; set; }
        public int TenantID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string UserType { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string PreferredCurrency { get; set; }

        // Address Info
        public int AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        // Verification & Security Info
        public bool TwoFactorEnabled { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsPhoneVerified { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        public string EmailVerificationToken { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? TokenExpiryDate { get; set; }

        // User Activity & Stats
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public string Wishlist { get; set; }
        public string ViewedProducts { get; set; }

        // Seller Info
        public int TotalListings { get; set; }
        public int ActiveListingsCount { get; set; }
        public decimal TotalSalesValue { get; set; }
        public decimal AverageRatingAsSeller { get; set; }
        public int ReviewCount { get; set; }

        // Business/Company Info
        public string CompanyName { get; set; }
        public string BusinessType { get; set; }
        public string DealerLicenseNumber { get; set; }
        public bool IsVerifiedDealer { get; set; }
        public string DealerProfileUrl { get; set; }

        // Referral & Signup Info
        public string SignupSource { get; set; }
        public string ReferralCode { get; set; }
        public string ReferredBy { get; set; }

        // Marketing & Notification Preferences
        public bool MarketingOptIn { get; set; }
        public bool EmailNotificationsEnabled { get; set; }
        public bool SMSNotificationsEnabled { get; set; }
        public bool AppPushEnabled { get; set; }
        public string PreferredContactMethod { get; set; }

        // Only simple collection of attribute values
    }

}
