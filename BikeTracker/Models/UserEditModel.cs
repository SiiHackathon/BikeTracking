using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BikeTracker.Models
{
    public class UserEditModel
    {
        public long UserId { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Image { get; set; }

        [Required]
        [DisplayName("Team")]
        public long TeamId { get; set; }

        public IEnumerable<SelectListItem> AvailableTeams { get; set; }
    }
}