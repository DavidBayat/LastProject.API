using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LastProject.API.Migrations
{
    public partial class LetstryAgain2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_User_UserGoogleId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_UserGoogleId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "UserGoogleId",
                table: "Recipe");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Recipe",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_GoogleId",
                table: "Recipe",
                column: "GoogleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_User_GoogleId",
                table: "Recipe",
                column: "GoogleId",
                principalTable: "User",
                principalColumn: "GoogleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_User_GoogleId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_GoogleId",
                table: "Recipe");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Recipe",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "UserGoogleId",
                table: "Recipe",
                type: "nvarchar(500)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_UserGoogleId",
                table: "Recipe",
                column: "UserGoogleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_User_UserGoogleId",
                table: "Recipe",
                column: "UserGoogleId",
                principalTable: "User",
                principalColumn: "GoogleId");
        }
    }
}
