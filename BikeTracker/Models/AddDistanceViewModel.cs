﻿using System;

namespace BikeTracker.Models
{
    public class AddDistanceViewModel
    {
        public long UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Length { get; set; }


        public string ReturnUrl { get; set; }
    }
}