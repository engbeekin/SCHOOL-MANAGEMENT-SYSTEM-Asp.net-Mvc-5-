using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolMangement.Models;

namespace SchoolMangement.Controllers
{
    public class TeacherController : Controller
    {
        private StudentMangementEntities db = new StudentMangementEntities();

        // GET: Teacher
        public ActionResult Index()
        {
            var techears = db.techears.Include(t => t.course);
            return View(techears.ToList());
        }

        // GET: Teacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            techear techear = db.techears.Find(id);
            if (techear == null)
            {
                return HttpNotFound();
            }
            return View(techear);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            ViewBag.coursename = new SelectList(db.courses, "id", "course1");
            return View();
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,coursename")] techear techear)
        {
            if (ModelState.IsValid)
            {
                db.techears.Add(techear);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.coursename = new SelectList(db.courses, "id", "course1", techear.coursename);
            return View(techear);
        }

        // GET: Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            techear techear = db.techears.Find(id);
            if (techear == null)
            {
                return HttpNotFound();
            }
            ViewBag.coursename = new SelectList(db.courses, "id", "course1", techear.coursename);
            return View(techear);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,coursename")] techear techear)
        {
            if (ModelState.IsValid)
            {
                db.Entry(techear).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.coursename = new SelectList(db.courses, "id", "course1", techear.coursename);
            return View(techear);
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            techear techear = db.techears.Find(id);
            if (techear == null)
            {
                return HttpNotFound();
            }
            return View(techear);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            techear techear = db.techears.Find(id);
            db.techears.Remove(techear);
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
