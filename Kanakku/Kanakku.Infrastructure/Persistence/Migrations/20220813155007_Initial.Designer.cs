﻿// <auto-generated />
using System;
using Kanakku.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kanakku.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220813155007_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kanakku.Domain.Attachment.BinaryResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("data");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on");

                    b.HasKey("Id")
                        .HasName("pk_binary_resources");

                    b.ToTable("binary_resources", (string)null);
                });

            modelBuilder.Entity("Kanakku.Domain.Inventory.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<int?>("ImageId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("image_id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.HasIndex("ImageId")
                        .HasDatabaseName("ix_products_image_id");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("Kanakku.Domain.Inventory.Work", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("Cost")
                        .HasColumnType("real")
                        .HasColumnName("cost");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<int?>("ImageId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("image_id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsPayPerHour")
                        .HasColumnType("boolean")
                        .HasColumnName("is_pay_per_hour");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.HasKey("Id")
                        .HasName("pk_works");

                    b.HasIndex("ImageId")
                        .HasDatabaseName("ix_works_image_id");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_works_product_id");

                    b.ToTable("works", (string)null);
                });

            modelBuilder.Entity("Kanakku.Domain.Inventory.WorkCostHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("Cost")
                        .HasColumnType("real")
                        .HasColumnName("cost");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<bool>("IsInUse")
                        .HasColumnType("boolean")
                        .HasColumnName("is_in_use");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on");

                    b.Property<int>("WorkId")
                        .HasColumnType("integer")
                        .HasColumnName("work_id");

                    b.HasKey("Id")
                        .HasName("pk_work_cost_histories");

                    b.HasIndex("WorkId")
                        .HasDatabaseName("ix_work_cost_histories_work_id");

                    b.ToTable("work_cost_histories", (string)null);
                });

            modelBuilder.Entity("Kanakku.Domain.Inventory.WorkHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid")
                        .HasColumnName("employee_id");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on");

                    b.Property<int>("WorkDuration")
                        .HasColumnType("integer")
                        .HasColumnName("work_duration");

                    b.Property<int>("WorkId")
                        .HasColumnType("integer")
                        .HasColumnName("work_id");

                    b.Property<DateTime>("WorkedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("worked_on");

                    b.HasKey("Id")
                        .HasName("pk_work_histories");

                    b.HasIndex("EmployeeId")
                        .HasDatabaseName("ix_work_histories_employee_id");

                    b.HasIndex("WorkId")
                        .HasDatabaseName("ix_work_histories_work_id");

                    b.ToTable("work_histories", (string)null);
                });

            modelBuilder.Entity("Kanakku.Domain.Lookup.LookupDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<int>("LookupMasterId")
                        .HasColumnType("integer")
                        .HasColumnName("lookup_master_id");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_lookup_details");

                    b.HasIndex("LookupMasterId")
                        .HasDatabaseName("ix_lookup_details_lookup_master_id");

                    b.ToTable("lookup_details", (string)null);
                });

            modelBuilder.Entity("Kanakku.Domain.Lookup.LookupMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("InternalName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("internal_name");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on");

                    b.HasKey("Id")
                        .HasName("pk_lookup_masters");

                    b.ToTable("lookup_masters", (string)null);
                });

            modelBuilder.Entity("Kanakku.Domain.User.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<int>("ImageId")
                        .HasColumnType("integer")
                        .HasColumnName("image_id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_app_users");

                    b.HasIndex("ImageId")
                        .HasDatabaseName("ix_app_users_image_id");

                    b.ToTable("app_users", (string)null);
                });

            modelBuilder.Entity("Kanakku.Domain.User.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AddressLineOne")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("address_line_one");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<int>("DistrictId")
                        .HasColumnType("integer")
                        .HasColumnName("district_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<int?>("ImageId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("image_id");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("Pincode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("pincode");

                    b.Property<int>("StateId")
                        .HasColumnType("integer")
                        .HasColumnName("state_id");

                    b.HasKey("Id")
                        .HasName("pk_employees");

                    b.HasIndex("DistrictId")
                        .HasDatabaseName("ix_employees_district_id");

                    b.HasIndex("ImageId")
                        .HasDatabaseName("ix_employees_image_id");

                    b.HasIndex("StateId")
                        .HasDatabaseName("ix_employees_state_id");

                    b.ToTable("employees", (string)null);
                });

            modelBuilder.Entity("Kanakku.Domain.Inventory.Product", b =>
                {
                    b.HasOne("Kanakku.Domain.Attachment.BinaryResource", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_products_binary_resources_image_id");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Kanakku.Domain.Inventory.Work", b =>
                {
                    b.HasOne("Kanakku.Domain.Attachment.BinaryResource", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_works_binary_resources_image_id");

                    b.HasOne("Kanakku.Domain.Inventory.Product", "Product")
                        .WithMany("Works")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_works_products_product_id");

                    b.Navigation("Image");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Kanakku.Domain.Inventory.WorkCostHistory", b =>
                {
                    b.HasOne("Kanakku.Domain.Inventory.Work", "Work")
                        .WithMany("WorkCostHistories")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_work_cost_histories_works_work_id");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("Kanakku.Domain.Inventory.WorkHistory", b =>
                {
                    b.HasOne("Kanakku.Domain.User.Employee", "Employee")
                        .WithMany("WorkHistories")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_work_histories_employees_employee_id");

                    b.HasOne("Kanakku.Domain.Inventory.Work", "Work")
                        .WithMany("WorkHistories")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_work_histories_works_work_id");

                    b.Navigation("Employee");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("Kanakku.Domain.Lookup.LookupDetail", b =>
                {
                    b.HasOne("Kanakku.Domain.Lookup.LookupMaster", "LookupMaster")
                        .WithMany("LookupDetails")
                        .HasForeignKey("LookupMasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_lookup_details_lookup_masters_lookup_master_id");

                    b.Navigation("LookupMaster");
                });

            modelBuilder.Entity("Kanakku.Domain.User.AppUser", b =>
                {
                    b.HasOne("Kanakku.Domain.Attachment.BinaryResource", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_app_users_binary_resources_image_id");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Kanakku.Domain.User.Employee", b =>
                {
                    b.HasOne("Kanakku.Domain.Lookup.LookupDetail", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_employees_lookup_details_district_id");

                    b.HasOne("Kanakku.Domain.Attachment.BinaryResource", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_employees_binary_resources_image_id");

                    b.HasOne("Kanakku.Domain.Lookup.LookupDetail", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_employees_lookup_details_state_id");

                    b.Navigation("District");

                    b.Navigation("Image");

                    b.Navigation("State");
                });

            modelBuilder.Entity("Kanakku.Domain.Inventory.Product", b =>
                {
                    b.Navigation("Works");
                });

            modelBuilder.Entity("Kanakku.Domain.Inventory.Work", b =>
                {
                    b.Navigation("WorkCostHistories");

                    b.Navigation("WorkHistories");
                });

            modelBuilder.Entity("Kanakku.Domain.Lookup.LookupMaster", b =>
                {
                    b.Navigation("LookupDetails");
                });

            modelBuilder.Entity("Kanakku.Domain.User.Employee", b =>
                {
                    b.Navigation("WorkHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
