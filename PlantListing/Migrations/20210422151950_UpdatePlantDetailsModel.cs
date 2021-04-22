using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantListing.Migrations
{
    public partial class UpdatePlantDetailsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_PlantCategory_CategoryId",
                table: "PlantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_WeightUnit_UnitId",
                table: "PlantDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlantDetails_CategoryId",
                table: "PlantDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlantDetails_UnitId",
                table: "PlantDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_CategoryId",
                table: "PlantDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_UnitId",
                table: "PlantDetails",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_PlantCategory_CategoryId",
                table: "PlantDetails",
                column: "CategoryId",
                principalTable: "PlantCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_WeightUnit_UnitId",
                table: "PlantDetails",
                column: "UnitId",
                principalTable: "WeightUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
