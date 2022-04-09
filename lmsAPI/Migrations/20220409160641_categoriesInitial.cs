using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace lmsAPI.Migrations
{
    public partial class categoriesInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_admin_roles_role_id",
                table: "admin");

            migrationBuilder.DropForeignKey(
                name: "FK_user_job_titles_jobtitle_id",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_role_id",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "jobtitle_decription",
                table: "job_titles",
                newName: "jobtitle_description");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "jobtitle_id",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "birthdate",
                table: "user",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "admin",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<string>(type: "text", nullable: false),
                    category_description = table.Column<string>(type: "text", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_admin_roles_role_id",
                table: "admin",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_job_titles_jobtitle_id",
                table: "user",
                column: "jobtitle_id",
                principalTable: "job_titles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_role_id",
                table: "user",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_admin_roles_role_id",
                table: "admin");

            migrationBuilder.DropForeignKey(
                name: "FK_user_job_titles_jobtitle_id",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_role_id",
                table: "user");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.RenameColumn(
                name: "jobtitle_description",
                table: "job_titles",
                newName: "jobtitle_decription");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "user",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "jobtitle_id",
                table: "user",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "birthdate",
                table: "user",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "admin",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_admin_roles_role_id",
                table: "admin",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_job_titles_jobtitle_id",
                table: "user",
                column: "jobtitle_id",
                principalTable: "job_titles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_role_id",
                table: "user",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id");
        }
    }
}
