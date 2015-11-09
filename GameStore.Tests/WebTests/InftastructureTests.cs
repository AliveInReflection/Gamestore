using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.WebUI.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class InftastructureTests
    {
        [TestMethod]
        public void Ban_Durations_Is_Not_Empty()
        {
            var result = BanDurationManager.GetKeys();

            Assert.AreNotEqual(0, result.Count());
        }
    }
}
