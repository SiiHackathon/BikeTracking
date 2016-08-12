using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BikeTracker.Models
{
    public class UserDetailsActivityViewModel
    {
        public long ActivityId { get; set; }
        [DisplayName("Date")]
        public DateTime ActivityDate { get; set; }
        [UIHint("Distance")]
        public decimal Distance { get; set; }
    }
}