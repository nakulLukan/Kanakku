using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kanakku.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BinaryResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Extension = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FileFullName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinaryResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LookupMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InternalName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DependentLookupMasterId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LookupMasters_LookupMasters_DependentLookupMasterId",
                        column: x => x.DependentLookupMasterId,
                        principalTable: "LookupMasters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSizeMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    MasterName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizeMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ImageId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_BinaryResources_ImageId",
                        column: x => x.ImageId,
                        principalTable: "BinaryResources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortCode = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ImageId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_BinaryResources_ImageId",
                        column: x => x.ImageId,
                        principalTable: "BinaryResources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LookupDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LookupMasterId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DependentLookupDetailId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LookupDetails_LookupDetails_DependentLookupDetailId",
                        column: x => x.DependentLookupDetailId,
                        principalTable: "LookupDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LookupDetails_LookupMasters_LookupMasterId",
                        column: x => x.LookupMasterId,
                        principalTable: "LookupMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InternalName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Size = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    MasterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSizes_ProductSizeMaster_MasterId",
                        column: x => x.MasterId,
                        principalTable: "ProductSizeMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    IsPayPerHour = table.Column<bool>(type: "boolean", nullable: false),
                    ImageId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Works_BinaryResources_ImageId",
                        column: x => x.ImageId,
                        principalTable: "BinaryResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Works_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResignedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PhoneNumber1 = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber2 = table.Column<string>(type: "text", nullable: true),
                    AddressLineOne = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DistrictId = table.Column<int>(type: "integer", nullable: true),
                    StateId = table.Column<int>(type: "integer", nullable: true),
                    Pincode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    EpfRegNo = table.Column<string>(type: "text", nullable: true),
                    EsiRegNo = table.Column<string>(type: "text", nullable: true),
                    DpImageId = table.Column<int>(type: "integer", nullable: true),
                    IdProofImageId = table.Column<int>(type: "integer", nullable: true),
                    DesignationId = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_BinaryResources_DpImageId",
                        column: x => x.DpImageId,
                        principalTable: "BinaryResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_BinaryResources_IdProofImageId",
                        column: x => x.IdProofImageId,
                        principalTable: "BinaryResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Designations_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_LookupDetails_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "LookupDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_LookupDetails_StateId",
                        column: x => x.StateId,
                        principalTable: "LookupDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductSizeId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInstances_ProductSizes_ProductSizeId",
                        column: x => x.ProductSizeId,
                        principalTable: "ProductSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInstances_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkCostHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkId = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    IsInUse = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCostHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkCostHistories_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpId = table.Column<Guid>(type: "uuid", nullable: false),
                    Period = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Salary = table.Column<float>(type: "real", nullable: false),
                    DaysPresent = table.Column<int>(type: "integer", nullable: false),
                    Bonus = table.Column<float>(type: "real", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryHistories_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductWorkInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductInstanceId = table.Column<int>(type: "integer", nullable: false),
                    WorkId = table.Column<int>(type: "integer", nullable: false),
                    NetQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWorkInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductWorkInstances_ProductInstances_ProductInstanceId",
                        column: x => x.ProductInstanceId,
                        principalTable: "ProductInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductWorkInstances_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkId = table.Column<int>(type: "integer", nullable: false),
                    VariantId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    WorkDuration = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkHistories_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkHistories_ProductInstances_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkHistories_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Designations",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[] { 1, null, null, null, null, "Tailor" });

            migrationBuilder.InsertData(
                table: "LookupMasters",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DependentLookupMasterId", "InternalName", "IsActive", "ModifiedBy", "ModifiedOn" },
                values: new object[] { 1, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), null, "state", true, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "ProductSizeMaster",
                columns: new[] { "Id", "MasterName", "Order" },
                values: new object[] { 1, "General", 1 });

            migrationBuilder.InsertData(
                table: "LookupDetails",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DependentLookupDetailId", "IsActive", "LookupMasterId", "ModifiedBy", "ModifiedOn", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), null, true, 1, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Kerala" },
                    { 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), null, true, 1, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Tamil Nadu" }
                });

            migrationBuilder.InsertData(
                table: "LookupMasters",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DependentLookupMasterId", "InternalName", "IsActive", "ModifiedBy", "ModifiedOn" },
                values: new object[] { 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, "district", true, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "ProductSizes",
                columns: new[] { "Id", "InternalName", "MasterId", "Order", "Size" },
                values: new object[,]
                {
                    { 1, "small", 1, 1, "S" },
                    { 2, "medium", 1, 2, "M" },
                    { 3, "large", 1, 3, "L" }
                });

            migrationBuilder.InsertData(
                table: "LookupDetails",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DependentLookupDetailId", "IsActive", "LookupMasterId", "ModifiedBy", "ModifiedOn", "Value" },
                values: new object[,]
                {
                    { 3, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Palakkad" },
                    { 4, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Ernakulam" },
                    { 5, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Trissur" },
                    { 6, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Malappuram" },
                    { 7, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Kozhikode" },
                    { 8, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Alappuzha" },
                    { 9, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Idukki" },
                    { 10, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Kannur" },
                    { 11, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Kasaragod" },
                    { 12, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Alappuzha" },
                    { 13, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Kollam" },
                    { 14, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Kottayam" },
                    { 15, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Pathanamthitta" },
                    { 16, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Thiruvananthapuram" },
                    { 17, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Wayanad" },
                    { 18, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 2, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Coimbatore" },
                    { 19, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 2, true, 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Chennai" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_ImageId",
                table: "AppUsers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Designations_Name",
                table: "Designations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesignationId",
                table: "Employees",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DistrictId",
                table: "Employees",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DpImageId",
                table: "Employees",
                column: "DpImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdProofImageId",
                table: "Employees",
                column: "IdProofImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StateId",
                table: "Employees",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryHistories_EmpId",
                table: "EmployeeSalaryHistories",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupDetails_DependentLookupDetailId",
                table: "LookupDetails",
                column: "DependentLookupDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupDetails_LookupMasterId",
                table: "LookupDetails",
                column: "LookupMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupMasters_DependentLookupMasterId",
                table: "LookupMasters",
                column: "DependentLookupMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInstances_ProductId",
                table: "ProductInstances",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInstances_ProductSizeId",
                table: "ProductInstances",
                column: "ProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageId",
                table: "Products",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShortCode",
                table: "Products",
                column: "ShortCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_MasterId",
                table: "ProductSizes",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWorkInstances_ProductInstanceId",
                table: "ProductWorkInstances",
                column: "ProductInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWorkInstances_WorkId",
                table: "ProductWorkInstances",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCostHistories_WorkId",
                table: "WorkCostHistories",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHistories_EmployeeId",
                table: "WorkHistories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHistories_VariantId",
                table: "WorkHistories",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHistories_WorkId",
                table: "WorkHistories",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_ImageId",
                table: "Works",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_ProductId",
                table: "Works",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "EmployeeSalaryHistories");

            migrationBuilder.DropTable(
                name: "ProductWorkInstances");

            migrationBuilder.DropTable(
                name: "WorkCostHistories");

            migrationBuilder.DropTable(
                name: "WorkHistories");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ProductInstances");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "LookupDetails");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "LookupMasters");

            migrationBuilder.DropTable(
                name: "ProductSizeMaster");

            migrationBuilder.DropTable(
                name: "BinaryResources");
        }
    }
}
