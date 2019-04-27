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

        [NonAction]
        private bool Check_Login()
        {
            User sesstion = null;
            sesstion = (User)Session["UserID"];
            if (sesstion != null)
            {
                if (sesstion.User_Type == 3)
                {
                    return true;
                }
            }
            return false;
        }

        // GET: Doctor
        public ActionResult Index(long? id)
        {
            if (Check_Login())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var doctors = db.Doctors.Find(id);
                if (doctors == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                var med = db.Medical_Organization.Where(model => model.Medical_Org_Id == doctors.Medical_Org_Id).Single();
                ViewBag.Medical_Organization = med.Medical_Org_Name;
                return View(doctors);
            }
            return RedirectToAction("Login", "Home");
        }

        // GET: Doctor/Details/5
        public ActionResult Details(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
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


        // GET: Doctor/Edit/5
        public ActionResult Edit(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }

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

        // POST: Doctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            Doctor doctor = null;
            doctor = db.Doctors.Find(long.Parse(Request.Form["Doctor_Id"]));
            if (doctor != null)
            {
                if (ModelState.IsValid)
                {
                    doctor.Person.First_Name = Request.Form["Person.First_Name"];
                    doctor.Person.Last_Name = Request.Form["Person.Last_Name"];
                    doctor.Person.National_Id = Request.Form["Person.National_Id"];
                    doctor.Person.Nationality = Request.Form["Person.Nationality"];
                    doctor.Person.User.City = Request.Form["Person.User.City"];
                    doctor.Person.User.Address = Request.Form["Person.User.Address"];
                    doctor.Person.Gender = Request.Form["Person.Gender"];
                    DateTime dateTemp = Convert.ToDateTime(Request.Form["Person.Birth_Date.Day"] + "/" + Request.Form["Person.Birth_Date.Month"] + "/" + Request.Form["Person.Birth_Date.Year"]);
                    doctor.Person.Birth_Date = dateTemp;
                    doctor.Person.User.Email = Request.Form["[0]"]; // refer to email field
                    doctor.Person.User.Phone = Request.Form["Person.User.Phone"];
                    doctor.Acadimic_Degree = Request.Form["Acadimic_Degree"];
                    doctor.Spacialization = Request.Form["Spacialization"];
                    doctor.Functional_Degree = Request.Form["Functional_Degree"];
                    if (Request.Form["Person.User.Password"].Split(' ')[0] != doctor.Person.User.Password.Split(' ')[0])
                    {
                        if (Request.Form["Person.User.Password"].Split(' ')[0] == Request.Form["Confirm_Password"].Split(' ')[0])
                        {
                            doctor.Person.User.Password = Request.Form["Person.User.Password"].Split(' ')[0];
                        }
                        else
                        {
                            ModelState.AddModelError("Confirm_Password", "Password do not Match Confirm Password, Password do not change");
                            return View(doctor);
                        }
                    }
                    db.Entry(doctor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = doctor.Doctor_Id });
                }
            }
            return View(doctor);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Manage_Account(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return RedirectToAction("Edit", new { id = doctor.Doctor_Id });
        }

        public ActionResult Schedule(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var med = db.Medical_Organization.Find(doctor.Medical_Org_Id);
            ViewBag.Medical_Organization_Photo = med.User.Photo_Url.Split(' ')[0];
            var schedule = db.Doctor_Schedule.Where(model => model.doctor_id == id);
            return View(schedule.ToList());
        }

        public ActionResult Index_Patients(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var bookings = db.Permissions.Where(model => model.Doctor_Id == id).ToList();
            bookings = DeletePastBookings(bookings);
            bookings = db.Permissions.Where(model => model.Booking_Date.Year == DateTime.Now.Year && model.Booking_Date.Month == DateTime.Now.Month && model.Booking_Date.Day == DateTime.Now.Day).ToList();
            bookings = bookings.OrderBy(model => model.hour).ToList();

            ViewBag.Doctor_Id = doctor.Doctor_Id;
            return View(bookings);
        }
        [NonAction]
        public List<Permission> DeletePastBookings(List<Permission> pr)
        {
            if (pr == null)
            {
                return pr;
            }
            foreach (var item in pr)
            {
                if (item.Booking_Date.Year <= DateTime.Now.Year)
                {
                    if (item.Booking_Date.Month <= DateTime.Now.Month)
                    {
                        if (item.Booking_Date.Day < DateTime.Now.Day)
                        {
                            pr.ToList().Remove(item);
                            db.Permissions.Remove(item);
                            db.SaveChanges();
                        }
                    }
                }
            }
            return pr;
        }

        [HttpPost]
        public ActionResult Search_In_Pateint(string Search)
        {
            if (Request.Form["Doctor_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(Int64.Parse(Request.Form["Doctor_Id"]));
            if (doctor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Doctor_Id = doctor.Doctor_Id;
            var patients = db.Permissions.Where(model => model.Doctor_Id == doctor.Doctor_Id);
            switch (Request.Form["Search_By"])
            {
                case "NID":
                    return View("Index_Patients", patients.Where(pr => pr.Patient.Person.National_Id.Contains(Search)).OrderBy(model => model.hour));
                case "Fname":
                    return View("Index_Patients", patients.Where(pr => pr.Patient.Person.First_Name.Contains(Search)).OrderBy(model => model.hour));
                case "Lname":
                    return View("Index_Patients", patients.Where(pr => pr.Patient.Person.Last_Name.Contains(Search)).OrderBy(model => model.hour));
                default:
                    break;
            }
            return RedirectToAction("Index_Patients", new { id = doctor.Doctor_Id });
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
            IQueryable<Family_History> fm = db.Family_History.Where(f => f.Patient_Id == pt.Patient_Id);

            var items = fm.Select(model => model.Disease_Type).Distinct().ToList();
            List<SelectListItem> list_item = new List<SelectListItem>();
            foreach (var item in items)
            {
                list_item.Add(new SelectListItem
                {
                    Value = item,
                    Text = item
                });
            }
            SelectList types = new SelectList(list_item, "Value", "Text");
            ViewBag.types = types;

            if (Request.Form["Search_By"] == "Name")
            {
                fm = db.Family_History.Where(f => f.Patient_Id == pt.Patient_Id && f.Disease_Name.Equals(Search, StringComparison.InvariantCultureIgnoreCase));
            }
            else if (Request.Form["Search_By"] == "Type")
            {
                fm = fm.Where(f => f.Disease_Type.Equals(Search, StringComparison.InvariantCultureIgnoreCase));
            }
            ViewBag.Patient_Id = pt.Patient_Id;

            return View("Family_History", fm.ToList());
        }

        [HttpPost]
        public ActionResult Search_In_Diseases(string Search)
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
            IQueryable<Disease> d = db.Diseases.Where(f => f.Patient_Id == pt.Patient_Id);

            var items = d.Select(model => model.Disease_Type).Distinct().ToList();
            List<SelectListItem> list_item = new List<SelectListItem>();
            foreach (var item in items)
            {
                list_item.Add(new SelectListItem
                {
                    Value = item,
                    Text = item
                });
            }
            SelectList types = new SelectList(list_item, "Value", "Text");
            ViewBag.types = types;

            if (Request.Form["Search_By"] == "Name")
            {
                d = d.Where(f => f.Patient_Id == pt.Patient_Id && f.Disease_Name.Equals(Search, StringComparison.InvariantCultureIgnoreCase));
            }
            else if (Request.Form["Search_By"] == "Type")
            {
                d = d.Where(f => f.Disease_Type.Equals(Search, StringComparison.InvariantCultureIgnoreCase));
            }
            ViewBag.Patient_Id = pt.Patient_Id;

            return View("Diseases", d.ToList());
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
            switch (Request.Form["Search_By"])
            {
                case "Hospital":
                    return View("Examinations", db.Examinations.Where(ex => ex.Patient_Id == pt.Patient_Id && ex.Medical_Organization.Medical_Org_Name.Contains(Search)));
                case "Doctor":
                    if (Search.Split(' ').Length > 1)
                    {
                        return View("Examinations", db.Examinations.Where(ex => ex.Patient_Id == pt.Patient_Id && (ex.Doctor.Person.First_Name + ex.Doctor.Person.Last_Name).Contains(Search)));
                    }
                    else
                    {
                        return View("Examinations", db.Examinations.Where(ex => ex.Patient_Id == pt.Patient_Id && ex.Doctor.Person.First_Name.Equals(Search, StringComparison.InvariantCultureIgnoreCase)));
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
            IQueryable<Review> rev = db.Reviews.Where(d => d.Patient_Id == pt.Patient_Id);
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
                        return View("Reviews", db.Reviews.Where(rv => rv.Patient_Id == pt.Patient_Id && rv.Doctor.Person.First_Name.Equals(Search, StringComparison.InvariantCultureIgnoreCase)));
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
            IQueryable<Operation> opr = db.Operations.Where(d => d.Patient_Id == pt.Patient_Id);
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
                        return View("Operations", db.Operations.Where(op => op.Patient_Id == pt.Patient_Id && op.Doctor.Person.First_Name.Equals(Search, StringComparison.InvariantCultureIgnoreCase)));
                    }
                case "Name":
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
            IQueryable<Lab> lab = db.Labs.Where(d => d.Patient_Id == pt.Patient_Id);
            switch (Request.Form["Search_By"])
            {
                case "Hospital":
                    return View("Laboratories_Radiology", db.Labs.Where(lb => lb.Patient_Id == pt.Patient_Id && lb.Medical_Organization.Medical_Org_Name.Contains(Search)));
                case "Name":
                    return View("Laboratories_Radiology", db.Labs.Where(lb => lb.Patient_Id == pt.Patient_Id && lb.Lab_Name.Contains(Search)));
                case "Type":
                    return View("Laboratories_Radiology", db.Labs.Where(lb => lb.Patient_Id == pt.Patient_Id && lb.Lab_Type.Contains(Search)));
                default:
                    break;
            }
            return View("Laboratories_Radiology", lab.ToList());
        }

        [HttpPost]
        public ActionResult Search_Date_Patients(DateTime Search)
        {
            if (Request.Form["Doctor_Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(Int64.Parse(Request.Form["Doctor_Id"]));
            if (doctor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Doctor_Id = doctor.Doctor_Id;

            if (Search.Year <= DateTime.Now.Year)
            {
                if (Search.Month <= DateTime.Now.Month)
                {
                    if (Search.Day < DateTime.Now.Day)
                    {
                        ModelState.AddModelError("Search", "Date In The Past");
                        return View("Index_Patients", db.Permissions.Where(model => model.Booking_Date.Year == DateTime.Now.Year &&
                         model.Booking_Date.Month == DateTime.Now.Month &&
                         model.Booking_Date.Day == DateTime.Now.Day).OrderBy(model => model.hour).ToList());
                    }
                }
            }
            return View("Index_Patients", db.Permissions.Where(model => model.Booking_Date.Year == Search.Year &&
                          model.Booking_Date.Month == Search.Month &&
                          model.Booking_Date.Day == Search.Day).OrderBy(model => model.hour).ToList());
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
            return View("Reviews", db.Reviews.Where(rv => rv.Patient_Id == pt.Patient_Id && rv.rev_date.Year == Search.Year && rv.rev_date.Month == Search.Month && rv.rev_date.Day == Search.Day));
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
            return View("Operations", db.Operations.Where(op => op.Patient_Id == pt.Patient_Id && op.Op_Date.Year == Search.Year && op.Op_Date.Month == Search.Month && op.Op_Date.Day == Search.Day));
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
            return View("Laboratories_Radiology", db.Labs.Where(lb => lb.Patient_Id == pt.Patient_Id && lb.Lab_Date.Year == Search.Year && lb.Lab_Date.Month == Search.Month && lb.Lab_Date.Day == Search.Day));
        }

        [HttpPost]
        public ActionResult End_Examination()
        {
            long? patient_id = Int64.Parse(Request.Form["Patient_Id"]);
            long? doctor_id = Int64.Parse(Request.Form["Doctor_Id"]);
            DateTime date = DateTime.Parse(Request.Form["date"]);
            if (patient_id == null || doctor_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pr = db.Permissions.Where(model => model.Doctor_Id == doctor_id && model.Patient_Id == patient_id);
            pr = pr.Where(model => model.Booking_Date.Year == date.Year && model.Booking_Date.Month == date.Month && model.Booking_Date.Day == date.Day);
            if (pr == null)
            {
                return RedirectToAction("Index_Patients", new { id = doctor_id });
            }
            var item = pr.First();
            db.Permissions.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index_Patients", new { id = doctor_id });
        }

        public ActionResult Examine(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            General_Examination g = db.General_Examination.Where(model => model.Patient_Id == id).SingleOrDefault();
            if(g == null)
            {
                g = new General_Examination();
                g.Patient_Id = pt.Patient_Id;
                g.Patient = pt;
            }
            return View(g);
        }

        [HttpPost]
        public ActionResult Examine()
        {
            if (ModelState.IsValid)
            {
                General_Examination g = db.General_Examination.Find(Int64.Parse(Request.Form["Patient_Id"]));
                g.Length = Int32.Parse(Request.Form["Length"]);
                g.Weight = Int32.Parse(Request.Form["Weight"]);
                g.Diabetes = Int32.Parse(Request.Form["Diabetes"]);
                g.Temperature = float.Parse(Request.Form["Temperature"]);
                g.Blood_Type = Request.Form["Blood_Type"];
                g.Blood_Pressure = Request.Form["Blood_Pressure"];
                db.Entry(g).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Examine", new { id = Int64.Parse(Request.Form["Patient_Id"]) });
        }

        public ActionResult Show_Habits(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = id;
            List<Habit> h = db.Habits.Where(model => model.Patient_Id == id).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in h)
            {
                list.Add(new SelectListItem { Text = item.Habit_Name, Value = item.Habit_Id.ToString() });
            }
            if (list.Count() == 0)
            {
                list.Add(new SelectListItem { Text = "Empty", Value = "-1" });
            }
            SelectList habits = new SelectList(list, "Value", "Text");
            ViewBag.Habits = habits;
            return View();
        }

        public ActionResult Show_Allergies(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = id;
            List<Allergy> a = db.Allergies.Where(model => model.Patient_Id == id).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in a)
            {
                list.Add(new SelectListItem { Text = item.Allergies_Name, Value = item.Allergies_Id.ToString() });
            }
            if (list.Count() == 0)
            {
                list.Add(new SelectListItem { Text = "Empty", Value = "-1" });
            }
            SelectList allergies = new SelectList(list, "Value", "Text");
            ViewBag.Allergies = allergies;
            return View();
        }


        public ActionResult Add_Family_History(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = id;

            return View();
        }

        public ActionResult Add_Diseases(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = id;

            return View();
        }

        public ActionResult Add_Examination(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = id;
            ViewBag.Doctor_Id = ((User)Session["UserID"]).User_Id;
            ViewBag.Medical_Org_Id = ((User)Session["UserID"]).Person.Doctor.Medical_Org_Id;
            return View();
        }

        public ActionResult Add_Review(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = id;
            ViewBag.Doctor_Id = ((User)Session["UserID"]).User_Id;
            ViewBag.Medical_Org_Id = ((User)Session["UserID"]).Person.Doctor.Medical_Org_Id;
            return View();
        }

        public ActionResult Add_Operation(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = id;
            ViewBag.Doctor_Id = ((User)Session["UserID"]).User_Id;
            ViewBag.Medical_Org_Id = ((User)Session["UserID"]).Person.Doctor.Medical_Org_Id;
            return View();
        }

        public ActionResult Add_Laboratories_Radiology(long?id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = id;
            ViewBag.Medical_Org_Id = ((User)Session["UserID"]).Person.Doctor.Medical_Org_Id;
            return View();
        }

        [HttpPost]
        public ActionResult Add_New_Habit()
        {
            long Id = Int64.Parse(Request.Form["Patient_Id"]);
            if (Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habit h = new Habit();
            if (Request.Form["Habits"] == "")
            {
                return RedirectToAction("Show_Habits", new { id = Id });
            }
            h.Habit_Name = Request.Form["Habits"];
            h.Patient_Id = Id;
            db.Habits.Add(h);
            db.SaveChanges();
            return RedirectToAction("Show_Habits", new { id = Id });
        }

        [HttpPost]
        public ActionResult Add_New_Allergies()
        {
            long Id = Int64.Parse(Request.Form["Patient_Id"]);
            if (Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Allergy h = new Allergy();
            if (Request.Form["Allergies"] == "")
            {
                return RedirectToAction("Show_Allergies", new { id = Id });
            }
            h.Allergies_Name = Request.Form["Allergies"];
            h.Patient_Id = Id;
            db.Allergies.Add(h);
            db.SaveChanges();
            return RedirectToAction("Show_Allergies", new { id = Id });
        }

        [HttpPost]
        public ActionResult Add_Family_History([Bind(Include = "Disease_Name,Disease_Type")] Family_History fm)
        {
            if (Int64.Parse(Request.Form["Patient_Id"]) == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            long Id = Int64.Parse(Request.Form["Patient_Id"]);
            if (ModelState.IsValid)
            {
                fm.Patient_Id = Id;
                db.Family_History.Add(fm);
                db.SaveChanges();
            }
            return RedirectToAction("Family_History", new { id = Id });
        }

        [HttpPost]
        public ActionResult Add_Diseases([Bind(Include = "Disease_Name,Disease_Type")] Disease d)
        {
            if (Int64.Parse(Request.Form["Patient_Id"]) == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            long Id = Int64.Parse(Request.Form["Patient_Id"]);
            if (ModelState.IsValid)
            {
                d.Patient_Id = Id;
                db.Diseases.Add(d);
                db.SaveChanges();
            }
            return RedirectToAction("Diseases", new { id = Id });
        }

        [HttpPost]
        public ActionResult Add_Examination([Bind(Include = "exm_description_result,exm_midicine")] Examination ex)
        {
            if (Int64.Parse(Request.Form["Patient_Id"]) == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            long Patient_Id = Int64.Parse(Request.Form["Patient_Id"]);
            long Doctor_Id = Int64.Parse(Request.Form["Doctor_Id"]);
            long Medical_Org_Id = Int64.Parse(Request.Form["Medical_Org_Id"]);
            if (ModelState.IsValid)
            {
                ex.Patient_Id = Patient_Id;
                ex.Doctor_Id = Doctor_Id;
                ex.Medical_Org_Id = Medical_Org_Id;
                ex.exm_date = DateTime.Now;
                db.Examinations.Add(ex);
                db.SaveChanges();
            }
            return RedirectToAction("Examinations", new { id = Patient_Id });
        }

        [HttpPost]
        public ActionResult Add_Review([Bind(Include = "rev_description_result,rev_midicine")] Review rev)
        {
            if (Int64.Parse(Request.Form["Patient_Id"]) == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            long Patient_Id = Int64.Parse(Request.Form["Patient_Id"]);
            long Doctor_Id = Int64.Parse(Request.Form["Doctor_Id"]);
            long Medical_Org_Id = Int64.Parse(Request.Form["Medical_Org_Id"]);
            if (ModelState.IsValid)
            {
                rev.Patient_Id = Patient_Id;
                rev.Doctor_Id = Doctor_Id;
                rev.Organization_Id = Medical_Org_Id;
                rev.rev_date = DateTime.Now;
                db.Reviews.Add(rev);
                db.SaveChanges();
            }
            return RedirectToAction("Reviews", new { id = Patient_Id });
        }

        [HttpPost]
        public ActionResult Add_Operation([Bind(Include = "Op_Name,Op_Type,Op_Date")] Operation opr)
        {
            if (Int64.Parse(Request.Form["Patient_Id"]) == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            long Patient_Id = Int64.Parse(Request.Form["Patient_Id"]);
            long Doctor_Id = Int64.Parse(Request.Form["Doctor_Id"]);
            long Medical_Org_Id = Int64.Parse(Request.Form["Medical_Org_Id"]);
            if (ModelState.IsValid)
            {
                opr.Patient_Id = Patient_Id;
                opr.Doctor_Id = Doctor_Id;
                opr.Organization_Id = Medical_Org_Id;
                db.Operations.Add(opr);
                db.SaveChanges();
            }
            return RedirectToAction("Operations", new { id = Patient_Id });
        }
        [HttpPost]
        public ActionResult Add_Laboratories_Radiology([Bind(Include = "Lab_Name,Lab_Type,Lab_Date,Lab_Description_Result")] Lab lab)
        {
 
            if (Int64.Parse(Request.Form["Patient_Id"]) == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            long Patient_Id = Int64.Parse(Request.Form["Patient_Id"]);
            long Medical_Org_Id = Int64.Parse(Request.Form["Medical_Org_Id"]);
            if (ModelState.IsValid)
            {
                lab.Patient_Id = Patient_Id;
                lab.Org_Id = Medical_Org_Id;
                db.Labs.Add(lab);
                db.SaveChanges();
                return RedirectToAction("Edit_Upload_Lab_File", new { id = lab.Lab_Id });
            }
            ViewBag.Patient_Id = Patient_Id;
            ViewBag.Medical_Org_Id = Medical_Org_Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete_Habit()
        {
            long Id = Int64.Parse(Request.Form["Habits"]);
            if (Id != -1)
            {
                Habit h = db.Habits.Find(Id);
                db.Habits.Remove(h);
                db.SaveChanges();
            }
            return RedirectToAction("Show_Habits", new { id = Id });
        }

        [HttpPost]
        public ActionResult Delete_Allergies()
        {
            long Id = Int64.Parse(Request.Form["Allergies"]);
            if (Id != -1)
            {
                Allergy h = db.Allergies.Find(Id);
                db.Allergies.Remove(h);
                db.SaveChanges();
            }
            return RedirectToAction("Show_Allergies", new { id = Id });
        }

        [HttpPost]
        public ActionResult Upload_Photo(HttpPostedFileBase file)
        {
            User u = db.Users.Find((long)Int64.Parse(Request.Form["Doctor_Id"]));

            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    return RedirectToAction("Manage_Account", "Doctor", new { id = Int64.Parse(Request.Form["Doctor_Id"]) });
                }

                if (u != null)
                {
                    if (file.FileName != null)
                    {
                        if (u.User_Type == 3)
                        {
                            file.SaveAs(Server.MapPath("~/Content/Doc_Photo/") + u.User_Id + ".jpg");
                            u.Photo_Url = "~/Content/Doc_Photo/" + u.User_Id + ".jpg";
                        }
                        db.Entry(u).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    ModelState.AddModelError("Doctor_Id", "User Not Exist");
                }
                return RedirectToAction("Manage_Account", new { id = Int64.Parse(Request.Form["Doctor_Id"]) });
            }
            return RedirectToAction("Manage_Account", new { id = Int64.Parse(Request.Form["Doctor_Id"]) });
        }

        public ActionResult Family_History(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            List<Family_History> f = db.Family_History.Where(model => model.Patient_Id == id).ToList();
            ViewBag.Patient_Id = id;

            var items = f.Select(model => model.Disease_Type).Distinct().ToList();
            List<SelectListItem> list_item = new List<SelectListItem>();
            foreach (var item in items)
            {
                list_item.Add(new SelectListItem
                {
                    Value = item,
                    Text = item
                });
            }
            SelectList types = new SelectList(list_item, "Value", "Text");
            ViewBag.types = types;

            return View(f);
        }


        public ActionResult Diseases(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            List<Disease> d = db.Diseases.Where(model => model.Patient_Id == id).ToList();
            ViewBag.Patient_Id = id;

            var items = d.Select(model => model.Disease_Type).Distinct().ToList();
            List<SelectListItem> list_item = new List<SelectListItem>();
            foreach (var item in items)
            {
                list_item.Add(new SelectListItem
                {
                    Value = item,
                    Text = item
                });
            }
            SelectList types = new SelectList(list_item, "Value", "Text");
            ViewBag.types = types;

            return View(d);
        }

        public ActionResult Examinations(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
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

        public ActionResult Reviews(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
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
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
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
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
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
            var lab = db.Labs.Where(lb => lb.Patient_Id == pt.Patient_Id);
            return View(lab.ToList());
        }

        public ActionResult Child_FollowUp_Form(long? id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var child = db.Child_FollowUp_Form.Find(pt.Patient_Id);
            if(child == null)
            {
                child = new Child_FollowUp_Form();
                child.Patient_Id = pt.Patient_Id;
                child.Feed_Type = "";
                child.Age_In_Month = (DateTime.Now.Year -  pt.Person.Birth_Date.Year)*12;
            }
            return View(child);
        }
        [HttpPost]
        public ActionResult Child_FollowUp_Form()
        {
            var child = db.Child_FollowUp_Form.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if(ModelState.IsValid)
            {
                if (child == null)
                {
                    child = new Child_FollowUp_Form();
                    child.Patient_Id = Int64.Parse(Request.Form["Patient_Id"]);
                    child.Feed_Type = Request.Form["Feed_Type"];
                    child.Age_In_Month = int.Parse(Request.Form["Age_In_Month"]);
                    child.Vaccination_Type = Request.Form["Vaccination_Type"];
                    child.Patient = db.Patients.Find(child.Patient_Id);
                    if (Request.Form["Vaccination_Date"].ToString() != "")
                    {
                        child.Vaccination_Date = DateTime.Parse(Request.Form["Vaccination_Date"]);
                    }
                    db.Child_FollowUp_Form.Add(child);
                    db.SaveChanges();
                }
                else
                {
                    child.Feed_Type = Request.Form["Feed_Type"];
                    child.Age_In_Month = int.Parse(Request.Form["Age_In_Month"]);
                    child.Vaccination_Type = Request.Form["Vaccination_Type"];
                    child.Patient = db.Patients.Find(child.Patient_Id);
                    if (Request.Form["Vaccination_Date"].ToString() != "")
                    {
                        child.Vaccination_Date = DateTime.Parse(Request.Form["Vaccination_Date"]);
                    }
                    db.Entry(child).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Child_FollowUp_Form", new { id = child.Patient_Id });
        }

        [HttpPost]
        public ActionResult Order_By_Family_History()
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
            List<Family_History> fm = db.Family_History.Where(f => f.Patient_Id == pt.Patient_Id).ToList();
            var items = fm.Select(model => model.Disease_Type).Distinct().ToList();
            List<SelectListItem> list_item = new List<SelectListItem>();
            foreach (var item in items)
            {
                list_item.Add(new SelectListItem
                {
                    Value = item,
                    Text = item
                });
            }
            SelectList types = new SelectList(list_item, "Value", "Text");
            ViewBag.types = types;


            switch (Request.Form["Order_By"])
            {
                case "Desc_Disease":
                    return View("Family_History", fm.OrderByDescending(f => f.Disease_Name));

                case "Desc_Type":
                    return View("Family_History", fm.OrderByDescending(f => f.Disease_Type));

                case "Asc_Disease":
                    return View("Family_History", fm.OrderBy(f => f.Disease_Name));

                case "Asc_Type":
                    return View("Family_History", fm.OrderBy(f => f.Disease_Type));

                default:
                    break;
            }
            return RedirectToAction("Family_History", new { id = pt.Patient_Id });
        }

        [HttpPost]
        public ActionResult Order_By_Diseases()
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
            List<Disease> d = db.Diseases.Where(f => f.Patient_Id == pt.Patient_Id).ToList();
            var items = d.Select(model => model.Disease_Type).Distinct().ToList();
            List<SelectListItem> list_item = new List<SelectListItem>();
            foreach (var item in items)
            {
                list_item.Add(new SelectListItem
                {
                    Value = item,
                    Text = item
                });
            }
            SelectList types = new SelectList(list_item, "Value", "Text");
            ViewBag.types = types;


            switch (Request.Form["Order_By"])
            {
                case "Desc_Disease":
                    return View("Diseases", d.OrderByDescending(f => f.Disease_Name));

                case "Desc_Type":
                    return View("Diseases", d.OrderByDescending(f => f.Disease_Type));

                case "Asc_Disease":
                    return View("Diseases", d.OrderBy(f => f.Disease_Name));

                case "Asc_Type":
                    return View("Diseases", d.OrderBy(f => f.Disease_Type));

                default:
                    break;
            }
            return RedirectToAction("Diseases", new { id = pt.Patient_Id });
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
                    return View("Examinations", db.Examinations.ToList().Where(ex => ex.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(ex => ex.Doctor.Person.First_Name + ex.Doctor.Person.Last_Name));

                case "Recent_Dates":
                    return View("Examinations", db.Examinations.ToList().Where(ex => ex.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(ex => ex.exm_date));

                case "Old_Dates":
                    return View("Examinations", db.Examinations.ToList().Where(ex => ex.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderBy(ex => ex.exm_date));

                default:
                    break;
            }
            return RedirectToAction("Examinations", new { id = pt.Patient_Id });
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
                    return View("Reviews", db.Reviews.ToList().Where(rv => rv.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(rv => rv.Medical_Organization.Medical_Org_Name));

                case "Doctor":
                    return View("Reviews", db.Reviews.ToList().Where(rv => rv.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(rv => rv.Doctor.Person.First_Name + rv.Doctor.Person.Last_Name));

                case "Recent_Dates":
                    return View("Reviews", db.Reviews.ToList().Where(rv => rv.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(rv => rv.rev_date));

                case "Old_Dates":
                    return View("Reviews", db.Reviews.ToList().Where(rv => rv.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderBy(rv => rv.rev_date));

                default:
                    break;
            }
            return RedirectToAction("Reviews", new { id = pt.Patient_Id });
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
            return RedirectToAction("Operations", new { id = pt.Patient_Id });
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
            switch (Request.Form["Order_By"])
            {
                case "Hospital":
                    return View("Laboratories_Radiology", db.Labs.ToList().Where(lb => lb.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(lb => lb.Medical_Organization.Medical_Org_Name));

                case "Name":
                    return View("Laboratories_Radiology", db.Labs.ToList().Where(lb => lb.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(lb => lb.Lab_Name));

                case "Type":
                    return View("Laboratories_Radiology", db.Labs.ToList().Where(lb => lb.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(lb => lb.Lab_Type));

                case "Recent_Dates":
                    return View("Laboratories_Radiology", db.Labs.ToList().Where(lb => lb.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderByDescending(lb => lb.Lab_Date));

                case "Old_Dates":
                    return View("Laboratories_Radiology", db.Labs.ToList().Where(lb => lb.Patient_Id == Int64.Parse(Request.Form["Patient_Id"])).OrderBy(lb => lb.Lab_Date));

                default:
                    break;
            }
            return RedirectToAction("Laboratories_Radiology", new { id = pt.Patient_Id });
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
            List<Family_History> fm = db.Family_History.Where(f => f.Patient_Id == pt.Patient_Id).ToList();
            var items = fm.Select(model => model.Disease_Type).Distinct().ToList();
            List<SelectListItem> list_item = new List<SelectListItem>();
            foreach (var item in items)
            {
                list_item.Add(new SelectListItem
                {
                    Value = item,
                    Text = item
                });
            }
            SelectList types = new SelectList(list_item, "Value", "Text");
            ViewBag.types = types;

            fm = fm.Where(f => String.Compare(f.Disease_Type, str, true) == 0).ToList();
            return View("Family_History", fm.ToList());
        }

        [HttpPost]
        public ActionResult Filter_Diseases()
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
            List<Disease> d = db.Diseases.Where(f => f.Patient_Id == pt.Patient_Id).ToList();
            var items = d.Select(model => model.Disease_Type).Distinct().ToList();
            List<SelectListItem> list_item = new List<SelectListItem>();
            foreach (var item in items)
            {
                list_item.Add(new SelectListItem
                {
                    Value = item,
                    Text = item
                });
            }
            SelectList types = new SelectList(list_item, "Value", "Text");
            ViewBag.types = types;

            d = d.Where(f => f.Disease_Type.Equals(str, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return View("Diseases", d.ToList());
        }

        public ActionResult Details_Lab_Radio(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab lab = db.Labs.Find(id);
            if (lab == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(lab);
        }

        public void Show_Lab_Radio(long? id)
        {
            if (id != null)
            {
                Lab lab = db.Labs.Find(id);
                if (lab != null)
                {
                    if (lab.Lab_File != null)
                    {
                        string filePath = Server.MapPath(lab.Lab_File);
                        string ext = System.IO.Path.GetExtension(filePath);
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
                                Response.ContentType = type;
                                Response.AddHeader("content-length", filebuffer.Length.ToString());
                                Response.BinaryWrite(filebuffer);
                            }
                        }
                    }
                }
            }
        }

        public ActionResult Download_Lab_File(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab lab = db.Labs.Find(id);
            if (lab == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            string path = Server.MapPath(lab.Lab_File);
            string filename = System.IO.Path.GetFileName(path);
            string type = "";
            string ext = System.IO.Path.GetExtension(path);
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
            System.IO.FileInfo file = new System.IO.FileInfo(path);

            if (file.Exists && type != "")
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            return RedirectToAction("Laboratories_Radiology", new { id = lab.Patient_Id });
        }

        public ActionResult Edit_Upload_Lab_File(long?id)
        {
            if(Check_Login() == false)
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab lab = db.Labs.Find(id);
            if (lab == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = lab.Patient_Id;
            return View(lab);
        }

        [HttpPost]
        public ActionResult Edit_Upload_Lab_File(HttpPostedFileBase file)
        {
            Lab lab = db.Labs.Find(Int64.Parse(Request.Form["Lab_Id"]));
            if (lab == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Patient_Id = lab.Patient_Id;
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ModelState.AddModelError("Lab_File", "Some Problems occures !");
                    return View(lab);
                }

                if (file.FileName != null)
                {

                    string ext = System.IO.Path.GetExtension(file.FileName);
                    if (ext != null)
                    {
                        switch (ext.ToLower())
                        {
                            case ".txt":
                                break;

                            case ".GIF":
                                break;

                            case ".pdf":
                                break;

                            case ".doc":
                                break;
                            default:
                                ModelState.AddModelError("Lab_File", "This Extension Not Allowed");
                                return View(lab);
                        }
                        file.SaveAs(Server.MapPath("~/Content/Lab_Radio/") + lab.Patient_Id + ext);
                        lab.Lab_File = "~/Content/Lab_Radio/" + lab.Patient_Id + ext;
                        db.Entry(lab).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            return View(lab);
        }

        [HttpPost]
        public ActionResult Delete_Death_Registration()
        {
            var pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if (pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                try { 
                pt.Death_Date = null;
                pt.Reason_Death = null;
                db.Entry(pt).State = EntityState.Modified;
                db.SaveChanges();
                 return RedirectToAction("Examine", new { id = pt.Patient_Id });
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("Patient_Id", ex.Message);
                }
            }
            General_Examination g = db.General_Examination.Find(pt.Patient_Id);
            if (g == null)
            {
                g = new General_Examination();
                g.Patient_Id = pt.Patient_Id;
                g.Patient = pt;
            }
            return View("Examine",g);
        }
        public ActionResult Death_Registration(long?id)
        {
            if(!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pt = db.Patients.Find(id);
            if(pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(pt);
        }

        [HttpPost]
        public ActionResult Death_Registration()
        {
            var pt = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
            if(pt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    pt.Death_Date = DateTime.Parse(Request.Form["Death_Date"]);
                    pt.Reason_Death = Request.Form["Reason_Death"];
                    db.Entry(pt).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Examine", new { id = pt.Patient_Id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Reason_Death", ex.Message);
                }
            }
            General_Examination g = db.General_Examination.Find(pt.Patient_Id);
            if (g == null)
            {
                g = new General_Examination();
                g.Patient_Id = pt.Patient_Id;
                g.Patient = pt;
            }
            return View("Examine", g);
        }
    }
}