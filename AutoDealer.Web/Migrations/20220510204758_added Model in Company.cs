using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoDealer.Web.Migrations
{
    public partial class addedModelinCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Models",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_CompanyId",
                table: "Models",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Companies_CompanyId",
                table: "Models",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarOwners_CarOwnerId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Companies_CompanyId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_CompanyId",
                table: "Models");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarOwners",
                table: "CarOwners");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Models");

            migrationBuilder.RenameTable(
                name: "CarOwners",
                newName: "CarOwner");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarOwner",
                table: "CarOwner",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarOwner_CarOwnerId",
                table: "Cars",
                column: "CarOwnerId",
                principalTable: "CarOwner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
