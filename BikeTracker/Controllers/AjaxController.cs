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
                .Select(Mapper.Map<TeamDetailsViewModel>);


			return Json(teams, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TrackCompleted(long teamId, int distance)
        {
            var repo = DependencyFactory.CreateTeamRepository;
            var team = repo.GetById(teamId);
            if (team == null)
                return Json("Error: team not found", JsonRequestBehavior.AllowGet);

            team.CurrentDistance = distance;
            team.ReverseRoute = true;
            team.TracksCompleted++;
            repo.Save(team);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}