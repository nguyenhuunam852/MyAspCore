using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebApp.Migrations
{
    /// <inheritdoc />
    public partial class FirstInital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sleep_entries",
                columns: table => new
                {
                    sleepentryid = table.Column<int>(name: "sleep_entry_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sleepentrydate = table.Column<DateTime>(name: "sleep_entry_date", type: "datetime2", nullable: true),
                    sleepentrypage = table.Column<int>(name: "sleep_entry_page", type: "int", nullable: false, defaultValue: 0),
                    sleepentrywakeuptime = table.Column<DateTime>(name: "sleep_entry_wakeuptime", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sleep_entries", x => x.sleepentryid);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userid = table.Column<int>(name: "user_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(name: "user_name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    userfullname = table.Column<string>(name: "user_fullname", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    useremail = table.Column<string>(name: "user_email", type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    userpassword = table.Column<string>(name: "user_password", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    userisdeleted = table.Column<bool>(name: "user_isdeleted", type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_user_name",
                table: "users",
                column: "user_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sleep_entries");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
