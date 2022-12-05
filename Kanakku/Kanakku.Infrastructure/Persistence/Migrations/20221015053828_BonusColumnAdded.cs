using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kanakku.Infrastructure.Persistence.Migrations
{
    public partial class BonusColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "bonus",
                table: "employee_salary_histories",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bonus",
                table: "employee_salary_histories");
        }
    }
}
