using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApi.Migrations
{
    public partial class ChangenamepropQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StrQuestionTxt",
                table: "Question",
                newName: "StrQuestion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StrQuestion",
                table: "Question",
                newName: "StrQuestionTxt");
        }
    }
}
