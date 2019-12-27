using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Moq;
using ratemyprofessors.Controllers;
using ratemyprofessors.Models;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace ratemyprofessorsTests
{
    public class CommentsControllerTest
    {

        private List<Comment> GetTestComments()
        {
            List<Comment> comments = new List<Comment>();
            for (int i = 0; i < 3; i++)
            {
                Comment comment = new Comment()
                {
                    ID = Guid.NewGuid(),
                    Professor = new Professor()
                    {
                        ID = Guid.NewGuid()
                    },
                    Email = new Email()
                    {
                        Verified = true,
                        Address = $"test{i}@example.com"
                    },
                    Verfied = true
                };
                comments.Add(comment);
            }

            return comments;
        }


        private CommentsController ConfigureCommentsController(string databaseName, IList<Comment> comments)
        {
            var option = new DbContextOptionsBuilder<DataBaseContext>().UseInMemoryDatabase(databaseName: databaseName).Options;

            var context = new DataBaseContext(option);

            if(comments != null)
            {
                context.Comments.AddRange(comments);
                context.SaveChanges();
            }
            

            var mockConf = new Mock<IConfiguration>();
            var mockHost = new Mock<IHostingEnvironment>();

            return new CommentsController(context, mockConf.Object, mockHost.Object);
        }

        private CommentsController ConfigureCommentsController(string databaseName, Comment comment)
        {
            return ConfigureCommentsController(databaseName, new List<Comment>() { comment });
        }




        #region Test GetComment That Returns List

        [Fact]
        public async void Check_GetComment_Returns_OKObjectResult()
        {

            var controller = ConfigureCommentsController("comments_width_ok_result", GetTestComments());

            var result = await controller.GetComment();

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async void Check_Getomments_Returns_NotFound_Width_EmptyList()
        {
            var controller = ConfigureCommentsController("not_found_error", new List<Comment>());

            var result = await controller.GetComment();

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async void Check_GetComment_Returns_Valid_CommentList()
        {
            var controller = ConfigureCommentsController("valid_comment_list", GetTestComments());

            var result = await controller.GetComment();

            var viewResult = Assert.IsAssignableFrom<OkObjectResult>(result);

            var model = Assert.IsAssignableFrom<IList<Comment>>(viewResult.Value);

            Assert.Equal(GetTestComments().Count, model.Count);

        }

        #endregion




        #region Test GetComment By Professor Id

        [Fact]
        public async void Check_GetCommentByProfessorId_Returns_BadRequest_On_ModelError()
        {
            var controller = ConfigureCommentsController("bad_request_by_professorId", new List<Comment>());

            controller.ModelState.AddModelError("samepleError", "an error");

            var result = await controller.GetComments(Guid.NewGuid());

            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async void Check_GetCommentByProfessorId_NotFound()
        {


            var comment = new Comment()
            {
                Email = new Email()
                {
                    Verified = true
                },
                Professor = new Professor()
                {
                    ID = Guid.NewGuid()
                },
                Verfied = true
            };
        

            var controller = ConfigureCommentsController("not_found_by_professorId", comment);
            var result = await controller.GetComments(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Check_CetCommentsByProfessorId_Returns_Ok()
        {

            var guid = Guid.NewGuid();

            var comment = new Comment()
            {

                Email = new Email()
                {
                    Verified = true
                },
                Professor = new Professor()
                {
                    ID = guid
                },
                Verfied = true
            };



            var controller = ConfigureCommentsController("ok_by_professorId", comment);

            var result = await controller.GetComments(guid);

            var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);

            var retrievedData = Assert.IsAssignableFrom<List<Comment>>(okResult.Value);

            Assert.Single(retrievedData);

        }

        #endregion




        #region Test GetCommentsAvg

        // CommentsAvg Test

        [Fact]
        public async void Check_CommetnsAvg_Returns_BadRquest_On_ModelError()
        {
            var controller = ConfigureCommentsController("bad_request_comment_avg", new List<Comment>());

            controller.ModelState.AddModelError("test", "test error");

            var result = await controller.GetCommentsAvg(Guid.NewGuid());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Check_CommentsAvg_Returns_NotFound()
        {
            var controller = ConfigureCommentsController("not_found_comment_avg", GetTestComments());

            var result = await controller.GetCommentsAvg(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Check_CommentsAvg_ReturnsOK()
        {
            var comments = GetTestComments();

            var guid = comments.Find(ct => true).Professor.ID;

            var controller = ConfigureCommentsController("ok_comment_avg", comments);

            var result = await controller.GetCommentsAvg(guid);

            Assert.IsType<OkObjectResult>(result);
        }
        #endregion




        #region Test GetComment
        //Test GetComment

        [Fact]
        public async void Check_GetComment_Returns_BadRequest_On_ModelError()
        {
            var controller = ConfigureCommentsController("bad_request_get_comment", new List<Comment>());

            controller.ModelState.AddModelError("test", "test error");

            var result = await controller.GetComment(Guid.NewGuid());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Check_GetComment_Returns_NotFound_On_InvalidId()
        {
            var controller = ConfigureCommentsController("not_found_on_InvalidId", GetTestComments());

            var result = await controller.GetComment(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async void Check_GetComment_Returns_NotFound_On_UnVerifiedComment()
        {
            var guid = Guid.NewGuid();
            var comment  = new Comment()
            {
                ID = guid,
                Verfied = false
            };

            var comments = GetTestComments();
            comments.Add(comment);

            var controller = ConfigureCommentsController("not_found_on_unverfied", comments);
            var result = await controller.GetComment(guid);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Check_GetComment_Returns_Ok_Data_With_ValidData()
        {
            var comments = GetTestComments();
            var comment = comments.Find(ct => true);

            var controller = ConfigureCommentsController("returns_ok_width_valid_data", comments);
            var result = await controller.GetComment(comment.ID);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<Comment>(okResult.Value);

            Assert.Equal(comment.ID, data.ID);

        }
        #endregion



        #region Test RatePlus
        // Test RateplusComment

        [Fact]
        public async void Check_RatePlus_Returns_BadRequest_On_ModelError()
        {
            var controller = ConfigureCommentsController("bad_request_model_error", GetTestComments());
            controller.ModelState.AddModelError("test", "test error");
            var result = await controller.RatePlusComment(Guid.NewGuid());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Check_RatePlus_Returns_NotFound_On_InvalidId()
        {
            var controller = ConfigureCommentsController("not_found_invalidId", GetTestComments());
            var result = await controller.RatePlusComment(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Check_RatePlus_Returns_NotFound_With_ValidData()
        {
            var comments = GetTestComments();
            var comment = comments.Find(ct => true);

            var controller = ConfigureCommentsController("ok_with_validData", comments);
            var result = await controller.RatePlusComment(comment.ID);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<Comment>(okResult.Value);
            Assert.Equal(1, data.Like);
        }
        #endregion



        #region Test RateNegetive
        // Test RateNegativeComment

        [Fact]
        public async void Check_RateNegetive_Returns_BadRequest_On_ModelError()
        {
            var controller = ConfigureCommentsController("bad_request_negative_modelError", new Comment());
            controller.ModelState.AddModelError("test", "test Error");
            var result = await controller.RateNegetiveComment(Guid.NewGuid());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Check_RateNegetive_Returns_NotFound_OnModelError()
        {
            var controller = ConfigureCommentsController("not_found_negetive_invalidId", GetTestComments());
            var result = await controller.RateNegetiveComment(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Check_RateNegetive_Returns_Ok_With_ValidData()
        {
            var comments = GetTestComments();
            var comment = comments.Find(ct => true);

            var controller = ConfigureCommentsController("ok_negative_validData", comments);
            var result = await controller.RateNegetiveComment(comment.ID);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<Comment>(okResult.Value);
            Assert.Equal(1, data.DisLike);
        }
        #endregion



        #region Test PostComment

        // Test PostComment
        [Fact]
        public async void Check_PostComment_Returns_BadRequest_On_ModelError()
        {
            var controller = ConfigureCommentsController("bad_request_postComment_modelError", new Comment());
            controller.ModelState.AddModelError("test", "test error");
            var result = await controller.PostComment(new Comment());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Check_PostComment_Returns_Ok_On_DuplicateComment()
        {
            var comments = GetTestComments();
            var comment = comments.Find(ct => true);
            //comment.Email.Address = "test@example.com";


            var controller = ConfigureCommentsController("Ok_postComment_duplicate_email", comments);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var result = await controller.PostComment(comment);

            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void Check_PostComment_Returns_Ok_On_UnVerified_Email()
        {
            var comments = GetTestComments();
            var comment = comments.Find(ct => true);

            var controller = ConfigureCommentsController("returns_ok_on_new_EmailAddress", comments);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var testComment = new Comment()
            {
                Email = new Email()
                {
                    Address = comment.Email.Address,
                    Verified = false
                },
                Professor = new Professor()
                {
                    ID = Guid.NewGuid()
                }
                
            };

            var result = await controller.PostComment(testComment);

            Assert.IsType<OkResult>(result);

        }

        [Fact]
        public async void Check_Returns_Ok_On_VerfiedDuplicateEmail()
        {
            var comments = GetTestComments();
            var comment = comments.Find(ct => true);

            var controller = ConfigureCommentsController("check_returns_ok_on_verfiedduplicateemail", comments);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var testComment = new Comment()
            {
                Email = new Email()
                {
                    Address = comment.Email.Address,
                    Verified = true
                },
                Professor = new Professor()
                {
                    ID = Guid.NewGuid()
                }

            };

            var result = await controller.PostComment(testComment);

            Assert.IsType<OkResult>(result);
        }
        #endregion


        #region Test Resend
        // Test Resend

        [Fact]
        public async void Check_Resend_Returns_NotFound()
        {
            var controller = ConfigureCommentsController("not_found_Resend", GetTestComments());
            var result = await controller.Resend("dummy email");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Check_Rsend_Returns_Ok_On_VerifiedEmail()
        {
            var comments = GetTestComments();
            var address = comments.Find(ct => true).Email.Address;

            var controller = ConfigureCommentsController("ok_resend", GetTestComments());
            var result = await controller.Resend(address);

            Assert.IsType<OkResult>(result);
        }
        #endregion

    }
}
