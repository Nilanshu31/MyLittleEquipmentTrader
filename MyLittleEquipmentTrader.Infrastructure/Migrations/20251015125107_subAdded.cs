using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLittleEquipmentTrader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class subAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    TargetAudience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDiscounted = table.Column<bool>(type: "bit", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PromoCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractLength = table.Column<int>(type: "int", nullable: false),
                    MinimumCommitmentPeriod = table.Column<int>(type: "int", nullable: false),
                    RenewalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUpgradeAvailable = table.Column<bool>(type: "bit", nullable: false),
                    AutoRenewal = table.Column<bool>(type: "bit", nullable: false),
                    CurrencySymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxActiveSessions = table.Column<int>(type: "int", nullable: false),
                    TrialPeriodDays = table.Column<int>(type: "int", nullable: false),
                    ReferralBonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserCapacity = table.Column<int>(type: "int", nullable: false),
                    MultiplePaymentMethods = table.Column<bool>(type: "bit", nullable: false),
                    AnnualSubscriptionPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuarterlySubscriptionPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlySubscriptionPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentGatewayFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataRetentionPolicy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractRenewalReminder = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentPeriodOptions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AutoUpgrade = table.Column<bool>(type: "bit", nullable: false),
                    IsMultiTenant = table.Column<bool>(type: "bit", nullable: false),
                    ClientRestrictions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MultiDeviceAccess = table.Column<bool>(type: "bit", nullable: false),
                    RegionalSubscriptionLimit = table.Column<int>(type: "int", nullable: false),
                    PriceLockPeriod = table.Column<int>(type: "int", nullable: false),
                    LimitationsOnAddOns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnnualRenewalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentProcessingFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaximumUserSeats = table.Column<int>(type: "int", nullable: false),
                    TrialExtensionEligibility = table.Column<bool>(type: "bit", nullable: false),
                    PrepaidPlanDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ResellerProgramEligibility = table.Column<bool>(type: "bit", nullable: false),
                    GlobalSupportAvailability = table.Column<bool>(type: "bit", nullable: false),
                    CustomerRetentionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerReferralCap = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPauseAvailability = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.SubscriptionId);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PlanId",
                table: "Subscriptions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_TenantId",
                table: "Subscriptions",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}
