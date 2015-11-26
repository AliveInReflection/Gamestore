using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.App_LocalResources.Localization;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class PublisherController : BaseController
    {
        private IPublisherService publisherService;

        public PublisherController(IPublisherService publisherService, IGameStoreLogger logger)
            :base(logger)
        {
            this.publisherService = publisherService;
        }

        [Claims(GameStoreClaim.Publishers, Permissions.Retreive)]
        public ActionResult Index()
        {
            var publishers = publisherService.GetAll();
            return View(Mapper.Map<IEnumerable<PublisherDTO>, IEnumerable<DisplayPublisherViewModel>>(publishers));
        }

        [HttpGet]
        [Claims(GameStoreClaim.Publishers, Permissions.Retreive)]
        public ActionResult Details(string companyName)
        {
            try
            {
                var publisher = publisherService.Get(companyName);
                return View(Mapper.Map<PublisherDTO, DisplayPublisherViewModel>(publisher));
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.NotFound);
                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        [ActionName("New")]
        [Claims(GameStoreClaim.Publishers, Permissions.Create)]
        public ActionResult Create()
        {
            return View(new CreatePublisherViewModel());
        }

        [HttpPost]
        [ActionName("New")]
        [Claims(GameStoreClaim.Publishers, Permissions.Create)]
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
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
            }
            
            return RedirectToAction("Index");

        }

        [Claims(GameStoreClaim.Publishers, Permissions.Update)]
        public ActionResult Update(int publisherId)
        {
            try
            {
                var publisher = publisherService.Get(publisherId);
                return View(Mapper.Map<PublisherDTO, UpdatePublisherViewModel>(publisher));
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.NotFound);
            }
            return RedirectToAction("Index", "Game");
        }

        [HttpPost]
        [Claims(GameStoreClaim.Publishers, Permissions.Update)]
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
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        [Claims(GameStoreClaim.Publishers, Permissions.Delete)]
        public ActionResult Delete(int publisherId)
        {
            try
            {
                publisherService.Delete(publisherId);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
            }
            return RedirectToAction("Index");
        }

        

    }
}
