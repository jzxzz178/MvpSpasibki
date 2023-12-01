using System.Net.Mime;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvpSpasibki.Db;

public class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Spasibki",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                From = table.Column<string>(type: "TEXT", nullable: true),
                To = table.Column<string>(type: "TEXT", nullable: true),
                Text = table.Column<string>(type: "TEXT", nullable: true)
            });
    }
}