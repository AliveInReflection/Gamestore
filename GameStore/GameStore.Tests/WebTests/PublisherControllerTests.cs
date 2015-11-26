using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.DTO;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Tests.Mocks;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class PublisherControllerTests
    {
        private ServiceMocks mocks;
        private string testCompanyName = "Blizzard";
        private int testPublisherId = 1;

        private PublisherController controller;



        #region Initialize


        private void InitializeTestEntities()
        {

        }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperWebProfile());
            });

            InitializeTestEntities();

            mocks = new ServiceMocks();
            controller = new PublisherController(mocks.PublisherService.Object, mocks.Logger.Object);
        }
        #endregion
        
        
        [TestMethod]
        public void Publisher_Index_Model_Is_Not_Null()
        {
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Publisher_Details_Model_Is_Not_Null()
        {
            var result = controller.Details(testCompanyName) as ViewResult;

            Assert.IsNotNull(result.Model);
        }


        [TestMethod]
        public void Publisher_Create_Get_Model_Is_Not_Null()
        {
            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Comment_Create_Post_Is_Redirect_Result()
        {
            var result = controller.Create(new CreatePublisherViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Publisher_Create_Post_Model_State_Is_Not_Valid_Result_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("Test", "Test");
            var result = controller.Create(new CreatePublisherViewModel()) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Publisher_Update_Get_Model_Is_Not_Null()
        {
            var result = controller.Update(testPublisherId) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Comment_Update_Post_Is_Redirect_Result()
        {
            var result = controller.Update(new UpdatePublisherViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Publisher_Update_Post_Model_State_Is_Not_Valid_Result_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("Test", "Test");
            var result = controller.Update(new UpdatePublisherViewModel()) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Comment_Delete_Post_Is_Redirect_Result()
        {
            var result = controller.Delete(testPublisherId);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Publisher_Details_Get_Exception_Error_Message_Is_Not_Null()
        {
            mocks.PublisherService.Setup(x => x.Get(It.IsAny<string>())).Throws<ValidationException>();

            controller.Details(testCompanyName);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Publisher_Create_Post_Exception_Error_Message_Is_Not_Null()
        {
            mocks.PublisherService.Setup(x => x.Create(It.IsAny<PublisherDTO>())).Throws<ValidationException>();

            controller.Create(new CreatePublisherViewModel());

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Publisher_Update_Get_Exception_Error_Message_Is_Not_Null()
        {
            mocks.PublisherService.Setup(x => x.Get(It.IsAny<int>())).Throws<ValidationException>();

            controller.Update(testPublisherId);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Publisher_Update_Get_Result_Is_Redirect()
        {
            mocks.PublisherService.Setup(x => x.Get(It.IsAny<int>())).Throws<ValidationException>();

            var result = controller.Update(testPublisherId);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Publisher_Update_Post_Exception_Error_Message_Is_Not_Null()
        {
            mocks.PublisherService.Setup(x => x.Update(It.IsAny<PublisherDTO>())).Throws<ValidationException>();

            controller.Update(new UpdatePublisherViewModel());

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Publisher_Delete_Post_Exception_Error_Message_Is_Not_Null()
        {
            mocks.PublisherService.Setup(x => x.Delete(It.IsAny<int>())).Throws<ValidationException>();

            controller.Delete(testPublisherId);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }
    }
}
