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
                if (Request.Form["Confirm_Password"].Equals(Request.Form["User.Password"]))
                {
                    medical_Organization.User.Password = Request.Form["User.Password"];
                    //return View(medical_Organization);
                }
                else
                {
                    ModelState.AddModelError("Confirm_Password", "Password do not match Confirm Password Feild");
                    return View(medical_Organization);
                }
                db.Entry(medical_Organization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            return View();
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

    }
}
