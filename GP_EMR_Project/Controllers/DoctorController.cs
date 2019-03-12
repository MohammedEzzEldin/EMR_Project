using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GP_EMR_Project.Models;

namespace GP_EMR_Project.Controllers
{
    public class DoctorController : Controller
    {
        private EMR_GP_DBEntities db = new EMR_GP_DBEntities();

        // GET: Doctor
        public ActionResult Index()
        {
            var doctors = db.Doctors.Include(d => d.Department).Include(d => d.Person);
            return View(doctors.ToList());
        }

        // GET: Doctor/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctor/Create
        public ActionResult Create()
        {
            ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Department_Name");
            ViewBag.Doctor_Id = new SelectList(db.People, "Person_Id", "First_Name");
            return View();
        }

        // POST: Doctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Doctor_Id,Medium_Rate,Acadimic_Degree,Functional_Degree,Spacialization,Medical_Org_Id,Department_Id")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Department_Name", doctor.Department_Id);
            ViewBag.Doctor_Id = new SelectList(db.People, "Person_Id", "First_Name", doctor.Doctor_Id);
            return View(doctor);
        }

        // GET: Doctor/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Department_Name", doctor.Department_Id);
            ViewBag.Doctor_Id = new SelectList(db.People, "Person_Id", "First_Name", doctor.Doctor_Id);
            return View(doctor);
        }

        // POST: Doctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Doctor_Id,Medium_Rate,Acadimic_Degree,Functional_Degree,Spacialization,Medical_Org_Id,Department_Id")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Department_Name", doctor.Department_Id);
            ViewBag.Doctor_Id = new SelectList(db.People, "Person_Id", "First_Name", doctor.Doctor_Id);
            return View(doctor);
        }

        // GET: Doctor/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
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
