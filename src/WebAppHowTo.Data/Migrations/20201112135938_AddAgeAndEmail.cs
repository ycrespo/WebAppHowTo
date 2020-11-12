using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppHowTo.Data.Migrations
{
    public partial class AddAgeAndEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Practices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Practices",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Practices");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Practices");
        }
    }
}
