using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerPostoffices.Migrations
{
    public partial class InitDbv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryStatuses",
                columns: table => new
                {
                    DeliveryStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryStatuses", x => x.DeliveryStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    RecipientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.RecipientId);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    TransportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.TransportId);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    TransportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageId);
                    table.ForeignKey(
                        name: "FK_Packages_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Packages_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Packages_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "TransportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PackageDeliveryHistory",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    DeliveryStatusId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCurrentStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageDeliveryHistory", x => new { x.PackageId, x.DeliveryStatusId });
                    table.ForeignKey(
                        name: "FK_PackageDeliveryHistory_DeliveryStatuses_DeliveryStatusId",
                        column: x => x.DeliveryStatusId,
                        principalTable: "DeliveryStatuses",
                        principalColumn: "DeliveryStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PackageDeliveryHistory_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeliveryStatuses",
                columns: new[] { "DeliveryStatusId", "Status", "StatusDescription", "UpdateTime" },
                values: new object[,]
                {
                    { 1, "In Progress", "Package is being processed", new DateTime(2023, 12, 4, 13, 39, 43, 186, DateTimeKind.Local).AddTicks(7996) },
                    { 2, "Delivered", "Package has been delivered", new DateTime(2023, 12, 4, 13, 39, 43, 186, DateTimeKind.Local).AddTicks(8005) }
                });

            migrationBuilder.InsertData(
                table: "Recipients",
                columns: new[] { "RecipientId", "ContactInfo", "Name" },
                values: new object[,]
                {
                    { 1, "john@example.com", "John Doe" },
                    { 2, "jane@example.com", "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "TransportId", "Name" },
                values: new object[,]
                {
                    { 1, "Carrier1" },
                    { 2, "Carrier2" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "City", "RecipientId", "State", "Street" },
                values: new object[] { 1, "City1", 1, "State1", "123 Main St" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "City", "RecipientId", "State", "Street" },
                values: new object[] { 2, "City2", 2, "State2", "456 Oak St" });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "PackageId", "AddressId", "RecipientId", "Size", "TransportId", "Value", "Weight" },
                values: new object[] { 1, 1, 1, "Small", 1, 50m, 2.5 });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "PackageId", "AddressId", "RecipientId", "Size", "TransportId", "Value", "Weight" },
                values: new object[] { 2, 2, 2, "Large", 2, 100m, 5.0 });

            migrationBuilder.InsertData(
                table: "PackageDeliveryHistory",
                columns: new[] { "DeliveryStatusId", "PackageId", "IsCurrentStatus", "TimeStamp" },
                values: new object[] { 1, 1, true, new DateTime(2023, 12, 4, 13, 39, 43, 186, DateTimeKind.Local).AddTicks(8013) });

            migrationBuilder.InsertData(
                table: "PackageDeliveryHistory",
                columns: new[] { "DeliveryStatusId", "PackageId", "IsCurrentStatus", "TimeStamp" },
                values: new object[] { 2, 2, true, new DateTime(2023, 12, 4, 13, 39, 43, 186, DateTimeKind.Local).AddTicks(8014) });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RecipientId",
                table: "Addresses",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageDeliveryHistory_DeliveryStatusId",
                table: "PackageDeliveryHistory",
                column: "DeliveryStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_AddressId",
                table: "Packages",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_RecipientId",
                table: "Packages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_TransportId",
                table: "Packages",
                column: "TransportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageDeliveryHistory");

            migrationBuilder.DropTable(
                name: "DeliveryStatuses");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "Recipients");
        }
    }
}
