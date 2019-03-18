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
    public class Medical_OrganizationController : Controller
    {
        private EMR_GP_DBEntities db = new EMR_GP_DBEntities();

        // GET: Medical_Organization
        public ActionResult Index()
        {
            User user = (User)Session["UserID"];
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if(user.User_Type != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        // GET: Medical_Organization/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medical_Organization medical_Organization = db.Medical_Organization.Find(id);
            if (medical_Organization == null)
            {
                return HttpNotFound();
            }
            return View(medical_Organization);
        }

        // GET: Medical_Organization/Create
        public ActionResult Create()
        {
            ViewBag.Medical_Org_Id = new SelectList(db.Users, "User_Id", "Email");
            return View();
        }

        // POST: Medical_Organization/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Medical_Org_Id,Medical_Org_Name,Medium_Rate")] Medical_Organization medical_Organization)
        {
            if (ModelState.IsValid)
            {
                db.Medical_Organization.Add(medical_Organization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Medical_Org_Id = new SelectList(db.Users, "User_Id", "Email", medical_Organization.Medical_Org_Id);
            return View(medical_Organization);
        }

        // GET: Medical_Organization/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medical_Organization medical_Organization = db.Medical_Organization.Find(id);
            if (medical_Organization == null)
            {
                return HttpNotFound();
            }
            ViewBag.Medical_Org_Id = new SelectList(db.Users, "User_Id", "Email", medical_Organization.Medical_Org_Id);
            return View(medical_Organization);
        }

        // POST: Medical_Organization/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Medical_Org_Id,Medical_Org_Name,Medium_Rate")] Medical_Organization medical_Organization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medical_Organization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Medical_Org_Id = new SelectList(db.Users, "User_Id", "Email", medical_Organization.Medical_Org_Id);
            return View(medical_Organization);
        }

        // GET: Medical_Organization/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medical_Organization medical_Organization = db.Medical_Organization.Find(id);
            if (medical_Organization == null)
            {
                return HttpNotFound();
            }
            return View(medical_Organization);
        }

        // POST: Medical_Organization/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Medical_Organization medical_Organization = db.Medical_Organization.Find(id);
            db.Medical_Organization.Remove(medical_Organization);
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

        public ActionResult Search(string Filter_by,string Search)
        {
            return RedirectToAction("Index");
        }

        public ActionResult Manage_Account()
        {
            return View();
        }

        public ActionResult Manage_Doctors()
        {
            return View();
        }

        public ActionResult Manage_Department()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add_New_Department()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add_New_Department(Department dep)
        {
            return RedirectToAction("Index");
        }
    }
}
