using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoDealer.Web.Migrations
{
    public partial class addedengineType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Engines");

            migrationBuilder.AlterColumn<float>(
                name: "Capacity",
                table: "Engines",
                type: "float(3,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float(2,2)");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Engines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EngineTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_TypeId",
                table: "Engines",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Engines_EngineTypes_TypeId",
                table: "Engines",
                column: "TypeId",
                principalTable: "EngineTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engines_EngineTypes_TypeId",
                table: "Engines");

            migrationBuilder.DropTable(
                name: "EngineTypes");

            migrationBuilder.DropIndex(
                name: "IX_Engines_TypeId",
                table: "Engines");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Engines");

            migrationBuilder.AlterColumn<float>(
                name: "Capacity",
                table: "Engines",
                type: "float(2,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float(3,2)");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Engines",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
