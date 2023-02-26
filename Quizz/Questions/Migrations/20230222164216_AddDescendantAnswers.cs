using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questions.Migrations
{
    public partial class AddDescendantAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnswerType",
                table: "Answer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CorrectIndex",
                table: "Answer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisplayIndex",
                table: "Answer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Answer",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerType",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "CorrectIndex",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "DisplayIndex",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Answer");
        }
    }
}
