using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class MaksUsersHaveManyScopes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scopes_Users_UserId",
                table: "Scopes");

            migrationBuilder.DropIndex(
                name: "IX_Scopes_UserId",
                table: "Scopes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Scopes");

            migrationBuilder.CreateTable(
                name: "UserScopes",
                columns: table => new
                {
                    ScopesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScopes", x => new { x.ScopesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserScopes_Scopes_ScopesId",
                        column: x => x.ScopesId,
                        principalTable: "Scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserScopes_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserScopes_UsersId",
                table: "UserScopes",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserScopes");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Scopes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Scopes_UserId",
                table: "Scopes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scopes_Users_UserId",
                table: "Scopes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
