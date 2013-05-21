using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP3.Models;

namespace MP3.Controllers
{
    public class SongController : BaseController
    {
        private Entities db = new Entities();

        //
        // GET: /Song/Details/5

        public ActionResult Details(string title, int id = 0)
        {
            Songs song = db.Songs.Find(id);
            song.View++;
            db.Entry(song).State = EntityState.Modified;
            db.SaveChanges();
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        //
        // POST: /Song/Like

        [HttpPost]
        public ActionResult Like(int songId)
        {
            object result;
            if (CurrentUser == null || db.Songs.Find(songId) == null)
                return HttpNotFound();

            UserProfile user = new UserProfile { UserId = CurrentUser.UserId };
            db.UserProfile.Attach(user);
            Songs song = db.Songs.Find(songId);

            if (song.LikedUsers.Where(u => u.UserId == CurrentUser.UserId).Count() == 0)
            {
                song.LikedUsers.Add(user);
            }
            else
            {
                song.LikedUsers.Remove(user);
            }
            db.SaveChanges();

            result = new { type = "SUCCESS" };
            JsonResult json = new JsonResult();
            json.Data = result;
            return json;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}