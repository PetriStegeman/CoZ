using CoZ.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {
            //Temporary code to give a location
            Location result = new Forest();
            //TODO Code to get the current location from database
            if (result.Monsters[0] != null)
            {
                return View("/Battle/Index");
            }
            return View(result);
        }
    }
}