﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Ratings.API.Infrastructure.Persistence;

#nullable disable

namespace Ratings.API.Infrastructure.Migrations
{
    [DbContext(typeof(RatingsDbContext))]
    [Migration("20230615175350_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RatingsLibrary.Models.AccommodationRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccommodationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTimeOfGrade")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Grade")
                        .HasColumnType("integer");

                    b.Property<Guid>("GuestId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("AccommodationRatings");
                });

            modelBuilder.Entity("RatingsLibrary.Models.HostRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTimeOfGrade")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Grade")
                        .HasColumnType("integer");

                    b.Property<Guid>("GuestId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("HostId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("HostRatings");
                });

            modelBuilder.Entity("RatingsLibrary.Models.ProminentHost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("HasFiveReservations")
                        .HasColumnType("boolean");

                    b.Property<Guid>("HostId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsCancellationRateAcceptable")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDurationOfReservationsAcceptable")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsGradeAcceptable")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("ProminentHosts");
                });

            modelBuilder.Entity("RatingsLibrary.Models.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccommodationId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Canceled")
                        .HasColumnType("boolean");

                    b.Property<Guid>("GuestId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("HostId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Reservations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d27b4d27-cd63-48fa-b2a6-e9f37c5d546a"),
                            AccommodationId = new Guid("63ce63e8-33bb-4be7-afd7-fdd7d273c756"),
                            Canceled = false,
                            GuestId = new Guid("e8867cd0-71f5-43af-93af-c5f316ed200d"),
                            HostId = new Guid("268f65b3-7dbc-4cdb-85e2-5ab421f0763c")
                        },
                        new
                        {
                            Id = new Guid("eddf846c-b1b0-4bc8-9c50-e7e544a918fd"),
                            AccommodationId = new Guid("2e0e5d5e-9dd3-40f0-a211-501150818478"),
                            Canceled = false,
                            GuestId = new Guid("75cbe6a5-e567-4c9f-ab50-adfb974d275c"),
                            HostId = new Guid("a230742a-372c-446f-9984-1e74e8b39db6")
                        });
                });

            modelBuilder.Entity("RatingsLibrary.Models.Reservation", b =>
                {
                    b.OwnsOne("Ratings.Utils.DateRange", "Period", b1 =>
                        {
                            b1.Property<Guid>("ReservationId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("End")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("End");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("Start");

                            b1.HasKey("ReservationId");

                            b1.ToTable("Reservations");

                            b1.WithOwner()
                                .HasForeignKey("ReservationId");

                            b1.HasData(
                                new
                                {
                                    ReservationId = new Guid("d27b4d27-cd63-48fa-b2a6-e9f37c5d546a"),
                                    End = new DateTime(2023, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                                    Start = new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified)
                                },
                                new
                                {
                                    ReservationId = new Guid("eddf846c-b1b0-4bc8-9c50-e7e544a918fd"),
                                    End = new DateTime(2023, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                                    Start = new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified)
                                });
                        });

                    b.Navigation("Period")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}