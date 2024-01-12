using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewAgeHS.Migrations
{
    /// <inheritdoc />
    public partial class edit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Students",
                newName: "StudID");

            migrationBuilder.RenameColumn(
                name: "EmpID",
                table: "Staff",
                newName: "EmployeeID");

            migrationBuilder.RenameColumn(
                name: "FKEmpID",
                table: "Course",
                newName: "FKEmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_Course_FKEmpID",
                table: "Course",
                newName: "IX_Course_FKEmployeeID");

            migrationBuilder.AlterColumn<int>(
                name: "StudID",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "Staff",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudID",
                table: "Students",
                newName: "StudentID");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "Staff",
                newName: "EmpID");

            migrationBuilder.RenameColumn(
                name: "FKEmployeeID",
                table: "Course",
                newName: "FKEmpID");

            migrationBuilder.RenameIndex(
                name: "IX_Course_FKEmployeeID",
                table: "Course",
                newName: "IX_Course_FKEmpID");

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "EmpID",
                table: "Staff",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
