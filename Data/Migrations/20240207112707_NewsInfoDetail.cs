using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBBulletin.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewsInfoDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BreakingNews",
                table: "Newsinfo",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DatePublished",
                table: "Newsinfo",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditorsPick",
                table: "Newsinfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Newsinfo",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickOfMonth",
                table: "Newsinfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReadingDuration",
                table: "Newsinfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TrendingNow",
                table: "Newsinfo",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakingNews",
                table: "Newsinfo");

            migrationBuilder.DropColumn(
                name: "DatePublished",
                table: "Newsinfo");

            migrationBuilder.DropColumn(
                name: "EditorsPick",
                table: "Newsinfo");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Newsinfo");

            migrationBuilder.DropColumn(
                name: "PickOfMonth",
                table: "Newsinfo");

            migrationBuilder.DropColumn(
                name: "ReadingDuration",
                table: "Newsinfo");

            migrationBuilder.DropColumn(
                name: "TrendingNow",
                table: "Newsinfo");
        }
    }
}
