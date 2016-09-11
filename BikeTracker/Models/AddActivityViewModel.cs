using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BikeTracker.Models
{
    public class AddActivityViewModel
    {
        [Required]
        [DisplayName("Rider")]
        public long UserId { get; set; }

        [Required]
        [DisplayName("Activity date")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [DisplayName("Length (in km)")]
        [Range(typeof(Decimal), "1", "9999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        public decimal Length { get; set; }

        public IEnumerable<SelectListItem> AvailableRiders { get; set; }

        public string ReturnUrl { get; set; }
    }
}