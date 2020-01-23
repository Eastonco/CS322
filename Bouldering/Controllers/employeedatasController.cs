using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bouldering.Models;

namespace Bouldering.Controllers
{
    public class employeedatasController : Controller
    {

        private UserEntities db = new UserEntities();
        // GET: employeedatas
        public ActionResult Index()
        {
            return View();
        }

        // GET: userdatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: userdatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employeeID,firstName,lastName,employeeEmail")] employeedata employeedata)
        {
            if (ModelState.IsValid)
            {
                db.employeedatas.Add(employeedata);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(employeedata);
        }
    }
}