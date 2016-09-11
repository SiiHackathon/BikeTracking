using AutoMapper;
using BikeTracker.Entities;
using BikeTracker.Models;
using System.Linq;
using System.IO;
using System.Web.Mvc;

namespace BikeTracker.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private const string DefaultRiderImgPath = "~/Images/Riders/Defaut.jpg";

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(DependencyFactory.CreateUserRepository.GetAll()
                .Select(GetViewModel));
        }

        public ActionResult List()
        {
            return View(DependencyFactory.CreateUserRepository.GetAll()
                .Select(GetViewModel));
        }

        [AllowAnonymous]
        public ActionResult Details(long id)
        {
            var user = DependencyFactory.CreateUserRepository.GetById(id);
            return View(new UserDetailsViewModel
            {
                UserId = user?.UserId ?? 0,
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                TeamName = GetTeamName(user),
                Image = user?.Image ?? DefaultRiderImgPath,
                TotalDistance = DependencyFactory.CreateActivityService.GetUserTotalDistance(id),
                Activities = DependencyFactory.CreateActivityRepository.GetByUserId(id)
                    .Select(activity => new UserDetailsActivityViewModel
                    {
                        ActivityId = activity.ActivityId,
                        ActivityDate = activity.ActivityDate,
                        Distance = (decimal)activity.Distance / 1000
                    })
                    .OrderBy(x=>x.ActivityDate)
                    .ToArray()
            });
        }
        
        public ActionResult Edit(long id)
        {
            var model = (id == 0) ?
                new UserEditModel() :
                Mapper.Map<UserEditModel>(DependencyFactory.CreateUserRepository.GetById(id));
            model.AvailableTeams = DependencyFactory.CreateTeamRepository.GetAll()
                        .Select(team => new SelectListItem { Value = team.TeamId.ToString(), Text = team.Name });
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditModel model)
        {
            try
            {
                model.AvailableTeams = DependencyFactory.CreateTeamRepository.GetAll()
                        .Select(team => new SelectListItem { Value = team.TeamId.ToString(), Text = team.Name });
                if (!ModelState.IsValid)
                    return View(model);

                User user = (model.UserId == 0) ?
                    new User() :
                    DependencyFactory.CreateUserRepository.GetById(model.UserId);
                Mapper.Map(model, user);

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images/Riders"), fileName);
                        file.SaveAs(path);
                        user.Image = $"~/Images/Riders/{fileName}";
                    }
                }

                DependencyFactory.CreateUserRepository.Save(user);

                return RedirectToAction("List");
            }
            catch
            {
                return View(model);
            }
        }
        
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                DependencyFactory.CreateUserRepository.DeleteById(id);

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
        
        private static UserViewModel GetViewModel(User user)
        {
            var model = Mapper.Map<UserViewModel>(user);
            model.TeamName = DependencyFactory.CreateTeamRepository.GetById(user.TeamId)?.Name;
            model.TotalDistance = DependencyFactory.CreateActivityService.GetUserTotalDistance(user.UserId);
            return model;
        }

        private static string GetTeamName(User user)
        {
            return DependencyFactory.CreateTeamRepository.GetById(user?.TeamId ?? 0)?.Name;
        }
    }
}
