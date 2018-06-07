using CoZ.Models.Monsters;
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
            //Tijdelijke boar creator
            Monster monster = new Boar();
            //TODO Haal uit de database de current location en daarvan de monster lijst en geef het monster weer
            return View(monster);
        }
        
        //Redirect the user to the Location view after defeating the monster(s)
        public ActionResult ReDirect()
        {
            //TODO Get current location from database
            //TODO Remove monster(s) from current location list
            return RedirectToAction("Index", "Location");
        }
    }
}