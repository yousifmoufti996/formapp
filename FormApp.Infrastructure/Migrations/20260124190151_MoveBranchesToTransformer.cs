using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveBranchesToTransformer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionBranches_Transactions_TransactionId",
                table: "SubscriptionBranches");

            migrationBuilder.DropColumn(
                name: "HasBranches",
                table: "SubscriptionBranches");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "SubscriptionBranches",
                newName: "TransformerId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionBranches_TransactionId",
                table: "SubscriptionBranches",
                newName: "IX_SubscriptionBranches_TransformerId");

            migrationBuilder.AddColumn<bool>(
                name: "HasBranches",
                table: "Transformers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SubscriptionBranches",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "SubscriptionBranches",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "SubscriptionBranches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionBranches_Transformers_TransformerId",
                table: "SubscriptionBranches",
                column: "TransformerId",
                principalTable: "Transformers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionBranches_Transformers_TransformerId",
                table: "SubscriptionBranches");

            migrationBuilder.DropColumn(
                name: "HasBranches",
                table: "Transformers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SubscriptionBranches");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "SubscriptionBranches");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "SubscriptionBranches");

            migrationBuilder.RenameColumn(
                name: "TransformerId",
                table: "SubscriptionBranches",
                newName: "TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionBranches_TransformerId",
                table: "SubscriptionBranches",
                newName: "IX_SubscriptionBranches_TransactionId");

            migrationBuilder.AddColumn<bool>(
                name: "HasBranches",
                table: "SubscriptionBranches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionBranches_Transactions_TransactionId",
                table: "SubscriptionBranches",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
