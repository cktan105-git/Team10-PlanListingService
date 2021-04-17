using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantListing.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "plant_category_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "plant_details_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "plant_unit_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "PlantCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantCategory", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "PlantDetails",
                columns: table => new
                {
                    PlantDetailsId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProducerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantDetails", x => x.PlantDetailsId);
                    table.ForeignKey(
                        name: "FK_PlantDetails_PlantCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "PlantCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantDetails_PlantUnit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "PlantUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_CategoryId",
                table: "PlantDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_UnitId",
                table: "PlantDetails",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlantDetails");

            migrationBuilder.DropTable(
                name: "PlantCategory");

            migrationBuilder.DropTable(
                name: "PlantUnit");

            migrationBuilder.DropSequence(
                name: "plant_category_hilo");

            migrationBuilder.DropSequence(
                name: "plant_details_hilo");

            migrationBuilder.DropSequence(
                name: "plant_unit_hilo");
        }
    }
}
