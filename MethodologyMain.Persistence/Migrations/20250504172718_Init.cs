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
                    logo = table.Column<string>(type: "text", nullable: false)
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
                    email = table.Column<string>(type: "text", nullable: false),
                    userName = table.Column<string>(type: "text", nullable: false),
                    photoUrl = table.Column<string>(type: "text", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    telegram = table.Column<string>(type: "text", nullable: false),
                    github = table.Column<string>(type: "text", nullable: false),
                    website = table.Column<string>(type: "text", nullable: false),
                    skills = table.Column<string>(type: "jsonb", nullable: false)
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
                    description = table.Column<string>(type: "text", nullable: false),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    imageUrl = table.Column<string>(type: "text", nullable: false),
                    minTeamSize = table.Column<int>(type: "integer", nullable: false),
                    maxTeamSize = table.Column<int>(type: "integer", nullable: false),
                    website = table.Column<string>(type: "text", nullable: false),
                    prize = table.Column<string>(type: "jsonb", nullable: false),
                    schedule = table.Column<string>(type: "jsonb", nullable: false)
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
                name: "hackathonTag",
                schema: "mainSchema",
                columns: table => new
                {
                    hackathonId = table.Column<Guid>(type: "uuid", nullable: false),
                    tagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hackathonTag", x => new { x.tagId, x.hackathonId });
                    table.ForeignKey(
                        name: "FK_hackathonTag_hackathon_hackathonId",
                        column: x => x.hackathonId,
                        principalSchema: "mainSchema",
                        principalTable: "hackathon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hackathonTag_tag_tagId",
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
                    maxMembers = table.Column<int>(type: "integer", nullable: false),
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
                name: "IX_hackathonTag_hackathonId",
                schema: "mainSchema",
                table: "hackathonTag",
                column: "hackathonId");

            migrationBuilder.CreateIndex(
                name: "IX_team_hackathonId",
                schema: "mainSchema",
                table: "team",
                column: "hackathonId");

            migrationBuilder.CreateIndex(
                name: "IX_teamTag_teamId",
                schema: "mainSchema",
                table: "teamTag",
                column: "teamId");

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
                name: "hackathonTag",
                schema: "mainSchema");

            migrationBuilder.DropTable(
                name: "teamTag",
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
