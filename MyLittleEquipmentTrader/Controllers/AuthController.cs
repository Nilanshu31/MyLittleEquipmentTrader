using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyLittleEquipmentTrader.Application.Models.DTOs;
using MyLittleEquipmentTrader.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using MyLittleEquipmentTrader.Infrastructure;
using MyLittleEquipmentTrader.Domain.Entities;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace MyLittleEquipmentTrader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<UserInfo> _passwordHasher;

        public AuthController(IUnitOfWork uow, IConfiguration config, IPasswordHasher<UserInfo> passwordHasher)
        {
            _uow = uow;
            _config = config;
            _passwordHasher = passwordHasher;
        }

        // ✅ Register new user
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var exists = _uow.UserInfoRepository.GetAll().Any(u => u.Email == dto.Email);
            if (exists)
                return BadRequest("Email already registered.");

            var user = new UserInfo
            {
                Username = dto.UserName,
                Email = dto.Email,
                Role = dto.Role ?? "User",
                TotalOrders = 0,
                TotalListings = 0,
                AccessFailedCount = 0,
                IsActive = true,
                RegistrationDate = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            await _uow.UserInfoRepository.AddAsync(user);
            await _uow.CommitAsync();

            return Ok(new
            {
                user.UserInfoID,
                user.Username,
                user.Email,
                user.Role
            });
        }

        // ✅ Login user
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _uow.UserInfoRepository.GetAll()
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token,
                user = new
                {
                    user.UserInfoID,
                    user.Username,
                    user.Email,
                    user.Role
                }
            });
        }

        // ✅ Generate JWT with role + permission claims
        private string GenerateJwtToken(UserInfo user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserInfoID.ToString()),
                new Claim(ClaimTypes.Name, user.Username ?? user.Email),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Role, user.Role ?? "User"),
                new Claim("UserType", user.UserType ?? "Individual")
            };

            // 🌐 Assign permissions based on Role & UserType
            switch (user.Role)
            {
                case "GlobalAdmin":
                    // ✅ View
                    claims.Add(new Claim("Permission", "ViewProducts"));
                    claims.Add(new Claim("Permission", "ViewManufacturers"));
                    claims.Add(new Claim("Permission", "ViewTenants"));
                    claims.Add(new Claim("Permission", "ViewPlans"));
                    claims.Add(new Claim("Permission", "ViewAccessRoles"));
                    claims.Add(new Claim("Permission", "ViewSalesOrders"));
                    claims.Add(new Claim("Permission", "ViewCategories"));
                    claims.Add(new Claim("Permission", "ViewReports"));
                    claims.Add(new Claim("Permission", "ViewUserInfos"));

                    // ✅ Create
                    claims.Add(new Claim("Permission", "CreateProducts"));
                    claims.Add(new Claim("Permission", "CreateSalesOrders"));
                    claims.Add(new Claim("Permission", "CreateCategories"));
                    claims.Add(new Claim("Permission", "CreateReports"));

                    // ✅ Delete
                    claims.Add(new Claim("Permission", "DeleteProducts"));
                    claims.Add(new Claim("Permission", "DeleteSalesOrders"));
                    claims.Add(new Claim("Permission", "DeleteCategories"));
                    claims.Add(new Claim("Permission", "DeleteReports"));

                    // ✅ Filter (list/search)
                    claims.Add(new Claim("Permission", "FilterProducts"));
                    claims.Add(new Claim("Permission", "FilterSalesOrders"));
                    claims.Add(new Claim("Permission", "FilterCategories"));
                    claims.Add(new Claim("Permission", "FilterReports"));
                    break;

                case "Admin":
                    // ✅ View
                    claims.Add(new Claim("Permission", "ViewProducts"));
                    claims.Add(new Claim("Permission", "ViewManufacturers"));
                    claims.Add(new Claim("Permission", "ViewPlans"));
                    claims.Add(new Claim("Permission", "ViewCategories"));
                    claims.Add(new Claim("Permission", "ViewReports"));

                    // ✅ Create
                    claims.Add(new Claim("Permission", "CreateProducts"));
                    claims.Add(new Claim("Permission", "CreateCategories"));

                    // ✅ Delete
                    claims.Add(new Claim("Permission", "DeleteProducts"));
                    claims.Add(new Claim("Permission", "DeleteCategories"));

                    // ✅ Filter
                    claims.Add(new Claim("Permission", "FilterProducts"));
                    claims.Add(new Claim("Permission", "FilterCategories"));
                    break;

                case "TenantAdmin":
                    // ✅ View
                    claims.Add(new Claim("Permission", "ViewProducts"));
                    claims.Add(new Claim("Permission", "ViewSalesOrders"));
                    claims.Add(new Claim("Permission", "ViewReports"));
                    claims.Add(new Claim("Permission", "ViewCategories"));

                    // ✅ Create
                    claims.Add(new Claim("Permission", "CreateProducts"));
                    claims.Add(new Claim("Permission", "CreateSalesOrders"));
                    claims.Add(new Claim("Permission", "CreateCategories"));
                    claims.Add(new Claim("Permission", "CreateReports"));

                    // ✅ Delete
                    claims.Add(new Claim("Permission", "DeleteProducts"));
                    claims.Add(new Claim("Permission", "DeleteSalesOrders"));
                    claims.Add(new Claim("Permission", "DeleteCategories"));
                    claims.Add(new Claim("Permission", "DeleteReports"));

                    // ✅ Filter
                    claims.Add(new Claim("Permission", "FilterProducts"));
                    claims.Add(new Claim("Permission", "FilterSalesOrders"));
                    claims.Add(new Claim("Permission", "FilterCategories"));
                    claims.Add(new Claim("Permission", "FilterReports"));
                    break;

                case "Dealer":
                    // ✅ View
                    claims.Add(new Claim("Permission", "ViewProducts"));
                    claims.Add(new Claim("Permission", "ViewSalesOrders"));

                    // ✅ Create
                    claims.Add(new Claim("Permission", "CreateSalesOrders"));

                    // ✅ Delete (optional)
                    claims.Add(new Claim("Permission", "DeleteSalesOrders"));

                    // ✅ Filter
                    claims.Add(new Claim("Permission", "FilterProducts"));
                    claims.Add(new Claim("Permission", "FilterSalesOrders"));
                    break;

                case "User":
                default:
                    // ✅ View
                    claims.Add(new Claim("Permission", "ViewProducts"));

                    // ✅ Filter
                    claims.Add(new Claim("Permission", "FilterProducts"));
                    break;
            }



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiryMinutes = int.TryParse(_config["Jwt:ExpiryMinutes"], out var m) ? m : 60;

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
