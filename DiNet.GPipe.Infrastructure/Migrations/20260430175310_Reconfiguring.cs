using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiNet.GPipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Reconfiguring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BranchWatcherConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    BranchName = table.Column<string>(type: "TEXT", nullable: false),
                    VersionType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchWatcherConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchWatcherConfigs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatcherSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastGlobalCheck = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PollInterval = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatcherSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatcherSettings_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchWatcherConfigs_ProjectId",
                table: "BranchWatcherConfigs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WatcherSettings_ProjectId",
                table: "WatcherSettings",
                column: "ProjectId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchWatcherConfigs");

            migrationBuilder.DropTable(
                name: "WatcherSettings");
        }
    }
}
