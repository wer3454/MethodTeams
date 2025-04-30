using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodologyMain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "track",
                schema: "mainSchema");

            migrationBuilder.RenameColumn(
                name: "Telegram",
                schema: "mainSchema",
                table: "teamMember",
                newName: "telegram");

            migrationBuilder.RenameColumn(
                name: "linkToWebsite",
                schema: "mainSchema",
                table: "organization",
                newName: "logo");

            migrationBuilder.RenameColumn(
                name: "additionalInfo",
                schema: "mainSchema",
                table: "hackathon",
                newName: "website");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                schema: "mainSchema",
                table: "teamMember",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "github",
                schema: "mainSchema",
                table: "teamMember",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "location",
                schema: "mainSchema",
                table: "teamMember",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "photoUrl",
                schema: "mainSchema",
                table: "teamMember",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "skills",
                schema: "mainSchema",
                table: "teamMember",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "website",
                schema: "mainSchema",
                table: "teamMember",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "startDate",
                schema: "mainSchema",
                table: "hackathon",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "prize",
                schema: "mainSchema",
                table: "hackathon",
                type: "text",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "endDate",
                schema: "mainSchema",
                table: "hackathon",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "mainSchema",
                table: "hackathon",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                schema: "mainSchema",
                table: "hackathon",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "location",
                schema: "mainSchema",
                table: "hackathon",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "schedule",
                schema: "mainSchema",
                table: "hackathon",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "teamTag",
                schema: "mainSchema",
                columns: table => new
                {
                    teamId = table.Column<Guid>(type: "uuid", nullable: false),
                    tagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teamTag", x => new { x.tagId, x.teamId });
                    table.ForeignKey(
                        name: "FK_teamTag_tag_tagId",
                        column: x => x.tagId,
                        principalSchema: "mainSchema",
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_teamTag_team_teamId",
                        column: x => x.teamId,
                        principalSchema: "mainSchema",
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_teamTag_teamId",
                schema: "mainSchema",
                table: "teamTag",
                column: "teamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "teamTag",
                schema: "mainSchema");

            migrationBuilder.DropColumn(
                name: "createdAt",
                schema: "mainSchema",
                table: "teamMember");

            migrationBuilder.DropColumn(
                name: "github",
                schema: "mainSchema",
                table: "teamMember");

            migrationBuilder.DropColumn(
                name: "location",
                schema: "mainSchema",
                table: "teamMember");

            migrationBuilder.DropColumn(
                name: "photoUrl",
                schema: "mainSchema",
                table: "teamMember");

            migrationBuilder.DropColumn(
                name: "skills",
                schema: "mainSchema",
                table: "teamMember");

            migrationBuilder.DropColumn(
                name: "website",
                schema: "mainSchema",
                table: "teamMember");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "mainSchema",
                table: "hackathon");

            migrationBuilder.DropColumn(
                name: "imageUrl",
                schema: "mainSchema",
                table: "hackathon");

            migrationBuilder.DropColumn(
                name: "location",
                schema: "mainSchema",
                table: "hackathon");

            migrationBuilder.DropColumn(
                name: "schedule",
                schema: "mainSchema",
                table: "hackathon");

            migrationBuilder.RenameColumn(
                name: "telegram",
                schema: "mainSchema",
                table: "teamMember",
                newName: "Telegram");

            migrationBuilder.RenameColumn(
                name: "logo",
                schema: "mainSchema",
                table: "organization",
                newName: "linkToWebsite");

            migrationBuilder.RenameColumn(
                name: "website",
                schema: "mainSchema",
                table: "hackathon",
                newName: "additionalInfo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "startDate",
                schema: "mainSchema",
                table: "hackathon",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<decimal>(
                name: "prize",
                schema: "mainSchema",
                table: "hackathon",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "endDate",
                schema: "mainSchema",
                table: "hackathon",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "track",
                schema: "mainSchema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hackathonId = table.Column<Guid>(type: "uuid", nullable: false),
                    trackAdditionalInfo = table.Column<string>(type: "text", nullable: false),
                    trackName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_track", x => x.id);
                    table.ForeignKey(
                        name: "FK_track_hackathon_hackathonId",
                        column: x => x.hackathonId,
                        principalSchema: "mainSchema",
                        principalTable: "hackathon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_track_hackathonId",
                schema: "mainSchema",
                table: "track",
                column: "hackathonId");
        }
    }
}
