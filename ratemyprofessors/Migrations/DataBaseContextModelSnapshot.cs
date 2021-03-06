﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ratemyprofessors.Models;

namespace ratemyprofessors.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ratemyprofessors.Models.Account", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Emain")
                        .HasMaxLength(60);

                    b.Property<bool>("ISAdmin");

                    b.Property<string>("LastName")
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("PassWord")
                        .HasMaxLength(20);

                    b.Property<DateTime>("RegistarationDate");

                    b.Property<string>("UserName")
                        .HasMaxLength(20);

                    b.HasKey("ID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ratemyprofessors.Models.Comment", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AccountID");

                    b.Property<byte?>("Angry");

                    b.Property<byte>("Answering");

                    b.Property<byte?>("Bad");

                    b.Property<byte?>("Bluntess");

                    b.Property<string>("Comments")
                        .HasMaxLength(399);

                    b.Property<DateTime>("DateTime");

                    b.Property<byte>("DisLike");

                    b.Property<byte?>("DoYourWork");

                    b.Property<Guid>("EmailID");

                    b.Property<byte>("Exhausting");

                    b.Property<byte>("HandOuts");

                    b.Property<byte>("HardExams");

                    b.Property<byte>("HomeWork");

                    b.Property<byte>("Knoledge");

                    b.Property<byte>("Like");

                    b.Property<byte>("Marking");

                    b.Property<byte>("Moods");

                    b.Property<byte>("OverAll");

                    b.Property<Guid>("ProfessorID");

                    b.Property<byte>("Project");

                    b.Property<byte>("RollCall");

                    b.Property<byte>("ScapeAtTheEnd");

                    b.Property<string>("ShowName")
                        .HasMaxLength(20);

                    b.Property<byte>("Teaching");

                    b.Property<byte>("Update");

                    b.Property<bool>("Verfied");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.HasIndex("EmailID");

                    b.HasIndex("ProfessorID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ratemyprofessors.Models.ContactUs", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MailAddress")
                        .HasMaxLength(60);

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("ratemyprofessors.Models.Course", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AliasNames")
                        .HasMaxLength(10);

                    b.Property<bool>("Approved");

                    b.Property<Guid?>("FacultyID");

                    b.Property<string>("Name")
                        .HasMaxLength(40);

                    b.HasKey("ID");

                    b.HasIndex("FacultyID");

                    b.HasIndex("Name", "FacultyID")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL AND [FacultyID] IS NOT NULL");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ratemyprofessors.Models.Email", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(60);

                    b.Property<bool>("Verified");

                    b.HasKey("ID");

                    b.HasIndex("Address")
                        .IsUnique()
                        .HasFilter("[Address] IS NOT NULL");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("ratemyprofessors.Models.Faculty", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AliasName")
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<byte>("UniversityID");

                    b.HasKey("ID");

                    b.HasIndex("AliasName")
                        .IsUnique()
                        .HasFilter("[AliasName] IS NOT NULL");

                    b.HasIndex("UniversityID");

                    b.HasIndex("Name", "UniversityID")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("ratemyprofessors.Models.ProfCourse", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CourseID");

                    b.Property<Guid>("ProfessorID");

                    b.HasKey("ID");

                    b.HasIndex("CourseID");

                    b.HasIndex("ProfessorID");

                    b.ToTable("ProfCourses");
                });

            modelBuilder.Entity("ratemyprofessors.Models.Professor", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Approved");

                    b.Property<string>("Comment");

                    b.Property<string>("ImageLink")
                        .HasMaxLength(200);

                    b.Property<string>("LastName")
                        .HasMaxLength(40);

                    b.Property<string>("Link")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .HasMaxLength(40);

                    b.Property<string>("PrivateLink")
                        .HasMaxLength(200);

                    b.Property<bool>("Staff");

                    b.Property<string>("WPLink")
                        .HasMaxLength(200);

                    b.HasKey("ID");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("ratemyprofessors.Models.ProfFac", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("FacultyID");

                    b.Property<Guid>("ProfessorID");

                    b.HasKey("ID");

                    b.HasIndex("FacultyID");

                    b.HasIndex("ProfessorID");

                    b.ToTable("ProfFacs");
                });

            modelBuilder.Entity("ratemyprofessors.Models.University", b =>
                {
                    b.Property<byte>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(40);

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("ratemyprofessors.Models.Comment", b =>
                {
                    b.HasOne("ratemyprofessors.Models.Account", "Account")
                        .WithMany("Comments")
                        .HasForeignKey("AccountID");

                    b.HasOne("ratemyprofessors.Models.Email", "Email")
                        .WithMany("Comments")
                        .HasForeignKey("EmailID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ratemyprofessors.Models.Professor", "Professor")
                        .WithMany("Comments")
                        .HasForeignKey("ProfessorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ratemyprofessors.Models.Course", b =>
                {
                    b.HasOne("ratemyprofessors.Models.Faculty", "Faculty")
                        .WithMany("Courses")
                        .HasForeignKey("FacultyID");
                });

            modelBuilder.Entity("ratemyprofessors.Models.Faculty", b =>
                {
                    b.HasOne("ratemyprofessors.Models.University", "University")
                        .WithMany("Faculties")
                        .HasForeignKey("UniversityID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ratemyprofessors.Models.ProfCourse", b =>
                {
                    b.HasOne("ratemyprofessors.Models.Course", "Course")
                        .WithMany("ProfCourses")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ratemyprofessors.Models.Professor", "Professor")
                        .WithMany("ProfCourses")
                        .HasForeignKey("ProfessorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ratemyprofessors.Models.ProfFac", b =>
                {
                    b.HasOne("ratemyprofessors.Models.Faculty", "Faculty")
                        .WithMany("ProfFacs")
                        .HasForeignKey("FacultyID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ratemyprofessors.Models.Professor", "Professor")
                        .WithMany("ProfFacs")
                        .HasForeignKey("ProfessorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
