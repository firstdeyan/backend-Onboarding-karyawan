using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace lmsAPI.Migrations
{
    public partial class DetialInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activity_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    activity_id = table.Column<int>(type: "integer", nullable: false),
                    detail_name = table.Column<string>(type: "text", nullable: false),
                    detail_desc = table.Column<string>(type: "text", nullable: false),
                    detail_link = table.Column<string>(type: "text", nullable: false),
                    detail_type = table.Column<string>(type: "text", nullable: false),
                    detail_urutan = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activity_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_activity_details_activities_activity_id",
                        column: x => x.activity_id,
                        principalTable: "activities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activity_details_activity_id",
                table: "activity_details",
                column: "activity_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activity_details");
        }
    }
}
