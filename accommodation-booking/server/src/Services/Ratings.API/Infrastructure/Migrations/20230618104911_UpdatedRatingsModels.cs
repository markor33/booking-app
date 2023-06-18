using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ratings.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRatingsModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: new Guid("d27b4d27-cd63-48fa-b2a6-e9f37c5d546a"));

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: new Guid("eddf846c-b1b0-4bc8-9c50-e7e544a918fd"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "HostRatings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "AccommodationRatings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "End", "Start", "AccommodationId", "Canceled", "GuestId", "HostId" },
                values: new object[,]
                {
                    { new Guid("06f77f0d-f089-417a-b186-3af1b1116830"), new DateTime(2023, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("549180d0-2b6c-4bf8-9c88-51d0b9f8e6b4"), false, new Guid("f44ced9f-88e3-4c24-9c76-14dcd3ccb7b9"), new Guid("8f172d06-3e60-4a6d-b0ca-2a59305fd10a") },
                    { new Guid("e1008dd3-2342-41ab-9135-27fc94ce088e"), new DateTime(2023, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0b20e8cc-088c-47d0-ae80-d1f88db19c87"), false, new Guid("6c0fdb9d-359e-43b6-bd68-cd11ff2289e7"), new Guid("3e1ead7b-1412-47c9-94e6-c3928b47b474") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: new Guid("06f77f0d-f089-417a-b186-3af1b1116830"));

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: new Guid("e1008dd3-2342-41ab-9135-27fc94ce088e"));

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "HostRatings");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "AccommodationRatings");

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "AccommodationId", "Canceled", "GuestId", "HostId", "End", "Start" },
                values: new object[,]
                {
                    { new Guid("d27b4d27-cd63-48fa-b2a6-e9f37c5d546a"), new Guid("63ce63e8-33bb-4be7-afd7-fdd7d273c756"), false, new Guid("e8867cd0-71f5-43af-93af-c5f316ed200d"), new Guid("268f65b3-7dbc-4cdb-85e2-5ab421f0763c"), new DateTime(2023, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("eddf846c-b1b0-4bc8-9c50-e7e544a918fd"), new Guid("2e0e5d5e-9dd3-40f0-a211-501150818478"), false, new Guid("75cbe6a5-e567-4c9f-ab50-adfb974d275c"), new Guid("a230742a-372c-446f-9984-1e74e8b39db6"), new DateTime(2023, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }
    }
}
