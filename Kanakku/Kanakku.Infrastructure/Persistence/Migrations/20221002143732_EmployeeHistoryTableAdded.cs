using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kanakku.Infrastructure.Persistence.Migrations
{
    public partial class EmployeeHistoryTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_work_instance_product_instances_product_instance_id",
                table: "product_work_instance");

            migrationBuilder.DropForeignKey(
                name: "fk_product_work_instance_works_work_id",
                table: "product_work_instance");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product_work_instance",
                table: "product_work_instance");

            migrationBuilder.RenameTable(
                name: "product_work_instance",
                newName: "product_work_instances");

            migrationBuilder.RenameIndex(
                name: "ix_product_work_instance_work_id",
                table: "product_work_instances",
                newName: "ix_product_work_instances_work_id");

            migrationBuilder.RenameIndex(
                name: "ix_product_work_instance_product_instance_id",
                table: "product_work_instances",
                newName: "ix_product_work_instances_product_instance_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_work_instances",
                table: "product_work_instances",
                column: "id");

            migrationBuilder.CreateTable(
                name: "employee_salary_histories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    emp_id = table.Column<Guid>(type: "uuid", nullable: false),
                    period = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    salary = table.Column<float>(type: "real", nullable: false),
                    days_present = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employee_salary_histories", x => x.id);
                    table.ForeignKey(
                        name: "fk_employee_salary_histories_employees_employee_id",
                        column: x => x.emp_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_employee_salary_histories_emp_id",
                table: "employee_salary_histories",
                column: "emp_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_work_instances_product_instances_product_instance_id",
                table: "product_work_instances",
                column: "product_instance_id",
                principalTable: "product_instances",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_product_work_instances_works_work_id",
                table: "product_work_instances",
                column: "work_id",
                principalTable: "works",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_work_instances_product_instances_product_instance_id",
                table: "product_work_instances");

            migrationBuilder.DropForeignKey(
                name: "fk_product_work_instances_works_work_id",
                table: "product_work_instances");

            migrationBuilder.DropTable(
                name: "employee_salary_histories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product_work_instances",
                table: "product_work_instances");

            migrationBuilder.RenameTable(
                name: "product_work_instances",
                newName: "product_work_instance");

            migrationBuilder.RenameIndex(
                name: "ix_product_work_instances_work_id",
                table: "product_work_instance",
                newName: "ix_product_work_instance_work_id");

            migrationBuilder.RenameIndex(
                name: "ix_product_work_instances_product_instance_id",
                table: "product_work_instance",
                newName: "ix_product_work_instance_product_instance_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_work_instance",
                table: "product_work_instance",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_work_instance_product_instances_product_instance_id",
                table: "product_work_instance",
                column: "product_instance_id",
                principalTable: "product_instances",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_product_work_instance_works_work_id",
                table: "product_work_instance",
                column: "work_id",
                principalTable: "works",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
