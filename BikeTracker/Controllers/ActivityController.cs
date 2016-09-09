using System;
using System.Linq;
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
                AvailableRiders = DependencyFactory.CreateUserRepository.GetAll()
                        .Select(user => new SelectListItem { Value = user.UserId.ToString(), Text = $"{user.FirstName} {user.LastName}" }),
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
                model.AvailableRiders = DependencyFactory.CreateUserRepository.GetAll()
                    .Select(user => new SelectListItem { Value = user.UserId.ToString(), Text = $"{user.FirstName} {user.LastName}" });

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

                if (team.CurrentDistance < 0)
                {
                    team.ReverseRoute = false;
                    team.CurrentDistance = -team.CurrentDistance;
                    team.TracksCompleted++;
                }

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