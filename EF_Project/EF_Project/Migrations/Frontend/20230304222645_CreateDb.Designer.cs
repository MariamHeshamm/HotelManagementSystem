﻿// <auto-generated />
using System;
using EF_Project.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EF_Project.Migrations.Frontend
{
    [DbContext(typeof(FrontendContext))]
    [Migration("20230304222645_CreateDb")]
    partial class CreateDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EF_Project.Entities.CardType", b =>
                {
                    b.Property<int>("CardTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CardTypeId"));

                    b.Property<int>("CardCVV")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<string>("CardTypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CardTypeId");

                    b.ToTable("CardTypes");
                });

            modelBuilder.Entity("EF_Project.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<int>("ApartementSuite")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("StatesId")
                        .HasColumnType("int");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("CustomerId");

                    b.HasIndex("StatesId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("EF_Project.Entities.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodId"));

                    b.Property<string>("FoodName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("FoodId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("EF_Project.Entities.FoodReservation", b =>
                {
                    b.Property<int>("FoosReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoosReservationId"));

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("FoosReservationId");

                    b.HasIndex("FoodId");

                    b.HasIndex("ReservationId");

                    b.ToTable("FoodReservations");
                });

            modelBuilder.Entity("EF_Project.Entities.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<DateTime>("CardExpDate")
                        .HasColumnType("date");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CardTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("CheckIn")
                        .HasColumnType("bit");

                    b.Property<bool>("Credit")
                        .HasColumnType("bit");

                    b.Property<bool>("Debit")
                        .HasColumnType("bit");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("CardTypeId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("EF_Project.Entities.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("date");

                    b.Property<bool>("Cleaning")
                        .HasColumnType("bit");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LeavingTime")
                        .HasColumnType("date");

                    b.Property<int>("Number_of_Guests")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<bool>("SweetestSurprise")
                        .HasColumnType("bit");

                    b.Property<bool>("Towels")
                        .HasColumnType("bit");

                    b.HasKey("ReservationId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("EF_Project.Entities.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"));

                    b.Property<bool>("Availability")
                        .HasColumnType("bit");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("RoomFloorId")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<int>("RoomTypeId")
                        .HasColumnType("int");

                    b.HasKey("RoomId");

                    b.HasIndex("RoomFloorId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("EF_Project.Entities.RoomFloor", b =>
                {
                    b.Property<int>("RoomFloorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomFloorId"));

                    b.Property<int>("Floor_Number")
                        .HasColumnType("int");

                    b.HasKey("RoomFloorId");

                    b.ToTable("RoomFloors");
                });

            modelBuilder.Entity("EF_Project.Entities.RoomType", b =>
                {
                    b.Property<int>("RoomTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomTypeId"));

                    b.Property<string>("RoomTypename")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("RoomTypeId");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("EF_Project.Entities.State", b =>
                {
                    b.Property<int>("StatesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatesId"));

                    b.Property<string>("StatesName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StatesId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("EF_Project.Entities.Customer", b =>
                {
                    b.HasOne("EF_Project.Entities.State", "State")
                        .WithMany("Customers")
                        .HasForeignKey("StatesId");

                    b.Navigation("State");
                });

            modelBuilder.Entity("EF_Project.Entities.FoodReservation", b =>
                {
                    b.HasOne("EF_Project.Entities.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_Project.Entities.Reservation", "Reservation")
                        .WithMany("FoodReservations")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("EF_Project.Entities.Payment", b =>
                {
                    b.HasOne("EF_Project.Entities.CardType", "CardType")
                        .WithMany("Payments")
                        .HasForeignKey("CardTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_Project.Entities.Reservation", "Reservation")
                        .WithMany("Payments")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardType");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("EF_Project.Entities.Reservation", b =>
                {
                    b.HasOne("EF_Project.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_Project.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("EF_Project.Entities.Room", b =>
                {
                    b.HasOne("EF_Project.Entities.RoomFloor", "RoomFloor")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomFloorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_Project.Entities.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoomFloor");

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("EF_Project.Entities.CardType", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("EF_Project.Entities.Reservation", b =>
                {
                    b.Navigation("FoodReservations");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("EF_Project.Entities.RoomFloor", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("EF_Project.Entities.RoomType", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("EF_Project.Entities.State", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
