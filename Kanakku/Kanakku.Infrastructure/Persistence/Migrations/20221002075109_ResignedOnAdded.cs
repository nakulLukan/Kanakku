using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kanakku.Infrastructure.Persistence.Migrations
{
    public partial class ResignedOnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "resigned_on",
                table: "employees",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "resigned_on",
                table: "employees");
        }
    }
}
