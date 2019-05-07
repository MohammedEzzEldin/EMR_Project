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
            else if (user.User_Type != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        // GET: Medical_Organization/Details/5
        public ActionResult Doctor_Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Medical_Organization/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Medical_Org_Id = new SelectList(db.Users, "User_Id", "Email");
        //    return View();
        //}

        //// POST: Medical_Organization/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Medical_Org_Id,Medical_Org_Name,Medium_Rate")] Medical_Organization medical_Organization)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Medical_Organization.Add(medical_Organization);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Medical_Org_Id = new SelectList(db.Users, "User_Id", "Email", medical_Organization.Medical_Org_Id);
        //    return View(medical_Organization);
        //}

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
        public ActionResult Edit()
        {
            Medical_Organization medical_Organization = db.Medical_Organization.Find((long)Int64.Parse(Request.Form["Medical_Org_Id"]));
            if (medical_Organization == null)
            {
                ModelState.AddModelError("", "Something Wrong is occured");
                return View(medical_Organization);
            }
            if (ModelState.IsValid)
            {
                medical_Organization.Medical_Org_Name = Request.Form["Medical_Org_Name"];
                medical_Organization.User.Email = Request.Form["[0]"]; // [0] refer to email feild in input tag
                medical_Organization.User.Phone = Request.Form["User.Phone"];
                medical_Organization.User.City = Request.Form["User.City"];
                medical_Organization.User.Address = Request.Form["User.Address"];
                if (Request.Form["User.Password"].Split(' ')[0] != medical_Organization.User.Password.Split(' ')[0])
                {
                    if (Request.Form["Confirm_Password"].Split(' ')[0] == Request.Form["User.Password"].Split(' ')[0])
                    {
                        medical_Organization.User.Password = Request.Form["User.Password"]; 
                    }
                    else
                    {
                        ModelState.AddModelError("Confirm_Password", "Password do not match Confirm Password Feild");
                        return View(medical_Organization);
                    }
                }
                db.Entry(medical_Organization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medical_Organization);
        }

        public ActionResult Edit_Doctor(long? id)
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

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Doctor()
        {
            Doctor doctor = null;
            doctor = db.Doctors.Find(long.Parse(Request.Form["Doctor_Id"]));
            if (doctor != null)
            {     
                if (ModelState.IsValid)
                {
                    doctor.Acadimic_Degree = Request.Form["Acadimic_Degree"];
                    doctor.Functional_Degree = Request.Form["Functional_Degree"];
                    doctor.Spacialization = Request.Form["Spacialization"];
                    doctor.Person.First_Name = Request.Form["Person.First_Name"];
                    doctor.Person.Last_Name = Request.Form["Person.Last_Name"];
                    doctor.Person.Nationality = Request.Form["Person.Nationality"];
                    doctor.Person.National_Id = Request.Form["Person.National_Id"];
                    doctor.Person.Gender = Request.Form["Person.Gender"];
                    DateTime dateTemp = Convert.ToDateTime(Request.Form["Person.Birth_Date.Day"] + "/" + Request.Form["Person.Birth_Date.Month"] + "/" + Request.Form["Person.Birth_Date.Year"]);
                    doctor.Person.Birth_Date = dateTemp;
                    doctor.Person.User.Email = Request.Form["[0]"];
                    doctor.Person.User.Phone = Request.Form["Person.User.Phone"];    
                    try { 
                    db.Entry(doctor).State = EntityState.Modified;
                    db.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                        return View(doctor);
                    }
                    return RedirectToAction("Manage_Doctors");
                }
            }
            return View(doctor);
        }


        // GET: Medical_Organization/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Medical_Organization medical_Organization = db.Medical_Organization.Find(id);
        //    if (medical_Organization == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(medical_Organization);
        //}

        //// POST: Medical_Organization/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    Medical_Organization medical_Organization = db.Medical_Organization.Find(id);
        //    db.Medical_Organization.Remove(medical_Organization);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search(string Filter_by, string Search)
        {
            return RedirectToAction("Index");
        }

        public ActionResult Manage_Account()
        {
            return RedirectToAction("Edit", new { id = ((User)Session["UserID"]).User_Id });
        }

        public ActionResult Manage_Doctors()
        {
            User user = (User)Session["UserID"];
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (user.User_Type != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            Medical_Organization medical_Organization = db.Medical_Organization.Find(user.User_Id);
            //list of doctors
            var doctor_list = db.Doctors.Where(d => d.Medical_Org_Id == medical_Organization.Medical_Org_Id);

            return View(doctor_list);
        }

        [HttpGet]
        public ActionResult Add_New_Doctor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_New_Doctor([Bind(Include = "Email,Address,City,Password,Phone,User_Type,Status_Of_Account")] User user)
        {

            if (ModelState.IsValid)
            {
                if (Request.Form["Confirm_Password"].Equals(Request.Form["Password"]))
                {
                    var u = db.Users.Where(m => m.Email.Contains(user.Email)).SingleOrDefault();
                    if (u != null)
                    {
                        return RedirectToAction("Doctor_Personal_Data", new { id = u.User_Id });
                    }
                    try
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        RedirectToAction("Index");
                    }
                    return RedirectToAction("Doctor_Personal_Data", new { id = user.User_Id });
                }
                else
                {
                    ModelState.AddModelError("Confirm_Password", "Password and Confirm Password does not match");
                    return View(user);
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Doctor_Personal_Data(long id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Person pr = new Person();
            pr.Person_Id = id;

            if (pr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(pr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Doctor_Personal_Data([Bind(Include = "First_Name,Last_Name,National_Id,Birth_Date,Gender,Nationality,Person_Id")] Person pr)
        {
            if (ModelState.IsValid)
            {
                if (pr.Birth_Date > DateTime.Now)
                {
                    ModelState.AddModelError("Birth_Date", "Wrong Date");
                }
                else
                {
                    var p = db.People.Find(pr.Person_Id);
                    if(p != null)
                    {
                        return RedirectToAction("Register_Doctor", new { id = p.Person_Id });
                    }
                    try
                    {
                        db.People.Add(pr);
                        db.SaveChanges();
                        return RedirectToAction("Register_Doctor", new { id = pr.Person_Id });
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError("First_Name", ex.Message);
                        return View(pr);
                    }

                }
            }
            return View(pr);
        }

        [HttpGet]
        public ActionResult Register_Doctor(long id)
        {
            User user = (User)Session["UserID"];
            var medicalOrgID = user.User_Id;
            ViewData["medicalOrgID"] = medicalOrgID;
            Medical_Organization medical_Organization = db.Medical_Organization.Find(user.User_Id);
            var getdepartmentslist = medical_Organization.Departments.ToList();
            getdepartmentslist.Insert(0, new Department { Department_Id = 0, Department_Name = "Select" });
            ViewBag.ListOfDepartments = getdepartmentslist;
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = new Doctor();
            doctor.Doctor_Id = id;
            if (doctor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register_Doctor([Bind(Include = "Medium_Rate,Medical_Org_Id,Doctor_Id,Acadimic_Degree,Functional_Degree,Spacialization,Department_Id")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                var d = db.Doctors.Find(doctor.Doctor_Id);
                if(d != null)
                {
                    return RedirectToAction("Manage_Doctors");
                }
                try
                {
                    db.Doctors.Add(doctor);
                    db.SaveChanges();
                    return RedirectToAction("Manage_Doctors");
                }
                catch(Exception ex)
                {
                    return RedirectToAction("Register_Doctor",new { id = doctor.Doctor_Id});
                }           
            }
            return RedirectToAction("Index");
        }

        public ActionResult Block_Doctor(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Block_Doctor(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Manage_Doctors");
            }
            return View(user);
        }

        public ActionResult Unblock_Doctor(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Unblock_Doctor(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Manage_Doctors");
            }
            return View(user);
        }

        public ActionResult Delete_Doctor(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete_Doctor")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Doctor_Confirmed(long id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            Person person = db.People.Find(id);
            db.People.Remove(person);
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("Manage_Doctors");
        }
        public ActionResult Manage_Department()
        {
            User user = (User)Session["UserID"];
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (user.User_Type != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            Medical_Organization medical_Organization = db.Medical_Organization.Find(user.User_Id);
            var id = (long)user.User_Id;
            ViewData["medical_org_id"] = id;
            return View(medical_Organization.Departments);
        }

        [HttpGet]
        public ActionResult Add_New_Department(long? id)
        {
            ViewData["medical_org_id"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_New_Department([Bind(Include = "Department_Id,Department_Name,Medical_Org_Id")]Department dep)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(dep);
                db.SaveChanges();
                return RedirectToAction("Manage_Department");
            }
            return View(dep);
        }

        public ActionResult Edit_Department(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Department(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Manage_Department");
            }
            return View(department);
        }

        public ActionResult Delete_Department(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Department(long id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Manage_Department");
        }



        [HttpPost]
        public ActionResult Upload_Photo(HttpPostedFileBase file)
        {
            User u = db.Users.Find((long)Int64.Parse(Request.Form["Medical_Org_Id"]));

            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    return RedirectToAction("Manage_Account", "Medical_Organization");
                }

                if (u != null)
                {
                    if (file.FileName != null)
                    {
                        if (u.User_Type == 2)
                        {
                            file.SaveAs(Server.MapPath("~/Content/Med_Photo/") + u.User_Id + ".jpg");
                            u.Photo_Url = "~/Content/Med_Photo/" + u.User_Id + ".jpg";
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

        public ActionResult Manage_Schedule(long? id)
        {
            User user = (User)Session["UserID"];
            if(user == null)
            {
                return RedirectToAction("Login","Home");
            }
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var schedule = db.Doctor_Schedule.Where(model => model.doctor_id == id);
            ViewBag.Doctor_Id = id;
            return View(schedule.ToList());
        }

        [HttpPost]
        public ActionResult Delete_Schedule(long Schedule_Id)
        {
            long? doctor_Id = null;
            if(ModelState.IsValid)
            {
                long s = Int64.Parse(Request.Form["Schedule_Id"]);
                if(Schedule_Id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                Doctor_Schedule schedule = db.Doctor_Schedule.Find(Schedule_Id);
                if(schedule != null)
                {
                    doctor_Id = schedule.doctor_id;
                    db.Doctor_Schedule.Remove(schedule);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Manage_Schedule", new { id = doctor_Id });
        }

        public ActionResult Edit_Schedule(long? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor_Schedule schedule = db.Doctor_Schedule.Find(id);
            if(schedule == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var doctor = db.Doctors.Find(schedule.doctor_id);
            if(doctor == null)
            {
                ViewBag.DoctorName = "";
            }
            else
            {
                ViewBag.DoctorName = doctor.Person.First_Name + " " + doctor.Person.Last_Name;
            }
            return View(schedule);
        }
        [HttpPost]
        public ActionResult Edit_Schedule([Bind(Include ="doctor_id,Schedule_Id,day,from,to")]Doctor_Schedule schedule)
        {
            var doctor = db.Doctors.Find(schedule.doctor_id);
            if (doctor == null)
            {
                ViewBag.DoctorName = "";
                ModelState.AddModelError("to", "doctor not found");
                return View(schedule);
            }
            else
            {
                ViewBag.DoctorName = doctor.Person.First_Name + " " + doctor.Person.Last_Name;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(schedule).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Manage_Schedule", new { id = schedule.doctor_id });
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("to", ex.Message);
                }
            }
            return View(schedule);
        }
        public ActionResult Add_New_Schedule(long?id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if(doctor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.Doctor_Id = doctor.Doctor_Id;
            ViewBag.DoctorName = doctor.Person.First_Name + " " + doctor.Person.Last_Name;
            return View();
        }
        [HttpPost]
        public ActionResult Add_New_Schedule([Bind(Include = "doctor_id,day,from,to")] Doctor_Schedule schedule)
        {
            var doctor = db.Doctors.Find(schedule.doctor_id);
            if (doctor == null)
            {
                ViewBag.DoctorName = "";
                ViewBag.Doctor_Id = 0;
                ModelState.AddModelError("to", "doctor not found");
                return View(schedule);
            }
            else
            {
                ViewBag.DoctorName = doctor.Person.First_Name + " " + doctor.Person.Last_Name;
                ViewBag.Doctor_Id = doctor.Doctor_Id;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Doctor_Schedule.Add(schedule);
                    db.SaveChanges();
                    return RedirectToAction("Manage_Schedule", new { id = schedule.doctor_id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("to", ex.InnerException);
                }
            }
            return View(schedule);
        }

        [HttpPost]
        public ActionResult Search_In_Doctors(string Search)
        {
            User user = (User)Session["UserID"];
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (user.User_Type != 2)
            {
                return RedirectToAction("Logout", "Home");
            }
            IEnumerable<Doctor> doctors = db.Doctors.Where(d => d.Medical_Org_Id == user.User_Id).ToList();
            switch (Request.Form["Search_By"])
            {
                case "Department":
                    return View("Manage_Doctors", doctors.Where(d => d.Department.Department_Name.Contains(Search)).OrderByDescending(model => model.Department.Department_Name));
                case "Doctor_F":
                    return View("Manage_Doctors", doctors.Where(d => d.Person.First_Name.Contains(Search)).OrderByDescending(model => model.Person.First_Name));
                case "Doctor_L":
                    return View("Manage_Doctors", doctors.Where(d => d.Person.Last_Name.Contains(Search)).OrderBy(model => model.Person.Last_Name));
                case "National_Id":
                    return View("Manage_Doctors", doctors.Where(d=> d.Person.National_Id.Equals(Search)));
                default:
                    break;
            }
            return View("Manage_Doctors",doctors);
        }

        public ActionResult Show_Doctors_Department(long? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dep = db.Departments.Find(id);
            if(dep == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var doctors = dep.Doctors.ToList();
            if(doctors == null)
            {
                return RedirectToAction("Manage_Department");
            }
            ViewBag.Dep_Name = dep.Department_Name;
            ViewBag.Dep_Id = dep.Department_Id;
            return View(doctors);
        }

        public ActionResult Assign_Doctors_Department(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dep = db.Departments.Find(id);
            if (dep == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var doctors = db.Doctors.Where(model => model.Medical_Org_Id == dep.Medical_Org_Id && (model.Department_Id == null || model.Department_Id == 0));
            if (doctors == null)
            {
                return RedirectToAction("Manage_Department");
            }
            ViewBag.Dep_Name = dep.Department_Name;
            ViewBag.Dep_Id = dep.Department_Id;
            return View(doctors.ToList());
        }

        [HttpPost]
        public ActionResult Assign_Doctors_Department()
        {
            long doc_id = Int64.Parse(Request.Form["Doctor_Id"]);
            var doctor = db.Doctors.Find(doc_id);
            if (doctor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            long dep_id = Int64.Parse(Request.Form["Dep_Id"]);
            if (dep_id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    doctor.Department_Id = dep_id;
                    db.Entry(doctor).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch(Exception)
                {
                    return RedirectToAction("Assign_Doctors_Department", new { id = dep_id });
                }
               
            }
            return RedirectToAction("Assign_Doctors_Department", new { id = dep_id });
        }

        [HttpPost]
        public ActionResult UnAssign_Doctors_Department()
        {
            long doc_id = Int64.Parse(Request.Form["Doctor_Id"]);
            long dep_id = Int64.Parse(Request.Form["Dep_Id"]);
            if (ModelState.IsValid)
            {
                try
                {
                    var doctor = db.Doctors.Find(doc_id);
                    if (doctor == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    doctor.Department_Id = null;
                    db.Entry(doctor).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return RedirectToAction("Show_Doctors_Department", new { id = dep_id });
                }

            }
            return RedirectToAction("Show_Doctors_Department", new { id = dep_id });
        }
    }
}
