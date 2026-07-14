using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexoraAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseNameAndDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "courses");
        }
    }
}
