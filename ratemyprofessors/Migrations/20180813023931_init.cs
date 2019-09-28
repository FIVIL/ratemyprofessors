using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ratemyprofessors.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 20, nullable: true),
                    PassWord = table.Column<string>(maxLength: 20, nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    LastName = table.Column<string>(maxLength: 20, nullable: true),
                    Emain = table.Column<string>(maxLength: 60, nullable: true),
                    RegistarationDate = table.Column<DateTime>(nullable: false),
                    ISAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    MailAddress = table.Column<string>(maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(maxLength: 60, nullable: true),
                    Verified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: true),
                    LastName = table.Column<string>(maxLength: 40, nullable: true),
                    Link = table.Column<string>(maxLength: 200, nullable: true),
                    PrivateLink = table.Column<string>(maxLength: 200, nullable: true),
                    WPLink = table.Column<string>(maxLength: 200, nullable: true),
                    ImageLink = table.Column<string>(maxLength: 200, nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Staff = table.Column<bool>(nullable: false),
                    Approved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    ID = table.Column<byte>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ShowName = table.Column<string>(maxLength: 20, nullable: true),
                    Teaching = table.Column<byte>(nullable: false),
                    Marking = table.Column<byte>(nullable: false),
                    HomeWork = table.Column<byte>(nullable: false),
                    Project = table.Column<byte>(nullable: false),
                    Moods = table.Column<byte>(nullable: false),
                    RollCall = table.Column<byte>(nullable: false),
                    Exhausting = table.Column<byte>(nullable: false),
                    HandOuts = table.Column<byte>(nullable: false),
                    Update = table.Column<byte>(nullable: false),
                    ScapeAtTheEnd = table.Column<byte>(nullable: false),
                    Answering = table.Column<byte>(nullable: false),
                    HardExams = table.Column<byte>(nullable: false),
                    Knoledge = table.Column<byte>(nullable: false),
                    OverAll = table.Column<byte>(nullable: false),
                    Comments = table.Column<string>(maxLength: 399, nullable: true),
                    Like = table.Column<byte>(nullable: false),
                    DisLike = table.Column<byte>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Angry = table.Column<byte>(nullable: true),
                    Bluntess = table.Column<byte>(nullable: true),
                    DoYourWork = table.Column<byte>(nullable: true),
                    Bad = table.Column<byte>(nullable: true),
                    ProfessorID = table.Column<Guid>(nullable: false),
                    AccountID = table.Column<Guid>(nullable: true),
                    EmailID = table.Column<Guid>(nullable: false),
                    Verfied = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comments_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Emails_EmailID",
                        column: x => x.EmailID,
                        principalTable: "Emails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Professors_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    AliasName = table.Column<string>(maxLength: 20, nullable: true),
                    UniversityID = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Faculties_Universities_UniversityID",
                        column: x => x.UniversityID,
                        principalTable: "Universities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: true),
                    AliasNames = table.Column<string>(maxLength: 10, nullable: true),
                    FacultyID = table.Column<Guid>(nullable: true),
                    Approved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Courses_Faculties_FacultyID",
                        column: x => x.FacultyID,
                        principalTable: "Faculties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfFacs",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProfessorID = table.Column<Guid>(nullable: false),
                    FacultyID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfFacs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProfFacs_Faculties_FacultyID",
                        column: x => x.FacultyID,
                        principalTable: "Faculties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfFacs_Professors_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfCourses",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProfessorID = table.Column<Guid>(nullable: false),
                    CourseID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfCourses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProfCourses_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfCourses_Professors_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AccountID",
                table: "Comments",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EmailID",
                table: "Comments",
                column: "EmailID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProfessorID",
                table: "Comments",
                column: "ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_FacultyID",
                table: "Courses",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Name_FacultyID",
                table: "Courses",
                columns: new[] { "Name", "FacultyID" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [FacultyID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_Address",
                table: "Emails",
                column: "Address",
                unique: true,
                filter: "[Address] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_AliasName",
                table: "Faculties",
                column: "AliasName",
                unique: true,
                filter: "[AliasName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_UniversityID",
                table: "Faculties",
                column: "UniversityID");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_Name_UniversityID",
                table: "Faculties",
                columns: new[] { "Name", "UniversityID" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProfCourses_CourseID",
                table: "ProfCourses",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfCourses_ProfessorID",
                table: "ProfCourses",
                column: "ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfFacs_FacultyID",
                table: "ProfFacs",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfFacs_ProfessorID",
                table: "ProfFacs",
                column: "ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_Universities_Name",
                table: "Universities",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "ProfCourses");

            migrationBuilder.DropTable(
                name: "ProfFacs");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
