using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bouldering_Company.Models;
using System.Windows;
using System.Globalization;
using System.Windows.Forms;


namespace Bouldering_Company.Controllers
{
    public class EmployeeController : Controller
    {
        //model for Climber Info
        private UserEntities db = new UserEntities();
        //model for Climber Logs
        private UserEntities1 db1 = new UserEntities1();
        //model for Employee Info
        private UserEntities2 db2 = new UserEntities2();
        //model for Admin Data
        private UserEntities3 db3 = new UserEntities3();

        public ActionResult Create()
        {
            return View();
        }

        // POST: employeedatas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employeeID,firstName,lastName,employeeEmail")] employeedata employeedata)
        {
            int xConvertID;
            string UserID;
            var Temp_UserID = db2.employeedatas.Max(x => x.employeeID);

            if (Temp_UserID == null)
            {
                UserID = "100001";
                employeedata.employeeID = UserID;
            }
            else
            {
                Int32.TryParse(Temp_UserID, out xConvertID);
                xConvertID = xConvertID + 1;
                UserID = xConvertID.ToString();
                employeedata.employeeID = UserID;

            }

            if (ModelState.IsValid)
            {
                db2.employeedatas.Add(employeedata);
                db2.SaveChanges();
                string message = "Your Employee ID is: " + UserID;
                var result = System.Windows.Forms.MessageBox.Show(message);

                return RedirectToAction("View_Employees");
            }

            return View(employeedata);
        }

        //employee sign in
        public ActionResult Employee_In()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Employee_In([Bind(Include = "employeeID,lastName")] employeedata employeedata)
        {
            if (db2.employeedatas.Where(x => x.employeeID == employeedata.employeeID).Any() && db2.employeedatas.Where(x => x.lastName == employeedata.lastName).Any())
            {

                Response.Write(@"<script language='javascript'>alert('Message: \n" + "Welcome!" + "');</script>");
                return RedirectToAction("In_Gym", "Employee");
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Message: \n" + "Your ID was Incorrect, Please Try again" + "');</script>");
                return View();
            }
        }

        //view employee list
        public ActionResult View_Employees()
        {
            return View(db2.employeedatas.ToList());
        }

        //employee details 
        public ActionResult Employee_Details(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employeedata employeedata = db2.employeedatas.Find(id);
            if (employeedata == null)
            {
                return HttpNotFound();
            }

            return View(employeedata);
        }

        // Get: employee/Delete/
        public ActionResult Employee_Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employeedata employeedata = db2.employeedatas.Find(id);
            if (employeedata == null)
            {
                return HttpNotFound();
            }
            return View(employeedata);
        }

        // POST: employee/Delete/
        [HttpPost, ActionName("Employee_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Employee_DeleteConfirmed(string id)
        {
            string response = Microsoft.VisualBasic.Interaction.InputBox("Please Enter Admin Password?", "Password", "", 0, 0);
            if (response == db3.AdminDatas.Max(x => x.adminPassword))
            {
                employeedata employeedata = db2.employeedatas.Find(id);
                db2.employeedatas.Remove(employeedata);
                db2.SaveChanges();
                return RedirectToAction("View_Employees");
            }
            else
            {
                return RedirectToAction("View_Employees");
            }
        }


        // GET: userdatas/Edit
        public ActionResult Employee_Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employeedata employeedata = db2.employeedatas.Find(id);
            if (employeedata == null)
            {
                return HttpNotFound();
            }

            return View(employeedata);

        }

        // POST: userdatas/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Employee_Edit([Bind(Include = "employeeID,firstName,lastName,employeeEmail")] employeedata employeedata)
        {
            string response = Microsoft.VisualBasic.Interaction.InputBox("Please Enter Admin Password?", "Password", "", 0, 0);

            if (ModelState.IsValid && response == db3.AdminDatas.Max(x => x.adminPassword))
            {
                db2.Entry(employeedata).State = EntityState.Modified;
                db2.SaveChanges();
                return RedirectToAction("View_Employees");
            }
            else
            {
                return RedirectToAction("View_Employees");
            }

        }


        /// <summary>
        /// Bellow this is Climber actions in employee portal
        /// </summary>
        /// <returns></returns>

        //view whos in the gym, and log in records
        public ActionResult In_Gym()
        {
            return View(db1.userLogs.ToList());
        }

        //view details on individuals
        public ActionResult Details(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userdata userdata = db.userdatas.Find(id);
            ViewBag.Logs = db1.userLogs.Where(x => x.userID == id).ToList();


            if (userdata == null)
            {
                return HttpNotFound();
            }

            return View(userdata);
        }

        //view all climbers
        public ActionResult View_Climbers()
        {
            return View(db.userdatas.ToList());
        }


        // GET: userdatas/Delete
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userdata userdata = db.userdatas.Find(id);
            if (userdata == null)
            {
                return HttpNotFound();
            }
            return View(userdata);
        }

        // POST: userdatas/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            userdata userdata = db.userdatas.Find(id);
            db.userdatas.Remove(userdata);
            db.SaveChanges();
            return RedirectToAction("View_Climbers");
        }

        //dispenser from microsoft
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: userdatas/Edit
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userdata userdata = db.userdatas.Find(id);
            if (userdata == null)
            {
                return HttpNotFound();
            }
            return View(userdata);
        }

        // POST: userdatas/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,firstName,lastName,userEmail")] userdata userdata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userdata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("View_Climbers");
            }
            return View(userdata);
        }

    }
}