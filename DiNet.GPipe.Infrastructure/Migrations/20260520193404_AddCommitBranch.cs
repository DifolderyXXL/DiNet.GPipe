using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiNet.GPipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCommitBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "CommitEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Branch",
                table: "CommitEntries");
        }
    }
}
