using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HockeySignups.Data;

namespace HockeySignups.Web.Controllers
{
    public class AdminController : Controller
    {
        private string _connectionString =
            @"Data Source=.\sqlexpress;Initial Catalog=HockeySignup;Integrated Security=True";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEvent(DateTime date, int maxPeople)
        {
            var db = new HockeySignupsDb(_connectionString);
            Event e = new Event {Date = date, MaxPeople = maxPeople};
            db.AddEvent(e);
            TempData["Message"] = "Event Successfully created, Id: " + e.Id;
            return RedirectToAction("Index", "Hockey");
        }

    }
}
