using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsToCategoriesAndScreens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScreenId",
                table: "Posts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScreenId",
                table: "Categories",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ScreenId",
                table: "Posts",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ScreenId",
                table: "Categories",
                column: "ScreenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Screens_ScreenId",
                table: "Categories",
                column: "ScreenId",
                principalTable: "Screens",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Screens_ScreenId",
                table: "Posts",
                column: "ScreenId",
                principalTable: "Screens",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Screens_ScreenId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Screens_ScreenId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ScreenId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ScreenId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ScreenId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ScreenId",
                table: "Categories");
        }
    }
}
