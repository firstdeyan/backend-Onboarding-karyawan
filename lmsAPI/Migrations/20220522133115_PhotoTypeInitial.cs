using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lmsAPI.Migrations
{
    public partial class PhotoTypeInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cover",
                table: "user",
                newName: "photo");

            migrationBuilder.AddColumn<string>(
                name: "photo",
                table: "admin",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo",
                table: "admin");

            migrationBuilder.RenameColumn(
                name: "photo",
                table: "user",
                newName: "cover");
        }
    }
}
