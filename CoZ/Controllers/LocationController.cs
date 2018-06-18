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
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character myChar = FindCharacter(id, DbContext.Characters);
                result = CopyLocation(myChar.CurrentLocation);
            }
            if (result.Monsters.Count != 0)
            {
                return RedirectToAction("Index", "Battle", result.Monsters.First());
            }
            return View(result);
        }

        public ActionResult GoNorth()
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character myChar = FindCharacter(id, DbContext.Characters);
                Location[][] locations = myChar.Map.WorldMap.ToArray();
                myChar.YCoord += 1;
                myChar.CurrentLocation = locations[myChar.XCoord][myChar.YCoord];
                result = CopyLocation(myChar.CurrentLocation);
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
                Character myChar = FindCharacter(id, DbContext.Characters);
                Location[][] locations = myChar.Map.WorldMap.ToArray();
                myChar.YCoord -= 1;
                myChar.CurrentLocation = locations[myChar.XCoord][myChar.YCoord];
                result = CopyLocation(myChar.CurrentLocation);
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
                Character myChar = FindCharacter(id, DbContext.Characters);
                Location[][] locations = myChar.Map.WorldMap.ToArray();
                myChar.XCoord += 1;
                myChar.CurrentLocation = locations[myChar.XCoord][myChar.YCoord];
                result = CopyLocation(myChar.CurrentLocation);
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
                Character myChar = FindCharacter(id, DbContext.Characters);
                Location[][] locations = myChar.Map.WorldMap.ToArray();
                myChar.XCoord -= 1;
                myChar.CurrentLocation = locations[myChar.XCoord][myChar.YCoord];
                result = CopyLocation(myChar.CurrentLocation);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", result);
        }


        //HELPER METHODS
        //TODO MAKE REGION SOMETIME

        public Character FindCharacter(string id, IQueryable<Character> characters)
        {
            Character result = null;
            foreach (Character c in characters)
            {
                if (c.userId != null)
                {
                    if (c.userId.Equals(id))
                    {
                        result = c;
                    }
                }
            }
            return result;
        }

        public Location CopyLocation(Location input)
        {
            Location output = new Plains();
            output.Altitude = input.Altitude;
            output.Description = input.Description;
            output.ShortDescription = input.ShortDescription;
            output.Monsters = input.Monsters;
            output.Items = input.Items;
            output.IsVisited = input.IsVisited;
            return output;
        }
    }
}