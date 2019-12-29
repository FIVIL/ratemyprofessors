using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Controllers;
using ratemyprofessors.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ratemyprofessorsTests
{
    public class CoursesControllerTest
    {

        private DataBaseContext ConfigureDatabase(string databaseName)
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>().UseInMemoryDatabase(databaseName).Options;

            return new DataBaseContext(options);
        }

        private CoursesController ConfigureCoursesController(string databaseName, List<Course> courses)
        {

            var database = ConfigureDatabase(databaseName);

            if (courses != null && courses.Count > 0)
            {
                database.Courses.AddRange(courses);
                database.SaveChanges();
            }

            return new CoursesController(database);
        }

        private List<Faculty> GetTestFaculties()
        {
            List<Faculty> faculties = new List<Faculty>();

            for (int i = 0; i < 3; i++)
            {
                var f = new Faculty()
                {
                    Name = "test" + i
                };

                faculties.Add(f);
            }

            return faculties;
        }


        [Fact]
        public void Check_Facs_Returns_Data()
        {
            var facility = GetTestFaculties();

            var database = ConfigureDatabase("ok_facs");
            database.Faculties.AddRange(facility);
            database.SaveChanges();

            var controller = new CoursesController(database);

            var result = controller.GetFacs();

            Assert.IsAssignableFrom<IEnumerable<Faculty>>(result);
        }


        #region Test CourseProf
        private Professor GetTestProfessor()
        {
            Professor pr = new Professor()
            {
                ProfCourses = new List<ProfCourse>()
                {

                    new ProfCourse()
                    {
                        ID = Guid.NewGuid(),
                        Course = new Course()
                        {
                            Approved = true
                        }
    
                    },
                    new ProfCourse()
                    {
                        ID = Guid.NewGuid(),
                        Course = new Course()
                        {
                            Approved = true
                        }
                    }
                }

            };

            return pr;
        }

        [Fact]
        public async void Check_CourseProf_Returns_BadRequest_On_ModelError()
        {
            var controller = ConfigureCoursesController("bad_request_on_modelError", null);

            controller.ModelState.AddModelError("test", "test error");
            var result = await controller.GetCourseProf(Guid.NewGuid());

            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async void Check_CourseProf_Returns_NotFound_On_InvalidId()
        {
            var database = ConfigureDatabase("not_found_courseProf");

            database.Professors.Add(GetTestProfessor());
            database.SaveChanges();

            var controller = new CoursesController(database);
            var result = await controller.GetCourseProf(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Check_CourseProf_Returns_Ok_With_ValidData()
        {
            var database = ConfigureDatabase("ok_courseProf");

            var professor = GetTestProfessor();

            database.Professors.Add(professor);
            database.SaveChanges();

            var controller = new CoursesController(database);
            var result = await controller.GetCourseProf(professor.ID);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsAssignableFrom<IEnumerable<Course>>(okResult.Value);
            

            var coursesNum = (professor.ProfCourses as List<ProfCourse>).Count;
            Assert.Equal(coursesNum, data.Count());
        }
        #endregion



        #region Test GetCourse
        [Fact]
        public async void Check_GetCourse_Returns_BadRequest_On_ModelError()
        {
            var database = ConfigureDatabase("bad_request_getCourse");

            var controller = new CoursesController(database);
            controller.ModelState.AddModelError("test", "test error");

            var result = await controller.GetCourse(string.Empty);
            Assert.IsType<BadRequestObjectResult>(result);
        }



        [Fact]
        public async void Check_GetCourse_Returns_NotFound_on_Model_On_InvalidAliasName()
        {

            var faci = new Faculty()
            {
                AliasName = "sdsdsds"
            };

            var database = ConfigureDatabase("not_found_faculity_invalidId");
            database.Faculties.Add(faci);
            database.SaveChanges();

            var controller = new CoursesController(database);

            var result = await controller.GetCourse("dsd");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Check_GetCourse_Returns_Ok_With_ValidData()
        {
            string name = "test";
            var faci = new Faculty()
            {
                AliasName = name,
                Courses = new List<Course>()
                {
                    new Course(){Approved =  true},
                    new Course(){Approved = false}
                }
            };

            var database = ConfigureDatabase("ok_getcours");
            database.Faculties.Add(faci);
            database.SaveChanges();

            var controller = new CoursesController(database);
            var result = await controller.GetCourse(name);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsAssignableFrom<IEnumerable<Course>>(okResult.Value);

            Assert.Equal(1, data.Count());
        }
        #endregion


        #region Test GetAllCourses
        [Fact]
        public void Check_GetAllCourses_Returns_Approved_Courses()
        {
            var courses = new List<Course>()
            {
                new Course(){Approved = true},
                new Course(){Approved = false},
                new Course(){Approved = true}
            };

            var database = ConfigureDatabase("get_all_course");
            database.Courses.AddRange(courses);
            database.SaveChanges();

            var controller = new CoursesController(database);
            var result = controller.GetAllCourses();

            var data = Assert.IsAssignableFrom<IEnumerable<Course>>(result);
            Assert.Equal(2, data.Count());
        }
        #endregion


        #region Test PostCourse
        [Fact]
        public async void Check_PostCourse_Returns_BadRequest_On_ModelError()
        {
            var database = ConfigureDatabase("bad_request_postCourse");
            var controller = new CoursesController(database);
            controller.ModelState.AddModelError("test", "test error");

            var result = await controller.PostCourses(new Course());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Check_PostCourse_Returns_BadRequest_On_InvalidFacId()
        {
            var database = ConfigureDatabase("bad_request_postCourse_invalidId");
            var controller = new CoursesController(database);


            var result = await controller.PostCourses(new Course());

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Check_PostCourse_Returns_BadRequest_On_InavlidFacility()
        {
            var database = ConfigureDatabase("bad_request_postCourse_invalidFACILITY");
            var testCourse = new Course()
            {
                FacID = Guid.NewGuid().ToString(),
                Profs = Guid.NewGuid() + ";" + Guid.NewGuid()
            };

            var controller = new CoursesController(database);

            var result = await controller.PostCourses(testCourse);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void Check_PostCourse_Returns_BadRequest_On_ValidData()
        {
            var database = ConfigureDatabase("OK_postCourse");

            var guid = Guid.NewGuid();

            var fac = new Faculty()
            {
                ID = guid
            };

            var testCourse = new Course()
            {
                FacID = guid.ToString(),
                Profs = Guid.NewGuid() + ";" + Guid.NewGuid()
            };

            database.Faculties.Add(fac);
            database.SaveChanges();

            var controller = new CoursesController(database);

            var result = await controller.PostCourses(testCourse);

            Assert.IsType<OkResult>(result);

        }
        #endregion

    }
}
