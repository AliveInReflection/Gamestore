
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.WebUI.App_LocalResources.Localization;
using System.Web.Mvc;

namespace GameStore.WebUI.Models
{
    public class CardPaymentInfoViewModel
    {
        public IEnumerable<SelectListItem> CardTypeList { get; set; }

        public int OrderId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "PaymentFullName")]
        public string FullName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "PaymentCardNumber")]
        [RegularExpression("^([0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4})$", ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldTemplateNotMatсh")]
        public string CardNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "PaymentSecureCode")]
        [Range(100, 999, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "WrongFormat")]
        public int SecureCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "PaymentExpirationMonth")]
        [Range(1, 31, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "WrongFormat")]
        public int ExpirationMonth { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "PaymentExpirationYear")]
        [Range(1990, 3000, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "WrongFormat")]
        public int ExpirationYear { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "PaymentCardType")]
        public string CardType { get; set; }

    }
}
