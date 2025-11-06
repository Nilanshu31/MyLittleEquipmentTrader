using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLittleEquipmentTrader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantID = table.Column<int>(type: "int", nullable: false),
                    ReportType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    DashboardWidgetID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomColumns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessRoles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Filters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsScheduled = table.Column<bool>(type: "bit", nullable: false),
                    ExportFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExportUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalSales = table.Column<int>(type: "int", nullable: false),
                    TotalRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalOrders = table.Column<int>(type: "int", nullable: false),
                    AverageSalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalFeesCollected = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncludeDiscounts = table.Column<bool>(type: "bit", nullable: false),
                    RegionWiseSales = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalListingsSold = table.Column<int>(type: "int", nullable: false),
                    TopListings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopCategories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryTurnover = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecordCount = table.Column<int>(type: "int", nullable: false),
                    TopSellers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopBuyers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserSegment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerPerformance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageViews = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Report");
        }
    }
}
