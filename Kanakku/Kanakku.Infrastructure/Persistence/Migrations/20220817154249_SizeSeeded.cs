using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kanakku.Infrastructure.Persistence.Migrations
{
    public partial class SizeSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "product_sizes",
                columns: new[] { "id", "internal_name", "order", "size" },
                values: new object[,]
                {
                    { 1, "small", 1, "S" },
                    { 2, "medium", 2, "M" },
                    { 3, "large", 3, "L" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_products_short_code",
                table: "products",
                column: "short_code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_products_short_code",
                table: "products");

            migrationBuilder.DeleteData(
                table: "product_sizes",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "product_sizes",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "product_sizes",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
