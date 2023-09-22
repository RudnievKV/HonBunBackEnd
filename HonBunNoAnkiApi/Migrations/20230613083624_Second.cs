using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HonbunNoAnkiApi.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartSRSDate",
                table: "Words",
                newName: "StartInitialSRSDate");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartCurrentSRSDate",
                table: "Words",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartCurrentSRSDate",
                table: "Words");

            migrationBuilder.RenameColumn(
                name: "StartInitialSRSDate",
                table: "Words",
                newName: "StartSRSDate");
        }
    }
}
