using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class PublisherController : Controller
    {
        private IPublisherService publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(string companyName)
        {
            var publisher = publisherService.Get(companyName);
            return View(Mapper.Map<PublisherDTO, DisplayPublisherViewModel>(publisher));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreatePublisherViewModel());
        }

        [HttpPost]
        public ActionResult Create(CreatePublisherViewModel publisher)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);
            }

            publisherService.Create(Mapper.Map<CreatePublisherViewModel, PublisherDTO>(publisher));
            return RedirectToAction("List", "Game");

        }

        

    }
}
