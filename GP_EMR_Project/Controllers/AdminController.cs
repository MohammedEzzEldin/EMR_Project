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
    public class AdminController : Controller
    {
        private EMR_GP_DBEntities db = new EMR_GP_DBEntities();

        // GET: Admin
        public ActionResult Index()
        {
            var med_org = db.Medical_Organization.Include(p => p.User);
            return View(med_org.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.Person_Id = new SelectList(db.Doctors, "Doctor_Id", "Acadimic_Degree");
            ViewBag.Person_Id = new SelectList(db.Patients, "Patient_Id", "Learning_Status");
            ViewBag.Person_Id = new SelectList(db.Users, "User_Id", "Email");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Person_Id,First_Name,Last_Name,Birth_Date,National_Id,Nationality,Gender")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Person_Id = new SelectList(db.Doctors, "Doctor_Id", "Acadimic_Degree", person.Person_Id);
            ViewBag.Person_Id = new SelectList(db.Patients, "Patient_Id", "Learning_Status", person.Person_Id);
            ViewBag.Person_Id = new SelectList(db.Users, "User_Id", "Email", person.Person_Id);
            return View(person);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.Person_Id = new SelectList(db.Doctors, "Doctor_Id", "Acadimic_Degree", person.Person_Id);
            ViewBag.Person_Id = new SelectList(db.Patients, "Patient_Id", "Learning_Status", person.Person_Id);
            ViewBag.Person_Id = new SelectList(db.Users, "User_Id", "Email", person.Person_Id);
            return View(person);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Person_Id,First_Name,Last_Name,Birth_Date,National_Id,Nationality,Gender")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Person_Id = new SelectList(db.Doctors, "Doctor_Id", "Acadimic_Degree", person.Person_Id);
            ViewBag.Person_Id = new SelectList(db.Patients, "Patient_Id", "Learning_Status", person.Person_Id);
            ViewBag.Person_Id = new SelectList(db.Users, "User_Id", "Email", person.Person_Id);
            return View(person);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User us = db.Users.Find(id);        
            if (us == null)
            {
                return HttpNotFound();
            }
            return View(us);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Details_Medical_Org(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medical_Organization med_org = db.Medical_Organization.Find(id);
            if (med_org == null)
            {
                return HttpNotFound();
            }
            return View(med_org);
        }

        public ActionResult Active_Medical_Org(long?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medical_Organization med_org = db.Medical_Organization.Find(id);
            if (med_org == null)
            {
                return HttpNotFound();
            }
            med_org.User.Status_Of_Account = 1;
            db.Entry(med_org).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
