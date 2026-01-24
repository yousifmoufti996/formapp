using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddManufacturingCompanyToMeterScaleAndOtherUnitsAfterMeterCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OtherUnitsAfterMeterCount",
                table: "SubscriptionViolations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ManufacturingCompany",
                table: "MeterScales",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherUnitsAfterMeterCount",
                table: "SubscriptionViolations");

            migrationBuilder.DropColumn(
                name: "ManufacturingCompany",
                table: "MeterScales");
        }
    }
}
