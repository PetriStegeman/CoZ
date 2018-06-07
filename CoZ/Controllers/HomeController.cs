using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Conquest of Zork";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";

            return View();
        }

        public ActionResult Manual()
        {
            ViewBag.Message = "Game Manual";

            return View();
        }

        public ActionResult PatchNotes()
        {
            ViewBag.Message = "Patch Notes";

            return View();
        }
    }
}