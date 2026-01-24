using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureTransactionRecordDecimalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_CreatedById",
                table: "TransactionRecords");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "TransactionRecords",
                type: "decimal(18,8)",
                precision: 18,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "TransactionRecords",
                type: "decimal(18,8)",
                precision: 18,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActualNeighborhood",
                table: "TransactionRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstimatedNeighborhood",
                table: "TransactionRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManufacturingCompany",
                table: "TransactionRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_CreatedById",
                table: "TransactionRecords",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_CreatedById",
                table: "TransactionRecords");

            migrationBuilder.DropColumn(
                name: "ActualNeighborhood",
                table: "TransactionRecords");

            migrationBuilder.DropColumn(
                name: "EstimatedNeighborhood",
                table: "TransactionRecords");

            migrationBuilder.DropColumn(
                name: "ManufacturingCompany",
                table: "TransactionRecords");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "TransactionRecords",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)",
                oldPrecision: 18,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "TransactionRecords",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)",
                oldPrecision: 18,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_CreatedById",
                table: "TransactionRecords",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
