using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index(int? id)
        {
            return View(id);
        }

        //Generate data to start a new game
        public ActionResult Create(int? id)
        {
            int charId = 0;
            using (var DbContext = ApplicationDbContext.Create())
            {
                Character character = new Character();
                character.Map = MapFactory.CreateBigMap();
                Location[][] locations = character.Map.WorldMap.ToArray();
                character.CurrentLocation = locations[21][19];
                character.XCoord = 21;
                character.YCoord = 19;
                charId = character.Id;
                DbContext.Characters.Add(character);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", "Location", charId);
        }
    }
}