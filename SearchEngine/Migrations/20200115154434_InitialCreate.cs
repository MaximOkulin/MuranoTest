using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchEngine.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Business");

            migrationBuilder.EnsureSchema(
                name: "Dictionaries");

            migrationBuilder.CreateTable(
                name: "SearchEngineType",
                schema: "Dictionaries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchEngineType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Search",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(nullable: false),
                    KeyWords = table.Column<string>(nullable: false),
                    SearchEngineTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Search", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Search_SearchEngineType_SearchEngineTypeId",
                        column: x => x.SearchEngineTypeId,
                        principalSchema: "Dictionaries",
                        principalTable: "SearchEngineType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchResult",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchResult_Search_SearchId",
                        column: x => x.SearchId,
                        principalSchema: "Business",
                        principalTable: "Search",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Dictionaries",
                table: "SearchEngineType",
                columns: new[] { "Id", "Code", "Description" },
                values: new object[] { 1, "Google", "Поисковая система Google" });

            migrationBuilder.InsertData(
                schema: "Dictionaries",
                table: "SearchEngineType",
                columns: new[] { "Id", "Code", "Description" },
                values: new object[] { 2, "Yandex", "Поисковая система Яндекс" });

            migrationBuilder.InsertData(
                schema: "Dictionaries",
                table: "SearchEngineType",
                columns: new[] { "Id", "Code", "Description" },
                values: new object[] { 3, "Bing", "Поисковая система Bing" });

            migrationBuilder.CreateIndex(
                name: "IX_Search_SearchEngineTypeId",
                schema: "Business",
                table: "Search",
                column: "SearchEngineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchResult_SearchId",
                schema: "Business",
                table: "SearchResult",
                column: "SearchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchResult",
                schema: "Business");

            migrationBuilder.DropTable(
                name: "Search",
                schema: "Business");

            migrationBuilder.DropTable(
                name: "SearchEngineType",
                schema: "Dictionaries");
        }
    }
}
