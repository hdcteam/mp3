using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP3.Models;
using System.Web.Security;

namespace MP3.Controllers
{
    public class BaseController : Controller
    {
        public UserProfile CurrentUser = null;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Entities db = new Entities();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Countries = db.Countries.ToList();
            base.OnActionExecuting(filterContext);

            if (User.Identity.IsAuthenticated)
            {
                CurrentUser = db.UserProfile.Where(u => u.UserName == User.Identity.Name).First();
                ViewBag.CurrentUser = CurrentUser;
            }
        }
    }
}
