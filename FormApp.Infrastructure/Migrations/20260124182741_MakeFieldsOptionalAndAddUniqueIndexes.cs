using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeFieldsOptionalAndAddUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_AccountNumber",
                table: "Subscribers",
                column: "AccountNumber",
                unique: true,
                filter: "[AccountNumber] IS NOT NULL AND [AccountNumber] != ''");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_SubscriptionNumber",
                table: "Subscribers",
                column: "SubscriptionNumber",
                unique: true,
                filter: "[SubscriptionNumber] IS NOT NULL AND [SubscriptionNumber] != ''");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subscribers_AccountNumber",
                table: "Subscribers");

            migrationBuilder.DropIndex(
                name: "IX_Subscribers_SubscriptionNumber",
                table: "Subscribers");
        }
    }
}
