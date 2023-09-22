using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HonbunNoAnkiApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Stage_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    StageNumber = table.Column<int>(type: "int", nullable: false),
                    StageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Stage_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentExperience = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "WordCollections",
                columns: table => new
                {
                    WordCollection_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    User_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordCollections", x => x.WordCollection_ID);
                    table.ForeignKey(
                        name: "FK_WordCollections_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Word_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordCollection_ID = table.Column<long>(type: "bigint", nullable: false),
                    Stage_ID = table.Column<long>(type: "bigint", nullable: true),
                    IsInSRS = table.Column<bool>(type: "bit", nullable: false),
                    StartSRSDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Word_ID);
                    table.ForeignKey(
                        name: "FK_Words_Stages_Stage_ID",
                        column: x => x.Stage_ID,
                        principalTable: "Stages",
                        principalColumn: "Stage_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Words_WordCollections_WordCollection_ID",
                        column: x => x.WordCollection_ID,
                        principalTable: "WordCollections",
                        principalColumn: "WordCollection_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeaningReadings",
                columns: table => new
                {
                    MeaningReading_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word_ID = table.Column<long>(type: "bigint", nullable: true),
                    PartOfSpeech = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Meaning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reading = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeaningReadings", x => x.MeaningReading_ID);
                    table.ForeignKey(
                        name: "FK_MeaningReadings_Words_Word_ID",
                        column: x => x.Word_ID,
                        principalTable: "Words",
                        principalColumn: "Word_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeaningReadings_Word_ID",
                table: "MeaningReadings",
                column: "Word_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WordCollections_User_ID",
                table: "WordCollections",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Words_Stage_ID",
                table: "Words",
                column: "Stage_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Words_WordCollection_ID",
                table: "Words",
                column: "WordCollection_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeaningReadings");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "WordCollections");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
