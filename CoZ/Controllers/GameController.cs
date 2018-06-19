using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Utility;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        //Generate data to start a new game
        public ActionResult Create()
        {
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character character = new Character(id);
                DbContext.Characters.Add(character);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", "Location");
        }
    }
}