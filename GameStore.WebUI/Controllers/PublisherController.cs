using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.App_LocalResources.Localization;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    [Localization]
    public class PublisherController : BaseController
    {
        private IPublisherService publisherService;
        private IGameStoreLogger logger;

        public PublisherController(IPublisherService publisherService, IGameStoreLogger logger)
        {
            this.publisherService = publisherService;
            this.logger = logger;
        }

        public ActionResult Index()
        {
            var publishers = publisherService.GetAll();
            return View(Mapper.Map<IEnumerable<PublisherDTO>, IEnumerable<DisplayPublisherViewModel>>(publishers));
        }

        [HttpGet]
        public ActionResult Details(string companyName)
        {
            try
            {
                var publisher = publisherService.Get(companyName);
                return View(Mapper.Map<PublisherDTO, DisplayPublisherViewModel>(publisher));
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.NotFound;
                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        [ActionName("New")]
        public ActionResult Create()
        {
            return View(new CreatePublisherViewModel());
        }

        [HttpPost]
        [ActionName("New")]
        public ActionResult Create(CreatePublisherViewModel publisher)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);
            }
            try
            {
                publisherService.Create(Mapper.Map<CreatePublisherViewModel, PublisherDTO>(publisher));
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }
            
            return RedirectToAction("Index");

        }

        public ActionResult Update(int publisherId)
        {
            try
            {
                var publisher = publisherService.Get(publisherId);
                return View(Mapper.Map<PublisherDTO, UpdatePublisherViewModel>(publisher));
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.NotFound;
            }
            return RedirectToAction("Index", "Game");
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
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Delete(int publisherId)
        {
            try
            {
                publisherService.Delete(publisherId);
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }
            return RedirectToAction("Index");
        }

        

    }
}
