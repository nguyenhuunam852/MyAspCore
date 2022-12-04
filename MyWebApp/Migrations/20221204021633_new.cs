using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebApp.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "sleep_entry_wakeuptime",
                table: "sleep_entries");

            migrationBuilder.AddColumn<bool>(
                name: "sleep_entry_wakeuptime",
                table: "sleep_entries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "sleep_entry_sleeptime",
                table: "sleep_entries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "sleep_entry_wakeuptime",
                table: "sleep_entries",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
