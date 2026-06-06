using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPMS_webapp.Data.Migrations
{
    /// <inheritdoc />
    public partial class paymenthistory_mdl_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "PaymentHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "SpotId",
                table: "PaymentHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PaymentHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpotId",
                table: "PaymentHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PaymentHistory");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "PaymentHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
