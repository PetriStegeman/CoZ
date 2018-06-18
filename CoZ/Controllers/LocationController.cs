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
                Character myChar = DbContext.Characters.Where(c => c.UserId == id).First();
                result = CopyLocation(myChar.CurrentLocation);
            }
            if (result.Monsters.Count != 0)
            {
                return RedirectToAction("Index", "Battle");
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
                Character myChar = DbContext.Characters.Where(c => c.UserId.Equals(id)).First();
                myChar.YCoord += 1;
                myChar.CurrentLocation = DbContext.Locations.Where(l => l.XCoord == myChar.XCoord && l.YCoord == myChar.YCoord).First();
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
                Character myChar = DbContext.Characters.Where(c => c.UserId.Equals(id)).First();
                myChar.YCoord -= 1;
                myChar.CurrentLocation = DbContext.Locations.Where(l => l.XCoord == myChar.XCoord && l.YCoord == myChar.YCoord).First();
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
                Character myChar = DbContext.Characters.Where(c => c.UserId.Equals(id)).First();
                myChar.XCoord += 1;
                myChar.CurrentLocation = DbContext.Locations.Where(l => l.XCoord == myChar.XCoord && l.YCoord == myChar.YCoord).First();
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
                Character myChar = DbContext.Characters.Where(c => c.UserId.Equals(id)).First();
                myChar.XCoord -= 1;
                myChar.CurrentLocation = DbContext.Locations.Where(l => l.XCoord == myChar.XCoord && l.YCoord == myChar.YCoord).First();
                result = CopyLocation(myChar.CurrentLocation);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Index", result);
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