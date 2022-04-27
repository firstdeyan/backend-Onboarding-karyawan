using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace lmsAPI.Migrations
{
    public partial class activitiesownedinitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activities_owned",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_email = table.Column<string>(type: "text", nullable: false),
                    activities_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<string>(type: "text", nullable: false),
                    end_date = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    validated = table.Column<bool>(type: "boolean", nullable: false),
                    mentor_email = table.Column<string>(type: "text", nullable: false),
                    activity_note = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activities_owned", x => x.id);
                    table.ForeignKey(
                        name: "FK_activities_owned_activities_activities_id",
                        column: x => x.activities_id,
                        principalTable: "activities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_activities_owned_user_user_email",
                        column: x => x.user_email,
                        principalTable: "user",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activities_owned_activities_id",
                table: "activities_owned",
                column: "activities_id");

            migrationBuilder.CreateIndex(
                name: "IX_activities_owned_user_email",
                table: "activities_owned",
                column: "user_email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activities_owned");
        }
    }
}
