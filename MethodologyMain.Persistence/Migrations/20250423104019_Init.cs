using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodologyMain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mainSchema");

            migrationBuilder.CreateTable(
                name: "organization",
                schema: "mainSchema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    linkToWebsite = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                schema: "mainSchema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tagName = table.Column<string>(type: "text", nullable: false),
                    tagClassName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teamMember",
                schema: "mainSchema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    birthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    education = table.Column<string>(type: "text", nullable: false),
                    firstName = table.Column<string>(type: "text", nullable: false),
                    lastName = table.Column<string>(type: "text", nullable: false),
                    middleName = table.Column<string>(type: "text", nullable: false),
                    userName = table.Column<string>(type: "text", nullable: false),
                    Telegram = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teamMember", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hackathon",
                schema: "mainSchema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    organizationId = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    prize = table.Column<decimal>(type: "numeric", nullable: false),
                    minTeamSize = table.Column<int>(type: "integer", nullable: false),
                    maxTeamSize = table.Column<int>(type: "integer", nullable: false),
                    startDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    endDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    additionalInfo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hackathon", x => x.id);
                    table.ForeignKey(
                        name: "FK_hackathon_organization_organizationId",
                        column: x => x.organizationId,
                        principalSchema: "mainSchema",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userTag",
                schema: "mainSchema",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uuid", nullable: false),
                    tagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userTag", x => new { x.tagId, x.userId });
                    table.ForeignKey(
                        name: "FK_userTag_tag_tagId",
                        column: x => x.tagId,
                        principalSchema: "mainSchema",
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userTag_teamMember_userId",
                        column: x => x.userId,
                        principalSchema: "mainSchema",
                        principalTable: "teamMember",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hackthonTag",
                columns: table => new
                {
                    hackathonId = table.Column<Guid>(type: "uuid", nullable: false),
                    tagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hackthonTag", x => new { x.tagId, x.hackathonId });
                    table.ForeignKey(
                        name: "FK_hackthonTag_hackathon_hackathonId",
                        column: x => x.hackathonId,
                        principalSchema: "mainSchema",
                        principalTable: "hackathon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hackthonTag_tag_tagId",
                        column: x => x.tagId,
                        principalSchema: "mainSchema",
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team",
                schema: "mainSchema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hackathonId = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    captainId = table.Column<Guid>(type: "uuid", nullable: false),
                    teamCreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team", x => x.id);
                    table.ForeignKey(
                        name: "FK_team_hackathon_hackathonId",
                        column: x => x.hackathonId,
                        principalSchema: "mainSchema",
                        principalTable: "hackathon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "track",
                schema: "mainSchema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hackathonId = table.Column<Guid>(type: "uuid", nullable: false),
                    trackName = table.Column<string>(type: "text", nullable: false),
                    trackAdditionalInfo = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "userTeam",
                schema: "mainSchema",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uuid", nullable: false),
                    teamId = table.Column<Guid>(type: "uuid", nullable: false),
                    joinedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userTeam", x => new { x.userId, x.teamId });
                    table.ForeignKey(
                        name: "FK_userTeam_teamMember_userId",
                        column: x => x.userId,
                        principalSchema: "mainSchema",
                        principalTable: "teamMember",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userTeam_team_teamId",
                        column: x => x.teamId,
                        principalSchema: "mainSchema",
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hackathon_organizationId",
                schema: "mainSchema",
                table: "hackathon",
                column: "organizationId");

            migrationBuilder.CreateIndex(
                name: "IX_hackthonTag_hackathonId",
                table: "hackthonTag",
                column: "hackathonId");

            migrationBuilder.CreateIndex(
                name: "IX_team_hackathonId",
                schema: "mainSchema",
                table: "team",
                column: "hackathonId");

            migrationBuilder.CreateIndex(
                name: "IX_track_hackathonId",
                schema: "mainSchema",
                table: "track",
                column: "hackathonId");

            migrationBuilder.CreateIndex(
                name: "IX_userTag_userId",
                schema: "mainSchema",
                table: "userTag",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_userTeam_teamId",
                schema: "mainSchema",
                table: "userTeam",
                column: "teamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hackthonTag");

            migrationBuilder.DropTable(
                name: "track",
                schema: "mainSchema");

            migrationBuilder.DropTable(
                name: "userTag",
                schema: "mainSchema");

            migrationBuilder.DropTable(
                name: "userTeam",
                schema: "mainSchema");

            migrationBuilder.DropTable(
                name: "tag",
                schema: "mainSchema");

            migrationBuilder.DropTable(
                name: "teamMember",
                schema: "mainSchema");

            migrationBuilder.DropTable(
                name: "team",
                schema: "mainSchema");

            migrationBuilder.DropTable(
                name: "hackathon",
                schema: "mainSchema");

            migrationBuilder.DropTable(
                name: "organization",
                schema: "mainSchema");
        }
    }
}
