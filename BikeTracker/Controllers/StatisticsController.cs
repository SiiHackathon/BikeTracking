﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeTracker.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        public ActionResult Index()
        {
            return View(new Models.StatisticsViewModel()
            {
                KilometersPassed = 12345,
                tralalal = 123456,
            });
        }
    }
}