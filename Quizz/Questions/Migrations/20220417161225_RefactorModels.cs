using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questions.Migrations
{
    public partial class RefactorModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CorrectAnswerIds",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CorrectIdOrder",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswerIds",
                table: "Question",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorrectIdOrder",
                table: "Question",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
