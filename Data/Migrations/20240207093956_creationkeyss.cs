using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBBulletin.Data.Migrations
{
    /// <inheritdoc />
    public partial class creationkeyss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsArticles");

            migrationBuilder.CreateTable(
                name: "Newsinfo",
                columns: table => new
                {
                    NewsinfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticlePicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Newsarticle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsGenreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsinfo", x => x.NewsinfoId);
                    table.ForeignKey(
                        name: "FK_Newsinfo_NewsGenres_NewsGenreId",
                        column: x => x.NewsGenreId,
                        principalTable: "NewsGenres",
                        principalColumn: "NewsGenreId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Newsinfo_NewsGenreId",
                table: "Newsinfo",
                column: "NewsGenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Newsinfo");

            migrationBuilder.CreateTable(
                name: "NewsArticles",
                columns: table => new
                {
                    NewsArticleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsGenreId = table.Column<int>(type: "int", nullable: true),
                    ArticlePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Newsarticle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsArticles", x => x.NewsArticleId);
                    table.ForeignKey(
                        name: "FK_NewsArticles_NewsGenres_NewsGenreId",
                        column: x => x.NewsGenreId,
                        principalTable: "NewsGenres",
                        principalColumn: "NewsGenreId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticles_NewsGenreId",
                table: "NewsArticles",
                column: "NewsGenreId");
        }
    }
}
