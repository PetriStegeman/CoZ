using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
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
        public ActionResult Index(int id)
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                IQueryable<Character> characters = DbContext.Characters;
                Character myChar = FindCharacter(id, characters);
                result = CopyLocation(myChar.CurrentLocation);
            }
            if (result.Monsters[0] != null)
            {
                return RedirectToAction("Index", "Battle", result.Monsters[0]);
            }
            return View(result);
        }

        public ActionResult GoNorth(int id)
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                IQueryable<Character> characters = DbContext.Characters;
                Character myChar = FindCharacter(id, characters);
                myChar.YCoord += 1;
                myChar.CurrentLocation = myChar.Map.WorldMap[myChar.XCoord, myChar.YCoord];
                result = CopyLocation(myChar.CurrentLocation);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", result);
        }

        public ActionResult GoSouth(int id)
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                IQueryable<Character> characters = DbContext.Characters;
                Character myChar = FindCharacter(id, characters);
                myChar.YCoord -= 1;
                myChar.CurrentLocation = myChar.Map.WorldMap[myChar.XCoord, myChar.YCoord];
                result = CopyLocation(myChar.CurrentLocation);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", result);
        }

        public ActionResult GoEast(int id)
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                IQueryable<Character> characters = DbContext.Characters;
                Character myChar = FindCharacter(id, characters);
                myChar.XCoord += 1;
                myChar.CurrentLocation = myChar.Map.WorldMap[myChar.XCoord, myChar.YCoord];
                result = CopyLocation(myChar.CurrentLocation);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", result);
        }

        public ActionResult GoWest(int id)
        {
            Location result = null;
            using (var DbContext = ApplicationDbContext.Create())
            {
                IQueryable<Character> characters = DbContext.Characters;
                Character myChar = FindCharacter(id, characters);
                myChar.XCoord -= 1;
                myChar.CurrentLocation = myChar.Map.WorldMap[myChar.XCoord, myChar.YCoord];
                result = CopyLocation(myChar.CurrentLocation);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", result);
        }


        //HELPER METHODS
        //TODO MAKE REGION SOMETIME
        public Character FindCharacter(int id, IQueryable characters)
        {
            Character result = null;
            foreach (Character c in characters)
            {
                if (c.Id == id)
                {
                    result = c;
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