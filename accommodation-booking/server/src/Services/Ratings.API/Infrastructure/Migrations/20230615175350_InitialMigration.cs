using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ratings.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccommodationRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GuestId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccommodationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Grade = table.Column<int>(type: "integer", nullable: false),
                    DateTimeOfGrade = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationRatings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HostRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GuestId = table.Column<Guid>(type: "uuid", nullable: false),
                    HostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Grade = table.Column<int>(type: "integer", nullable: false),
                    DateTimeOfGrade = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostRatings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProminentHosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HostId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsGradeAcceptable = table.Column<bool>(type: "boolean", nullable: false),
                    IsCancellationRateAcceptable = table.Column<bool>(type: "boolean", nullable: false),
                    HasFiveReservations = table.Column<bool>(type: "boolean", nullable: false),
                    IsDurationOfReservationsAcceptable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProminentHosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GuestId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccommodationId = table.Column<Guid>(type: "uuid", nullable: false),
                    HostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Canceled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "End", "Start", "AccommodationId", "Canceled", "GuestId", "HostId" },
                values: new object[,]
                {
                    { new Guid("d27b4d27-cd63-48fa-b2a6-e9f37c5d546a"), new DateTime(2023, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("63ce63e8-33bb-4be7-afd7-fdd7d273c756"), false, new Guid("e8867cd0-71f5-43af-93af-c5f316ed200d"), new Guid("268f65b3-7dbc-4cdb-85e2-5ab421f0763c") },
                    { new Guid("eddf846c-b1b0-4bc8-9c50-e7e544a918fd"), new DateTime(2023, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2e0e5d5e-9dd3-40f0-a211-501150818478"), false, new Guid("75cbe6a5-e567-4c9f-ab50-adfb974d275c"), new Guid("a230742a-372c-446f-9984-1e74e8b39db6") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccommodationRatings");

            migrationBuilder.DropTable(
                name: "HostRatings");

            migrationBuilder.DropTable(
                name: "ProminentHosts");

            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
