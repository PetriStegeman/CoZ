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

        //Thread safe? wait for result from FindCharacter before using myChar
        public ActionResult GoNorth()
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                Character myChar = FindCharacter(id, DbContext.Characters);
                myChar.YCoord += 1;
                myChar.CurrentLocation = FindLocation(myChar.XCoord, myChar.YCoord, myChar.Map.WorldMap);
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
                myChar.YCoord -= 1;
                myChar.CurrentLocation = FindLocation(myChar.XCoord, myChar.YCoord, myChar.Map.WorldMap);
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
                myChar.XCoord += 1;
                myChar.CurrentLocation = FindLocation(myChar.XCoord, myChar.YCoord, myChar.Map.WorldMap);
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
                myChar.XCoord -= 1;
                myChar.CurrentLocation = FindLocation(myChar.XCoord, myChar.YCoord, myChar.Map.WorldMap);
                result = CopyLocation(myChar.CurrentLocation);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", result);
        }


        //HELPER METHODS
        //TODO MAKE REGION SOMETIME
        //Find Character based on UserId string
        public Character FindCharacter(string id, IQueryable<Character> characters)
        {
            Character result = null;
            foreach (Character c in characters)
            {
                if (c.userId != null && c.userId.Equals(id))
                {
                    result = c;
                }
            }
            return result;
        }

        //Find a location based on its x and y coords
        public Location FindLocation(int x, int y, ICollection<Location> map)
        {
            Location result = null;
            foreach (Location l in map)
            {
                if (l != null && l.XCoord == x && l.YCoord == y)
                {
                    result = l;
                }
            }
            return result;
        }

        //Copy the characteristics of a Location from the DB
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