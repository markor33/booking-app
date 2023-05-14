using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservations.API.Infrastructure.Persistence.Migrations
{
    public partial class PriceRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRequests_Prices_PriceId",
                table: "ReservationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Prices_PriceId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_PriceId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_ReservationRequests_PriceId",
                table: "ReservationRequests");

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("0c0a8dec-640a-46a0-baf0-37ee9363603c"));

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("4548347e-9366-4f96-8663-f34d1bfad7bc"));

            migrationBuilder.DeleteData(
                table: "ReservationRequests",
                keyColumn: "Id",
                keyValue: new Guid("d6fa6cba-9711-4db9-84e7-aa46c5619331"));

            migrationBuilder.DeleteData(
                table: "ReservationRequests",
                keyColumn: "Id",
                keyValue: new Guid("deb4e537-bf82-446f-841a-5f89ec5d4fe5"));

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("131111a6-34cd-46cf-9eec-281b1cc6a311"));

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("9135606b-dde1-4732-9060-24dc85f69daa"));

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "ReservationRequests");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "ReservationRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Accommodations",
                columns: new[] { "Id", "AutoConfirmation", "HostId" },
                values: new object[,]
                {
                    { new Guid("426bb5cd-11c7-4a6b-b69d-a295cf8ad810"), false, new Guid("1ea0022e-cff0-4256-ba25-2c12cc0dfadc") },
                    { new Guid("6dd36b67-30e0-40df-bceb-1d03e29afe28"), true, new Guid("878962ca-7be7-4cd2-9e27-1963749e19be") },
                    { new Guid("7bd43bc3-3607-4861-8de3-ac46a639419b"), false, new Guid("cd241f3d-3641-43d0-9227-bd49f5c4cc20") },
                    { new Guid("8ae6ed88-2755-4ad8-9a1f-0c64dab88da5"), true, new Guid("06d65b58-5b3e-4dd3-b886-341ced7d0d51") }
                });

            migrationBuilder.InsertData(
                table: "ReservationRequests",
                columns: new[] { "Id", "AccommodationId", "GuestId", "NumOfGuests", "Price", "Status", "End", "Start" },
                values: new object[,]
                {
                    { new Guid("09927483-c298-4c0a-8a33-88d92548e12b"), new Guid("6dd36b67-30e0-40df-bceb-1d03e29afe28"), new Guid("0e41276d-8cac-400c-9dcb-0fa771023864"), 2, 500, 0, new DateTime(2023, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8e39a8d9-cb9d-4d13-804c-a1002ca0964b"), new Guid("7bd43bc3-3607-4861-8de3-ac46a639419b"), new Guid("f07fd2d7-5fcf-45a5-98ea-c9826be346a4"), 1, 400, 0, new DateTime(2023, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("426bb5cd-11c7-4a6b-b69d-a295cf8ad810"));

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("8ae6ed88-2755-4ad8-9a1f-0c64dab88da5"));

            migrationBuilder.DeleteData(
                table: "ReservationRequests",
                keyColumn: "Id",
                keyValue: new Guid("09927483-c298-4c0a-8a33-88d92548e12b"));

            migrationBuilder.DeleteData(
                table: "ReservationRequests",
                keyColumn: "Id",
                keyValue: new Guid("8e39a8d9-cb9d-4d13-804c-a1002ca0964b"));

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("6dd36b67-30e0-40df-bceb-1d03e29afe28"));

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("7bd43bc3-3607-4861-8de3-ac46a639419b"));

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ReservationRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "PriceId",
                table: "Reservations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PriceId",
                table: "ReservationRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.InsertData(
                table: "Accommodations",
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
                name: "IX_Reservations_PriceId",
                table: "Reservations",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRequests_PriceId",
                table: "ReservationRequests",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationRequests_Prices_PriceId",
                table: "ReservationRequests",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Prices_PriceId",
                table: "Reservations",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
