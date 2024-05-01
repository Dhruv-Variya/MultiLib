using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiLib.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "analysis",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timesClicked = table.Column<int>(type: "int", nullable: true),
                    upVote = table.Column<int>(type: "int", nullable: true),
                    downVote = table.Column<int>(type: "int", nullable: true),
                    trailerReach = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analysis", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "animeStorage",
                columns: table => new
                {
                    animeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    animeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeCatagory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeCast = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeLanguages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeTrailerURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeSeasons = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animePoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    animeBackPoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isUpcoming = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animeStorage", x => x.animeId);
                });

            migrationBuilder.CreateTable(
                name: "Catagory",
                columns: table => new
                {
                    catagoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatagoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catagory", x => x.catagoryId);
                });

            migrationBuilder.CreateTable(
                name: "itemCatagories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    catagoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemCatagories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "itemLanguages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    languageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemLanguages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "languages",
                columns: table => new
                {
                    languageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    languageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_languages", x => x.languageId);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cast = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<float>(type: "real", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TreailerURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoviePoster = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "movieStorage",
                columns: table => new
                {
                    movieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    movieCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    movieTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    movieCast = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    movieRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    movieReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    movieTrailerURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    movieDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    moviePoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    movieBackPoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isUpcoming = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movieStorage", x => x.movieId);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    movie_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    movie_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tvShowStorage",
                columns: table => new
                {
                    tvShowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tvShowCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowCatagory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowCast = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowLanguages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowTrailerURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowSeasons = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowPoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tvShowBackPoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isUpcoming = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tvShowStorage", x => x.tvShowId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analysis");

            migrationBuilder.DropTable(
                name: "animeStorage");

            migrationBuilder.DropTable(
                name: "Catagory");

            migrationBuilder.DropTable(
                name: "itemCatagories");

            migrationBuilder.DropTable(
                name: "itemLanguages");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "movieStorage");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "tvShowStorage");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
