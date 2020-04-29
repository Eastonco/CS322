using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using Bouldering_Company.Models;
using System.Globalization;

namespace Bouldering_Company.Controllers
{
    public class ClimberController : Controller
    {
        private UserEntities db = new UserEntities();
        private UserEntities1 db1 = new UserEntities1();


        // GET: userdatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: userdatas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,firstName,lastName,userEmail")] userdata userdata)
        {
            int xConvertID;
            string UserID;
            var Temp_UserID = db.userdatas.Max(x => x.userID);

            if (Temp_UserID == null)
            {
                UserID = "100001";
                userdata.userID = UserID;
            }
            else
            {
                Int32.TryParse(Temp_UserID, out xConvertID);
                xConvertID = xConvertID + 1;
                UserID = xConvertID.ToString();
                userdata.userID = UserID;


            }

            if (ModelState.IsValid)
            {
                db.userdatas.Add(userdata);
                db.SaveChanges();
                Response.Write(@"<script language='javascript'>alert('Message: \n" + "Welcome! Your User ID is:  " + UserID + "');</script>");
            }

            return View(userdata);
        }


        // climber sign in, add to logs
        public ActionResult Climber_In()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Climber_In([Bind(Include = "userID,timeStamp,stampRecord")] userLog userLog)
        {
            if (db.userdatas.Where(x => x.userID == userLog.userID).Any())
            {
                //creats a time stamp for the user when they sign in
                DateTime localDate = DateTime.Now;
                //converts time stamp to string to store in database
                string TimeStamp = localDate.ToString("MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
                userLog.timeStamp = TimeStamp;


                int convert;
                var Temp_Record = db1.userLogs.Max(x => x.stampRecord);

                if (Temp_Record == null)
                {
                    Temp_Record = "1";
                    userLog.stampRecord = Temp_Record;
                }
                else
                {
                    //convert int to string
                    convert = Int32.Parse(Temp_Record);
                    convert = convert + 1;
                    var NewStamp = convert.ToString();
                    userLog.stampRecord = NewStamp;
                }

                if (ModelState.IsValid)
                {
                    db1.userLogs.Add(userLog);
                    db1.SaveChanges();
                    Response.Write(@"<script language='javascript'>alert('Message: \n" + "Welcome!" + "');</script>");
                }

                return View(userLog);
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Message: \n" + "Your ID was Incorrect, Please Try again" + "');</script>");
                return View();
            }
        }




    }
}
