using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using ratemyprofessors;
using ratemyprofessors.Controllers;
using ratemyprofessors.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ratemyprofessorsTests
{
    public class ProfessorsControllerTest
    {

       

        private DataBaseContext ConfigureDatabase(string databaseName)
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>().UseInMemoryDatabase(databaseName).Options;
            return new DataBaseContext(options);
        }

        private Mock<ProfessorCache> GetMockedProfessorChache()
        {
            var mockConf = new Mock<IConfiguration>();
            return new Mock<ProfessorCache>(mockConf.Object);
        }

        #region Test GetById

        [Fact]
        public async void Check_GetByID_Returns_NotFound_On_InvalidId()
        {
            var database = ConfigureDatabase("not_found_getbyId");

            var controller = new ProfessorsController(database, GetMockedProfessorChache().Object);

            var result = await controller.GetByID(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async void Check_GetByID_Returns_Ok_With_ValidData()
        {
            var database = ConfigureDatabase("not_found_getbyId");

            var guid = Guid.NewGuid();

            var professors = new List<Professor>()
            {
            new Professor() {ID = guid},
            new Professor() {ID = Guid.NewGuid()}
            };
        
          

            database.AddRange(professors);
            database.SaveChanges();

            var controller = new ProfessorsController(database, GetMockedProfessorChache().Object);
            var result = await controller.GetByID(guid);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<Professor>(okResult.Value);
            Assert.Equal(guid.ToString(), data.ID.ToString());
        }
        #endregion


        #region Test GetProfessors

        [Fact]
        public async void Check_GetProfessors_Return_BadRequest_OnModelError()
        {
            var database = ConfigureDatabase("bad_request_on_modelError");
            var controller = new ProfessorsController(database, GetMockedProfessorChache().Object);
            controller.ModelState.AddModelError("test", "test error");

            var result = await controller.GetProfessors(Guid.NewGuid());

            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async void Chect_GetProfessors_Returns_NotFound_On_InvalidCourseId()
        {
            var database = ConfigureDatabase("not_found_getProfessors_InvalidId");

            var controller = new ProfessorsController(database, GetMockedProfessorChache().Object);
            var result = await controller.GetProfessors(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async void Check_GetProfessors_Returns_Ok_With_ValidData()
        {
            var database = ConfigureDatabase("get_professors_with_valid_data");

            var guid = Guid.NewGuid();

            var testCourse = new Course()
            {
                ID = guid,
                ProfCourses = new List<ProfCourse>()
                {
                    new ProfCourse()
                    {
                        Professor = new Professor()
                        {
                            Approved = true
                        }
                    },
                    new ProfCourse()
                    {
                        Professor = new Professor()
                        {
                            Approved = false
                        }
                    },

                    new ProfCourse()
                    {
                        Professor = new Professor()
                        {
                            Approved = true
                        }
                    }
                }
            };

            database.Courses.Add(testCourse);
            database.SaveChanges();

            var controller = new ProfessorsController(database, GetMockedProfessorChache().Object);
            var result = await controller.GetProfessors(guid);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsAssignableFrom<IEnumerable<Professor>>(okResult.Value);
            Assert.Equal(2, data.Count());

        }

        #endregion


        #region Test PostProfessor

        [Fact]
        public async void Check_PostProfessor_Returns_BadRequest_On_ModelError()
        {
            var database = ConfigureDatabase("bad_request_PostProfessor_modelError");
            var controller = new ProfessorsController(database, GetMockedProfessorChache().Object);
            controller.ModelState.AddModelError("test", "test error");

            var result = await controller.PostProfessor(new Professor());

            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async void Check_PostProfessor_Returns_Ok_With_ValidData()
        {

            var testPorfessor = new Professor()
            {
                Courses = Guid.NewGuid() + ";" + Guid.NewGuid(),
                Facs = Guid.NewGuid() + ";" + Guid.NewGuid()
            };


            var databse = ConfigureDatabase("pk_PostPrfessor");
            var controller = new ProfessorsController(databse, GetMockedProfessorChache().Object);

            var result = await controller.PostProfessor(testPorfessor);

            Assert.IsType<OkResult>(result);
            
        }

        #endregion
    }
}
