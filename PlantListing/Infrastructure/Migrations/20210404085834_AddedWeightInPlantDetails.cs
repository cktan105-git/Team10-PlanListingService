using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantListing.Infrastructure.Migrations
{
    public partial class AddedWeightInPlantDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_PlantUnit_UnitId",
                table: "PlantDetails");

            migrationBuilder.DropTable(
                name: "PlantUnit");

            migrationBuilder.DropSequence(
                name: "plant_unit_hilo");

            migrationBuilder.CreateSequence(
                name: "weight_unit_hilo",
                incrementBy: 10);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "PlantDetails",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "WeightUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightUnit", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 1L,
                column: "Weight",
                value: 1.0m);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 2L,
                column: "Weight",
                value: 0.5m);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 3L,
                column: "Weight",
                value: 500.0m);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 4L,
                column: "Weight",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 5L,
                column: "Weight",
                value: 100.0m);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 6L,
                column: "Weight",
                value: 100.0m);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 7L,
                column: "Weight",
                value: 100.0m);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 8L,
                column: "Weight",
                value: 100.0m);

            migrationBuilder.InsertData(
                table: "WeightUnit",
                columns: new[] { "Id", "Unit" },
                values: new object[,]
                {
                    { 1, "kg" },
                    { 2, "g" },
                    { 3, "bundle" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_WeightUnit_UnitId",
                table: "PlantDetails",
                column: "UnitId",
                principalTable: "WeightUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_WeightUnit_UnitId",
                table: "PlantDetails");

            migrationBuilder.DropTable(
                name: "WeightUnit");

            migrationBuilder.DropSequence(
                name: "weight_unit_hilo");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "PlantDetails");

            migrationBuilder.CreateSequence(
                name: "plant_unit_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "PlantUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantUnit", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PlantUnit",
                columns: new[] { "Id", "Unit" },
                values: new object[] { 1, "kg" });

            migrationBuilder.InsertData(
                table: "PlantUnit",
                columns: new[] { "Id", "Unit" },
                values: new object[] { 2, "g" });

            migrationBuilder.InsertData(
                table: "PlantUnit",
                columns: new[] { "Id", "Unit" },
                values: new object[] { 3, "bundle" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_PlantUnit_UnitId",
                table: "PlantDetails",
                column: "UnitId",
                principalTable: "PlantUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
