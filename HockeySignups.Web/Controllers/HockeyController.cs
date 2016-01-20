using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HockeySignups.Data;
using HockeySignups.Web.Models;

namespace HockeySignups.Web.Controllers
{
    public class HockeyController : Controller
    {
        private string _connectionString =
          @"Data Source=.\sqlexpress;Initial Catalog=HockeySignup;Integrated Security=True";

        public ActionResult Index()
        {
            HomePageViewModel vm = new HomePageViewModel();
            if (TempData["Message"] != null)
            {
                vm.Message = (string)TempData["Message"];
            }
            else if (TempData["Error"] != null)
            {
                vm.ErrorMesage = (string) TempData["Error"];
            }

            return View(vm);
        }

        public ActionResult LatestEvent()
        {
            var db = new HockeySignupsDb(_connectionString);
            Event latestEvent = db.GetLatestEvent();
            EventSignupViewModel vm = new EventSignupViewModel();
            vm.Event = latestEvent;
            vm.EventStatus = db.GetEventStatus(latestEvent);
            return View(vm);
        }

        [HttpPost]
        public ActionResult EventSignup(string firstName, string lastName, string email, int eventId)
        {
            var db = new HockeySignupsDb(_connectionString);
            var e = db.GetEventById(eventId);
            var status = db.GetEventStatus(e);
            if (status == EventStatus.InThePast)
            {
                TempData["Error"] = "You cannot sign up to a game in the past. Jerk.";
                return RedirectToAction("Index");
            }
            else if (status == EventStatus.Full)
            {
                TempData["Error"] = "Nice try sucker....";
                return RedirectToAction("Index");
            }
            EventSignup s = new EventSignup
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                EventId = eventId
            };
            db.AddEventSignup(s);

            TempData["Message"] =
                "You have succesfully signed up for this weeks game, looking forward to checking you into the boards!";
            return RedirectToAction("Index");
        }

    }
}
