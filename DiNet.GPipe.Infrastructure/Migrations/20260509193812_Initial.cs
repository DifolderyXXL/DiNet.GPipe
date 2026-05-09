using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiNet.GPipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                });

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

            migrationBuilder.CreateTable(
                name: "CommitEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    Hash = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BuildVersion_alpha = table.Column<int>(type: "INTEGER", nullable: false),
                    BuildVersion_beta = table.Column<int>(type: "INTEGER", nullable: false),
                    BuildVersion_release = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommitEntries_Projects_ProjectId",
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

            migrationBuilder.CreateTable(
                name: "Builds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommitId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BuildType = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    ErrorText = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    ApkUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Builds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Builds_CommitEntries_CommitId",
                        column: x => x.CommitId,
                        principalTable: "CommitEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommitId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompletedSuccessfully = table.Column<bool>(type: "INTEGER", nullable: false),
                    ErrorText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestEntries_CommitEntries_CommitId",
                        column: x => x.CommitId,
                        principalTable: "CommitEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchWatcherConfigs_ProjectId",
                table: "BranchWatcherConfigs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildRegistries_ProjectId",
                table: "BuildRegistries",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Builds_CommitId",
                table: "Builds",
                column: "CommitId");

            migrationBuilder.CreateIndex(
                name: "IX_CommitEntries_ProjectId",
                table: "CommitEntries",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_GitUrl",
                table: "Projects",
                column: "GitUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name",
                table: "Projects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestEntries_CommitId",
                table: "TestEntries",
                column: "CommitId");

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
                name: "BuildRegistries");

            migrationBuilder.DropTable(
                name: "Builds");

            migrationBuilder.DropTable(
                name: "TestEntries");

            migrationBuilder.DropTable(
                name: "WatcherSettings");

            migrationBuilder.DropTable(
                name: "CommitEntries");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
