using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HrPlatformProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.CreateTable(
                name: "candidates",
                columns: table => new
                {
                    idcandidates = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_unicode_ci"),
                    dateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    contactNumber = table.Column<int>(type: "int", nullable: true),
                    email = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_unicode_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idcandidates);
                })
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "skills",
                columns: table => new
                {
                    idskills = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_unicode_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idskills);
                })
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "candidateskills",
                columns: table => new
                {
                    skills_idskills = table.Column<int>(type: "int", nullable: false),
                    candidates_idcandidates = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.skills_idskills, x.candidates_idcandidates })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_skills_has_candidates_candidates1",
                        column: x => x.candidates_idcandidates,
                        principalTable: "candidates",
                        principalColumn: "idcandidates");
                    table.ForeignKey(
                        name: "fk_skills_has_candidates_skills",
                        column: x => x.skills_idskills,
                        principalTable: "skills",
                        principalColumn: "idskills");
                })
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.InsertData(
                table: "candidates",
                columns: new[] { "idcandidates", "contactNumber", "dateOfBirth", "email", "name" },
                values: new object[,]
                {
                    { 1, 641234567, new DateOnly(2000, 3, 15), "marko@gmail.com", "Marko Markovic" },
                    { 2, 659876543, new DateOnly(1998, 7, 22), "ana@gmail.com", "Ana Jovanovic" }
                });

            migrationBuilder.InsertData(
                table: "skills",
                columns: new[] { "idskills", "name" },
                values: new object[,]
                {
                    { 1, "C# programming" },
                    { 2, "Java programming" },
                    { 3, "Database design" },
                    { 4, "English language" },
                    { 5, "Russian language" },
                    { 6, "German language" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_candidates_email",
                table: "candidates",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_skills_has_candidates_candidates1_idx",
                table: "candidateskills",
                column: "candidates_idcandidates");

            migrationBuilder.CreateIndex(
                name: "fk_skills_has_candidates_skills_idx",
                table: "candidateskills",
                column: "skills_idskills");

            migrationBuilder.CreateIndex(
                name: "IX_skills_name",
                table: "skills",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidateskills");

            migrationBuilder.DropTable(
                name: "candidates");

            migrationBuilder.DropTable(
                name: "skills");
        }
    }
}
