using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLittleEquipmentTrader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedplan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanSystemAlias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanCategoryAssignment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanUrlSlug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EarlyTerminationFeeRule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomSupportLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupportHours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DedicatedAccountManager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceLimits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionalAvailability = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditLogSettings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnalyticsReportingLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionLimits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowedPaymentCycles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BulkDiscountRule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountEligibility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutomatedRenewDiscount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountTypeTag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BandwidthDataAllocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceSeatAccessRule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EligibilityToggles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GlobalPlanVisibility = table.Column<bool>(type: "bit", nullable: false),
                    OfferExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanLaunchDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OverageChargeRule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionPausePolicy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanArchivingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataExportOptions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DynamicPricingEngine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceUpgradePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InternalReviewCycle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomBillingCycle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.PlanId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
