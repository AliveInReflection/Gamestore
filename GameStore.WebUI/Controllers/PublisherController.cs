using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.WebUI.Models;
using GameStore.BLL.Infrastructure;

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
        [ActionName("New")]
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

        public ActionResult Update(int id)
        {
            try
            {
                var publisher = publisherService.Get(id);
                return View(Mapper.Map<PublisherDTO, UpdatePublisherViewModel>(publisher));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error";
                return RedirectToAction("List", "Game");
            }
        }

        [HttpPost]
        public ActionResult Update(UpdatePublisherViewModel publisher)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);
            }

            try
            {
                publisherService.Update(Mapper.Map<UpdatePublisherViewModel, PublisherDTO>(publisher));
                return RedirectToAction("List", "Game");
            }
            catch (ValidationException e)
            {
                ModelState.AddModelError("PublisherId", e.Message);
                return View(publisher);
            }

        }

        

    }
}
