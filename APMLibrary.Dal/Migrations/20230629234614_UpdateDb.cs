using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APMLibrary.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Authors_AuthorId",
                table: "Publications");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Readers_ReaderId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Readers_ReaderId",
                table: "Ratings");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Bookmarks");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Publications",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Publications",
                newName: "PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Publications_AuthorId",
                table: "Publications",
                newName: "IX_Publications_PublisherId");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Publications",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Reference",
                table: "Profiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "Subscriber",
                table: "Profiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Profiles_PublisherId",
                table: "Publications",
                column: "PublisherId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Profiles_ReaderId",
                table: "Quotes",
                column: "ReaderId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Profiles_ReaderId",
                table: "Ratings",
                column: "ReaderId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Profiles_PublisherId",
                table: "Publications");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Profiles_ReaderId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Profiles_ReaderId",
                table: "Ratings");

            migrationBuilder.DropTable(
                name: "ProfilePublication");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Subscriber",
                table: "Profiles");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Publications",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PublisherId",
                table: "Publications",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Publications_PublisherId",
                table: "Publications",
                newName: "IX_Publications_AuthorId");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProfileId = table.Column<int>(type: "integer", nullable: false),
                    Reference = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProfileId = table.Column<int>(type: "integer", nullable: false),
                    Reference = table.Column<Guid>(type: "uuid", nullable: false),
                    Subscriber = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Readers_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookmarks",
                columns: table => new
                {
                    PublicationsId = table.Column<int>(type: "integer", nullable: false),
                    ReadersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmarks", x => new { x.PublicationsId, x.ReadersId });
                    table.ForeignKey(
                        name: "FK_Bookmarks_Publications_PublicationsId",
                        column: x => x.PublicationsId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookmarks_Readers_ReadersId",
                        column: x => x.ReadersId,
                        principalTable: "Readers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Id",
                table: "Authors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ProfileId",
                table: "Authors",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_ReadersId",
                table: "Bookmarks",
                column: "ReadersId");

            migrationBuilder.CreateIndex(
                name: "IX_Readers_Id",
                table: "Readers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Readers_ProfileId",
                table: "Readers",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Authors_AuthorId",
                table: "Publications",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Readers_ReaderId",
                table: "Quotes",
                column: "ReaderId",
                principalTable: "Readers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Readers_ReaderId",
                table: "Ratings",
                column: "ReaderId",
                principalTable: "Readers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
