using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using BikeTracker.Entities;
using BikeTracker.Models;
using BikeTracker.Repositories;
using System.Web.Mvc;

namespace BikeTracker.Controllers
{
    public class AjaxController : Controller
    {
        public JsonResult GetRoute()
        {
            // TODO load it from DB
            var model = new RouteModel
            {
                Origin = "54.402658,18.572466",
                Destination = "52.945601,-1.159719"
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeamStandings()
        {
            var teamRepo = DependencyFactory.CreateTeamRepository;
            var teams = teamRepo.GetAll()
                .Select(Mapper.Map<TeamStandingsModel>);

            //var teams = new List<TeamStandingsModel>();
            //teams.Add(new TeamStandingsModel
            //{
            //    TeamId = 1,
            //    Name = "Webminions",
            //    CurrentDistance = 659600
            //});
            //teams.Add(new TeamStandingsModel
            //{
            //    TeamId = 2,
            //    Name = "Someone",
            //    CurrentDistance = 183000
            //});

            return Json(teams, JsonRequestBehavior.AllowGet);
        }
        
    }
}