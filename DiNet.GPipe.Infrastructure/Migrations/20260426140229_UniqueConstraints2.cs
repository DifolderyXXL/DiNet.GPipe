using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiNet.GPipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueConstraints2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Projects_GitUrl",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Projects_GitUrl",
                table: "Projects",
                column: "GitUrl");
        }
    }
}
