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

    }
}
