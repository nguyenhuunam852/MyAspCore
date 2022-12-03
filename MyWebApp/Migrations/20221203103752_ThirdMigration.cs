using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebApp.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "user_isadmin",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "sleep_entries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_sleep_entries_UserId",
                table: "sleep_entries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_sleep_entries_users_UserId",
                table: "sleep_entries",
                column: "UserId",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sleep_entries_users_UserId",
                table: "sleep_entries");

            migrationBuilder.DropIndex(
                name: "IX_sleep_entries_UserId",
                table: "sleep_entries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "sleep_entries");

            migrationBuilder.AlterColumn<bool>(
                name: "user_isadmin",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }
    }
}
