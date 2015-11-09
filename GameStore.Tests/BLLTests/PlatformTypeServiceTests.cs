using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Services;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class PlatformTypeServiceTests
    {
        private TestCollections collections;
        private UOWMock mock;
        
        private string testGameKey = "SCII";
        private string notExistedGameKey = "Test";

        private int existedPlatformTypeId = 1;

        private PlatformTypeDTO platformTypeToAdd;
        private PlatformTypeDTO platformTypeToUpdate;

        private PlatformTypeService service;

        
        private void InitializeTestEntities()
        {

            platformTypeToAdd = new PlatformTypeDTO()
            {
                PlatformTypeId = 3,
                PlatformTypeName = "TestName"
            };

            platformTypeToUpdate = new PlatformTypeDTO()
            {
                PlatformTypeId = 1,
                PlatformTypeName = "TestName"
            };

            service = new PlatformTypeService(mock.UnitOfWork);
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
        public void Get_Platform_Types_By_Game_Key_Is_Not_Null()
        {
            var result = service.Get(collections.Games[0].GameKey);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Get_All_Platform_Types_Is_Not_Null()
        {
            var result = service.GetAll();

            Assert.IsNotNull(result);
        }


        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Platform_Type_With_Null_Refference_Expected_Exception()
        {
            service.Create(null);
        }

        [TestMethod]
        public void Add_Platform_Type()
        {
            var expectedCount = collections.PlatformTypes.Count + 1;

            service.Create(platformTypeToAdd);

            Assert.AreEqual(expectedCount, collections.PlatformTypes.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Update_Platform_Type_With_Null_Refference_Expected_Exception()
        {
            service.Update(null);
        }


        [TestMethod]
        public void Update_Platform_Type()
        {
            service.Update(platformTypeToUpdate);

            Assert.AreEqual(platformTypeToUpdate.PlatformTypeName, collections.PlatformTypes[0].PlatformTypeName);
        }

        [TestMethod]
        public void Delete_Platform_Type()
        {
            var expectedCount = collections.PlatformTypes.Count - 1;

            service.Delete(1);

            Assert.AreEqual(expectedCount, collections.PlatformTypes.Count);
        }

        [TestMethod]
        public void Get_Platform_Type_By_Id_Result_Is_Not_Null()
        {
            var result = service.Get(existedPlatformTypeId);

            Assert.IsNotNull(result);
        }
    }




    
}
