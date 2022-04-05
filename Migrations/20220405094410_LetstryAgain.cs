using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LastProject.API.Migrations
{
    public partial class LetstryAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    GoogleId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.GoogleId);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    idMeal = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    strMeal = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    strMealThumb = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GoogleId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserGoogleId = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.idMeal);
                    table.ForeignKey(
                        name: "FK_Recipe_User_UserGoogleId",
                        column: x => x.UserGoogleId,
                        principalTable: "User",
                        principalColumn: "GoogleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_UserGoogleId",
                table: "Recipe",
                column: "UserGoogleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
