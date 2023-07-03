using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APMLibrary.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfilePublication");

            migrationBuilder.CreateTable(
                name: "Bookmarks",
                columns: table => new
                {
                    BookmarksId = table.Column<int>(type: "integer", nullable: false),
                    ReadersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmarks", x => new { x.BookmarksId, x.ReadersId });
                    table.ForeignKey(
                        name: "FK_Bookmarks_Profiles_ReadersId",
                        column: x => x.ReadersId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookmarks_Publications_BookmarksId",
                        column: x => x.BookmarksId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_ReadersId",
                table: "Bookmarks",
                column: "ReadersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookmarks");

            migrationBuilder.CreateTable(
                name: "ProfilePublication",
                columns: table => new
                {
                    BookmarksId = table.Column<int>(type: "integer", nullable: false),
                    ReadersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePublication", x => new { x.BookmarksId, x.ReadersId });
                    table.ForeignKey(
                        name: "FK_ProfilePublication_Profiles_ReadersId",
                        column: x => x.ReadersId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfilePublication_Publications_BookmarksId",
                        column: x => x.BookmarksId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePublication_ReadersId",
                table: "ProfilePublication",
                column: "ReadersId");
        }
    }
}
