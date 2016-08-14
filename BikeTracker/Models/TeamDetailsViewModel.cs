using System.Collections.Generic;
using System.ComponentModel;

namespace BikeTracker.Models
{
    public class TeamDetailsViewModel
    {
        public long TeamId { get; set; }

        public string Name { get; set; }

        [DisplayName("Raiders")]
        public IEnumerable<TeamDetailsUserViewModel> Users { get; set; }
    }
}