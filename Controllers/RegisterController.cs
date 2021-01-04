using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using SchoolMangement.Models;

namespace SchoolMangement.Controllers
{
    public class RegisterController : Controller
    {
        private StudentMangementEntities db = new StudentMangementEntities();

        // GET: Register
        public ActionResult Index()
        {
            var registrations = db.registrations.Include(r => r.batch).Include(r => r.course).Include(r => r.techear);
            return View(registrations.ToList());
        }

        //Reports

            public ActionResult Report(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/student.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "StudentDataSet";
            reportDataSource.Value = db.registrations.ToList();
            localReport.DataSources.Add(reportDataSource);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if( reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            } 
           else if( reportType == "Word")
            {
                fileNameExtension = "docx";
            } 
          else  if( reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }
            else
            {
                fileNameExtension = "jpg";
            }

            string[ ] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension,
                out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment:filename=student_report." + fileNameExtension);


            return File(renderedByte, fileNameExtension);
        }
        // GET: Register/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registration registration = db.registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Register/Create
        public ActionResult Create()
        {
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1");
            ViewBag.course_id = new SelectList(db.courses, "id", "course1");
            ViewBag.techearname = new SelectList(db.techears, "id", "name");
            return View();
        }

        // POST: Register/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id,techearname,mobile")] registration registration)
        {
            if (ModelState.IsValid)
            {
                db.registrations.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registration.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course1", registration.course_id);
            ViewBag.techearname = new SelectList(db.techears, "id", "name", registration.techearname);
            return View(registration);
        }

        // GET: Register/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registration registration = db.registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registration.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course1", registration.course_id);
            ViewBag.techearname = new SelectList(db.techears, "id", "name", registration.techearname);
            return View(registration);
        }

        // POST: Register/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id,techearname,mobile")] registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registration.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course1", registration.course_id);
            ViewBag.techearname = new SelectList(db.techears, "id", "name", registration.techearname);
            return View(registration);
        }

        // GET: Register/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registration registration = db.registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            registration registration = db.registrations.Find(id);
            db.registrations.Remove(registration);
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
