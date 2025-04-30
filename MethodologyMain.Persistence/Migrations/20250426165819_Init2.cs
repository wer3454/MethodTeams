using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodologyMain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hackthonTag_hackathon_hackathonId",
                table: "hackthonTag");

            migrationBuilder.DropForeignKey(
                name: "FK_hackthonTag_tag_tagId",
                table: "hackthonTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hackthonTag",
                table: "hackthonTag");

            migrationBuilder.RenameTable(
                name: "hackthonTag",
                newName: "hackathonTag",
                newSchema: "mainSchema");

            migrationBuilder.RenameIndex(
                name: "IX_hackthonTag_hackathonId",
                schema: "mainSchema",
                table: "hackathonTag",
                newName: "IX_hackathonTag_hackathonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hackathonTag",
                schema: "mainSchema",
                table: "hackathonTag",
                columns: new[] { "tagId", "hackathonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_hackathonTag_hackathon_hackathonId",
                schema: "mainSchema",
                table: "hackathonTag",
                column: "hackathonId",
                principalSchema: "mainSchema",
                principalTable: "hackathon",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hackathonTag_tag_tagId",
                schema: "mainSchema",
                table: "hackathonTag",
                column: "tagId",
                principalSchema: "mainSchema",
                principalTable: "tag",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hackathonTag_hackathon_hackathonId",
                schema: "mainSchema",
                table: "hackathonTag");

            migrationBuilder.DropForeignKey(
                name: "FK_hackathonTag_tag_tagId",
                schema: "mainSchema",
                table: "hackathonTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hackathonTag",
                schema: "mainSchema",
                table: "hackathonTag");

            migrationBuilder.RenameTable(
                name: "hackathonTag",
                schema: "mainSchema",
                newName: "hackthonTag");

            migrationBuilder.RenameIndex(
                name: "IX_hackathonTag_hackathonId",
                table: "hackthonTag",
                newName: "IX_hackthonTag_hackathonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hackthonTag",
                table: "hackthonTag",
                columns: new[] { "tagId", "hackathonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_hackthonTag_hackathon_hackathonId",
                table: "hackthonTag",
                column: "hackathonId",
                principalSchema: "mainSchema",
                principalTable: "hackathon",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hackthonTag_tag_tagId",
                table: "hackthonTag",
                column: "tagId",
                principalSchema: "mainSchema",
                principalTable: "tag",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
