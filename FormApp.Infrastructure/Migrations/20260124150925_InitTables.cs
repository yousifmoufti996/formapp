using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeterScales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActualMeasurementNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MeasurementUnitActual = table.Column<int>(type: "int", nullable: true),
                    MeasurementUnitScan = table.Column<int>(type: "int", nullable: true),
                    ReadingNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsNotRealReadingNumber = table.Column<bool>(type: "bit", nullable: false),
                    MultiplicationFactor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MeterStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterScales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubscriptionNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mobile1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Mobile2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AddressStatus = table.Column<int>(type: "int", nullable: false),
                    AnyAddress = table.Column<int>(type: "int", nullable: false),
                    EstimatedNeighborhood = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ActualNeighborhood = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: true),
                    NearestPoint = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EstimatedAlley = table.Column<int>(type: "int", nullable: true),
                    EstimatedHouseNumber = table.Column<int>(type: "int", nullable: true),
                    ActualAlley = table.Column<int>(type: "int", nullable: true),
                    ActualHouseNumber = table.Column<int>(type: "int", nullable: true),
                    EstimatedDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommercialAccountName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FieldPersonName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FieldElectricCompanyCompanion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriberName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubscriptionType = table.Column<int>(type: "int", nullable: false),
                    ListBuildingType = table.Column<int>(type: "int", nullable: false),
                    ActualBuildingType = table.Column<int>(type: "int", nullable: false),
                    BeneficiaryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionViolations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoMatchingList = table.Column<bool>(type: "bit", nullable: false),
                    NoMatchingMeasurement = table.Column<bool>(type: "bit", nullable: false),
                    HasDestructionOrDamage = table.Column<bool>(type: "bit", nullable: false),
                    IsSharedMeter = table.Column<bool>(type: "bit", nullable: false),
                    HasUnitsFromOtherSubscriptions = table.Column<bool>(type: "bit", nullable: false),
                    HasUnitsBeforeMeter = table.Column<bool>(type: "bit", nullable: false),
                    Other = table.Column<bool>(type: "bit", nullable: false),
                    BenefitingUnitsCount = table.Column<int>(type: "int", nullable: false),
                    OtherUnitsBeforeMeterCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionViolations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transformers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransformerCapacity = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    TransformerSerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ManufacturingCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTransformerWorking = table.Column<bool>(type: "bit", nullable: false),
                    HasRoomButtons = table.Column<bool>(type: "bit", nullable: false),
                    IsRoomSuitable = table.Column<bool>(type: "bit", nullable: false),
                    CanAddPartitions = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transformers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacilityRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubscriptionNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mobile1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Mobile2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SubscriberName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubscriptionType = table.Column<int>(type: "int", nullable: false),
                    ListBuildingType = table.Column<int>(type: "int", nullable: false),
                    ActualBuildingType = table.Column<int>(type: "int", nullable: false),
                    BeneficiaryStatus = table.Column<int>(type: "int", nullable: false),
                    MeasurementNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActualMeasurementNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MeasurementUnitActual = table.Column<int>(type: "int", nullable: true),
                    MeasurementUnitScan = table.Column<int>(type: "int", nullable: true),
                    ReadingNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsNotRealReadingNumber = table.Column<bool>(type: "bit", nullable: false),
                    MultiplicationFactor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeterStatus = table.Column<int>(type: "int", nullable: false),
                    AddressStatus = table.Column<int>(type: "int", nullable: false),
                    AnyAddress = table.Column<int>(type: "int", nullable: false),
                    NoMatchingList = table.Column<bool>(type: "bit", nullable: false),
                    NoMatchingMeasurement = table.Column<bool>(type: "bit", nullable: false),
                    HasDestructionOrDamage = table.Column<bool>(type: "bit", nullable: false),
                    IsSharedMeter = table.Column<bool>(type: "bit", nullable: false),
                    HasUnitsFromOtherSubscriptions = table.Column<bool>(type: "bit", nullable: false),
                    HasUnitsBeforeMeter = table.Column<bool>(type: "bit", nullable: false),
                    Other = table.Column<bool>(type: "bit", nullable: false),
                    BenefitingUnitsCount = table.Column<int>(type: "int", nullable: false),
                    OtherUnitsBeforeMeterCount = table.Column<int>(type: "int", nullable: false),
                    TransformerCapacity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransformerSerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsTransformerWorking = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: true),
                    NearestPoint = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EstimatedAlley = table.Column<int>(type: "int", nullable: true),
                    EstimatedHouseNumber = table.Column<int>(type: "int", nullable: true),
                    ActualAlley = table.Column<int>(type: "int", nullable: true),
                    ActualHouseNumber = table.Column<int>(type: "int", nullable: true),
                    EstimatedDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchType = table.Column<int>(type: "int", nullable: true),
                    BranchCount = table.Column<int>(type: "int", nullable: false),
                    HasBranches = table.Column<bool>(type: "bit", nullable: false),
                    HasRoomButtons = table.Column<bool>(type: "bit", nullable: false),
                    IsRoomSuitable = table.Column<bool>(type: "bit", nullable: false),
                    CanAddPartitions = table.Column<bool>(type: "bit", nullable: false),
                    CommercialAccountName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FieldPersonName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FieldElectricCompanyCompanion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "TransactionRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriberName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionType = table.Column<int>(type: "int", nullable: false),
                    ListBuildingType = table.Column<int>(type: "int", nullable: false),
                    ActualBuildingType = table.Column<int>(type: "int", nullable: false),
                    BeneficiaryStatus = table.Column<int>(type: "int", nullable: false),
                    MeasurementNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActualMeasurementNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeasurementUnitActual = table.Column<int>(type: "int", nullable: true),
                    MeasurementUnitScan = table.Column<int>(type: "int", nullable: true),
                    ReadingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNotRealReadingNumber = table.Column<bool>(type: "bit", nullable: false),
                    MultiplicationFactor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeterStatus = table.Column<int>(type: "int", nullable: false),
                    AddressStatus = table.Column<int>(type: "int", nullable: false),
                    AnyAddress = table.Column<int>(type: "int", nullable: false),
                    NoMatchingList = table.Column<bool>(type: "bit", nullable: false),
                    NoMatchingMeasurement = table.Column<bool>(type: "bit", nullable: false),
                    HasDestructionOrDamage = table.Column<bool>(type: "bit", nullable: false),
                    IsSharedMeter = table.Column<bool>(type: "bit", nullable: false),
                    HasUnitsFromOtherSubscriptions = table.Column<bool>(type: "bit", nullable: false),
                    HasUnitsBeforeMeter = table.Column<bool>(type: "bit", nullable: false),
                    Other = table.Column<bool>(type: "bit", nullable: false),
                    BenefitingUnitsCount = table.Column<int>(type: "int", nullable: false),
                    OtherUnitsBeforeMeterCount = table.Column<int>(type: "int", nullable: false),
                    TransformerCapacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransformerSerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTransformerWorking = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NearestPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimatedAlley = table.Column<int>(type: "int", nullable: true),
                    EstimatedHouseNumber = table.Column<int>(type: "int", nullable: true),
                    ActualAlley = table.Column<int>(type: "int", nullable: true),
                    ActualHouseNumber = table.Column<int>(type: "int", nullable: true),
                    EstimatedDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchType = table.Column<int>(type: "int", nullable: true),
                    BranchCount = table.Column<int>(type: "int", nullable: false),
                    HasBranches = table.Column<bool>(type: "bit", nullable: false),
                    HasRoomButtons = table.Column<bool>(type: "bit", nullable: false),
                    IsRoomSuitable = table.Column<bool>(type: "bit", nullable: false),
                    CanAddPartitions = table.Column<bool>(type: "bit", nullable: false),
                    CommercialAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldPersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldElectricCompanyCompanion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionRecords_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeterScaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ViolationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransformerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_MeterScales_MeterScaleId",
                        column: x => x.MeterScaleId,
                        principalTable: "MeterScales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_SubscriptionViolations_ViolationId",
                        column: x => x.ViolationId,
                        principalTable: "SubscriptionViolations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Transformers_TransformerId",
                        column: x => x.TransformerId,
                        principalTable: "Transformers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacilityAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "SubscriptionBranches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchType = table.Column<int>(type: "int", nullable: true),
                    BranchCount = table.Column<int>(type: "int", nullable: false),
                    HasBranches = table.Column<bool>(type: "bit", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionBranches_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FileType = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionAttachments_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionAttachments_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionAttachments_UploadedFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "UploadedFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionBranches_TransactionId",
                table: "SubscriptionBranches",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionAttachments_CreatedById",
                table: "TransactionAttachments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionAttachments_FileId",
                table: "TransactionAttachments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionAttachments_TransactionId",
                table: "TransactionAttachments",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRecords_CreatedById",
                table: "TransactionRecords",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreatedById",
                table: "Transactions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_MeterScaleId",
                table: "Transactions",
                column: "MeterScaleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SubscriberId",
                table: "Transactions",
                column: "SubscriberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SubscriptionId",
                table: "Transactions",
                column: "SubscriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransformerId",
                table: "Transactions",
                column: "TransformerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ViolationId",
                table: "Transactions",
                column: "ViolationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FacilityAttachments");

            migrationBuilder.DropTable(
                name: "SubscriptionBranches");

            migrationBuilder.DropTable(
                name: "TransactionAttachments");

            migrationBuilder.DropTable(
                name: "TransactionRecords");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "FacilityRecords");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MeterScales");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "SubscriptionViolations");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Transformers");
        }
    }
}
