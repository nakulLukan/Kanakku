using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kanakku.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "binary_resources",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    extension = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    file_full_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    data = table.Column<byte[]>(type: "bytea", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_binary_resources", x => x.id);
                });

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

            migrationBuilder.CreateTable(
                name: "lookup_masters",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    internal_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    dependent_lookup_master_id = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lookup_masters", x => x.id);
                    table.ForeignKey(
                        name: "fk_lookup_masters_lookup_masters_dependent_lookup_master_id",
                        column: x => x.dependent_lookup_master_id,
                        principalTable: "lookup_masters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_size_master",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order = table.Column<int>(type: "integer", nullable: false),
                    master_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_size_master", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    image_id = table.Column<int>(type: "integer", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_users_binary_resources_image_id",
                        column: x => x.image_id,
                        principalTable: "binary_resources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    short_code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    image_id = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_binary_resources_image_id",
                        column: x => x.image_id,
                        principalTable: "binary_resources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lookup_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lookup_master_id = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    dependent_lookup_detail_id = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lookup_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_lookup_details_lookup_details_dependent_lookup_detail_id",
                        column: x => x.dependent_lookup_detail_id,
                        principalTable: "lookup_details",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_lookup_details_lookup_masters_lookup_master_id",
                        column: x => x.lookup_master_id,
                        principalTable: "lookup_masters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_sizes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    internal_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false),
                    size = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    master_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_sizes", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_sizes_product_size_master_master_id",
                        column: x => x.master_id,
                        principalTable: "product_size_master",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    provider_key = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "works",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    cost = table.Column<float>(type: "real", nullable: false),
                    is_pay_per_hour = table.Column<bool>(type: "boolean", nullable: false),
                    image_id = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_works", x => x.id);
                    table.ForeignKey(
                        name: "fk_works_binary_resources_image_id",
                        column: x => x.image_id,
                        principalTable: "binary_resources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_works_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_of_joining = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    resigned_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    phone_number1 = table.Column<string>(type: "text", nullable: false),
                    phone_number2 = table.Column<string>(type: "text", nullable: true),
                    address_line_one = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    district_id = table.Column<int>(type: "integer", nullable: true),
                    state_id = table.Column<int>(type: "integer", nullable: true),
                    pincode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    epf_reg_no = table.Column<string>(type: "text", nullable: true),
                    esi_reg_no = table.Column<string>(type: "text", nullable: true),
                    dp_image_id = table.Column<int>(type: "integer", nullable: true),
                    id_proof_image_id = table.Column<int>(type: "integer", nullable: true),
                    designation_id = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.id);
                    table.ForeignKey(
                        name: "fk_employees_binary_resources_display_picture_id",
                        column: x => x.dp_image_id,
                        principalTable: "binary_resources",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_employees_binary_resources_id_proof_id",
                        column: x => x.id_proof_image_id,
                        principalTable: "binary_resources",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_employees_designations_designation_id",
                        column: x => x.designation_id,
                        principalTable: "designations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_employees_lookup_details_district_id",
                        column: x => x.district_id,
                        principalTable: "lookup_details",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_employees_lookup_details_state_id",
                        column: x => x.state_id,
                        principalTable: "lookup_details",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_instances",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_size_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_instances", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_instances_product_sizes_product_size_id",
                        column: x => x.product_size_id,
                        principalTable: "product_sizes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_instances_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_cost_histories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    work_id = table.Column<int>(type: "integer", nullable: false),
                    cost = table.Column<float>(type: "real", nullable: false),
                    is_in_use = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_work_cost_histories", x => x.id);
                    table.ForeignKey(
                        name: "fk_work_cost_histories_works_work_id",
                        column: x => x.work_id,
                        principalTable: "works",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    bonus = table.Column<float>(type: "real", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "product_work_instances",
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
                    table.PrimaryKey("pk_product_work_instances", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_work_instances_product_instances_product_instance_id",
                        column: x => x.product_instance_id,
                        principalTable: "product_instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_work_instances_works_work_id",
                        column: x => x.work_id,
                        principalTable: "works",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_histories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    work_id = table.Column<int>(type: "integer", nullable: false),
                    variant_id = table.Column<int>(type: "integer", nullable: false),
                    employee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    worked_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    work_duration = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_work_histories", x => x.id);
                    table.ForeignKey(
                        name: "fk_work_histories_employees_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_work_histories_product_instances_variant_id",
                        column: x => x.variant_id,
                        principalTable: "product_instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_work_histories_works_work_id",
                        column: x => x.work_id,
                        principalTable: "works",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "designations",
                columns: new[] { "id", "created_by", "created_on", "modified_by", "modified_on", "name" },
                values: new object[] { 1, null, null, null, null, "Tailor" });

            migrationBuilder.InsertData(
                table: "lookup_masters",
                columns: new[] { "id", "created_by", "created_on", "dependent_lookup_master_id", "internal_name", "is_active", "modified_by", "modified_on" },
                values: new object[] { 1, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), null, "state", true, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "product_size_master",
                columns: new[] { "id", "master_name", "order" },
                values: new object[] { 1, "General", 1 });

            migrationBuilder.InsertData(
                table: "lookup_details",
                columns: new[] { "id", "created_by", "created_on", "dependent_lookup_detail_id", "is_active", "lookup_master_id", "modified_by", "modified_on", "value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), null, true, 1, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Kerala" },
                    { 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), null, true, 1, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Tamil Nadu" }
                });

            migrationBuilder.InsertData(
                table: "lookup_masters",
                columns: new[] { "id", "created_by", "created_on", "dependent_lookup_master_id", "internal_name", "is_active", "modified_by", "modified_on" },
                values: new object[] { 2, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1, "district", true, null, new DateTime(2022, 8, 14, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "product_sizes",
                columns: new[] { "id", "internal_name", "master_id", "order", "size" },
                values: new object[,]
                {
                    { 1, "small", 1, 1, "S" },
                    { 2, "medium", 1, 2, "M" },
                    { 3, "large", 1, 3, "L" }
                });

            migrationBuilder.InsertData(
                table: "lookup_details",
                columns: new[] { "id", "created_by", "created_on", "dependent_lookup_detail_id", "is_active", "lookup_master_id", "modified_by", "modified_on", "value" },
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
                name: "ix_asp_net_role_claims_role_id",
                table: "AspNetRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "AspNetUserRoles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_users_image_id",
                table: "AspNetUsers",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_designations_name",
                table: "designations",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_employee_salary_histories_emp_id",
                table: "employee_salary_histories",
                column: "emp_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_designation_id",
                table: "employees",
                column: "designation_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_district_id",
                table: "employees",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_dp_image_id",
                table: "employees",
                column: "dp_image_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_id_proof_image_id",
                table: "employees",
                column: "id_proof_image_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_state_id",
                table: "employees",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "ix_lookup_details_dependent_lookup_detail_id",
                table: "lookup_details",
                column: "dependent_lookup_detail_id");

            migrationBuilder.CreateIndex(
                name: "ix_lookup_details_lookup_master_id",
                table: "lookup_details",
                column: "lookup_master_id");

            migrationBuilder.CreateIndex(
                name: "ix_lookup_masters_dependent_lookup_master_id",
                table: "lookup_masters",
                column: "dependent_lookup_master_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_instances_product_id",
                table: "product_instances",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_instances_product_size_id",
                table: "product_instances",
                column: "product_size_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_sizes_master_id",
                table: "product_sizes",
                column: "master_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_work_instances_product_instance_id",
                table: "product_work_instances",
                column: "product_instance_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_work_instances_work_id",
                table: "product_work_instances",
                column: "work_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_image_id",
                table: "products",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_short_code",
                table: "products",
                column: "short_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_work_cost_histories_work_id",
                table: "work_cost_histories",
                column: "work_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_histories_employee_id",
                table: "work_histories",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_histories_variant_id",
                table: "work_histories",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_histories_work_id",
                table: "work_histories",
                column: "work_id");

            migrationBuilder.CreateIndex(
                name: "ix_works_image_id",
                table: "works",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "ix_works_product_id",
                table: "works",
                column: "product_id");
        }

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
                name: "employee_salary_histories");

            migrationBuilder.DropTable(
                name: "product_work_instances");

            migrationBuilder.DropTable(
                name: "work_cost_histories");

            migrationBuilder.DropTable(
                name: "work_histories");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "product_instances");

            migrationBuilder.DropTable(
                name: "works");

            migrationBuilder.DropTable(
                name: "designations");

            migrationBuilder.DropTable(
                name: "lookup_details");

            migrationBuilder.DropTable(
                name: "product_sizes");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "lookup_masters");

            migrationBuilder.DropTable(
                name: "product_size_master");

            migrationBuilder.DropTable(
                name: "binary_resources");
        }
    }
}
