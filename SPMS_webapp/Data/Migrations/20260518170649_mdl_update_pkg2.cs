using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPMS_webapp.Data.Migrations
{
    /// <inheritdoc />
    public partial class mdl_update_pkg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverLicenseNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverProfile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkingStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParkingEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalBill = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCheckedOut = table.Column<bool>(type: "bit", nullable: false),
                    DriverProfileId = table.Column<int>(type: "int", nullable: false),
                    ParkingSpotId = table.Column<int>(type: "int", nullable: false),
                    PaymentHistoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingHistory_DriverProfile_DriverProfileId",
                        column: x => x.DriverProfileId,
                        principalTable: "DriverProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkingHistory_ParkingSpot_ParkingSpotId",
                        column: x => x.ParkingSpotId,
                        principalTable: "ParkingSpot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkingHistory_PaymentHistory_PaymentHistoryId",
                        column: x => x.PaymentHistoryId,
                        principalTable: "PaymentHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ParkingReserveHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalBill = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    DriverProfileId = table.Column<int>(type: "int", nullable: false),
                    ParkingSpotId = table.Column<int>(type: "int", nullable: false),
                    PaymentHistoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingReserveHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingReserveHistory_DriverProfile_DriverProfileId",
                        column: x => x.DriverProfileId,
                        principalTable: "DriverProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkingReserveHistory_ParkingSpot_ParkingSpotId",
                        column: x => x.ParkingSpotId,
                        principalTable: "ParkingSpot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkingReserveHistory_PaymentHistory_PaymentHistoryId",
                        column: x => x.PaymentHistoryId,
                        principalTable: "PaymentHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverProfile_UserId",
                table: "DriverProfile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingHistory_DriverProfileId",
                table: "ParkingHistory",
                column: "DriverProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingHistory_ParkingSpotId",
                table: "ParkingHistory",
                column: "ParkingSpotId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingHistory_PaymentHistoryId",
                table: "ParkingHistory",
                column: "PaymentHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingReserveHistory_DriverProfileId",
                table: "ParkingReserveHistory",
                column: "DriverProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingReserveHistory_ParkingSpotId",
                table: "ParkingReserveHistory",
                column: "ParkingSpotId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingReserveHistory_PaymentHistoryId",
                table: "ParkingReserveHistory",
                column: "PaymentHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingHistory");

            migrationBuilder.DropTable(
                name: "ParkingReserveHistory");

            migrationBuilder.DropTable(
                name: "DriverProfile");

            migrationBuilder.DropTable(
                name: "PaymentHistory");
        }
    }
}
