using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kanakku.Infrastructure.Persistence.Migrations
{
    public partial class ProductWorkInstanceTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "net_quantity",
                table: "product_instances");

            migrationBuilder.CreateTable(
                name: "product_work_instance",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_instance_id = table.Column<int>(type: "integer", nullable: false),
                    work_id = table.Column<int>(type: "integer", nullable: false),
                    net_quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_work_instance", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_work_instance_product_instances_product_instance_id",
                        column: x => x.product_instance_id,
                        principalTable: "product_instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_work_instance_works_work_id",
                        column: x => x.work_id,
                        principalTable: "works",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_work_instance_product_instance_id",
                table: "product_work_instance",
                column: "product_instance_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_work_instance_work_id",
                table: "product_work_instance",
                column: "work_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_work_instance");

            migrationBuilder.AddColumn<int>(
                name: "net_quantity",
                table: "product_instances",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
