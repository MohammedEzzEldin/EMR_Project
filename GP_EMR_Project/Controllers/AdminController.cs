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
            User us = (User) Session["UserID"];
            if(us == null)
            {
                return RedirectToAction("Index","Home");
            }
            else if(us.User_Type != 1) {
                return RedirectToAction("Index", "Home");
            }
            var med_org = db.Medical_Organization.Include(p => p.User).Where(med => med.User.Status_Of_Account == 0);
            return View(med_org.ToList());
        }

        //GET: Medical Organization
        public ActionResult Index_Medical_Org()
        {
            User us = (User)Session["UserID"];
            if (us == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (us.User_Type != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            var med_org = db.Medical_Organization.Include(p => p.User).OrderByDescending(md => md.Medium_Rate).Take(10);
            return View(med_org.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(long? id)
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
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
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

        [HttpPost]
        public ActionResult Search(string Filter_by, string Search)
        {
            if (ModelState.IsValid)
            {
                if (Search.Contains('@'))
                {
                    return View(db.Users.ToList().Where(u => u.Email.Contains(Search)));
                }
                else if (Filter_by.Equals("Hospitals"))
                {
                    return View(db.Users.ToList().Where(md => md.User_Type == 2 && md.Medical_Organization.Medical_Org_Name.Contains(Search)));
                }
                else if (Filter_by.Equals("Doctors"))
                {
                    if (Search.Contains(' '))
                    {
                        return View(db.Users.ToList().Where(dc => dc.User_Type == 3 && String.Compare(dc.Person.First_Name.Split(' ')[0] + " " + dc.Person.Last_Name.Split(' ')[0], Search, true) == 0));
                    }
                    else
                    {
                        return View(db.Users.ToList().Where(dc => dc.User_Type == 3 && dc.Person.First_Name.Contains(Search)));
                    }
                }
                else if (Filter_by.Equals("Patients"))
                {
                    if (Search.Contains(' '))
                    {
                        return View(db.Users.ToList().Where(pt => pt.User_Type == 4 && String.Compare(pt.Person.First_Name.Split(' ')[0] + " " + pt.Person.Last_Name.Split(' ')[0], Search, true) == 0));
                    }
                    else
                    {
                        return View(db.Users.ToList().Where(pt => pt.User_Type == 4 && pt.Person.First_Name.Contains(Search)));
                    }
                }
                else
                {
                    return View(db.Users.ToList().Where(u => (u.User_Type == 3 && String.Compare(u.Person.First_Name.Split(' ')[0] + " " + u.Person.Last_Name.Split(' ')[0], Search, true) == 0)
                    || (u.User_Type == 2 && u.Medical_Organization.Medical_Org_Name.Contains(Search))));
                }
            }
            return RedirectToAction("Index","Admin");
        }

        [HttpPost]
        public ActionResult Search_Medical_Org(string Search, string Search_by)
        {
            switch (Search_by)
            {
                case "med_phone":
                    return View("Index_Medical_Org", db.Medical_Organization.Where(med => med.User.Phone.Contains(Search)));

                case "med_name":
                    return View("Index_Medical_Org", db.Medical_Organization.Where(med => med.Medical_Org_Name.Contains(Search)));

                case "med_city":
                    return View("Index_Medical_Org",db.Medical_Organization.Where(med => med.User.City.Contains(Search)));

                case "med_email":
                    return View("Index_Medical_Org",db.Medical_Organization.Where(med => med.User.Email.Contains(Search)));
                default:
                    return RedirectToAction("Index_Medical_Org");
            }
        }

        [HttpPost]
        public ActionResult Search_Patient(string Search,string Search_by)
        {
            switch (Search_by)
            {
                case "pt_phone":
                    return View("Index_Patients", db.Patients.Where(pt => pt.Person.User.Phone.Contains(Search)));

                case "pt_fname":
                    return View("Index_Patients", db.Patients.Where(pt => pt.Person.First_Name.Contains(Search)));

                case "pt_Lname":
                    return View("Index_Patients", db.Patients.Where(pt => pt.Person.Last_Name.Contains(Search)));

                case "pt_email":
                    return View("Index_Patients", db.Patients.Where(pt => pt.Person.User.Email.Contains(Search)));
                case "pt_city":
                    return View("Index_Patients", db.Patients.Where(pt => pt.Person.User.City.Contains(Search)));
                default:
                    return RedirectToAction("Index_Patients");
            }
        }

        public ActionResult Filter(string Filter_by, string Order_by)
        {
            if(Order_by.Equals("asc"))
            {
                    switch (Filter_by)
                    {
                       case "High_Rate":
                        return View("Index_Medical_Org", db.Medical_Organization.OrderByDescending(med => med.Medium_Rate));
                       case "Low_Rate":
                        return View("Index_Medical_Org", db.Medical_Organization.OrderBy(med => med.Medium_Rate));
                       case "Name":
                        return View("Index_Medical_Org", db.Medical_Organization.OrderBy(med => med.Medical_Org_Name));
                       case "City":
                        return View("Index_Medical_Org", db.Medical_Organization.OrderBy(med => med.User.City));
                       default:
                            break;
                    }
            }
            else if (Order_by.Equals("dsc"))
            {
                switch (Filter_by)
                {
                    case "High_Rate":
                        return View("Index_Medical_Org", db.Medical_Organization.OrderByDescending(med => med.Medium_Rate));
                    case "Low_Rate":
                        return View("Index_Medical_Org", db.Medical_Organization.OrderBy(med => med.Medium_Rate));
                    case "Name":
                        return View("Index_Medical_Org", db.Medical_Organization.OrderByDescending(med => med.Medical_Org_Name));
                    case "City":
                        return View("Index_Medical_Org", db.Medical_Organization.OrderByDescending(med => med.User.City));
                    default:
                        break;
                }
            }
            return RedirectToAction("Index_Medical_Org");
        }

        public ActionResult Filter_Patient(string Filter_by, string Order_by)
        {
            if (Order_by.Equals("asc"))
            {
                switch (Filter_by)
                {
                    case "Death":
                        return View("Index_Patients", db.Patients.Where(pt => pt.Death_Date != null).OrderByDescending(pt => pt.Death_Date));
                    case "Lname":
                        return View("Index_Patients", db.Patients.OrderBy(pt => pt.Person.Last_Name));
                    case "fname":
                        return View("Index_Patients", db.Patients.OrderBy(pt => pt.Person.First_Name));
                    case "City":
                        return View("Index_Patients", db.Patients.OrderBy(pt => pt.Person.User.City));
                    default:
                        break;
                }
            }
            else if (Order_by.Equals("dsc"))
            {
                switch (Filter_by)
                {
                    case "Death":
                        return View("Index_Patients", db.Patients.Where(pt => pt.Death_Date != null).OrderByDescending(pt => pt.Death_Date));
                    case "Lname":
                        return View("Index_Patients", db.Patients.OrderBy(pt => pt.Person.Last_Name));
                    case "fname":
                        return View("Index_Patients", db.Patients.OrderByDescending(pt => pt.Person.First_Name));
                    case "City":
                        return View("Index_Patients", db.Patients.OrderByDescending(pt => pt.Person.User.City));
                    default:
                        break;
                }
            }
            return RedirectToAction("Index_Patients");
        }
        public ActionResult Index_Patients()
        {
            User us = (User)Session["UserID"];
            if (us == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (us.User_Type != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            var patient = db.Patients.Include(p => p.Person).Include(u => u.Person.User).OrderBy(md => md.Person.First_Name+" "+md.Person.Last_Name).Take(10);
            return View(patient.ToList());
        }

        public ActionResult Doctors()
        {
            return View();
        }

        public ActionResult Medical_Organizations()
        {
            return View();
        }

        public ActionResult Active_Medical_Org(long? id)
        {
            if(id == null)
            {
                return  new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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

        public ActionResult Block(long? id)
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
            us.Status_Of_Account = 2;
            db.Entry(us).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index_Medical_Org");
        }
    }
}
