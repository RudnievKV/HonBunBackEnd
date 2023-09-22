using Microsoft.EntityFrameworkCore.Migrations;

namespace HonbunNoAnkiApi.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeaningReadings_Words_Word_ID",
                table: "MeaningReadings");

            migrationBuilder.AddForeignKey(
                name: "FK_MeaningReadings_Words_Word_ID",
                table: "MeaningReadings",
                column: "Word_ID",
                principalTable: "Words",
                principalColumn: "Word_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeaningReadings_Words_Word_ID",
                table: "MeaningReadings");

            migrationBuilder.AddForeignKey(
                name: "FK_MeaningReadings_Words_Word_ID",
                table: "MeaningReadings",
                column: "Word_ID",
                principalTable: "Words",
                principalColumn: "Word_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
