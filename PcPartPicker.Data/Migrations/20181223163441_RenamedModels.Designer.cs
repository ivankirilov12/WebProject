﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PcPartPicker.Data;

namespace PcPartPicker.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181223163441_RenamedModels")]
    partial class RenamedModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PcPartPicker.Data.Models.Case", b =>
                {
                    b.Property<int>("CaseId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Manufacturer");

                    b.Property<string>("Model");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 5)");

                    b.Property<string>("Type");

                    b.HasKey("CaseId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("PcPartPicker.Data.Models.Cpu", b =>
                {
                    b.Property<int>("CpuId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CacheMemory");

                    b.Property<string>("Manufacturer");

                    b.Property<string>("Model");

                    b.Property<int>("NumberOfCores");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 5)");

                    b.Property<string>("Socket");

                    b.HasKey("CpuId");

                    b.ToTable("Cpus");
                });

            modelBuilder.Entity("PcPartPicker.Data.Models.Gpu", b =>
                {
                    b.Property<int>("GpuId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Manufacturer");

                    b.Property<string>("Memory");

                    b.Property<string>("Model");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 5)");

                    b.HasKey("GpuId");

                    b.ToTable("Gpus");
                });

            modelBuilder.Entity("PcPartPicker.Data.Models.Motherboard", b =>
                {
                    b.Property<int>("MotherboardId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CpuSocket");

                    b.Property<string>("Manufacturer");

                    b.Property<string>("Model");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 5)");

                    b.HasKey("MotherboardId");

                    b.ToTable("Motherboards");
                });

            modelBuilder.Entity("PcPartPicker.Data.Models.Ram", b =>
                {
                    b.Property<int>("RamId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Manufacturer");

                    b.Property<int>("MemoryCapacity");

                    b.Property<float>("MemoryFrequency");

                    b.Property<string>("MemoryType");

                    b.Property<string>("Model");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 5)");

                    b.HasKey("RamId");

                    b.ToTable("Rams");
                });

            modelBuilder.Entity("PcPartPicker.Data.Models.Storage", b =>
                {
                    b.Property<int>("StorageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Capacity");

                    b.Property<string>("Manufacturer");

                    b.Property<string>("Model");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 5)");

                    b.Property<string>("Type");

                    b.HasKey("StorageId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("PcPartPicker.Data.Models.SystemBuild", b =>
                {
                    b.Property<int>("SystemBuildId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CaseId");

                    b.Property<int?>("CpuId");

                    b.Property<int?>("GpuId");

                    b.Property<int?>("MotherboardId");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 5)");

                    b.Property<int?>("RamId");

                    b.Property<int?>("StorageId");

                    b.HasKey("SystemBuildId");

                    b.HasIndex("CaseId");

                    b.HasIndex("CpuId");

                    b.HasIndex("GpuId");

                    b.HasIndex("MotherboardId");

                    b.HasIndex("RamId");

                    b.HasIndex("StorageId");

                    b.ToTable("SystemBuilds");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PcPartPicker.Data.Models.SystemBuild", b =>
                {
                    b.HasOne("PcPartPicker.Data.Models.Case", "Case")
                        .WithMany("SystemBuilds")
                        .HasForeignKey("CaseId");

                    b.HasOne("PcPartPicker.Data.Models.Cpu", "Cpu")
                        .WithMany("SystemBuilds")
                        .HasForeignKey("CpuId");

                    b.HasOne("PcPartPicker.Data.Models.Gpu", "Gpu")
                        .WithMany("SystemBuilds")
                        .HasForeignKey("GpuId");

                    b.HasOne("PcPartPicker.Data.Models.Motherboard", "Motherboard")
                        .WithMany("SystemBuilds")
                        .HasForeignKey("MotherboardId");

                    b.HasOne("PcPartPicker.Data.Models.Ram", "Ram")
                        .WithMany("SystemBuilds")
                        .HasForeignKey("RamId");

                    b.HasOne("PcPartPicker.Data.Models.Storage", "Storage")
                        .WithMany("SystemBuilds")
                        .HasForeignKey("StorageId");
                });
#pragma warning restore 612, 618
        }
    }
}