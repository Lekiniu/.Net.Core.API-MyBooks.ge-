using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBooks.Data.Migrations
{
    public partial class _2ndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Users_UserId",
                table: "WishList");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WishList",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Users_UserId",
                table: "WishList",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Users_UserId",
                table: "WishList");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WishList",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Users_UserId",
                table: "WishList",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
