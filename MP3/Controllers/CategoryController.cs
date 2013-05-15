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
        // GET: /Category/

        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id = 0)
        {
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categories);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = db.Categories.Find(id);
            db.Categories.Remove(categories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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