using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodologyMain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TeamEntityFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "maxMembers",
                schema: "mainSchema",
                table: "team",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maxMembers",
                schema: "mainSchema",
                table: "team");
        }
    }
}
