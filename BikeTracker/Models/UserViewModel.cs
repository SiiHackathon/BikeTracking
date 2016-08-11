using System.Collections.Generic;
using System.Web.Mvc;

namespace BikeTracker.Models
{
    public class UserViewModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long TeamId { get; set; }
        public string TeamName { get; set; }
        public IEnumerable<SelectListItem> AvailableTeams { get; set; }
    }
}