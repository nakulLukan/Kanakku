using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kanakku.Infrastructure.Persistence.Migrations
{
    public partial class DesignationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "designation_id",
                table: "employees",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "designations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designations", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "designations",
                columns: new[] { "id", "created_by", "created_on", "modified_by", "modified_on", "name" },
                values: new object[] { 1, null, null, null, null, "Tailor" });

            migrationBuilder.CreateIndex(
                name: "ix_employees_designation_id",
                table: "employees",
                column: "designation_id");

            migrationBuilder.CreateIndex(
                name: "ix_designations_name",
                table: "designations",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_designations_designation_id",
                table: "employees",
                column: "designation_id",
                principalTable: "designations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_designations_designation_id",
                table: "employees");

            migrationBuilder.DropTable(
                name: "designations");

            migrationBuilder.DropIndex(
                name: "ix_employees_designation_id",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "designation_id",
                table: "employees");
        }
    }
}
