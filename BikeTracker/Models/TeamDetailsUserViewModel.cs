using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BikeTracker.Models
{
    public class TeamDetailsUserViewModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("Raider Name")]
        public string Name => string.Join(" ", FirstName, LastName);
        [DisplayName("Total Distance")]
        [UIHint("Distance")]
        public decimal TotalDistance { get; set; }
    }
}