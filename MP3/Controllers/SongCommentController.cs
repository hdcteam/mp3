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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}