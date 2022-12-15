using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolPracticeDB.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearPublished = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseName", "Credits" },
                values: new object[,]
                {
                    { 1, "Math 1", 2 },
                    { 2, "English Writing", 1 },
                    { 3, "Network", 3 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "DateOfBirth", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2010, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "fbrown@gmail.com", "Frank", "Brown" },
                    { 2, new DateTime(2011, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "CrazyLaura@gmail.com", "Laura", "White" },
                    { 3, new DateTime(2012, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "thebrampton@gmail.com", "Brian", "Brampton" },
                    { 4, new DateTime(1988, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "thompsonsarah@gmail.com", "Sarah", "Thompson" },
                    { 5, new DateTime(2001, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "bobgrownie223@gmail.com", "Bobby", "Grownie" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "CourseId", "Title", "YearPublished" },
                values: new object[,]
                {
                    { 1, "Dave Brooks", 1, "Math 101", 2019 },
                    { 2, "William Books", 2, "English Writing for dummies", 2009 },
                    { 3, "Bumba York", 2, "Holy Stuff", 2011 },
                    { 4, "Dave Brooks", 1, "Crazy George", 2020 },
                    { 5, "Dave Brooks", 3, "Huehue 1999", 2011 }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "CourseId", "StudentId", "Grade" },
                values: new object[,]
                {
                    { 1, 1, 67 },
                    { 3, 1, 87 },
                    { 1, 2, 93 },
                    { 2, 3, 67 },
                    { 3, 4, 65 },
                    { 2, 5, 57 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CourseId",
                table: "Books",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
