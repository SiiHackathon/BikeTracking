﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BikeTracker.Models
{
    public class UserViewModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("Raider")]
        public string Name { get { return string.Join(" ", FirstName, LastName); } }
        [DisplayName("Team")]
        public string TeamName { get; set; }
        [DisplayName("Total Distance")]
        [UIHint("Distance")]
        public decimal TotalDistance { get; set; }
    }
}