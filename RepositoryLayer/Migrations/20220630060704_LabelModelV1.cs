using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class LabelModelV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabelNames",
                columns: table => new
                {
                    LabelNameId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LabelName = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelNames", x => x.LabelNameId);
                    table.ForeignKey(
                        name: "FK_LabelNames_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabelNotes",
                columns: table => new
                {
                    LabelNoteId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LabelNameId = table.Column<int>(nullable: false),
                    NoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelNotes", x => x.LabelNoteId);
                    table.ForeignKey(
                        name: "FK_LabelNotes_LabelNames_LabelNameId",
                        column: x => x.LabelNameId,
                        principalTable: "LabelNames",
                        principalColumn: "LabelNameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabelNotes_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabelNames_UserId",
                table: "LabelNames",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelNotes_LabelNameId",
                table: "LabelNotes",
                column: "LabelNameId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelNotes_NoteId",
                table: "LabelNotes",
                column: "NoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabelNotes");

            migrationBuilder.DropTable(
                name: "LabelNames");
        }
    }
}
