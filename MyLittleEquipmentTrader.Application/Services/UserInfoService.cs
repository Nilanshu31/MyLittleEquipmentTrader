using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Application.Helpers;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Models.Dtos;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Application.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserInfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Get all user infos
        public async Task<IEnumerable<UserInfoDto>> GetAllUserInfosAsync()
        {
            var userInfos = await _unitOfWork.UserInfos.GetQueryable().ToListAsync();
            return userInfos.Select(MapToDto).ToList();
        }

        // Get filtered and paginated user infos
        public async Task<PagedResult<UserInfoDto>> GetFilteredUserInfosAsync(ProductFilterRequest filterRequest)
        {
            filterRequest ??= new ProductFilterRequest();

            var query = _unitOfWork.UserInfos.GetQueryable();

            // Apply filters dynamically
            if (filterRequest.Filters != null && filterRequest.Filters.Any())
            {
                query = FilterHelper.ApplyFilters(query, filterRequest.Filters);
            }

            // Apply sorting dynamically
            query = FilterHelper.ApplySorting(query, filterRequest.SortBy, filterRequest.SortOrder);

            // Get total count after filters
            var totalCount = await query.CountAsync();

            // Apply pagination
            var items = await query
                .Skip((filterRequest.Page - 1) * filterRequest.PageSize)
                .Take(filterRequest.PageSize)
                .ToListAsync();

            var dtos = items.Select(MapToDto).ToList();

            var totalPages = filterRequest.PageSize <= 0
                ? 1
                : (int)Math.Ceiling(totalCount / (double)filterRequest.PageSize);

            return new PagedResult<UserInfoDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = filterRequest.Page,
                PageSize = filterRequest.PageSize <= 0 ? totalCount : filterRequest.PageSize
            };
        }

        // Map UserInfo entity to DTO
        private UserInfoDto MapToDto(UserInfo userInfo)
        {
            return new UserInfoDto
            {
                UserInfoID = userInfo.UserInfoID,
                TenantID = userInfo.TenantID,
                Email = userInfo.Email,
                Username = userInfo.Username,
                PasswordHash = userInfo.PasswordHash,
                UserType = userInfo.UserType,
                Role = userInfo.Role,
                IsActive = userInfo.IsActive,
                RegistrationDate = userInfo.RegistrationDate,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                ProfilePictureUrl = userInfo.ProfilePictureUrl,
                DateOfBirth = userInfo.DateOfBirth ?? DateTime.MinValue,
                Gender = userInfo.Gender,
                PhoneNumber = userInfo.PhoneNumber,
                PreferredCurrency = userInfo.PreferredCurrency,
                AddressID = userInfo.AddressID,
                AddressLine1 = userInfo.AddressLine1,
                City = userInfo.City,
                State = userInfo.State,
                ZipCode = userInfo.ZipCode,
                Country = userInfo.Country,
                TwoFactorEnabled = userInfo.TwoFactorEnabled,
                IsEmailVerified = userInfo.IsEmailVerified,
                IsPhoneVerified = userInfo.IsPhoneVerified,
                AccessFailedCount = userInfo.AccessFailedCount,
                LastLoginDate = userInfo.LastLoginDate,
                LastPasswordChangeDate = userInfo.LastPasswordChangeDate,
                EmailVerificationToken = userInfo.EmailVerificationToken,
                PasswordResetToken = userInfo.PasswordResetToken,
                TokenExpiryDate = userInfo.TokenExpiryDate,
                TotalOrders = userInfo.TotalOrders,
                TotalSpent = userInfo.TotalSpent,
                LastOrderDate = userInfo.LastOrderDate,
                Wishlist = userInfo.Wishlist,
                ViewedProducts = userInfo.ViewedProducts,
                TotalListings = userInfo.TotalListings,
                ActiveListingsCount = userInfo.ActiveListingsCount,
                TotalSalesValue = userInfo.TotalSalesValue,
                AverageRatingAsSeller = userInfo.AverageRatingAsSeller,
                ReviewCount = userInfo.ReviewCount,
                CompanyName = userInfo.CompanyName,
                BusinessType = userInfo.BusinessType,
                DealerLicenseNumber = userInfo.DealerLicenseNumber,
                IsVerifiedDealer = userInfo.IsVerifiedDealer,
                DealerProfileUrl = userInfo.DealerProfileUrl,
                SignupSource = userInfo.SignupSource,
                ReferralCode = userInfo.ReferralCode,
                ReferredBy = userInfo.ReferredBy,
                MarketingOptIn = userInfo.MarketingOptIn,
                EmailNotificationsEnabled = userInfo.EmailNotificationsEnabled,
                SMSNotificationsEnabled = userInfo.SMSNotificationsEnabled,
                AppPushEnabled = userInfo.AppPushEnabled,
                PreferredContactMethod = userInfo.PreferredContactMethod
            };
        }
    }
}
