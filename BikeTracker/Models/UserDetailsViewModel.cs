using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BikeTracker.Models
{
    public class UserDetailsViewModel
    {
        public long UserId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Team Name")]
        public string TeamName { get; set; }

        [DisplayName("Total Distance")]
        [UIHint("Distance")]
        public decimal TotalDistance { get; set; }

        public string Image { get; set; }

        public IEnumerable<UserDetailsActivityViewModel> Activities { get; set; }
    }
}