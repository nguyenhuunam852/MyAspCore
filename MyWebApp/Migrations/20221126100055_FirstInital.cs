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
                name: "users",
                columns: table => new
                {
                    userid = table.Column<int>(name: "user_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(name: "user_name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    userfullname = table.Column<string>(name: "user_fullname", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    useremail = table.Column<string>(name: "user_email", type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    userpassword = table.Column<string>(name: "user_password", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "states",
                columns: table => new
                {
                    stateid = table.Column<int>(name: "state_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    statepage = table.Column<int>(name: "state_page", type: "int", nullable: false, defaultValue: 0),
                    statefilter = table.Column<string>(name: "state_filter", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    statesortby = table.Column<string>(name: "state_sortby", type: "nvarchar(max)", nullable: false),
                    stateisdesc = table.Column<bool>(name: "state_isdesc", type: "bit", nullable: false, defaultValue: false),
                    userstateid = table.Column<int>(name: "user_state_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_states", x => x.stateid);
                    table.ForeignKey(
                        name: "FK_states_users_user_state_id",
                        column: x => x.userstateid,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_states_user_state_id",
                table: "states",
                column: "user_state_id",
                unique: true);

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
                name: "states");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
