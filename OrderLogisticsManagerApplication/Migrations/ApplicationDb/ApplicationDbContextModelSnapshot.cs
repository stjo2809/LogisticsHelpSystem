﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderLogisticsManagerApplication.Models.Database.Application;

namespace OrderLogisticsManagerApplication.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.Component", b =>
                {
                    b.Property<int>("ComponentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("ComponentDepth")
                        .HasColumnType("real");

                    b.Property<float>("ComponentHeigth")
                        .HasColumnType("real");

                    b.Property<string>("ComponentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ComponentPartNumber")
                        .HasColumnType("int");

                    b.Property<float>("ComponentWeigth")
                        .HasColumnType("real");

                    b.Property<float>("ComponentWidth")
                        .HasColumnType("real");

                    b.HasKey("ComponentID");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.Delivery", b =>
                {
                    b.Property<int>("DeliveryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DeliveryAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("DeliveryID");

                    b.HasIndex("OrderID");

                    b.HasIndex("UserID");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.Log", b =>
                {
                    b.Property<int>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LogAction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogOn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LogTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("LogID");

                    b.HasIndex("UserID");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ComponentID")
                        .HasColumnType("int");

                    b.Property<int>("OrderAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrderEnteredByUserID")
                        .HasColumnType("int");

                    b.Property<int>("OrderFeedbackNumber")
                        .HasColumnType("int");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderStartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderID");

                    b.HasIndex("ComponentID");

                    b.HasIndex("OrderEnteredByUserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.PackingMaterial", b =>
                {
                    b.Property<int>("MaterialID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("HasDimension")
                        .HasColumnType("bit");

                    b.Property<float>("MaterialDepth")
                        .HasColumnType("real");

                    b.Property<float>("MaterialHeigth")
                        .HasColumnType("real");

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaterialPartNumber")
                        .HasColumnType("int");

                    b.Property<float>("MaterialWeigth")
                        .HasColumnType("real");

                    b.Property<float>("MaterialWidth")
                        .HasColumnType("real");

                    b.HasKey("MaterialID");

                    b.ToTable("PackingMaterials");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.PackingMaterialUsedOnOrder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("MaterialID")
                        .HasColumnType("int");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MaterialID");

                    b.HasIndex("OrderID");

                    b.ToTable("PackingMaterialUsedOnOrders");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.Pickup", b =>
                {
                    b.Property<int>("PickupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("PickupTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("PickupID");

                    b.HasIndex("UserID");

                    b.ToTable("Pickups");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.PickupRequest", b =>
                {
                    b.Property<int>("PickupRequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int?>("PickupID")
                        .HasColumnType("int");

                    b.Property<int>("PickupRequestAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("PickupRequestTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("PickupRequestID");

                    b.HasIndex("OrderID");

                    b.HasIndex("PickupID");

                    b.HasIndex("UserID");

                    b.ToTable("PickupRequests");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserGUID")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.Delivery", b =>
                {
                    b.HasOne("OrderLogisticsManagerApplication.Models.Database.Application.Order", "Order")
                        .WithMany("Delivered")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderLogisticsManagerApplication.Models.Database.Application.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.Log", b =>
                {
                    b.HasOne("OrderLogisticsManagerApplication.Models.Database.Application.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.Order", b =>
                {
                    b.HasOne("OrderLogisticsManagerApplication.Models.Database.Application.Component", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentID");

                    b.HasOne("OrderLogisticsManagerApplication.Models.Database.Application.User", "OrderEnteredBy")
                        .WithMany()
                        .HasForeignKey("OrderEnteredByUserID");

                    b.Navigation("Component");

                    b.Navigation("OrderEnteredBy");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.PackingMaterialUsedOnOrder", b =>
                {
                    b.HasOne("OrderLogisticsManagerApplication.Models.Database.Application.PackingMaterial", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialID");

                    b.HasOne("OrderLogisticsManagerApplication.Models.Database.Application.Order", "Order")
                        .WithMany("PackingMaterialUsed")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.Pickup", b =>
                {
                    b.HasOne("OrderLogisticsManagerApplication.Models.Database.Application.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.PickupRequest", b =>
                {
                    b.HasOne("OrderLogisticsManagerApplication.Models.Database.Application.Order", "Order")
                        .WithMany("PickupRequested")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderLogisticsManagerApplication.Models.Database.Application.Pickup", "Pickup")
                        .WithMany()
                        .HasForeignKey("PickupID");

                    b.HasOne("OrderLogisticsManagerApplication.Models.Database.Application.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("Order");

                    b.Navigation("Pickup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderLogisticsManagerApplication.Models.Database.Application.Order", b =>
                {
                    b.Navigation("Delivered");

                    b.Navigation("PackingMaterialUsed");

                    b.Navigation("PickupRequested");
                });
#pragma warning restore 612, 618
        }
    }
}