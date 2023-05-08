using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservations.API.Infrastructure.Persistence.Migrations
{
    public partial class InitMigration : Migration
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
                    { new Guid("0c0a8dec-640a-46a0-baf0-37ee9363603c"), true, new Guid("cae096ba-f38b-4951-8848-d137a12d547b") },
                    { new Guid("131111a6-34cd-46cf-9eec-281b1cc6a311"), true, new Guid("55a7756f-a328-4ddf-bae8-0c67c63a622e") },
                    { new Guid("4548347e-9366-4f96-8663-f34d1bfad7bc"), false, new Guid("d8741974-8d00-4d0e-8b2e-9c28e16edeee") },
                    { new Guid("9135606b-dde1-4732-9060-24dc85f69daa"), false, new Guid("69496110-94f1-442a-bec0-60d7c153a8aa") }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "Amount", "Type" },
                values: new object[,]
                {
                    { new Guid("5ac921d1-d75b-4656-8002-86c2e3d689e6"), 350f, 1 },
                    { new Guid("94200799-698e-4a9f-9aa7-b6acaab93b14"), 250f, 0 }
                });

            migrationBuilder.InsertData(
                table: "ReservationRequests",
                columns: new[] { "Id", "AccommodationId", "GuestId", "NumOfGuests", "PriceId", "Status", "End", "Start" },
                values: new object[,]
                {
                    { new Guid("d6fa6cba-9711-4db9-84e7-aa46c5619331"), new Guid("131111a6-34cd-46cf-9eec-281b1cc6a311"), new Guid("3f3a155b-3fd7-4e75-b3da-9057e1850ffc"), 2, new Guid("94200799-698e-4a9f-9aa7-b6acaab93b14"), 0, new DateTime(2023, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("deb4e537-bf82-446f-841a-5f89ec5d4fe5"), new Guid("9135606b-dde1-4732-9060-24dc85f69daa"), new Guid("739a3a88-1680-49f2-8d57-ba7bb720ab63"), 1, new Guid("5ac921d1-d75b-4656-8002-86c2e3d689e6"), 0, new DateTime(2023, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
