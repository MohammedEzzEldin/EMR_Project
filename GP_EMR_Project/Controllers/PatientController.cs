using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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

        [NonAction]
        private bool Check_Login()
        {
            User sesstion = null;
            sesstion = (User) Session["UserID"];
            if(sesstion !=  null){
                if (sesstion.User_Type == 4)
                {
                    return true;
                }
            }
            return false;
        }
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

        public ActionResult Details_Lab_Radio(long? id ,string name,DateTime date)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if(pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Lab lab = db.Labs.Where(lb => lb.Patient_Id == id && lb.Lab_Date == date && lb.Lab_Name == name).Single();
                return View(lab);
        }
        public void Show_Lab_Radio(long? id, string name, DateTime date)
        {
            if (id != null)
            {
                Lab lab = db.Labs.Where(lb => lb.Patient_Id == id && lb.Lab_Date == date && lb.Lab_Name == name).Single();
                if(lab != null)
                {
                    string filePath = Server.MapPath(lab.Lab_File);
                    string ext = Path.GetExtension(filePath);
                    string type = "";

                    if (ext != null)
                    {
                        switch (ext.ToLower())
                        {
                            case ".txt":
                                type = "text/plain";
                                break;

                            case ".GIF":
                                type = "image/GIF";
                                break;

                            case ".pdf":
                                type = "Application/pdf";
                                break;

                            case ".doc":
                            case ".rtf":
                                type = "Application/msword";
                                break;
                        }
                        WebClient client = new WebClient();
                        byte[] filebuffer = client.DownloadData(filePath);
                        if (filebuffer != null && type != "")
                        {
                            Response.ContentType =type;
                            Response.AddHeader("content-length", filebuffer.Length.ToString());
                            Response.BinaryWrite(filebuffer);
                        }
                    } 
                }
            }
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
            ViewBag.Patient_Id = pt.Patient_Id;
            var fm = db.Family_History.Where(f => f.Patient_Id == pt.Patient_Id);     
            return View(fm.ToList());
        }

        [HttpPost]
        public ActionResult Search_In_Family_History(string Search)
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            IQueryable<Family_History> fm = db.Family_History;
            if (Request.Form["Search_By"] == "Name")
            {
                fm = db.Family_History.Where(f => f.Patient_Id == pt.Patient_Id && String.Compare(f.Disease_Name, Search, true) == 0);
            }
            else if (Request.Form["Search_By"] == "Type")
            {
                fm = db.Family_History.Where(f => f.Patient_Id == pt.Patient_Id && String.Compare(f.Disease_Type, Search, true) == 0);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            return View("Family_History",fm.ToList());
        }

        [HttpPost]
        public ActionResult Search_In_Your_Disease(string Search)
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            IQueryable<Disease> ds = db.Diseases; 
            if (Request.Form["Search_By"] == "Name")
            {
                ds = db.Diseases.Where(f => f.Patient_Id == pt.Patient_Id && String.Compare(f.Disease_Name, Search, true) == 0);
            }
            else if(Request.Form["Search_By"] == "Type")
            {
                ds = db.Diseases.Where(f => f.Patient_Id == pt.Patient_Id && String.Compare(f.Disease_Type, Search, true) == 0);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            return View("Your_Diseases", ds.ToList());
        }

        [HttpPost]
        public ActionResult Search_In_Medical_Examinations(string Search)
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            IQueryable<Examination> ds = db.Examinations.Where(d => d.Patient_Id == pt.Patient_Id);
            switch(Request.Form["Search_By"])
            {
                case "Hospital":
                        return View("Examinations", db.Examinations.Where(ex => ex.Patient_Id == pt.Patient_Id && ex.Medical_Organization.Medical_Org_Name.Contains(Search)));
                case "Doctor":
                    if(Search.Split(' ').Length > 1)
                    {
                        return View("Examinations", db.Examinations.Where(ex => ex.Patient_Id == pt.Patient_Id && (ex.Doctor.Person.First_Name+ex.Doctor.Person.Last_Name).Contains(Search)));
                    }
                    else{
                        return View("Examinations", db.Examinations.Where(ex => ex.Patient_Id == pt.Patient_Id && String.Compare(ex.Doctor.Person.First_Name , Search, true) == 0));
                    }
                case "Diagnosis":
                    return View("Examinations", db.Examinations.Where(ex => ex.Patient_Id == pt.Patient_Id && ex.exm_description_result.Contains(Search)));
                case "Treatments":
                    return View("Examinations", db.Examinations.Where(ex => ex.Patient_Id == pt.Patient_Id && ex.exm_midicine.Contains(Search)));
                default:
                    break;
            }
            return View("Examinations", ds.ToList());
        }

        [HttpPost]
        public ActionResult Search_In_Reviews(string Search)
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            IQueryable<Review> rev = db.Reviews.Where(rv => rv.Patient_Id == pt.Patient_Id);
            switch (Request.Form["Search_By"])
            {
                case "Hospital":
                    return View("Reviews", db.Reviews.Where(rv => rv.Patient_Id == pt.Patient_Id && rv.Medical_Organization.Medical_Org_Name.Contains(Search)));
                case "Doctor":
                    if (Search.Split(' ').Length > 1)
                    {
                        return View("Reviews", db.Reviews.Where(rv => rv.Patient_Id == pt.Patient_Id && (rv.Doctor.Person.First_Name + rv.Doctor.Person.Last_Name).Contains(Search)));
                    }
                    else
                    {
                        return View("Reviews", db.Reviews.Where(rv => rv.Patient_Id == pt.Patient_Id && String.Compare(rv.Doctor.Person.First_Name, Search, true) == 0));
                    }
                case "Diagnosis":
                    return View("Reviews", db.Reviews.Where(rv => rv.Patient_Id == pt.Patient_Id && rv.rev_description_result.Contains(Search)));
                case "Treatments":
                    return View("Reviews", db.Reviews.Where(rv => rv.Patient_Id == pt.Patient_Id && rv.rev_midicine.Contains(Search)));
                default:
                    break;
            }
            return View("Reviews", rev.ToList());
        }

        [HttpPost]
        public ActionResult Search_In_Operations(string Search)
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            IQueryable<Operation> opr = db.Operations.Where(op => op.Patient_Id == pt.Patient_Id);
            switch (Request.Form["Search_By"])
            {
                case "Hospital":
                    return View("Operations", db.Operations.Where(op => op.Patient_Id == pt.Patient_Id && op.Medical_Organization.Medical_Org_Name.Contains(Search)));
                case "Doctor":
                    if (Search.Split(' ').Length > 1)
                    {
                        return View("Operations", db.Operations.Where(op => op.Patient_Id == pt.Patient_Id && (op.Doctor.Person.First_Name + op.Doctor.Person.Last_Name).Contains(Search)));
                    }
                    else
                    {
                        return View("Operations", db.Operations.Where(op => op.Patient_Id == pt.Patient_Id && String.Compare(op.Doctor.Person.First_Name, Search, true) == 0));
                    }
                case "Operation":
                    return View("Operations", db.Operations.Where(op => op.Patient_Id == pt.Patient_Id && op.Op_Name.Contains(Search)));
                case "Type":
                    return View("Operations", db.Operations.Where(op => op.Patient_Id == pt.Patient_Id && op.Op_Type.Contains(Search)));
                default:
                    break;
            }
            return View("Operations", opr.ToList());
        }

        [HttpPost]
        public ActionResult Search_In_Laboratories_Radiology(string Search)
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            IQueryable<Lab> labs = db.Labs.Where(op => op.Patient_Id == pt.Patient_Id);
            switch (Request.Form["Search_By"])
            {
                case "Hospital":
                    return View("Laboratories_Radiology", db.Labs.Where(lab => lab.Patient_Id == pt.Patient_Id && lab.Medical_Organization.Medical_Org_Name.Contains(Search)));
                case "Name":
                        return View("Laboratories_Radiology", db.Labs.Where(lab => lab.Patient_Id == pt.Patient_Id && lab.Lab_Name.Contains(Search)));
                case "Type":
                    return View("Laboratories_Radiology", db.Labs.Where(lab => lab.Patient_Id == pt.Patient_Id && lab.Lab_Type.Contains(Search)));
                default:
                    break;
            }
            return View("Laboratories_Radiology", labs.ToList());
        }

        [HttpPost]
        public ActionResult Search_In_Bookings(DateTime Search)
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            IEnumerable<Permission> result = db.Permissions.Where(p => p.Patient_Id == pt.Patient_Id && p.Booking_Date.Year == Search.Year && p.Booking_Date.Month == Search.Month && p.Booking_Date.Day == Search.Day);
            if(result.Count() == 0)
            {
                return RedirectToAction("Details_Of_Booking", new { id = pt.Patient_Id });
            }
            return View("Details_Of_Booking",  result.ToList());
        }

        [HttpPost]
        public ActionResult Search_Date_Examination(DateTime Search)
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            return View("Examinations", db.Examinations.Where(ex => ex.Patient_Id == pt.Patient_Id && ex.exm_date.Year == Search.Year && ex.exm_date.Month == Search.Month && ex.exm_date.Day == Search.Day));
        }

        [HttpPost]
        public ActionResult Search_Date_Reviews(DateTime Search)
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            return View("Reviews", db.Reviews.Where(rev => rev.Patient_Id == pt.Patient_Id && rev.rev_date.Year == Search.Year && rev.rev_date.Month == Search.Month && rev.rev_date.Day == Search.Day));
        }

        [HttpPost]
        public ActionResult Search_Date_Operations(DateTime Search)
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            var opr = db.Operations.Where(op => op.Patient_Id == pt.Patient_Id &&
             op.Op_Date.Year == Search.Year &&
             op.Op_Date.Month == Search.Month &&
             op.Op_Date.Day == Search.Day);
            return View("Operations",opr);
        }

        [HttpPost]
        public ActionResult Search_Date_Laboratories_Radiology(DateTime Search)
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            var labs = db.Labs.Where(lab => lab.Patient_Id == pt.Patient_Id && lab.Lab_Date.Day == Search.Day
            && lab.Lab_Date.Month == Search.Month
            && lab.Lab_Date.Year == Search.Year);
            return View("Laboratories_Radiology", labs);
        }

        public ActionResult Your_Diseases(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            var ds = db.Diseases.Where(f => f.Patient_Id == pt.Patient_Id);
            return View(ds.ToList());
        }

        [HttpPost]
        public ActionResult Order_By_Family_History()
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if(pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            switch (Request.Form["Order_By"])
            {
                case "Desc_Disease":
                    return View("Family_History",db.Family_History.ToList().Where(f =>f.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(f => f.Disease_Name));

                case "Desc_Type":
                    return View("Family_History", db.Family_History.ToList().Where(f => f.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(f => f.Disease_Type));
                   
                case "Asc_Disease":
                    return View("Family_History", db.Family_History.ToList().Where(f => f.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderBy(f => f.Disease_Name));

                case "Asc_Type":
                    return View("Family_History", db.Family_History.ToList().Where(f => f.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderBy(f => f.Disease_Type));

                default:
                    break;
            }
            return RedirectToAction("Family_History");
        }

        [HttpPost]
        public ActionResult Order_By_Your_Disease()
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            switch (Request.Form["Order_By"])
            {
                case "Desc_Disease":
                    return View("Your_Diseases", db.Diseases.ToList().Where(d => d.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(d => d.Disease_Name));

                case "Desc_Type":
                    return View("Your_Diseases", db.Diseases.ToList().Where(d => d.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(d => d.Disease_Type));

                case "Asc_Disease":
                    return View("Your_Diseases", db.Diseases.ToList().Where(d => d.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderBy(d => d.Disease_Name));

                case "Asc_Type":
                    return View("Your_Diseases", db.Diseases.ToList().Where(d => d.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderBy(d => d.Disease_Type));

                default:
                    break;
            }
            return RedirectToAction("Your_Diseases");
        }

        [HttpPost]
        public ActionResult Order_Examination()
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            switch (Request.Form["Order_By"])
            {
                case "Hospital":
                    return View("Examinations", db.Examinations.ToList().Where(ex => ex.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(ex => ex.Medical_Organization.Medical_Org_Name));

                case "Doctor":
                    return View("Examinations", db.Examinations.ToList().Where(ex => ex.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(ex => ex.Doctor.Person.First_Name+ex.Doctor.Person.Last_Name));

                case "Recent_Dates":
                    return View("Examinations", db.Examinations.ToList().Where(ex => ex.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(ex => ex.exm_date));

                case "Old_Dates":
                    return View("Examinations", db.Examinations.ToList().Where(ex => ex.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderBy(ex => ex.exm_date));

                default:
                    break;
            }
            return RedirectToAction("Examinations");
        }

        [HttpPost]
        public ActionResult Order_Reviews()
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            switch (Request.Form["Order_By"])
            {
                case "Hospital":
                    return View("Reviews", db.Reviews.ToList().Where(rev => rev.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(rev => rev.Medical_Organization.Medical_Org_Name));

                case "Doctor":
                    return View("Reviews", db.Reviews.ToList().Where(rev => rev.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(rev => rev.Doctor.Person.First_Name + rev.Doctor.Person.Last_Name));

                case "Recent_Dates":
                    return View("Reviews", db.Reviews.ToList().Where(rev => rev.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(rev => rev.rev_date));

                case "Old_Dates":
                    return View("Reviews", db.Reviews.ToList().Where(rev => rev.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderBy(rev => rev.rev_date));

                default:
                    break;
            }
            return RedirectToAction("Reviews");
        }

        [HttpPost]
        public ActionResult Order_Operations()
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            switch (Request.Form["Order_By"])
            {
                case "Hospital":
                    return View("Operations", db.Operations.ToList().Where(op => op.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(op => op.Medical_Organization.Medical_Org_Name));

                case "Doctor":
                    return View("Operations", db.Operations.ToList().Where(op => op.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(op => op.Doctor.Person.First_Name + op.Doctor.Person.Last_Name));

                case "Recent_Dates":
                    return View("Operations", db.Operations.ToList().Where(op => op.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(op => op.Op_Date));

                case "Old_Dates":
                    return View("Operations", db.Operations.ToList().Where(op => op.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderBy(op => op.Op_Date));

                default:
                    break;
            }
            return RedirectToAction("Operations");
        }

        [HttpPost]
        public ActionResult Order_Laboratories_Radiology()
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            long patient_id = Int64.Parse(Request.Form["Patient_Id"]);
            switch (Request.Form["Order_By"])
            {
                case "Hospital":
                    return View("Laboratories_Radiology", db.Labs.ToList().Where(lab => lab.Patient_Id == patient_id).OrderByDescending(lab => lab.Medical_Organization.Medical_Org_Name));

                case "Name":
                    return View("Laboratories_Radiology", db.Labs.ToList().Where(lab => lab.Patient_Id == patient_id).OrderByDescending(lab => lab.Lab_Name));

                case "Type":
                    return View("Laboratories_Radiology", db.Labs.ToList().Where(lab => lab.Patient_Id == patient_id).OrderByDescending(lab => lab.Lab_Type));

                case "Recent_Dates":
                    return View("Laboratories_Radiology", db.Labs.ToList().Where(lab => lab.Patient_Id == patient_id).OrderByDescending(lab => lab.Lab_Date));

                case "Old_Dates":
                    return View("Laboratories_Radiology", db.Labs.ToList().Where(lab => lab.Patient_Id == patient_id).OrderBy(lab => lab.Lab_Date));

                default:
                    break;
            }
            return RedirectToAction("Laboratories_Radiology",new { id = pt.Patient_Id});
        }

        [HttpPost]
        public ActionResult Order_By_Bookings()
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            long patient_id = Int64.Parse(Request.Form["Patient_Id"]);
            switch (Request.Form["Order_By"])
            {
                case "last_dates":
                    return View("Details_Of_Booking", db.Permissions.ToList().Where(p => p.Patient_Id == patient_id).OrderByDescending(p => p.Booking_Date));

                case "old_dates":
                    return View("Details_Of_Booking", db.Permissions.ToList().Where(p => p.Patient_Id == patient_id).OrderBy(p => p.Booking_Date));

                default:
                    break;
            }
            return RedirectToAction("Details_Of_Booking",new { id = patient_id });
        }

        [HttpPost]
        public ActionResult Filter_Family_History()
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            string str = Request["Filter"]; // for solve NotSupport Exception
            var fm = db.Family_History.Where(f => f.Patient_Id == pt.Patient_Id && String.Compare(f.Disease_Type, str, true) == 0);
             return View("Family_History", fm.ToList());
        }
        [HttpPost]
        public ActionResult Filter_Your_Disease()
        {
            if (Request.Form["Patient_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            string str = Request["Filter"]; // for solve NotSupport Exception
            var ds = db.Diseases.Where(d => d.Patient_Id == pt.Patient_Id && String.Compare(d.Disease_Type, str, true) == 0);
            return View("Your_Diseases", ds.ToList());
        }

        public ActionResult Examinations(long?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            var exm = db.Examinations.Where(ex => ex.Patient_Id == pt.Patient_Id);
            return View(exm.ToList());
        }

        public ActionResult Reviews(long?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            var rev = db.Reviews.Where(ex => ex.Patient_Id == pt.Patient_Id);
            return View(rev.ToList());
        }

        public ActionResult Operations(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            var opr = db.Operations.Where(op => op.Patient_Id == pt.Patient_Id);
            return View(opr.ToList());
        }

        public ActionResult Laboratories_Radiology(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = pt.Patient_Id;
            IEnumerable<Lab> labs = db.Labs.Where(lab => lab.Patient_Id == pt.Patient_Id); 
            return View(labs.ToList());
        }

        public ActionResult Child_Followup(long?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Child_FollowUp_Form child_followUp_form = db.Child_FollowUp_Form.Find(pt.Patient_Id);
            return View(child_followUp_form);
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

        public ActionResult Download_Lab_File(long? id, string name, DateTime date)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab lab = db.Labs.Where(lb => lb.Patient_Id == id && lb.Lab_Date == date && lb.Lab_Name == name).Single();
            if(lab == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            string path = Server.MapPath(lab.Lab_File);
            string filename = Path.GetFileName(path);
            string type = "";
            string ext = Path.GetExtension(path);
            if (ext != null)
            {
                switch (ext.ToLower())
                {
                    case ".txt":
                        type = "text/plain";
                        break;

                    case ".GIF":
                        type = "image/GIF";
                        break;

                    case ".pdf":
                        type = "Application/pdf";
                        break;

                    case ".doc":
                    case ".rtf":
                        type = "Application/msword";
                        break;
                }
            }
            FileInfo file = new FileInfo(path);

            if(file.Exists && type != "")
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            return RedirectToAction("Laboratories_Radiology",new { id = lab.Patient_Id});
        }

        public ActionResult Book(long? id)
        {
            if(Check_Login())
            {
                if(id==null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //ViewBag.Patient_Id = id;
                var ct = db.Medical_Organization.Select(model => model.User.City).Distinct().ToList();
                List<SelectListItem> cities = new List<SelectListItem>();
                foreach(var item in ct)
                {
                    cities.Add(new SelectListItem { Text = item, Value = item });
                }
                SelectList Cities = new SelectList(cities,"Value","Text");
                ViewBag.Cities = Cities;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult Book()
        {
            string search = Request.Form["Search"];
            string city = Request.Form["Cities"];
            string email = Request.Form["Email"];
            string phone = Request.Form["Phone"];
            IEnumerable<User> med = db.Users.Where(model => model.Medical_Organization.Medical_Org_Name.Contains(search));
            if(city != "")
            {
                med = med.Where(model => model.City.Contains(city));
            }
            if(email != "")
            {
                med = med.Where(model => model.Email.Contains(email));
            }
            if ( phone != "")
            {
                med = med.Where(model => model.Phone.Contains(phone));
            }
            if(med == null)
            {
                var ct = db.Medical_Organization.Select(model => model.User.City).Distinct().ToList();
                List<SelectListItem> cities = new List<SelectListItem>();
                foreach (var item in ct)
                {
                    cities.Add(new SelectListItem { Text = item, Value = item });
                }
                SelectList Cities = new SelectList(cities, "Value", "Text");
                ViewBag.Cities = Cities;

                ModelState.AddModelError("Phone", "Not Found The Medical Organization");
                return View();
            }
            return View("Search", med.ToList());
        }

       [HttpGet]
        public ActionResult Select_Dep_For_Book(long? Patient_id,long? Med_Org_Id)
        {
            if(Check_Login())
            {
                if (Patient_id == null || Med_Org_Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Medical_Organization med = db.Medical_Organization.Find(Med_Org_Id);
                if(Patient_id == ((User)Session["UserId"]).User_Id)
                {
                    if(med == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }

                    var dep = med.Departments.ToList();
                    List<SelectListItem> departments = new List<SelectListItem>();
                    foreach (var item in dep)
                    {
                        departments.Add(new SelectListItem { Text = item.Department_Name, Value = item.Department_Id.ToString() });
                    }
                    SelectList Department = new SelectList(departments, "Value", "Text");
                    ViewBag.Patient_Id = Patient_id;
                    ViewBag.Med_Org_Id = Med_Org_Id;
                    ViewBag.Depratment = Department;
                    return View();
                }
            }
            return RedirectToAction("Login","Home"); 
        }


        [HttpPost]
        public ActionResult Select_Dep_For_Book()
        {
            long med_Id = Int64.Parse(Request.Form["Med_Org_Id"]); 
            long patient_Id = Int64.Parse(Request.Form["Patient_Id"]);
            long dep_Id = Int64.Parse(Request.Form["Depratment"]);
            if(med_Id <= 0 || patient_Id <= 0 || dep_Id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Patient_Id = patient_Id;
            ViewBag.Med_Org_Id = med_Id;
            ViewBag.Depratment = dep_Id;
            Department dep = db.Departments.Find(dep_Id);
            var doc = dep.Doctors.ToList();
            List<SelectListItem> doctors = new List<SelectListItem>();
            foreach (var item in doc)
            {
                doctors.Add(new SelectListItem { Text = item.Person.First_Name+" "+item.Person.Last_Name, Value = item.Doctor_Id.ToString() });
            }
            SelectList Doctors = new SelectList(doctors, "Value", "Text");
            ViewBag.Doctors = Doctors;
            return View("Select_Doctor_For_Book");
        }

        [HttpGet]
        public ActionResult Final_Booking(long? Patient_Id,long? Doctor)
        {
            if(Patient_Id == null || Doctor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Patient_Id = Patient_Id;
            ViewBag.Doctor_Id = Doctor;
            var table = db.Doctor_Schedule.Where(model => model.doctor_id == Doctor);
            return View(table.ToList());
        }

        [HttpPost]
        public ActionResult Final_Booking()
        {
            Permission pr = new Permission();
            pr.Patient_Id = Int64.Parse(Request.Form["Patient_Id"]);
            pr.Patient = db.Patients.Find(pr.Patient_Id);
            pr.Doctor_Id = Int64.Parse(Request.Form["Doctor_Id"]);
            pr.Doctor = db.Doctors.Find(pr.Doctor_Id);
            pr.Booking_Date = DateTime.Parse(Request.Form["Booking_Date"]);
            pr.hour = decimal.Parse(Request.Form["hour"]);
            IEnumerable<Doctor_Schedule> table = db.Doctor_Schedule.Where(model => model.doctor_id == pr.Doctor_Id);
            //---------------------------------------------------------------------------------------------------------------------
          try
            {
                IEnumerable<Doctor_Schedule> validation_booking_date = table.Where(model => model.day.Equals(pr.Booking_Date.DayOfWeek.ToString(), StringComparison.InvariantCultureIgnoreCase));
                if (validation_booking_date == null)
                {
                    ViewBag.Patient_Id = pr.Patient_Id;
                    ViewBag.Doctor_Id = pr.Doctor_Id;
                    var schedule = db.Doctor_Schedule.Where(model => model.doctor_id == pr.Doctor_Id);
                    ModelState.AddModelError("Booking_Date", "Your select day out the schedule of doctor");
                    return View(schedule.ToList());
                }
                if(pr.Booking_Date < DateTime.Today)
                {
                    ViewBag.Patient_Id = pr.Patient_Id;
                    ViewBag.Doctor_Id = pr.Doctor_Id;
                    var schedule = db.Doctor_Schedule.Where(model => model.doctor_id == pr.Doctor_Id);
                    ModelState.AddModelError("Booking_Date", "You cannot book this date");
                    return View(schedule.ToList());
                }
                db.Permissions.Add(pr);
                db.SaveChanges();
                return RedirectToAction("Details_Of_Booking", pr.Patient_Id);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Booking_Date", ex.Message);
                ViewBag.Patient_Id = pr.Patient_Id;
                ViewBag.Doctor_Id = pr.Doctor_Id;
                var schedule = db.Doctor_Schedule.Where(model => model.doctor_id == pr.Doctor_Id);
                return View(schedule.ToList());
            }  
        }

        public ActionResult Details_Of_Booking(long? id)
        {
            if(Check_Login())
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
                IEnumerable<Permission> pr = db.Permissions.Where(p => p.Patient_Id == pt.Patient_Id);
                pr = DeletePastBookings(pr);
                ViewBag.Patient_Id = pt.Patient_Id;
                return View(pr.ToList().OrderByDescending(p => p.Booking_Date));
            }
            return RedirectToAction("Login","Home");
        }

        // delete bookings that in past
        [NonAction]
        public IEnumerable<Permission> DeletePastBookings(IEnumerable<Permission> pr)
        {
            foreach(var item in pr)
            {
                if(item.Booking_Date < DateTime.Today)
                {
                    try
                    {
                        pr.ToList().Remove(item);
                        db.Permissions.Remove(item);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            return pr;
        }

        [HttpPost]
        public ActionResult DeleteBooking()
        { 
            long id =Int64.Parse(Request.Form["Booking_Id"]);
            Permission pr = db.Permissions.Find(id);
            if(pr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            long patient_id = pr.Patient_Id;
            try
            {
                db.Permissions.Remove(pr);
                db.SaveChanges();
                return RedirectToAction("Details_Of_Booking", "Patient", new { id = patient_id });
            }
           catch(Exception ex)
            {
                ModelState.AddModelError("DeleteBooking", ex.Message);
            }
            return RedirectToAction("Details_Of_Booking", "Patient", new { id =((User)Session["UserId"]).User_Id });
        }

        [HttpPost]
        public ActionResult Rating()
        {
            long patient_id = ((User)Session["UserID"]).User_Id;
            if (Check_Login())
            {
                long id = Int64.Parse(Request.Form["User_Id"]);
                User us = db.Users.Find(id);
                if(us == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                if(us.User_Type == 2)
                {
                    Evaluation_Medical_Org ev = new Evaluation_Medical_Org();
                    ev.Medical_Org_Id = us.User_Id;
                    ev.Patient_Id = patient_id;
                    ev.Rate = float.Parse(Request.Form["Rate"]);
                    if(us.Medical_Organization.Medium_Rate == 0 || us.Medical_Organization.Medium_Rate == null)
                    {
                        us.Medical_Organization.Medium_Rate = ev.Rate;
                    }
                    else
                    {
                        us.Medical_Organization.Medium_Rate = (ev.Rate + us.Medical_Organization.Medium_Rate) / 2;
                    }
                    db.Entry(us.Medical_Organization).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var e = db.Evaluation_Medical_Org.Where(model => model.Patient_Id == patient_id && model.Medical_Org_Id == us.User_Id).Single();
                        ev.Evaluation_Medical_Id = e.Evaluation_Medical_Id;
                        db.Entry(ev).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception) {
                        db.Evaluation_Medical_Org.Add(ev);
                        db.SaveChanges();
                    }
                }
                if(us.User_Type == 3)
                {
                    Evaluation_Doctor evd = new Evaluation_Doctor();
                    evd.Patient_Id = patient_id;
                    evd.Doctor_Id = us.User_Id;
                    evd.Rate = float.Parse(Request.Form["Rate"]);
                    if(us.Person.Doctor.Medium_Rate == 0 || us.Person.Doctor.Medium_Rate == null)
                    {
                        us.Person.Doctor.Medium_Rate = evd.Rate;
                    }
                    else
                    {
                        us.Person.Doctor.Medium_Rate = (evd.Rate + us.Person.Doctor.Medium_Rate) / 2;
                    }
                    db.Entry(us.Person.Doctor).State = EntityState.Modified;
                    db.SaveChanges();
                    try
                    {
                        var e = db.Evaluation_Doctor.Where(model => model.Patient_Id == patient_id && model.Doctor_Id == us.User_Id).Single();
                        evd.Evaluation_Doctor_Id = e.Evaluation_Doctor_Id;
                        db.Entry(evd).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception) {

                        db.Evaluation_Doctor.Add(evd);
                        db.SaveChanges();
                    }   
                }
            }
            return RedirectToAction("Index",new { id =patient_id});
        }
        
    }
}
