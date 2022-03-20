using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lmsAPI.Migrations
{
    public partial class adminRoleRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    email = table.Column<string>(type: "text", nullable: false),
                    admin_name = table.Column<string>(type: "text", nullable: false),
                    passwordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    passwordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.email);
                    table.ForeignKey(
                        name: "FK_admin_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_admin_role_id",
                table: "admin",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");
        }
    }
}
