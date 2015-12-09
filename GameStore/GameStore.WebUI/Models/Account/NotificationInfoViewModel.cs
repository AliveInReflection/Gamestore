using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GameStore.WebUI.Models.Account
{
    public class NotificationInfoViewModel
    {
        public int UserId { get; set; }

        [Required]
        public string NotificationMethod { get; set; }

        [Required]
        public string Target { get; set; }
    }
}
