using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    public class LocationController : Controller
    {
        // GET: Current Location from character by character id
        public ActionResult Index()
        {
            Location result = null;
            int counter = 0;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character myChar = DbContext.Characters.Where(c => c.UserId == id).First();
                Location currentLocation = DbContext.Locations.Where(l => l.XCoord == myChar.XCoord && l.YCoord == myChar.YCoord).First();
                result = CopyLocation(currentLocation);
                counter = DbContext.Monsters.Where(c => c.Location.LocationId == currentLocation.LocationId).Count();
            }
            if (counter != 0)
            {
                return RedirectToAction("Index", "Battle");
            }
            else return View(result);
        }

        //Thread safe? wait for result from FindCharacter before using myChar
        public ActionResult GoNorth()
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character myChar = DbContext.Characters.Where(c => c.UserId.Equals(id)).First();
                myChar.YCoord += 1;
                Location copy = DbContext.Locations.Where(l => l.XCoord == myChar.XCoord && l.YCoord == myChar.YCoord).First();
                result = CopyLocation(copy);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", result);
        }

        public ActionResult GoSouth()
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character myChar = DbContext.Characters.Where(c => c.UserId.Equals(id)).First();
                myChar.YCoord -= 1;
                Location copy = DbContext.Locations.Where(l => l.XCoord == myChar.XCoord && l.YCoord == myChar.YCoord).First();
                result = CopyLocation(copy);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", result);
        }

        public ActionResult GoEast()
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character myChar = DbContext.Characters.Where(c => c.UserId.Equals(id)).First();
                myChar.XCoord += 1;
                Location copy = DbContext.Locations.Where(l => l.XCoord == myChar.XCoord && l.YCoord == myChar.YCoord).First();
                result = CopyLocation(copy);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", result);
        }

        public ActionResult GoWest()
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character myChar = DbContext.Characters.Where(c => c.UserId.Equals(id)).First();
                myChar.XCoord -= 1;
                Location copy = DbContext.Locations.Where(l => l.XCoord == myChar.XCoord && l.YCoord == myChar.YCoord).First();
                result = CopyLocation(copy);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", result);
        }

        public ActionResult Camp()
        {
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character myChar = DbContext.Characters.Where(c => c.UserId.Equals(id)).First();
                myChar.CurrentHp = myChar.MaxHp;
                DbContext.SaveChanges();
            }
            return View();
        }

        //Copy the characteristics of a Location from the DB
        public Location CopyLocation(Location input)
        {
            Location output = new EmptyLocation
            {
                Altitude = input.Altitude,
                Description = input.Description,
                ShortDescription = input.ShortDescription,
                Monsters = input.Monsters,
                Items = input.Items,
                IsVisited = input.IsVisited
            };
            return output;
        }
    }
}