using AutoMapper;
using GameStore.Auth;
using GameStore.CL.AutomapperProfiles;
using GameStore.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GameStore.Tests.AuthTests
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        private TestCollections collections;
        private UOWMock mock;


        private AuthenticationService service;

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperBLLProfile());
            });


            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(new StringWriter())); 

            collections = new TestCollections();
            mock = new UOWMock(collections);
            service = new AuthenticationService(mock.UnitOfWork, null);
        }


        [TestMethod]
        public void Auth_Login()
        {
            var result = service.Login(collections.Users[0].UserName, collections.Users[0].Password, false);

            Assert.IsTrue(result);
        }
    }
}
