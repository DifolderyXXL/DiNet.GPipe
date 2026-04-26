using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiNet.GPipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    GitUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.UniqueConstraint("AK_Projects_GitUrl", x => x.GitUrl);
                });

            migrationBuilder.CreateTable(
                name: "BuildRegistries",
                columns: table => new
                {
                    CommitHash = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    CommitDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_alpha = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_beta = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_release = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildRegistries", x => x.CommitHash);
                    table.ForeignKey(
                        name: "FK_BuildRegistries_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildRegistries_ProjectId",
                table: "BuildRegistries",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildRegistries");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
