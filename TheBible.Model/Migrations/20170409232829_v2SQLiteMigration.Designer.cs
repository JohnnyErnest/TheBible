using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TheBible.Model.DataLoader;

namespace TheBible.Model.Migrations
{
    [DbContext(typeof(SQLitePrefs))]
    [Migration("20170409232829_v2SQLiteMigration")]
    partial class v2SQLiteMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("TheBible.Model.DataLoader.SQLDataLoader+Books", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookIndex");

                    b.Property<string>("BookName");

                    b.Property<string>("BookShortName");

                    b.Property<string>("Testament");

                    b.Property<int>("TotalChapters");

                    b.Property<long>("TranslationID");

                    b.HasKey("ID");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("TheBible.Model.DataLoader.SQLDataLoader+Translations", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TranslationLongName");

                    b.Property<string>("TranslationShortName");

                    b.HasKey("ID");

                    b.ToTable("Translation");
                });

            modelBuilder.Entity("TheBible.Model.DataLoader.SQLDataLoader+Verses", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("BookID");

                    b.Property<int>("ChapterIndex");

                    b.Property<int>("VerseIndex");

                    b.Property<string>("VerseText");

                    b.HasKey("ID");

                    b.ToTable("Verse");
                });
        }
    }
}
