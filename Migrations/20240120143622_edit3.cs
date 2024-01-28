using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewAgeHS.Migrations
{
    /// <inheritdoc />
    public partial class edit3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FKStudentID",
                table: "ReportCard",
                newName: "FKStudID");

            migrationBuilder.RenameIndex(
                name: "IX_ReportCard_FKStudentID",
                table: "ReportCard",
                newName: "IX_ReportCard_FKStudID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FKStudID",
                table: "ReportCard",
                newName: "FKStudentID");

            migrationBuilder.RenameIndex(
                name: "IX_ReportCard_FKStudID",
                table: "ReportCard",
                newName: "IX_ReportCard_FKStudentID");
        }
    }
}
