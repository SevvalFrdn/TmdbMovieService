using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TmdbMovie.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    RowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    adult = table.Column<bool>(type: "bit", nullable: true),
                    backdrop_path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    genre_ids = table.Column<int>(type: "int", nullable: true),
                    id = table.Column<int>(type: "int", nullable: true),
                    original_language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    original_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    overview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    popularity = table.Column<float>(type: "real", nullable: true),
                    poster_path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    release_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    video = table.Column<bool>(type: "bit", nullable: true),
                    vote_average = table.Column<float>(type: "real", nullable: true),
                    vote_count = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.RowId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
