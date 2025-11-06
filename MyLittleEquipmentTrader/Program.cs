using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using MyLittleEquipmentTrader.Application.Services;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure;
using MyLittleEquipmentTrader.Infrastructure.Data;
using MyLittleEquipmentTrader.Infrastructure.Repositories;
using System.Text;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// ===========================
// Database Configuration
// ===========================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ===========================
// Dependency Injection
// ===========================
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPasswordHasher<UserInfo>, PasswordHasher<UserInfo>>();
builder.Services.AddScoped<IUserInfoRepository, UserInfoRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPlanService, PlanService>();

builder.Services.AddScoped<IPlanRepository, PlanRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ISalesOrderService, SalesOrderService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<IAccessRoleService, AccessRoleService>();
// ===========================
// Controllers & Scalar Setup
// ===========================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ===========================
// OpenAPI (Scalar) Setup
// ===========================
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new OpenApiComponents();

        document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter: Bearer {your JWT token}"
        };

        document.SecurityRequirements.Add(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        return Task.CompletedTask;
    });
});

// ===========================
// JWT Authentication
// ===========================
var key = builder.Configuration["Jwt:Key"] ?? "ReplaceThisWithStrongKey";
var issuer = builder.Configuration["Jwt:Issuer"] ?? "MyIssuer";
var audience = builder.Configuration["Jwt:Audience"] ?? "MyAudience";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

// ===========================
// Authorization (Role + Claims)
// ===========================
builder.Services.AddAuthorization(options =>
{
    // Role-based policies (if needed later)
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("GlobalAdminOnly", policy => policy.RequireRole("GlobalAdmin"));
    options.AddPolicy("TenantAdminOnly", policy => policy.RequireRole("TenantAdmin"));

    // ✅ View / Filter policies for entities
    options.AddPolicy("CanViewProducts", policy =>
        policy.RequireClaim("Permission", "ViewProducts"));
    options.AddPolicy("CanFilterProducts", policy =>
        policy.RequireClaim("Permission", "FilterProducts"));
    options.AddPolicy("CanCreateProducts", policy =>
        policy.RequireClaim("Permission", "CreateProducts"));
    options.AddPolicy("CanDeleteProducts", policy =>
        policy.RequireClaim("Permission", "DeleteProducts"));

    options.AddPolicy("CanViewManufacturers", policy =>
        policy.RequireClaim("Permission", "ViewManufacturers"));

    options.AddPolicy("CanViewTenants", policy =>
        policy.RequireClaim("Permission", "ViewTenants"));

    options.AddPolicy("CanViewPlans", policy =>
        policy.RequireClaim("Permission", "ViewPlans"));

    options.AddPolicy("CanViewAccessRoles", policy =>
        policy.RequireClaim("Permission", "ViewAccessRoles"));

    options.AddPolicy("CanViewSalesOrders", policy =>
        policy.RequireClaim("Permission", "ViewSalesOrders"));
    options.AddPolicy("CanFilterSalesOrders", policy =>
        policy.RequireClaim("Permission", "FilterSalesOrders"));
    options.AddPolicy("CanCreateSalesOrders", policy =>
        policy.RequireClaim("Permission", "CreateSalesOrders"));
    options.AddPolicy("CanDeleteSalesOrders", policy =>
        policy.RequireClaim("Permission", "DeleteSalesOrders"));

    options.AddPolicy("CanViewCategories", policy =>
        policy.RequireClaim("Permission", "ViewCategories"));

    options.AddPolicy("CanViewReports", policy =>
        policy.RequireClaim("Permission", "ViewReports"));

    options.AddPolicy("CanViewUserInfos", policy =>
        policy.RequireClaim("Permission", "ViewUserInfos"));
    // Create, Delete, Filter policies
    options.AddPolicy("CanCreateProducts", policy => policy.RequireClaim("Permission", "CreateProducts"));
    options.AddPolicy("CanDeleteProducts", policy => policy.RequireClaim("Permission", "DeleteProducts"));
    options.AddPolicy("CanFilterProducts", policy => policy.RequireClaim("Permission", "FilterProducts"));

    options.AddPolicy("CanCreateCategories", policy => policy.RequireClaim("Permission", "CreateCategories"));
    options.AddPolicy("CanDeleteCategories", policy => policy.RequireClaim("Permission", "DeleteCategories"));
    options.AddPolicy("CanFilterCategories", policy => policy.RequireClaim("Permission", "FilterCategories"));

    options.AddPolicy("CanCreateSalesOrders", policy => policy.RequireClaim("Permission", "CreateSalesOrders"));
    options.AddPolicy("CanDeleteSalesOrders", policy => policy.RequireClaim("Permission", "DeleteSalesOrders"));
    options.AddPolicy("CanFilterSalesOrders", policy => policy.RequireClaim("Permission", "FilterSalesOrders"));


    options.AddPolicy("CanCreateReports", policy => policy.RequireClaim("Permission", "CreateReports"));
    options.AddPolicy("CanDeleteReports", policy => policy.RequireClaim("Permission", "DeleteReports"));
    options.AddPolicy("CanFilterReports", policy => policy.RequireClaim("Permission", "FilterReports"));

});


// ===========================
// Build App
// ===========================
var app = builder.Build();

// ===========================
// Middleware
// ===========================
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.MapOpenApi();
    app.MapScalarApiReference(opts =>
    {
        opts.Title = "MyLittleEquipmentTrader API";
        opts.WithPreferredScheme("Bearer");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
