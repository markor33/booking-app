using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationsLibrary.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accomodations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HostId = table.Column<Guid>(type: "uuid", nullable: false),
                    AutoConfirmation = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accomodations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccommodationId = table.Column<Guid>(type: "uuid", nullable: false),
                    GuestId = table.Column<Guid>(type: "uuid", nullable: true),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    End = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NumOfGuests = table.Column<int>(type: "integer", nullable: false),
                    PriceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationRequests_Accomodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accomodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationRequests_Prices_PriceId",
                        column: x => x.PriceId,
                        principalTable: "Prices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccommodationId = table.Column<Guid>(type: "uuid", nullable: false),
                    GuestId = table.Column<Guid>(type: "uuid", nullable: true),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    End = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NumOfGuests = table.Column<int>(type: "integer", nullable: false),
                    PriceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Canceled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Accomodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accomodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Prices_PriceId",
                        column: x => x.PriceId,
                        principalTable: "Prices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accomodations",
                columns: new[] { "Id", "AutoConfirmation", "HostId" },
                values: new object[,]
                {
                    { new Guid("930393b4-cd6c-4106-9eef-fd4ebf03cf0e"), false, new Guid("c68fed54-5220-4eaf-a944-beff9e859375") },
                    { new Guid("d8ebaf2e-caf3-4566-a265-1ce7426def45"), true, new Guid("f71b8747-5b16-4f93-977c-25da98a40fd7") },
                    { new Guid("ebbe75b0-11f4-4140-95a9-42cf2fb7b8c0"), false, new Guid("fffe2bf1-2473-4db4-bdde-0e7d1f125fb2") },
                    { new Guid("f19adfc8-f591-4610-8b47-a17c50562e31"), true, new Guid("a736791d-2582-4e3f-85c5-3ba7ace7edb6") }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "Amount", "Type" },
                values: new object[,]
                {
                    { new Guid("555a5103-1290-4c49-b7e2-e23ca36fb4b5"), 350f, 1 },
                    { new Guid("e651c085-7bb1-402b-a73d-d5f0ed71b631"), 250f, 0 }
                });

            migrationBuilder.InsertData(
                table: "ReservationRequests",
                columns: new[] { "Id", "AccommodationId", "GuestId", "NumOfGuests", "PriceId", "Status", "End", "Start" },
                values: new object[,]
                {
                    { new Guid("04a23eed-6b83-4312-a86c-149b813ea7bd"), new Guid("f19adfc8-f591-4610-8b47-a17c50562e31"), new Guid("ff36e765-8c9b-4342-afae-b19cf2650008"), 2, new Guid("e651c085-7bb1-402b-a73d-d5f0ed71b631"), 0, new DateTime(2023, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2bcd34ab-76da-44c2-969d-b1491a6f5d3e"), new Guid("ebbe75b0-11f4-4140-95a9-42cf2fb7b8c0"), new Guid("f90d7f80-6e4c-47b8-b51f-d84c06157b3f"), 1, new Guid("555a5103-1290-4c49-b7e2-e23ca36fb4b5"), 0, new DateTime(2023, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRequests_AccommodationId",
                table: "ReservationRequests",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRequests_PriceId",
                table: "ReservationRequests",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AccommodationId",
                table: "Reservations",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PriceId",
                table: "Reservations",
                column: "PriceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationRequests");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Accomodations");

            migrationBuilder.DropTable(
                name: "Prices");
        }
    }
}
