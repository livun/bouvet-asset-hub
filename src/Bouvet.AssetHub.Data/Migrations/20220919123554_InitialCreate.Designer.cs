﻿// <auto-generated />
using System;
using Bouvet.AssetHub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bouvet.AssetHub.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220919123554_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Bouvet.AssetHub.Data.Models.AssetEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("QrIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("SerialNumber")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("Bouvet.AssetHub.Data.Models.Bsd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bsd");
                });

            modelBuilder.Entity("Bouvet.AssetHub.Data.Models.EmployeeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EmployeeNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Bouvet.AssetHub.Data.Models.LoanEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<int>("AssignedToId")
                        .HasColumnType("int");

                    b.Property<int?>("BsdReferenceId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("AssignedToId");

                    b.HasIndex("BsdReferenceId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("Bouvet.AssetHub.Data.Models.LoanEntity", b =>
                {
                    b.HasOne("Bouvet.AssetHub.Data.Models.AssetEntity", "Asset")
                        .WithMany()
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bouvet.AssetHub.Data.Models.EmployeeEntity", "AssignedTo")
                        .WithMany()
                        .HasForeignKey("AssignedToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bouvet.AssetHub.Data.Models.Bsd", "BsdReference")
                        .WithMany()
                        .HasForeignKey("BsdReferenceId");

                    b.Navigation("Asset");

                    b.Navigation("AssignedTo");

                    b.Navigation("BsdReference");
                });
#pragma warning restore 612, 618
        }
    }
}
