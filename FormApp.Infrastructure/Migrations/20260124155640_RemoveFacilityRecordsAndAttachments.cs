using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFacilityRecordsAndAttachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacilityAttachments");

            migrationBuilder.DropTable(
                name: "FacilityRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacilityRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActualAlley = table.Column<int>(type: "int", nullable: true),
                    ActualBuildingType = table.Column<int>(type: "int", nullable: false),
                    ActualDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualHouseNumber = table.Column<int>(type: "int", nullable: true),
                    ActualMeasurementNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddressStatus = table.Column<int>(type: "int", nullable: false),
                    AnyAddress = table.Column<int>(type: "int", nullable: false),
                    BeneficiaryStatus = table.Column<int>(type: "int", nullable: false),
                    BenefitingUnitsCount = table.Column<int>(type: "int", nullable: false),
                    BranchCount = table.Column<int>(type: "int", nullable: false),
                    BranchType = table.Column<int>(type: "int", nullable: true),
                    CanAddPartitions = table.Column<bool>(type: "bit", nullable: false),
                    CommercialAccountName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedAlley = table.Column<int>(type: "int", nullable: true),
                    EstimatedDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimatedHouseNumber = table.Column<int>(type: "int", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FieldElectricCompanyCompanion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FieldPersonName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HasBranches = table.Column<bool>(type: "bit", nullable: false),
                    HasDestructionOrDamage = table.Column<bool>(type: "bit", nullable: false),
                    HasRoomButtons = table.Column<bool>(type: "bit", nullable: false),
                    HasUnitsBeforeMeter = table.Column<bool>(type: "bit", nullable: false),
                    HasUnitsFromOtherSubscriptions = table.Column<bool>(type: "bit", nullable: false),
                    IsNotRealReadingNumber = table.Column<bool>(type: "bit", nullable: false),
                    IsRoomSuitable = table.Column<bool>(type: "bit", nullable: false),
                    IsSharedMeter = table.Column<bool>(type: "bit", nullable: false),
                    IsTransformerWorking = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: true),
                    ListBuildingType = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: true),
                    MeasurementNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MeasurementUnitActual = table.Column<int>(type: "int", nullable: true),
                    MeasurementUnitScan = table.Column<int>(type: "int", nullable: true),
                    MeterStatus = table.Column<int>(type: "int", nullable: false),
                    Mobile1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Mobile2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MultiplicationFactor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NearestPoint = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NoMatchingList = table.Column<bool>(type: "bit", nullable: false),
                    NoMatchingMeasurement = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other = table.Column<bool>(type: "bit", nullable: false),
                    OtherUnitsBeforeMeterCount = table.Column<int>(type: "int", nullable: false),
                    ReadingNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubscriberName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubscriptionNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubscriptionType = table.Column<int>(type: "int", nullable: false),
                    TransformerCapacity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransformerSerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityRecords_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacilityAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityAttachments_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacilityAttachments_FacilityRecords_FacilityRecordId",
                        column: x => x.FacilityRecordId,
                        principalTable: "FacilityRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacilityAttachments_UploadedFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "UploadedFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacilityAttachments_CreatedById",
                table: "FacilityAttachments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityAttachments_FacilityRecordId",
                table: "FacilityAttachments",
                column: "FacilityRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityAttachments_FileId",
                table: "FacilityAttachments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityRecords_CreatedById",
                table: "FacilityRecords",
                column: "CreatedById");
        }
    }
}
