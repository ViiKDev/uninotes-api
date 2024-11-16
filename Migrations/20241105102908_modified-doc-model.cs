using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniNotesAPI.Migrations
{
    public partial class modifieddocmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Documents",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Documents",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Documents",
                type: "TEXT",
                nullable: true);
        }
    }
}
