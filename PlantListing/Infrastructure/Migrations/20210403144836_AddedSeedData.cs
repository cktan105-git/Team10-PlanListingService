using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantListing.Infrastructure.Migrations
{
    public partial class AddedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PlantCategory",
                columns: new[] { "Id", "Category" },
                values: new object[,]
                {
                    { 1, "Vegetable" },
                    { 2, "Fruit" },
                    { 3, "Flower" },
                    { 4, "Herb" },
                    { 5, "Spice" }
                });

            migrationBuilder.InsertData(
                table: "PlantUnit",
                columns: new[] { "Id", "Unit" },
                values: new object[,]
                {
                    { 1, "kg" },
                    { 2, "g" },
                    { 3, "bundle" }
                });

            migrationBuilder.InsertData(
                table: "PlantDetails",
                columns: new[] { "PlantDetailsId", "CategoryId", "Description", "ImageName", "Name", "Price", "ProducerId", "Stock", "UnitId" },
                values: new object[,]
                {
                    { 1L, 1, "Green vegetable", null, "Broccoli", 2.00m, 1L, 50, 1 },
                    { 2L, 2, "Red color sour fruit", null, "Tomato", 1.00m, 1L, 50, 1 },
                    { 3L, 2, "Green color fruit", null, "Japanese Cucumber", 1.00m, 2L, 50, 2 },
                    { 5L, 5, "Home grown fresh garlic", null, "Garlic", 0.50m, 4L, 50, 2 },
                    { 6L, 5, "Add flavor to your dish", null, "Spring Onion", 0.50m, 4L, 50, 2 },
                    { 7L, 5, "Red Chilli", null, "Red Chilli", 1.00m, 4L, 100, 2 },
                    { 8L, 5, "Green Chilli", null, "Green Chilli", 1.00m, 4L, 100, 2 },
                    { 4L, 3, "Flower chasing the sun", null, "Sunflower", 50.00m, 3L, 10, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlantCategory",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "PlantCategory",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlantCategory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PlantCategory",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PlantCategory",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PlantUnit",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlantUnit",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PlantUnit",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
