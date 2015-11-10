using System.ComponentModel.DataAnnotations;

namespace GameStore.WebUI.Models
{
    public class IboxPaymentViewModel
    {
        [Display(Name = "Invoice number")]
        public int InvoiceId { get; set; }

        [Display(Name = "Order number")]
        public int OrderId { get; set; }

        [Display(Name = "Ammount number")]
        public decimal Amount { get; set; }
    }
}