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
    public class PatientController : Controller
    {
        private EMR_GP_DBEntities db = new EMR_GP_DBEntities();

        // GET: Patient
        public ActionResult Index(long?id)
        {
            User sesstion = (User)Session["UserID"];
            if (Session["UserID"] != null && sesstion.User_Type == 4)
            {
                    if (id != sesstion.User_Id)
                    {
                    id = sesstion.User_Id;
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    }
                    var patient = db.Patients.Find(id);
                    if (patient == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }
                    return View(patient);  
            }
            return RedirectToAction("Index", "Home");
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
                else
                {
                    return View(db.Users.ToList().Where(u => (u.User_Type == 3 && String.Compare(u.Person.First_Name.Split(' ')[0] + " " + u.Person.Last_Name.Split(' ')[0], Search, true) == 0)
                    || (u.User_Type == 2 && u.Medical_Organization.Medical_Org_Name.Contains(Search))));
                }
            }
            return RedirectToAction("Index","Patient");
        }
        // GET: Patient/Details/5
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
            if(us.Status_Of_Account == 1)
            {
                if (us.User_Type == 3)
                {
                    Medical_Organization med = db.Medical_Organization.Find(us.Person.Doctor.Medical_Org_Id);
                    ViewBag.Medical_Org_Name = med.Medical_Org_Name;
                    ViewBag.Medical_Org_Email = med.User.Email;
                    ViewBag.Medical_Org_Phone = med.User.Phone;
                    ViewBag.Medical_Org_City = med.User.City;
                    ViewBag.Medical_Org_Address = med.User.Address;
                }
                return View(us);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Manage_Account()
        {
            return RedirectToAction("Edit",new { id = ((User) Session["UserID"]).User_Id});
        }
        // GET: Patient/Create
        public ActionResult Create()
        {
            ViewBag.Patient_Id = new SelectList(db.Child_FollowUp_Form, "Patient_Id", "Feed_Type");
            ViewBag.Patient_Id = new SelectList(db.General_Examination, "Patient_Id", "Blood_Type");
            ViewBag.Patient_Id = new SelectList(db.People, "Person_Id", "First_Name");
            return View();
        }

        // POST: Patient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Patient_Id,Learning_Status,Social_Status,Death_Date,Alternative_Phone,Job,Reason_Death")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Patient_Id = new SelectList(db.Child_FollowUp_Form, "Patient_Id", "Feed_Type", patient.Patient_Id);
            ViewBag.Patient_Id = new SelectList(db.General_Examination, "Patient_Id", "Blood_Type", patient.Patient_Id);
            ViewBag.Patient_Id = new SelectList(db.People, "Person_Id", "First_Name", patient.Patient_Id);
            return View(patient);
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            ViewBag.Patient_Id = new SelectList(db.Child_FollowUp_Form, "Patient_Id", "Feed_Type", patient.Patient_Id);
            ViewBag.Patient_Id = new SelectList(db.General_Examination, "Patient_Id", "Blood_Type", patient.Patient_Id);
            ViewBag.Patient_Id = new SelectList(db.People, "Person_Id", "First_Name", patient.Patient_Id);
           
            return View(patient);
        }

        // POST: Patient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            Patient patient = null;
            patient = db.Patients.Find(long.Parse(Request.Form["Patient_Id"]));
            if (patient != null)
            {
                if (ModelState.IsValid)
                {

                    patient.Person.First_Name = Request.Form["Person.First_Name"];
                    patient.Person.Last_Name = Request.Form["Person.Last_Name"];
                    patient.Person.National_Id = Request.Form["Person.National_Id"];
                    patient.Person.Nationality = Request.Form["Person.Nationality"];
                    patient.Person.User.City = Request.Form["Person.User.City"];
                    patient.Person.User.Address = Request.Form["Person.User.Address"];
                    patient.Person.Gender = Request.Form["Person.Gender"];
                    DateTime dateTemp = Convert.ToDateTime(Request.Form["Person.Birth_Date.Day"]+"/"+ Request.Form["Person.Birth_Date.Month"]+"/"+ Request.Form["Person.Birth_Date.Year"]);
                    patient.Person.Birth_Date = dateTemp;
                    patient.Person.User.Email = Request.Form["[0]"]; // refer to email field
                    patient.Person.User.Phone = Request.Form["Person.User.Phone"];
                    patient.Alternative_Phone = Request.Form["Alternative_Phone"];
                    patient.Learning_Status = Request.Form["Learning_Status"];
                    patient.Social_Status = Request.Form["Social_Status"];
                    patient.Job = Request.Form["Job"];
                    if (Request.Form["Person.User.Password"].Split(' ')[0] == Request.Form["Confirm_Password"].Split(' ')[0])
                    {
                        patient.Person.User.Password = Request.Form["Person.User.Password"].Split(' ')[0];
                    }
                    else
                    {
                        ModelState.AddModelError("Confirm_Password", "Password do not Match Confirm Password, Password do not change");
                        return View(patient);
                    }
                    db.Entry(patient).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Patient_Id = new SelectList(db.Child_FollowUp_Form, "Patient_Id", "Feed_Type", patient.Patient_Id);
            ViewBag.Patient_Id = new SelectList(db.General_Examination, "Patient_Id", "Blood_Type", patient.Patient_Id);
            ViewBag.Patient_Id = new SelectList(db.People, "Person_Id", "First_Name", patient.Patient_Id);
            return View(patient);
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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

        public ActionResult Family_History(long? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id); 
            if(pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var fm = db.Family_History.Where(f => f.Patient_Id == pt.Patient_Id);
            return View(fm.ToList());
        }

        public ActionResult Your_Diseases()
        {
            return View();
        }

        public ActionResult Examinations()
        {
            return View();
        }

        public ActionResult Reviews()
        {
            return View();
        }

        public ActionResult Operations()
        {
            return View();
        }

        public ActionResult Laboratories_Radiology()
        {
            return View();
        }

        public ActionResult Child_Followup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload_Photo(HttpPostedFileBase file)
        {
            User u = db.Users.Find((long)Int64.Parse(Request.Form["Patient_Id"]));

            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    return RedirectToAction("Manage_Account","Patient");
                }

                if (u != null)
                {
                    if (file.FileName != null)
                    {
                        if (u.User_Type == 4)
                        {
                            file.SaveAs(Server.MapPath("~/Content/Patient_Photo/") + u.User_Id + ".jpg");
                            u.Photo_Url = "~/Content/Patient_Photo/" + u.User_Id + ".jpg";
                        }
                        db.Entry(u).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    ModelState.AddModelError("User_Id", "User Not Exist");
                }
                return RedirectToAction("Manage_Account");
            }
            return RedirectToAction("Manage_Account");
        }

        public ActionResult Book(object obj)
        {
            return null;
        }

    }
}
