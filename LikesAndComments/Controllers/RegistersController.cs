using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LikesAndComments.Models;

namespace LikesAndComments.Controllers
{
    public class RegistersController : Controller
    {
        private SampleProjectEntities3 db = new SampleProjectEntities3();

        // GET: Registers
        public ActionResult Index()
        {
            return View(db.Registers.ToList());
        }

        // GET: Registers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = db.Registers.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // GET: Registers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Registers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Gender,Email,PhoneNo,Password,ConfirmPassword")] Register register)
        {
            if (ModelState.IsValid)
            {
                db.Registers.Add(register);
                db.SaveChanges();
                return RedirectToAction("login");
            }

            return View(register);
        }

        // GET: Registers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = db.Registers.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // POST: Registers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Gender,Email,PhoneNo,Password,ConfirmPassword")] Register register)
        {
            if (ModelState.IsValid)
            {
                db.Entry(register).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(register);
        }

        // GET: Registers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = db.Registers.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // POST: Registers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Register register = db.Registers.Find(id);
            db.Registers.Remove(register);
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

        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        //public ActionResult login(Register reg, string email, string pass)
        //{
        //     reg.Email = email;
        //     reg.Password = pass;
        //     var obj = db.Registers.ToList();
        //     var user = obj.SingleOrDefault(usr => usr.Email == reg.Email && usr.Password == reg.Password);
        //     if (user != null)
        //     {
        //         Session["email"] = user.Email.ToString();
        //         Session["password"] = user.Password.ToString();
        //         return RedirectToAction("Index", "SetLikes");
        //     }
        //    return View();
        //}
        public ActionResult login(Register reg, string email, string password)
        {
            reg.Email = email;
            reg.Password = password;
            var obj = db.Registers.ToList();
            var user = obj.SingleOrDefault(usr => usr.Email == reg.Email && usr.Password == reg.Password);
            if (user != null)
            {
                Session["Email"] = user.Email.ToString();
                Session["Password"] = user.Password.ToString();
                return RedirectToAction("Index", "SetLikes");
            }
            return View();
        }
    }
}
