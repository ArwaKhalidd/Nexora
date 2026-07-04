using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexoraAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    code_module = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    code_presentation = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => new { x.code_module, x.code_presentation });
                });

            migrationBuilder.CreateTable(
                name: "assessments",
                columns: table => new
                {
                    id_assessment = table.Column<int>(type: "int", nullable: false),
                    code_module = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    code_presentation = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    assessment_type = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    date = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__assessme__0A74B8277284752F", x => x.id_assessment);
                    table.ForeignKey(
                        name: "FK__assessments__398D8EEE",
                        columns: x => new { x.code_module, x.code_presentation },
                        principalTable: "courses",
                        principalColumns: new[] { "code_module", "code_presentation" });
                });

            migrationBuilder.CreateTable(
                name: "studentInfo",
                columns: table => new
                {
                    code_module = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    code_presentation = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    id_student = table.Column<int>(type: "int", nullable: false),
                    gender = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    imd_band = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    highest_education = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    age_band = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    num_of_prev_attempts = table.Column<int>(type: "int", nullable: true),
                    studied_credits = table.Column<int>(type: "int", nullable: true),
                    region = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    disability = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    final_result = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentInfo", x => new { x.code_module, x.code_presentation, x.id_student });
                    table.UniqueConstraint("AK_studentInfo_id_student", x => x.id_student);
                    table.ForeignKey(
                        name: "FK__studentInfo__3F466844",
                        columns: x => new { x.code_module, x.code_presentation },
                        principalTable: "courses",
                        principalColumns: new[] { "code_module", "code_presentation" });
                });

            migrationBuilder.CreateTable(
                name: "vle",
                columns: table => new
                {
                    id_site = table.Column<int>(type: "int", nullable: false),
                    code_module = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    code_presentation = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    activity_type = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    week_from = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    week_to = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__vle__4594B5E6AC690155", x => x.id_site);
                    table.ForeignKey(
                        name: "FK__vle__3C69FB99",
                        columns: x => new { x.code_module, x.code_presentation },
                        principalTable: "courses",
                        principalColumns: new[] { "code_module", "code_presentation" });
                });

            migrationBuilder.CreateTable(
                name: "studentAssessment",
                columns: table => new
                {
                    id_student = table.Column<int>(type: "int", nullable: true),
                    id_assessment = table.Column<int>(type: "int", nullable: true),
                    date_submitted = table.Column<int>(type: "int", nullable: true),
                    is_banked = table.Column<byte>(type: "tinyint", nullable: true),
                    score = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__studentAs__id_as__4316F928",
                        column: x => x.id_assessment,
                        principalTable: "assessments",
                        principalColumn: "id_assessment");
                });

            migrationBuilder.CreateTable(
                name: "studentRegistration",
                columns: table => new
                {
                    code_module = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    code_presentation = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    id_student = table.Column<int>(type: "int", nullable: true),
                    date_registration = table.Column<int>(type: "int", nullable: true),
                    date_unregistration = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__studentRegistrat__412EB0B6",
                        columns: x => new { x.code_module, x.code_presentation, x.id_student },
                        principalTable: "studentInfo",
                        principalColumns: new[] { "code_module", "code_presentation", "id_student" });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_studentInfo_StudentId",
                        column: x => x.StudentId,
                        principalTable: "studentInfo",
                        principalColumn: "id_student",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "studentVle",
                columns: table => new
                {
                    code_module = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    code_presentation = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    id_student = table.Column<int>(type: "int", nullable: true),
                    id_site = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<int>(type: "int", nullable: true),
                    sum_click = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__studentVl__id_si__45F365D3",
                        column: x => x.id_site,
                        principalTable: "vle",
                        principalColumn: "id_site");
                    table.ForeignKey(
                        name: "FK__studentVle__44FF419A",
                        columns: x => new { x.code_module, x.code_presentation, x.id_student },
                        principalTable: "studentInfo",
                        principalColumns: new[] { "code_module", "code_presentation", "id_student" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_assessments_code_module_code_presentation",
                table: "assessments",
                columns: new[] { "code_module", "code_presentation" });

            migrationBuilder.CreateIndex(
                name: "IX_studentAssessment_id_assessment",
                table: "studentAssessment",
                column: "id_assessment");

            migrationBuilder.CreateIndex(
                name: "IX_studentRegistration_code_module_code_presentation_id_student",
                table: "studentRegistration",
                columns: new[] { "code_module", "code_presentation", "id_student" });

            migrationBuilder.CreateIndex(
                name: "IX_studentVle_code_module_code_presentation_id_student",
                table: "studentVle",
                columns: new[] { "code_module", "code_presentation", "id_student" });

            migrationBuilder.CreateIndex(
                name: "IX_studentVle_id_site",
                table: "studentVle",
                column: "id_site");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StudentId",
                table: "Users",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vle_code_module_code_presentation",
                table: "vle",
                columns: new[] { "code_module", "code_presentation" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "studentAssessment");

            migrationBuilder.DropTable(
                name: "studentRegistration");

            migrationBuilder.DropTable(
                name: "studentVle");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "assessments");

            migrationBuilder.DropTable(
                name: "vle");

            migrationBuilder.DropTable(
                name: "studentInfo");

            migrationBuilder.DropTable(
                name: "courses");
        }
    }
}
