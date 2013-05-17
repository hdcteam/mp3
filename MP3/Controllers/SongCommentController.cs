using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP3.Models;
using System.Web.Script.Serialization;
using System.IO;

namespace MP3.Controllers
{
    public class SongCommentController : BaseController
    {
        private Entities db = new Entities();

        //
        // GET: /SongComment/

        public ActionResult Index()
        {
            var songcomment = db.SongComment.Include(s => s.Song).Include(s => s.User);
            return View(songcomment.ToList());
        }

        //
        // GET: /SongComment/Details/5

        public ActionResult Details(int id = 0)
        {
            SongComment songcomment = db.SongComment.Find(id);
            if (songcomment == null)
            {
                return HttpNotFound();
            }
            return View(songcomment);
        }

        ////
        //// GET: /SongComment/Create

        //public ActionResult Create()
        //{
        //    ViewBag.SongId = new SelectList(db.Songs, "Id", "Name");
        //    ViewBag.UserId = new SelectList(db.UserProfile, "UserId", "UserName");
        //    return View();
        //}

        //
        // POST: /SongComment/Create

        [HttpPost]
        public ActionResult Create(int songId, string content)
        {
            object result;

            if (CurrentUser == null) {
                result = new { type = "ERROR", msg = "Bạn cần đăng nhập để đăng bình luận!" };
            }

            SongComment songComment = new SongComment();
            songComment.UserId = CurrentUser.UserId;
            songComment.SongId = songId;
            songComment.Content = content;
            songComment.PostTime = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.SongComment.Add(songComment);
                db.SaveChanges();
                ViewEngineResult view = ViewEngines.Engines.FindPartialView(ControllerContext, "~/Views/Song/_CommentDisplay.cshtml");
                using (StringWriter writer = new StringWriter())
                {
                    db.Entry(songComment).Reference(s => s.User).Load();
                    db.Entry(songComment).Reference(s => s.Song).Load();
                    this.ViewData.Model = songComment;
                    var context = new ViewContext(ControllerContext, view.View, ViewData, TempData, writer);
                    view.View.Render(context, writer);
                    writer.Flush();
                    content = writer.ToString();
                }
                result = new { type = "SUCCESS", html = content };
            }
            else
                result = new { type = "ERROR", msg = "Có lỗi xảy ra!" };

            JsonResult jsonResult = new JsonResult();
            jsonResult.Data = result;
            jsonResult.ContentEncoding = System.Text.Encoding.UTF8;

            return jsonResult;
        }

        //
        // GET: /SongComment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SongComment songcomment = db.SongComment.Find(id);
            if (songcomment == null)
            {
                return HttpNotFound();
            }
            ViewBag.SongId = new SelectList(db.Songs, "Id", "Name", songcomment.SongId);
            ViewBag.UserId = new SelectList(db.UserProfile, "UserId", "UserName", songcomment.UserId);
            return View(songcomment);
        }

        //
        // POST: /SongComment/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SongComment songcomment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(songcomment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SongId = new SelectList(db.Songs, "Id", "Name", songcomment.SongId);
            ViewBag.UserId = new SelectList(db.UserProfile, "UserId", "UserName", songcomment.UserId);
            return View(songcomment);
        }

        //
        // GET: /SongComment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SongComment songcomment = db.SongComment.Find(id);
            if (songcomment == null)
            {
                return HttpNotFound();
            }
            return View(songcomment);
        }

        //
        // POST: /SongComment/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SongComment songcomment = db.SongComment.Find(id);
            db.SongComment.Remove(songcomment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}