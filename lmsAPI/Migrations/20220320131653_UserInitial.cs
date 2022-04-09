using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lmsAPI.Migrations
{
    public partial class UserInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    email = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    passwordHash = table.Column<byte[]>(type: "bytea", nullable: true),
                    passwordSalt = table.Column<byte[]>(type: "bytea", nullable: true),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    jobtitle_id = table.Column<int>(type: "integer", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    birthdate = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    progress = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.email);
                    table.ForeignKey(
                        name: "FK_user_job_titles_jobtitle_id",
                        column: x => x.jobtitle_id,
                        principalTable: "job_titles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_jobtitle_id",
                table: "user",
                column: "jobtitle_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
