using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Source.Migrations
{
    /// <inheritdoc />
    public partial class Added_QueueItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_RecipeQueue_RecipeQueueId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_RecipeQueueId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "TimeAdded",
                table: "RecipeQueue");

            migrationBuilder.DropColumn(
                name: "RecipeQueueId",
                table: "Recipe");

            migrationBuilder.CreateTable(
                name: "RecipeQueueItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeQueueId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    TimeAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeQueueItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeQueueItems_RecipeQueue_RecipeQueueId",
                        column: x => x.RecipeQueueId,
                        principalTable: "RecipeQueue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeQueueItems_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeQueueItems_RecipeId",
                table: "RecipeQueueItems",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeQueueItems_RecipeQueueId",
                table: "RecipeQueueItems",
                column: "RecipeQueueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeQueueItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeAdded",
                table: "RecipeQueue",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecipeQueueId",
                table: "Recipe",
                type: "int",
                nullable: true);

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
    }
}
