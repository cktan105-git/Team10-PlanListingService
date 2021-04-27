using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantListing.Migrations
{
    public partial class UpdatedToUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProducerId",
                table: "PlantDetails");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PlantDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 1L,
                column: "UserId",
                value: "cktan");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 2L,
                column: "UserId",
                value: "cktan");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 3L,
                column: "UserId",
                value: "mgkoh");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 4L,
                column: "UserId",
                value: "user0001");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 5L,
                column: "UserId",
                value: "wpkeoh");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 6L,
                column: "UserId",
                value: "wpkeoh");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 7L,
                column: "UserId",
                value: "wpkeoh");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 8L,
                column: "UserId",
                value: "wpkeoh");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PlantDetails");

            migrationBuilder.AddColumn<long>(
                name: "ProducerId",
                table: "PlantDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 1L,
                column: "ProducerId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 2L,
                column: "ProducerId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 3L,
                column: "ProducerId",
                value: 2L);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 4L,
                column: "ProducerId",
                value: 3L);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 5L,
                column: "ProducerId",
                value: 4L);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 6L,
                column: "ProducerId",
                value: 4L);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 7L,
                column: "ProducerId",
                value: 4L);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 8L,
                column: "ProducerId",
                value: 4L);
        }
    }
}
