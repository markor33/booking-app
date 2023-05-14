using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservations.API.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HostId = table.Column<Guid>(type: "uuid", nullable: false),
                    AutoConfirmation = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.Id);
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
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationRequests_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
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
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Canceled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accommodations",
                columns: new[] { "Id", "AutoConfirmation", "HostId" },
                values: new object[,]
                {
                    { new Guid("21dbd1b8-b523-4d9b-a8c8-05e0c567316f"), true, new Guid("142eac06-d30f-4836-80ad-fb52725149dd") },
                    { new Guid("b2702b82-40d6-498f-b9a7-fa6d5e0fa029"), false, new Guid("a62dafca-14d6-4907-aa4a-cd8ca5607665") },
                    { new Guid("c609fafc-3c32-4e73-9cec-84877cf50eb1"), false, new Guid("5d604ef4-7149-4cab-a1cf-4c6497e9768e") },
                    { new Guid("f653f8e7-efde-45c2-b82e-16566de0c27a"), true, new Guid("ee6f2fa4-8bcf-4530-b97a-686f9be133d2") }
                });

            migrationBuilder.InsertData(
                table: "ReservationRequests",
                columns: new[] { "Id", "AccommodationId", "GuestId", "NumOfGuests", "Price", "Status", "End", "Start" },
                values: new object[,]
                {
                    { new Guid("5ace11c7-4bbc-402c-9a42-0a85998fbec1"), new Guid("21dbd1b8-b523-4d9b-a8c8-05e0c567316f"), new Guid("5fbd1fd1-ab26-4728-aef2-e44fef2e41ab"), 2, 500, 0, new DateTime(2023, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d8ef57a7-f0c6-4111-90f7-2b85ebc6827b"), new Guid("c609fafc-3c32-4e73-9cec-84877cf50eb1"), new Guid("97a98ce3-7426-4f76-96cd-1e96472a9ece"), 1, 400, 0, new DateTime(2023, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRequests_AccommodationId",
                table: "ReservationRequests",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AccommodationId",
                table: "Reservations",
                column: "AccommodationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationRequests");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Accommodations");
        }
    }
}
