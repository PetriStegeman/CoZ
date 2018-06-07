using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    public class BattleController : Controller
    {
        // GET: Battle
        public ActionResult Index()
        {
            return View();
        }
        
        //Redirect the user to the Location view after defeating the monster(s)
        public ActionResult ReDirect()
        {
            //TODO Get current location from database
            //TODO Remove monster(s) from current location list
            return View("/Location/Index");
        }
    }
}