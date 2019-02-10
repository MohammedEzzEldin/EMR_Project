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
      public ActionResult Login(string username,string password)
        {
            if (ModelState.IsValid) { 
            try {
            User user = db.Users.Single(u => u.Email == username);
                    // if user do not exist 
                    if (user == null)
                    {
                        ModelState.AddModelError("Email", "You do not register !");
                    }
                    else if (user.Status_Of_Account == 1) {
                     string pss = user.Password.Split(' ')[0];
                     if (pss.Equals(password))
                    {
                        FormsAuthentication.SetAuthCookie(user.User_Id.ToString(), true);
                        Session.Add("UserID", user.User_Id.ToString());
                        Session.Add("UserType", user.User_Type.ToString());
                        Session.Timeout = 1440;
                        user.Last_Date_Of_Login = DateTime.Now;
                        if (user.User_Type == 1) {
                                return RedirectToAction("Index");//Response.Redirect(""); 
                            }
                        if(user.User_Type == 2) {  Response.Redirect(""); }
                        if(user.User_Type == 3) {  Response.Redirect(""); }
                        if(user.User_Type == 4) {  Response.Redirect(""); }
                    }
                        else
                        {
                            ModelState.AddModelError("Password", "Wrong Password");
                        }
                    }
                    else if(user.Status_Of_Account == 0)
                    {
                        ModelState.AddModelError("Password","Something Wrong happend, Please contact with us");
                    }
            }
            catch (Exception ex)
            {
                    ModelState.AddModelError("Email", "You do not register ! "+ex.Message);
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
        public ActionResult Register_Hospital(User us)
        {
            if ( us.User_Id <=0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                Medical_Organization md_org = db.Medical_Organization.Find(us.User_Id); 
            if(md_org == null)
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
                db.Entry(md_org).State = EntityState.Modified;  
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(md_org);
        }

        //Patient Registeration
        public ActionResult Register_Patient(Person pr)
        {
            if (pr.Person_Id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(pr.Person_Id);
            if(patient == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(patient);
        }

        //Patient Registeration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register_Patient( [Bind( Include = "Alternative_Phone,Job,Learning_Status,Social_Status,Patient_Id")] Patient patient)
        {
            if (ModelState.IsValid)
            {  
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                patient = db.Patients.Find(Int64.Parse(Request.Form["Patient_Id"]));
                if(patient != null)
                {
                    patient.Alternative_Phone = Request.Form["Alternative_Phone"];
                    patient.Job = Request.Form["Job"];
                    patient.Learning_Status = Request.Form["Learning_Status"];
                    patient.Social_Status = Request.Form["Social_Status"];
                    db.Entry(patient).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(patient);
        }

        //Person Registeration
        public ActionResult Register_Person(User us)
        {
            if(us.User_Id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person pr = db.People.Find(us.User_Id);
            if(pr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(pr);
        }

        //Person Registeration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register_Person( [Bind(Include = "First_Name,Last_Name,National_Id,Birth_Date,Gender,Nationality,Person_Id")] Person pr)
        {
            if (ModelState.IsValid)
            {
                long pt_id = pr.Person_Id;
                Patient pt = new Patient();  
                pt.Patient_Id = pt_id;             
                db.Entry(pr).State = EntityState.Modified;
                db.SaveChanges();    
                //if equal new patient 
                if(db.Patients.Find(pt_id) == null) 
                 { 
                db.Patients.Add(pt);
                db.SaveChanges();
                }
                return RedirectToAction("Register_Patient",pr);
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
        public ActionResult Register([Bind(Include = "Email,Address,City,Phone,User_Type,Photo_Url,Password")] User us)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form["Confirm Password"] != Request.Form["Password"])
                {
                    ModelState.AddModelError("Confirm_Password", "Password do not Match");
                }  
                else
                {
                    //Access the File using the Name of HTML INPUT File.
                   // HttpPostedFileBase postedFile =  Request.Files["Photo_Url"];

                    //Check if File is available.
                  //  if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        //Save the File.
                         //us.Photo_Url = Server.MapPath("~/Content/Patient_Photo") + System.IO.Path.GetFileName(postedFile.FileName);
                        //postedFile.SaveAs(us.Photo_Url);
                    }
                    
                    try
                    {
                        var check_User_exist = db.Users.Single(u => u.Email == us.Email);
                        //search user if already exist or not
                        if (check_User_exist != null)
                        {
                            // Registeration_Id = check_User_exist.User_Id;
                            if (us.User_Type == 4) { return RedirectToAction("Register_Person", check_User_exist); }
                            else if (us.User_Type == 2) { return RedirectToAction("Register_Hospital", check_User_exist); }
                        }
                    }
                    catch (Exception ex) { }

                    if (us.User_Type == 4)
                    {
                        us.Status_Of_Account = 1;
                        long pr_id = us.User_Id;
                        Person person = new Person();
                        person.Person_Id = pr_id;
                        person.First_Name = "";
                        person.Last_Name = "";
                        person.Birth_Date = new DateTime(1888, 1, 1);
                        person.National_Id = "";
                        person.Gender = "";
                        db.Users.Add(us);
                        db.People.Add(person);
                        db.SaveChanges();
                        return RedirectToAction("Register_Person", us);
                    }
                    else if (us.User_Type == 2)
                    {
                        long med_org_id = us.User_Id;
                        Medical_Organization med = new Medical_Organization();
                        med.Medical_Org_Id = med_org_id;
                        med.Medical_Org_Name = "";
                        med.Medium_Rate = 0.0;
                        db.Users.Add(us);
                        db.Medical_Organization.Add(med);
                        db.SaveChanges();
                        return RedirectToAction("Register_Hospital", us);
                    }
                }
            }
            return View(us);
        }
       
    }
}