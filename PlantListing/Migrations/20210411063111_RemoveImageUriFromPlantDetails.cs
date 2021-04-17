using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantListing.Migrations
{
    public partial class RemoveImageUriFromPlantDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 1L,
                column: "ImageName",
                value: "898ed47a-c779-4d07-bcf5-d241adc55f89_Broccoli.jpg");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 2L,
                column: "ImageName",
                value: "ddbd68bd-da6d-4ba2-a70f-5a32813ec261_Tomato.jpg");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 3L,
                column: "ImageName",
                value: "af25243b-9f08-4c1f-ad8f-d031695154d5_Japanese Cucumber.jpg");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 4L,
                column: "ImageName",
                value: "df9b2a47-3975-474d-82b6-5f6bcd88f743_Sunflower.jpg");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 5L,
                column: "ImageName",
                value: "a624fc48-0349-4e28-985e-d5aa4c859c0c_Garlic.jpg");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 6L,
                column: "ImageName",
                value: "1c1b6571-cbbe-4f45-a5ed-01704a8885b8_Spring Onion.jpg");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 7L,
                column: "ImageName",
                value: "1a048a12-4815-4ce1-9df3-02088c3f82c9_Red Chilli.jpg");

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 8L,
                column: "ImageName",
                value: "e095626b-d72f-4fc7-ac7b-0fcc6393ae00_Green Chilli.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 1L,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 2L,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 3L,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 4L,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 5L,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 6L,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 7L,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "PlantDetails",
                keyColumn: "PlantDetailsId",
                keyValue: 8L,
                column: "ImageName",
                value: null);
        }
    }
}
