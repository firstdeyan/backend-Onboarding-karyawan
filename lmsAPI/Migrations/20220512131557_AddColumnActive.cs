using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lmsAPI.Migrations
{
    public partial class AddColumnActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "user",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "assignedActivities",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "finishedActivities",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "admin",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "user");

            migrationBuilder.DropColumn(
                name: "assignedActivities",
                table: "user");

            migrationBuilder.DropColumn(
                name: "finishedActivities",
                table: "user");

            migrationBuilder.DropColumn(
                name: "active",
                table: "admin");
        }
    }
}
