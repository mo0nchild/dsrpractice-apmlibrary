using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APMLibrary.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_BookCovers_BookCoverId",
                table: "Publications");

            migrationBuilder.DropTable(
                name: "BookCovers");

            migrationBuilder.DropIndex(
                name: "IX_Publications_BookCoverId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "BookCoverId",
                table: "Publications");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Publications",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Publications");

            migrationBuilder.AddColumn<int>(
                name: "BookCoverId",
                table: "Publications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookCovers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BackCover = table.Column<byte[]>(type: "bytea", nullable: true),
                    FrontCover = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCovers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publications_BookCoverId",
                table: "Publications",
                column: "BookCoverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_BookCovers_BookCoverId",
                table: "Publications",
                column: "BookCoverId",
                principalTable: "BookCovers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
