using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP3.Models;
using PagedList;

namespace MP3.Controllers
{
    public class CategoryController : BaseController
    {
        private Entities db = new Entities();

        //
        // GET: /Category/ListMusic

        public ActionResult ListMusic(int id, string title, int? page)
        {
            Categories category = db.Categories.Find(id);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.id = id;
            ViewBag.categoryTitle = title;
            ViewBag.category = category;
            return View(category.Songs.Where(s => s.VideoFile == null).ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Category/ListVideo

        public ActionResult ListVideo(int id, string title, int? page)
        {
            Categories category = db.Categories.Find(id);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.id = id;
            ViewBag.categoryTitle = title;
            ViewBag.category = category;
            return View(category.Songs.Where(s => s.MusicFile == null).ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Album/ListVideo

        public ActionResult ListAlbum(int id, string title, int? page)
        {
            Categories category = db.Categories.Find(id);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.id = id;
            ViewBag.categoryTitle = title;
            ViewBag.category = category;
            return View(category.Playlists.Where(s => s.User == null).ToPagedList(pageNumber, pageSize));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}