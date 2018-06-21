using CoZ.Models;
using CoZ.Models.Locations;
using CoZ.Models.Monsters;
using CoZ.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoZ.Controllers
{
    [Authorize]
    public class LocationController : Controller
    {
        private LocationRepository locationRepository;
        protected LocationRepository LocationRepository
        {
            get
            {
                if (locationRepository == null)
                {
                    return new LocationRepository();
                }
                else
                {
                    return locationRepository;
                }
            }
        }
        
        public ActionResult Index()
        {
            Location location = null;
            int monsterCounter = 0;
            using (var DbContext = ApplicationDbContext.Create())
            {
                string id = User.Identity.GetUserId();
                location = LocationRepository.FindByCharacterId(id);
                monsterCounter = location.Monsters.Count();
            }
            return BattleRedirect(location, monsterCounter);
        }

        private ActionResult BattleRedirect(Location location, int monsterCounter)
        {
            if (monsterCounter != 0)
            {
                return RedirectToAction("Index", "Battle");
            }
            else return View("Index", location);
        }

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