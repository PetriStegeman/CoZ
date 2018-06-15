using CoZ.Models;
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
        public ActionResult Index(int id)
        {
            return View(id);
        }

        //Generate data to start a new game
        public ActionResult Create(int id)
        {
            using (var DbContext = ApplicationDbContext.Create())
            {
                Character character = new Character();
                character.Id = id;
                character.Map = MapFactory.CreateBigMap();
                DbContext.Characters.Add(character);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", "Location", id);
        }
    }
}