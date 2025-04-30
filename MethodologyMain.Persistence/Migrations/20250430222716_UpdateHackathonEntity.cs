using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodologyMain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHackathonEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "schedule",
                schema: "mainSchema",
                table: "hackathon",
                type: "jsonb USING \"schedule\"::jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "prize",
                schema: "mainSchema",
                table: "hackathon",
                type: "jsonb USING \"prize\"::jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "schedule",
                schema: "mainSchema",
                table: "hackathon",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "prize",
                schema: "mainSchema",
                table: "hackathon",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb");
        }
    }
}
