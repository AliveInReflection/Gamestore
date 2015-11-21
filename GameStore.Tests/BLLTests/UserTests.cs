using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Services;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameStore.Tests.Mocks;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class UserTests
    {
        private TestCollections collections;
        private UOWMock mock;

        private UserService service;

        private UserDTO testUser;

        private RoleDTO testRole;

        private ChangePasswordDTO changePasswordModel;

        private const string newUserName = "Test";

        #region initialize

        private void InitializeTestEntities()
        {
            service = new UserService(mock.UnitOfWork, null);

            testUser = new UserDTO()
            {
                UserId = 10,
                UserName = "Test",
                Password = "qwerty",
                Country = Countries.Ukraine,
                DateOfBirth = DateTime.UtcNow,
                Roles = new List<RoleDTO>(),
                Claims = new List<Claim>()
            };

            testRole = new RoleDTO()
            {
                RoleId = 10,
                RoleName = "TestRole",
                Claims = new List<Claim>()
            };

            changePasswordModel = new ChangePasswordDTO()
            {
                UserId = collections.Users[0].UserId,
                OldPassword = collections.Users[0].Password,
                NewPassword = "asdasda"
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
        #endregion

        [TestMethod]
        public void User_Service_Is_Free()
        {
            var result = service.IsFree(newUserName);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void User_Service_Ban()
        {
            service.Ban(collections.Users[0].UserId, TimeSpan.FromDays(30));

            Assert.IsNotNull(collections.Users[0].BanExpirationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void User_Service_Create_Null_Refference_Expected_Exception()
        {
            service.Create(null);
        }

        [TestMethod]
        public void User_Service_Create()
        {
            var expectedCount = collections.Users.Count + 1;
            
            service.Create(testUser);

            Assert.AreEqual(expectedCount, collections.Users.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void User_Service_Change_Password_Wrong_Old_Password_Expected_Exception()
        {
            changePasswordModel.OldPassword = "zxcxcvxcv";
            service.ChangePassword(changePasswordModel);
        }

        [TestMethod]
        public void User_Service_Change_Password()
        {
            service.ChangePassword(changePasswordModel);

            Assert.AreEqual(changePasswordModel.NewPassword, collections.Users[0].Password);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void User_Service_Update_Null_Refference_Expected_Exception()
        {
            service.Create(null);
        }

        [TestMethod]
        public void User_Service_Update()
        {
            testUser.UserId = collections.Users[0].UserId;

            service.Update(testUser);

            Assert.AreEqual(collections.Users[0].UserName, testUser.UserName);
            Assert.AreEqual(collections.Users[0].Password, testUser.Password);
        }

        [TestMethod] public void User_Service_Delete()
        {
            var expectedCount = collections.Users.Count - 1;

            service.Delete(collections.Users[0].UserId);

            Assert.AreEqual(expectedCount, collections.Users.Count);
        }

        [TestMethod]
        public void User_Service_Get_By_Id_Result_Is_Not_Null()
        {
            var result = service.Get(collections.Users[0].UserId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void User_Service_Get_By_Name_Result_Is_Not_Null()
        {
            var result = service.Get(collections.Users[0].UserName);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void User_Service_Get_All_Is_Not_Empty()
        {
            var result = service.GetAll();

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void User_Service_Get_All_Roles_Is_Not_Empty()
        {
            var result = service.GetAllRoles();

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void User_Service_Ge_Role_By_Id_Is_Not_Null()
        {
            var result = service.GetRole(collections.Roles[0].RoleId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void User_Service_Create_Role_Null_Refference_Expected_Exception()
        {
            service.CreateRole(null);
        }

        [TestMethod]
        public void User_Service_Create_Role()
        {
            var expectedCount = collections.Roles.Count + 1;

            service.CreateRole(testRole);

            Assert.AreEqual(expectedCount, collections.Roles.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void User_Service_Update_Role_Null_Refference_Expected_Exception()
        {
            service.UpdateRole(null);
        }

        [TestMethod]
        public void User_Service_Update_Role()
        {
            testRole.RoleId = collections.Roles[0].RoleId;

            service.UpdateRole(testRole);

            Assert.AreEqual(testRole.RoleName, collections.Roles[0].RoleName);
        }

        [TestMethod]
        public void User_Service_Delete_Role()
        {
            var expectedCount = collections.Roles.Count - 1;

            service.DeleteRole(collections.Roles[0].RoleId);

            Assert.AreEqual(expectedCount, collections.Roles.Count);
        }
    }
}
