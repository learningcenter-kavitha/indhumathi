using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using LikesAndComments.Models;

namespace LikesAndComments.Controllers
{
    public class SetLikesController : Controller
    {
        private SampleProjectEntities db = new SampleProjectEntities();

        // GET: SetLikes
        public ActionResult Index()
        {
            return View(db.SetLikes.ToList());
        }

        // GET: SetLikes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetLike setLike = db.SetLikes.Find(id);
            if (setLike == null)
            {
                return HttpNotFound();
            }
            return View(setLike);
        }

        // GET: SetLikes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SetLikes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "Id,Image,Text,PostDate,PostLike")] */SetLike setLike,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                setLike.PostLike = 0;
                setLike.PostDate = DateTime.Now;
                string path = Path.Combine(Server.MapPath("~/image/"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                db.SetLikes.Add(new SetLike
                {
                    Id=setLike.Id,Image="~/image/" + file.FileName,Text=setLike.Text,PostDate=setLike.PostDate,PostLike=setLike.PostLike
                });

                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(setLike);
        }

        public ActionResult Like(int id)
        {
            SetLike update = db.SetLikes.ToList().Find(u => u.Id == id);
            update.PostLike += 1;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SetLikes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetLike setLike = db.SetLikes.Find(id);
            if (setLike == null)
            {
                return HttpNotFound();
            }
            return View(setLike);
        }

        // POST: SetLikes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Image,Text,PostDate,PostLike")] SetLike setLike)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setLike).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(setLike);
        }

        // GET: SetLikes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetLike setLike = db.SetLikes.Find(id);
            if (setLike == null)
            {
                return HttpNotFound();
            }
            return View(setLike);
        }

        // POST: SetLikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SetLike setLike = db.SetLikes.Find(id);
            db.SetLikes.Remove(setLike);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
