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
    public class ContactControllerTest
    {

        private ContactController ConfigureContactController(string databaseName)
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>().UseInMemoryDatabase(databaseName).Options;
            var database = new DataBaseContext(options);

            return new ContactController(database);
        }

        [Fact]
        public async void Check_PostContact_Returns_BadRequest_On_ModelError()
        {

            var controller = ConfigureContactController("bad_request_model_error");
            controller.ModelState.AddModelError("test", "test error");

            var result = await controller.PostContact(new ContactUs());

            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        public async void Check_PostContact_Returns_Ok()
        {
            var controller = ConfigureContactController("ok_post_contact");
            var result = await controller.PostContact(new ContactUs());

            Assert.IsType<OkResult>(result);
        }

    }
}
