using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GP_EMR_Project.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Security;
using System.IO;
using System.Web.Helpers;

namespace GP_EMR_Project.Controllers
{
    public class HomeController : Controller
    {
        private EMR_GP_DBEntities db = new EMR_GP_DBEntities();
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = db.Users.Single(u => u.Email == username);
                    // if user do not exist 
                    if (user == null)
                    {
                        ModelState.AddModelError("Email", "You do not register !");
                    }
                    else if (user.Status_Of_Account == 1)
                    {
                        string pss = user.Password.Split(' ')[0];
                        if (pss.Equals(password))
                        {
                            FormsAuthentication.SetAuthCookie(user.User_Id.ToString(), true);
                            Session.Add("UserID", user);
                            Session.Timeout = 1440;
                            user.Last_Date_Of_Login = DateTime.Now;
                            db.Entry(user).State = EntityState.Modified;
                            db.SaveChanges();
                            if (user.User_Type == 1)
                            {
                                return RedirectToAction("Index","Admin"); 
                            }
                            if (user.User_Type == 2)
                            {
                                return RedirectToAction("Index", "Medical_Organization");
                            }
                            if (user.User_Type == 3)
                            {
                                return RedirectToAction("Index", "Doctor",new { id = user.User_Id});
                            }
                            if (user.User_Type == 4)
                            {
                                return RedirectToAction("Index", "Patient",new { id = user.User_Id });
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Password", "Wrong Password");
                        }
                    }
                    else if (user.Status_Of_Account == 0)
                    {
                        ModelState.AddModelError("Password", "Something Wrong happend, Please contact with us");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Email", "You do not register ! " + ex.Message);
                }
            }
            return View();
        }

        //[Authorize]
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        //Hospiatal Registeration
        public ActionResult Register_Hospital(long id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medical_Organization md_org = new Medical_Organization();
            md_org.Medical_Org_Id = id;
            if (md_org == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(md_org);
        }

        //Hospital Registeration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register_Hospital([Bind(Include = "Medical_Org_Name,Medical_Org_Id")] Medical_Organization md_org)
        {
            if (ModelState.IsValid)
            {
                md_org.Medium_Rate = 0.0;
                db.Medical_Organization.Add(md_org);
                db.SaveChanges();
                return RedirectToAction("UploadFile",new { id = md_org.Medical_Org_Id});
            }
            return View(md_org);
        }

        //Patient Registeration
        public ActionResult Register_Patient(long id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = new Patient();
            patient.Patient_Id = id;
            if (patient == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(patient);
        }

        //Patient Registeration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register_Patient([Bind(Include = "Alternative_Phone,Job,Learning_Status,Social_Status,Patient_Id")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("UploadFile",new { id = patient.Patient_Id});
            }
           
            return View(patient);
        }

        //Person Registeration
        [HttpGet]
        public ActionResult Register_Person(long id)
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

        //Person Registeration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register_Person([Bind(Include = "First_Name,Last_Name,National_Id,Birth_Date,Gender,Nationality,Person_Id")] Person pr)
        {
            if (ModelState.IsValid)
            {
                if (pr.Birth_Date > DateTime.Now)
                {
                    ModelState.AddModelError("Birth_Date", "Wrong Date");
                }
                else
                {
                    db.People.Add(pr);
                    db.SaveChanges();
                    return RedirectToAction("Register_Patient", new { id = pr.Person_Id });
                }
            }
            return View(pr);
        }

        //General Registeration
        public ActionResult Register()
        {
            return View();
        }

        // General Registeration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Email,Address,City,Password,Phone,User_Type")] User us)
        {

            if (ModelState.IsValid)
            {
                if (Request.Form["Confirm Password"] != Request.Form["Password"])
                {
                    ModelState.AddModelError("Confirm_Password", "Password do not Match");
                }
                else
                {          
                    try
                    {
                        var check_User_exist = db.Users.Single(u => u.Email == Request.Form["Email"]);
                        //search user if already exist or not
                        if (check_User_exist != null)
                        {
                            ModelState.AddModelError("", "User already exist !");
                        }
                       
                    }
                    catch  { }

                    if (us.User_Type == 4)
                    {
                        us.Status_Of_Account = 1;    
                        db.Users.Add(us);   
                        db.SaveChanges();   
                        return RedirectToAction("Register_Person", new { id = us.User_Id });
                    }
                    else if (us.User_Type == 2)
                    {
                        db.Users.Add(us);
                        db.SaveChanges();
                        return RedirectToAction("Register_Hospital",new { id = us.User_Id });
                    }
                }
            }
            return View(us);
        }

