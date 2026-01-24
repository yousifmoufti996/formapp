using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // The InitialCreate migration doesn't include ManufacturingCompanies, Neighborhoods, or FacilityRecords
            // So there's nothing to drop in a fresh database
            // This migration is here for documentation purposes
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Nothing to reverse
        }
    }
}
