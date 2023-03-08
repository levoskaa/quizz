using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questions.Migrations
{
    public partial class RefactorModels2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Answer");

            migrationBuilder.AddColumn<bool>(
                name: "CorrectAnswer",
                table: "Question",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "Question");

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Answer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Answer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
