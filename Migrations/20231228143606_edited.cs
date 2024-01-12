using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewAgeHS.Migrations
{
    /// <inheritdoc />
    public partial class edited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    EmpID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Staff__AF2DBA79AD4BDA2D", x => x.EmpID);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    FKEmpID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Class_Staff",
                        column: x => x.FKEmpID,
                        principalTable: "Staff",
                        principalColumn: "EmpID");
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FKEmpID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Course__C92D718797872F69", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Course_Staff",
                        column: x => x.FKEmpID,
                        principalTable: "Staff",
                        principalColumn: "EmpID");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FKClassName = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    FKCourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Students__AA2FFB8510E92956", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Students_Class",
                        column: x => x.FKClassName,
                        principalTable: "Class",
                        principalColumn: "ClassName");
                    table.ForeignKey(
                        name: "FK_Students_Course",
                        column: x => x.FKCourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID");
                });

            migrationBuilder.CreateTable(
                name: "ReportCard",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Grades = table.Column<int>(type: "int", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "date", nullable: true),
                    FKStudentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCard", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK__ReportCar__FKPer__5DCAEF64",
                        column: x => x.FKStudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Class_FKEmpID",
                table: "Class",
                column: "FKEmpID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_FKEmpID",
                table: "Course",
                column: "FKEmpID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCard_FKStudentID",
                table: "ReportCard",
                column: "FKStudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FKClassName",
                table: "Students",
                column: "FKClassName");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FKCourseID",
                table: "Students",
                column: "FKCourseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportCard");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
