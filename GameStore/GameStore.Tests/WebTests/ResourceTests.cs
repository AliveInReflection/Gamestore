using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.WebUI.App_LocalResources.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class ResourceTests
    {
        [TestMethod]
        public void Test_Get_Infrastructure_Resources()
        {

            var properties = typeof(InfrastructureRes).GetProperties().Where(p => p.GetGetMethod().ReturnType == typeof(string));
            var values = new List<string>();

            foreach (var property in properties)
            {
                values.Add(property.GetGetMethod().Invoke(null, null).ToString());
            }

            Assert.IsTrue(values.All(v => !string.IsNullOrEmpty(v)));
        }

        [TestMethod]
        public void Test_Get_Model_Resources()
        {

            var properties = typeof(ModelRes).GetProperties().Where(p => p.GetGetMethod().ReturnType == typeof(string));
            var values = new List<string>();

            foreach (var property in properties)
            {
                values.Add(property.GetGetMethod().Invoke(null, null).ToString());
            }

            Assert.IsTrue(values.All(v => !string.IsNullOrEmpty(v)));
        }

        [TestMethod]
        public void Test_Get_Validation_Resources()
        {

            var properties = typeof(ValidationRes).GetProperties().Where(p => p.GetGetMethod().ReturnType == typeof(string));
            var values = new List<string>();

            foreach (var property in properties)
            {
                values.Add(property.GetGetMethod().Invoke(null, null).ToString());
            }

            Assert.IsTrue(values.All(v => !string.IsNullOrEmpty(v)));
        }

        [TestMethod]
        public void Test_Get_Views_Resources()
        {

            var properties = typeof(ViewsRes).GetProperties().Where(p => p.GetGetMethod().ReturnType == typeof(string));
            var values = new List<string>();

            foreach (var property in properties)
            {
                values.Add(property.GetGetMethod().Invoke(null, null).ToString());
            }

            Assert.IsTrue(values.All(v => !string.IsNullOrEmpty(v)));
        }
    }
}