        [HttpPost]
        public ActionResult Search(string Filter_by, string Search)
        {
            if (ModelState.IsValid)
            {
               if(Search.Contains('@'))
                {
                    return View(db.Users.ToList().Where(u => u.Email.Contains(Search)));
                }
                else if(Filter_by.Equals("Hospitals"))
                {
                        return View(db.Users.ToList().Where(md => md.User_Type==2 && md.Medical_Organization.Medical_Org_Name.Contains(Search)) );
                 }
               else if (Filter_by.Equals("Doctors"))
                {
                    if(Search.Contains(' '))
                    {
                        return View(db.Users.ToList().Where(dc => dc.User_Type == 3 && String.Compare(dc.Person.First_Name.Split(' ')[0]+" "+ dc.Person.Last_Name.Split(' ')[0],Search,true) == 0));
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
            return RedirectToAction("Index");
        }

        public ActionResult UploadFile(long id)
        {
            if(id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User u = db.Users.Find(id);
            if(u == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(u);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            User u = db.Users.Find((long)Int64.Parse(Request.Form["User_Id"]));

            if (ModelState.IsValid) {
                if(file == null) {
                    return  RedirectToAction("Index");
                }

                if (u != null) {   
                if(file.FileName != null) { 
                    if(u.User_Type == 2) { 
                    file.SaveAs(Server.MapPath("~/Content/Med_Photo/") + u.User_Id + ".jpg");
                    u.Photo_Url = "~/Content/Med_Photo/" + u.User_Id+".jpg";
                    }
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
               return RedirectToAction("Index");
            }
            return View(u);
        }


       public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword([Bind(Include = "Email,User_Type")] User user)
        {
            User us = new User();
            try {
            us = db.Users.Where(model => model.Email == user.Email).Single();
            }
            catch {
            }
            if (ModelState.IsValid)
            {
                if(us == null) { ModelState.AddModelError("","User Not Found, pls register now"); }
                else
                {
                    us.Email = us.Email.Split(' ')[0];
                    if(us.Email == user.Email)
                    {
                        if (us.User_Type == user.User_Type)
                        {
                            try {
                            WebMail.SmtpServer = "smtp.mail.yahoo.com";
                            WebMail.SmtpPort = 587;
                            WebMail.EnableSsl = true;
                            WebMail.SmtpUseDefaultCredentials = true;    
                            WebMail.UserName = "EMR_GP@yahoo.com";
                            WebMail.Password = "Password_GP";
                            WebMail.From = "EMR_GP@yahoo.com";
                            WebMail.Send(user.Email, "Reset Password", " Your password is "+us.Password);
                                return RedirectToAction("Login");
                            }catch(Exception ex)
                            {
                                ModelState.AddModelError("Email", "Sorry, We cannot send message to your email because " + ex.StackTrace);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("User_Type", "User Type is Wrong");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email is Wrong");
                    }
                }
            }
            return View(user);
        }

        public ActionResult Details_Of_Doctor(long? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }
            Doctor dc = db.Doctors.Find(id);
            if(dc == null)
            {
                return RedirectToAction("Index");
            }
            Medical_Organization med = db.Medical_Organization.Find(dc.Medical_Org_Id);
            ViewBag.Medical_Org_Name = med.Medical_Org_Name;
            ViewBag.Medical_Org_Email = med.User.Email;
            ViewBag.Medical_Org_Phone = med.User.Phone;
            ViewBag.Medical_Org_City = med.User.City;
            ViewBag.Medical_Org_Address = med.User.Address;
            return View(dc);
        }

        public ActionResult Details_Of_Hospital(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Medical_Organization org = db.Medical_Organization.Find(id);
            if (org == null)
            {
                return RedirectToAction("Index");
            }
            return View(org);
        }
    }
}