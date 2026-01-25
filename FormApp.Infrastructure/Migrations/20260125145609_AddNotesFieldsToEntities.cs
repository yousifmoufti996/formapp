using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesFieldsToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransformerNotes",
                table: "Transformers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubscriberNotes",
                table: "Subscribers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeterNotes",
                table: "MeterScales",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransformerNotes",
                table: "Transformers");

            migrationBuilder.DropColumn(
                name: "SubscriberNotes",
                table: "Subscribers");

            migrationBuilder.DropColumn(
                name: "MeterNotes",
                table: "MeterScales");
        }
    }
}
