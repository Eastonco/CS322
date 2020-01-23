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
    public class userlogsController : Controller
    {
        private UserEntities2 db = new UserEntities2();

        // GET: userdatas
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
        public ActionResult Create([Bind(Include = "userID, timeStamp")] userLog userLog)
        {
            if (ModelState.IsValid)
            {
                db.userLogs.Add(userLog);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(userLog);
        }

    }
}
