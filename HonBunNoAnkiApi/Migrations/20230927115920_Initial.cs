using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                    StartInitialSRSDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    StartCurrentSRSDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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
                        principalColumn: "Stage_ID");
                    table.ForeignKey(
                        name: "FK_Words_WordCollections_WordCollection_ID",
                        column: x => x.WordCollection_ID,
                        principalTable: "WordCollections",
                        principalColumn: "WordCollection_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordDefinitions",
                columns: table => new
                {
                    WordDefinition_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word_ID = table.Column<long>(type: "bigint", nullable: true),
                    OriginalEntry = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordDefinitions", x => x.WordDefinition_ID);
                    table.ForeignKey(
                        name: "FK_WordDefinitions_Words_Word_ID",
                        column: x => x.Word_ID,
                        principalTable: "Words",
                        principalColumn: "Word_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meanings",
                columns: table => new
                {
                    Meaning_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartOfSpeech = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WordDefinition_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meanings", x => x.Meaning_ID);
                    table.ForeignKey(
                        name: "FK_Meanings_WordDefinitions_WordDefinition_ID",
                        column: x => x.WordDefinition_ID,
                        principalTable: "WordDefinitions",
                        principalColumn: "WordDefinition_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Readings",
                columns: table => new
                {
                    Reading_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WordDefinition_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readings", x => x.Reading_ID);
                    table.ForeignKey(
                        name: "FK_Readings_WordDefinitions_WordDefinition_ID",
                        column: x => x.WordDefinition_ID,
                        principalTable: "WordDefinitions",
                        principalColumn: "WordDefinition_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeaningValues",
                columns: table => new
                {
                    MeaningValue_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Meaning_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeaningValues", x => x.MeaningValue_ID);
                    table.ForeignKey(
                        name: "FK_MeaningValues_Meanings_Meaning_ID",
                        column: x => x.Meaning_ID,
                        principalTable: "Meanings",
                        principalColumn: "Meaning_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meanings_WordDefinition_ID",
                table: "Meanings",
                column: "WordDefinition_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MeaningValues_Meaning_ID",
                table: "MeaningValues",
                column: "Meaning_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Readings_WordDefinition_ID",
                table: "Readings",
                column: "WordDefinition_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WordCollections_User_ID",
                table: "WordCollections",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WordDefinitions_Word_ID",
                table: "WordDefinitions",
                column: "Word_ID");

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
                name: "MeaningValues");

            migrationBuilder.DropTable(
                name: "Readings");

            migrationBuilder.DropTable(
                name: "Meanings");

            migrationBuilder.DropTable(
                name: "WordDefinitions");

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
