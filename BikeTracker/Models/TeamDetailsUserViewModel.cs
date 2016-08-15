using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BikeTracker.Models
{
    public class TeamDetailsUserViewModel
    {
        public long UserId { get; set; }

        public string Image { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DisplayName("Raider Name")]
        public string Name => $"{FirstName} {LastName}";

        [DisplayName("Total Distance")]
        [UIHint("Distance")]
        public decimal TotalDistance { get; set; }
    }
}