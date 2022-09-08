using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kanakku.Infrastructure.Persistence.Migrations
{
    public partial class WorkHistoryVariantColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "variant_id",
                table: "work_histories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_work_histories_variant_id",
                table: "work_histories",
                column: "variant_id");

            migrationBuilder.AddForeignKey(
                name: "fk_work_histories_product_instances_variant_id",
                table: "work_histories",
                column: "variant_id",
                principalTable: "product_instances",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_work_histories_product_instances_variant_id",
                table: "work_histories");

            migrationBuilder.DropIndex(
                name: "ix_work_histories_variant_id",
                table: "work_histories");

            migrationBuilder.DropColumn(
                name: "variant_id",
                table: "work_histories");
        }
    }
}
