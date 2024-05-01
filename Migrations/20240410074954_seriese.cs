using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiLib.Migrations
{
    /// <inheritdoc />
    public partial class seriese : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "animeStorage");

            migrationBuilder.DropTable(
                name: "tvShowStorage");

            migrationBuilder.CreateTable(
                name: "episodes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    seasonNumber = table.Column<int>(type: "int", nullable: true),
                    episodeNumber = table.Column<int>(type: "int", nullable: true),
                    episodeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    episodeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    episodeRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    episodeReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    poster = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_episodes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "seasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    seasonNumber = table.Column<int>(type: "int", nullable: true),
                    seasonName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seasons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "series",
                columns: table => new
                {
                    itemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemCast = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemTrailerURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemPoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemBackPoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemSeasons = table.Column<int>(type: "int", nullable: true),
                    isUpcoming = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_series", x => x.itemId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "episodes");

            migrationBuilder.DropTable(
                name: "seasons");

            migrationBuilder.DropTable(
                name: "series");

            migrationBuilder.CreateTable(
                name: "animeStorage",
                columns: table => new
                {
                    animeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    animeBackPoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeCast = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeCatagory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeLanguages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animePoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeSeasons = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeTrailerURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isUpcoming = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animeStorage", x => x.animeId);
                });

            migrationBuilder.CreateTable(
                name: "tvShowStorage",
                columns: table => new
                {
                    tvShowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isUpcoming = table.Column<bool>(type: "bit", nullable: true),
                    tvShowBackPoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowCast = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowCatagory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowLanguages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowPoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowSeasons = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowTrailerURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tvShowStorage", x => x.tvShowId);
                });
        }
    }
}
