using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Services;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class CommentServiceTest
    {
        private TestCollections collections;
        private UOWMock mock;
        private CommentDTO commentToAdd;
        private CommentDTO commentToUpdate;



        private CommentService service;
        

        private void InitializeTestEntities()
        {
            service = new CommentService(mock.UnitOfWork);
            
            commentToAdd = new CommentDTO()
            {
                CommentId = 5,
                UserName = "Sarah",
                Content = "Test comment",
                ChildComments = new List<CommentDTO>()
            };

            commentToUpdate = new CommentDTO()
            {
                CommentId = 1,
                Content = "Test",
                Quote = "Test"
            };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperBLLProfile());
            });
            collections = new TestCollections();
            mock = new UOWMock(collections);

            InitializeTestEntities();
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Add_Comment_With_Null_Reference_Expected_Exception()
        {
            CommentDTO commentToAdd = null;

            service.Create(commentToAdd);
        }

       

        [TestMethod]
        public void Add_Comment_For_Game_By_Key()
        {
            var expectedCount = collections.Comments.Count + 1;
            
            service.Create(commentToAdd);

            Assert.AreEqual(expectedCount, collections.Comments.Count);
        }


        [TestMethod]
        public void Get_Comment_By_Id_Result_Is_Not_Null()
        {
            var result = service.Get(collections.Comments[0].CommentId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof (NullReferenceException))]
        public void Update_Comment_Null_Refference_Expected_Exception()
        {
            service.Update(null);
        }

        [TestMethod]
        public void Update_Comment()
        {
            var entry = collections.Comments.First(m => m.CommentId.Equals(commentToUpdate.CommentId));
            
            service.Update(commentToUpdate);

            Assert.AreEqual(commentToUpdate.Content, entry.Content);
            Assert.AreEqual(commentToUpdate.Quote, entry.Quote);
        }

        
         

    }
}
