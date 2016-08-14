using System;
using System.Web.Mvc;
using AutoMapper;
using BikeTracker.Entities;
using BikeTracker.Models;
using BikeTracker.Repositories;

namespace BikeTracker.Controllers
{
    public class DistanceController : Controller
    {
        public ActionResult Add()
        {
            var model = new AddDistanceViewModel
            {
                CreatedOn = DateTime.Today,
                ReturnUrl = HttpContext.Request.UrlReferrer.PathAndQuery
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AddDistanceViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var rider = RepositoryFactory.CreateUserRepository.GetById(model.UserId);
                if (rider == null)
                {
                    ModelState.AddModelError("UserId", "Invalid User Id");
                    return View(model);
                }
                var teamRepo = RepositoryFactory.CreateTeamRepository;
                var team = teamRepo.GetById(rider.TeamId);
                if (team == null)
                {
                    ModelState.AddModelError("UserId", "User team not found");
                    return View(model);
                }
                var distance = Mapper.Map<Distance>(model);
                team.CurrentDistance = (team.ReverseRoute) ?
                    team.CurrentDistance - distance.Length :
                    team.CurrentDistance + distance.Length;

                teamRepo.Save(team);
                RepositoryFactory.CreateDistanceRepository.Save(distance);

                return Redirect(model.ReturnUrl);
            }
            catch
            {
                return View(model);
            }
        }
    }
}