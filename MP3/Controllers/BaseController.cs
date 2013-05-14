﻿using System;
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
            base.OnActionExecuting(filterContext);

            MembershipUser user = Membership.GetUser(User.Identity.Name);
            if (user != null)
            {
                CurrentUser = db.UserProfile.Where(u => u.UserName == user.UserName).First();
                ViewBag.CurrentUser = CurrentUser;
            }
        }
    }
}
