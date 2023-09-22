using Microsoft.EntityFrameworkCore.Migrations;

namespace HonbunNoAnkiApi.Migrations
{
    public partial class another : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginalEntry",
                table: "MeaningReadings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalEntry",
                table: "MeaningReadings");
        }
    }
}
