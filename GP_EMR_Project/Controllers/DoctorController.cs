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
            if(Check_Login())
            {
                if(id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
               var doctors = db.Doctors.Find(id);
                if(doctors == null)
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
            if(!Check_Login())
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
                    if(Request.Form["Person.User.Password"].Split(' ')[0] != doctor.Person.User.Password.Split(' ')[0])
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
                    return RedirectToAction("Index",new { id = doctor.Doctor_Id});
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

        public ActionResult Manage_Account(long?id)
        {
            if (!Check_Login())
            {
                return RedirectToAction("Login", "Home");
            }

            if (id ==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if(doctor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return RedirectToAction("Edit",new { id = doctor.Doctor_Id });
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
            if(doctor == null)
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
            if(pr == null)
            {
                return pr;
            }
            foreach(var item in pr)
            {
                if(item.Booking_Date.Year <= DateTime.Now.Year)
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
            return RedirectToAction("Index_Patients",new { id = doctor.Doctor_Id});
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
                if(Search.Month <= DateTime.Now.Month)
                {
                    if(Search.Day < DateTime.Now.Day)
                    {
                        ModelState.AddModelError("Search", "Date In The Past");
                        return View("Index_Patients", db.Permissions.Where(model => model.Booking_Date.Year == DateTime.Now.Year &&
                         model.Booking_Date.Month == DateTime.Now.Month &&
                         model.Booking_Date.Day == DateTime.Now.Day).OrderBy(model => model.hour).ToList());
                    }
                }
            }
            return View("Index_Patients", db.Permissions.Where(model => model.Booking_Date.Year == Search.Year &&
                          model.Booking_Date.Month ==Search.Month &&
                          model.Booking_Date.Day == Search.Day).OrderBy(model => model.hour).ToList());
        }

        [HttpPost]
        public ActionResult Upload_Photo(HttpPostedFileBase file)
        {
            User u = db.Users.Find((long)Int64.Parse(Request.Form["Doctor_Id"]));

            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    return RedirectToAction("Manage_Account", "Doctor",new { id = Int64.Parse(Request.Form["Doctor_Id"]) });
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
    }
}
