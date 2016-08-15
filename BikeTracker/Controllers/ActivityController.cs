using System;
using System.Web.Mvc;
using AutoMapper;
using BikeTracker.Entities;
using BikeTracker.Models;

namespace BikeTracker.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {
        public ActionResult Add()
        {
            var model = new AddActivityViewModel
            {
                CreatedOn = DateTime.Today,
                ReturnUrl = HttpContext.Request.UrlReferrer.PathAndQuery
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddActivityViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var rider = DependencyFactory.CreateUserRepository.GetById(model.UserId);
                if (rider == null)
                {
                    ModelState.AddModelError("UserId", "Invalid User Id");
                    return View(model);
                }
                var teamRepo = DependencyFactory.CreateTeamRepository;
                var team = teamRepo.GetById(rider.TeamId);
                if (team == null)
                {
                    ModelState.AddModelError("UserId", "User team not found");
                    return View(model);
                }
                var activity = Mapper.Map<Activity>(model);
                team.CurrentDistance = (team.ReverseRoute) ?
                    team.CurrentDistance - activity.Distance :
                    team.CurrentDistance + activity.Distance;

                teamRepo.Save(team);
                DependencyFactory.CreateActivityRepository.Save(activity);

                return Redirect(model.ReturnUrl);
            }
            catch
            {
                return View(model);
            }
        }
    }
}