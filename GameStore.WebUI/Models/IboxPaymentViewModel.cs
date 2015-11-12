using System.ComponentModel.DataAnnotations;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{
    public class IboxPaymentViewModel
    {
        [Display(ResourceType = typeof(ModelRes), Name = "IboxInvoiseId")]
        public int InvoiceId { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "IboxOrderId")]
        public int OrderId { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "IboxAmount")]
        public decimal Amount { get; set; }
    }
}