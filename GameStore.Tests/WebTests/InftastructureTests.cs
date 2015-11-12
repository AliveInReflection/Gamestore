﻿using System.Linq;
using GameStore.WebUI.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class InftastructureTests
    {
        [TestMethod]
        public void Ban_Durations_List_Is_Not_Empty()
        {
            var result = BanDurationManager.GetKeys();

            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod]
        public void Game_Publiting_Date_Filtering_Manager_List_Is_Not_Empty()
        {
            var result = GamePublishingDateFilteringManager.GetKeys();

            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod]
        public void Game_Sorting_Mode_Manager_List_Is_Not_Empty()
        {
            var result = GameSortingModeManager.GetKeys();

            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod]
        public void Paging_Manager_List_Is_Not_Empty()
        {
            var result = PagingManager.GetKeys();

            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod]
        public void Paymant_Mode_Manager_List_Is_Not_Empty()
        {
            var result = PaymentModeManager.GetAll();

            Assert.AreNotEqual(0, result.Count());
        }
    }
}