using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeWork_61_Instagram.Migrations
{
    /// <inheritdoc />
    public partial class RenamedpropertyInUserFollowModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_AspNetUsers_FollowingId",
                table: "UserFollows");

            migrationBuilder.RenameColumn(
                name: "FollowingId",
                table: "UserFollows",
                newName: "FollowerId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFollows_FollowingId",
                table: "UserFollows",
                newName: "IX_UserFollows_FollowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_AspNetUsers_FollowerId",
                table: "UserFollows",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_AspNetUsers_FollowerId",
                table: "UserFollows");

            migrationBuilder.RenameColumn(
                name: "FollowerId",
                table: "UserFollows",
                newName: "FollowingId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFollows_FollowerId",
                table: "UserFollows",
                newName: "IX_UserFollows_FollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_AspNetUsers_FollowingId",
                table: "UserFollows",
                column: "FollowingId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
