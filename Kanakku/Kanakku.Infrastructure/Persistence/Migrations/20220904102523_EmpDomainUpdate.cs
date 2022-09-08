using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kanakku.Infrastructure.Persistence.Migrations
{
    public partial class EmpDomainUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_binary_resources_image_id",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "ix_employees_image_id",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "image_id",
                table: "employees");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "employees",
                newName: "phone_number1");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "employees",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "date_of_birth",
                table: "employees",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "dp_image_id",
                table: "employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "epf_reg_no",
                table: "employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "esi_reg_no",
                table: "employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "id_proof_image_id",
                table: "employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "phone_number2",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_employees_dp_image_id",
                table: "employees",
                column: "dp_image_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_id_proof_image_id",
                table: "employees",
                column: "id_proof_image_id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_binary_resources_display_picture_id",
                table: "employees",
                column: "dp_image_id",
                principalTable: "binary_resources",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_binary_resources_id_proof_id",
                table: "employees",
                column: "id_proof_image_id",
                principalTable: "binary_resources",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_binary_resources_display_picture_id",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_binary_resources_id_proof_id",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "ix_employees_dp_image_id",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "ix_employees_id_proof_image_id",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "code",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "dp_image_id",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "epf_reg_no",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "esi_reg_no",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "id_proof_image_id",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "phone_number2",
                table: "employees");

            migrationBuilder.RenameColumn(
                name: "phone_number1",
                table: "employees",
                newName: "phone_number");

            migrationBuilder.AddColumn<int>(
                name: "image_id",
                table: "employees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_employees_image_id",
                table: "employees",
                column: "image_id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_binary_resources_image_id",
                table: "employees",
                column: "image_id",
                principalTable: "binary_resources",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
