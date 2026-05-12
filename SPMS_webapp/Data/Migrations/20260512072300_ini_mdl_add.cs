using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPMS_webapp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ini_mdl_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IOTEnabledParkingMeter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOTEnabledParkingMeter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpotNumber = table.Column<int>(type: "int", nullable: false),
                    IsOccupied = table.Column<bool>(type: "bit", nullable: false),
                    ParkingMeterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingSpot_IOTEnabledParkingMeter_ParkingMeterId",
                        column: x => x.ParkingMeterId,
                        principalTable: "IOTEnabledParkingMeter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpot_ParkingMeterId",
                table: "ParkingSpot",
                column: "ParkingMeterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSpot");

            migrationBuilder.DropTable(
                name: "IOTEnabledParkingMeter");
        }
    }
}
