using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBible.Model.Migrations
{
    public partial class v1SQLiteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookIndex = table.Column<int>(nullable: false),
                    BookName = table.Column<string>(nullable: true),
                    BookShortName = table.Column<string>(nullable: true),
                    Testament = table.Column<string>(nullable: true),
                    TotalChapters = table.Column<int>(nullable: false),
                    TranslationID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TranslationLongName = table.Column<string>(nullable: true),
                    TranslationShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Verse",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookID = table.Column<long>(nullable: false),
                    ChapterIndex = table.Column<int>(nullable: false),
                    VerseIndex = table.Column<int>(nullable: false),
                    VerseText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verse", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "Verse");
        }
    }
}
