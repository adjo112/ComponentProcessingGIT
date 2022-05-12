﻿// <auto-generated />
using System;
using ComponentProcessing.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ComponentProcessing.Migrations
{
    [DbContext(typeof(CompContext))]
    [Migration("20220510052037_mig1")]
    partial class mig1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ComponentProcessing.Models.ProcReq", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ComponentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComponentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreditCardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPriorityRequest")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderPlacedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("UserName");

                    b.ToTable("ProcessRequest");
                });

            modelBuilder.Entity("ComponentProcessing.Models.ProcRes", b =>
                {
                    b.Property<int>("Id_res")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfDelivery")
                        .HasColumnType("datetime2");

                    b.Property<double>("PackagingAndDeliveryCharge")
                        .HasColumnType("float");

                    b.Property<double>("ProcessingCharge")
                        .HasColumnType("float");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<double>("TotalCharge")
                        .HasColumnType("float");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_res");

                    b.ToTable("ProcessResponse");
                });
#pragma warning restore 612, 618
        }
    }
}
