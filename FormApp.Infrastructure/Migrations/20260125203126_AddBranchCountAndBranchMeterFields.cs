using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBranchCountAndBranchMeterFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchCount",
                table: "Transformers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BranchActualMeasurementNumber",
                table: "SubscriptionBranches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BranchIsNotRealReadingNumber",
                table: "SubscriptionBranches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BranchManufacturingCompany",
                table: "SubscriptionBranches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchMeasurementNumber",
                table: "SubscriptionBranches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchMeasurementUnitActual",
                table: "SubscriptionBranches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchMeasurementUnitScan",
                table: "SubscriptionBranches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchMeterNotes",
                table: "SubscriptionBranches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchMeterStatus",
                table: "SubscriptionBranches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchMultiplicationFactor",
                table: "SubscriptionBranches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchReadingNumber",
                table: "SubscriptionBranches",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchCount",
                table: "Transformers");

            migrationBuilder.DropColumn(
                name: "BranchActualMeasurementNumber",
                table: "SubscriptionBranches");

            migrationBuilder.DropColumn(
                name: "BranchIsNotRealReadingNumber",
                table: "SubscriptionBranches");

            migrationBuilder.DropColumn(
                name: "BranchManufacturingCompany",
                table: "SubscriptionBranches");

            migrationBuilder.DropColumn(
                name: "BranchMeasurementNumber",
                table: "SubscriptionBranches");

            migrationBuilder.DropColumn(
                name: "BranchMeasurementUnitActual",
                table: "SubscriptionBranches");

            migrationBuilder.DropColumn(
                name: "BranchMeasurementUnitScan",
                table: "SubscriptionBranches");

            migrationBuilder.DropColumn(
                name: "BranchMeterNotes",
                table: "SubscriptionBranches");

            migrationBuilder.DropColumn(
                name: "BranchMeterStatus",
                table: "SubscriptionBranches");

            migrationBuilder.DropColumn(
                name: "BranchMultiplicationFactor",
                table: "SubscriptionBranches");

            migrationBuilder.DropColumn(
                name: "BranchReadingNumber",
                table: "SubscriptionBranches");
        }
    }
}
