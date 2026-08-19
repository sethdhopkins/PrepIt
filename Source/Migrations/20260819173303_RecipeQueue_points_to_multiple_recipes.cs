using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Source.Migrations
{
    /// <inheritdoc />
    public partial class RecipeQueue_points_to_multiple_recipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeQueue_Recipe_RecipeId",
                table: "RecipeQueue");

            migrationBuilder.DropIndex(
                name: "IX_RecipeQueue_RecipeId",
                table: "RecipeQueue");

            migrationBuilder.DropIndex(
                name: "IX_RecipeQueue_UserId",
                table: "RecipeQueue");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "RecipeQueue");

            migrationBuilder.AddColumn<int>(
                name: "RecipeQueueId",
                table: "Recipe",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeQueue_UserId",
                table: "RecipeQueue",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_RecipeQueueId",
                table: "Recipe",
                column: "RecipeQueueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_RecipeQueue_RecipeQueueId",
                table: "Recipe",
                column: "RecipeQueueId",
                principalTable: "RecipeQueue",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_RecipeQueue_RecipeQueueId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_RecipeQueue_UserId",
                table: "RecipeQueue");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_RecipeQueueId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "RecipeQueueId",
                table: "Recipe");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "RecipeQueue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeQueue_RecipeId",
                table: "RecipeQueue",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeQueue_UserId",
                table: "RecipeQueue",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeQueue_Recipe_RecipeId",
                table: "RecipeQueue",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
