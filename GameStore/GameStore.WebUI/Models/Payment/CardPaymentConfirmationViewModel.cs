using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models.Payment
{
    public class CardPaymentConfirmationViewModel
    {
        public int OrderId { get; set; }
        public string CardNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "CardConfirmationCode")]
        public string ConfirmationCode { get; set; }
    }
}